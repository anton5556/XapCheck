using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using XapCheck.Data;
using XapCheck.Models;

namespace XapCheck.Controllers
{
    public class MedicineController
    {
        private readonly HomePharmacyContext _dbContext;

        public MedicineController(HomePharmacyContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IReadOnlyList<Medicine> GetAllForUser(int userProfileId)
        {
            return _dbContext.Medicines
                .AsNoTracking()
                .Where(m => m.UserProfileId == userProfileId)
                .OrderBy(m => m.Name)
                .ToList();
        }

        public Medicine AddMedicine(int userProfileId, Medicine medicine, string performedBy = "system")
        {
            if (medicine == null) throw new ArgumentNullException(nameof(medicine));
            medicine.UserProfileId = userProfileId;
            _dbContext.Medicines.Add(medicine);
            _dbContext.SaveChanges();

            Log(ActionType.Add, performedBy, $"Added medicine {medicine.Name}", userProfileId, medicine.Id, null);
            EvaluatePurchaseSuggestion(medicine, userProfileId);
            return medicine;
        }

        public Medicine UpdateMedicine(Medicine medicine, string performedBy = "system")
        {
            if (medicine == null) throw new ArgumentNullException(nameof(medicine));
            _dbContext.Medicines.Update(medicine);
            _dbContext.SaveChanges();

            Log(ActionType.Update, performedBy, $"Updated medicine {medicine.Name}", medicine.UserProfileId, medicine.Id, null);
            EvaluatePurchaseSuggestion(medicine, medicine.UserProfileId);
            return medicine;
        }

        public void DeleteMedicine(int medicineId, string performedBy = "system")
        {
            var medicine = _dbContext.Medicines.FirstOrDefault(m => m.Id == medicineId);
            if (medicine == null) return;
            var userId = medicine.UserProfileId;
            var name = medicine.Name;
            _dbContext.Medicines.Remove(medicine);
            _dbContext.SaveChanges();

            Log(ActionType.Delete, performedBy, $"Deleted medicine {name}", userId, medicineId, null);

            // Also clean up purchase list items
            var items = _dbContext.PurchaseList.Where(p => p.MedicineId == medicineId).ToList();
            if (items.Count > 0)
            {
                _dbContext.PurchaseList.RemoveRange(items);
                _dbContext.SaveChanges();
            }
        }

        public Medicine AdjustQuantity(int medicineId, int delta, string performedBy = "system")
        {
            var medicine = _dbContext.Medicines.FirstOrDefault(m => m.Id == medicineId);
            if (medicine == null) return null;

            medicine.Quantity += delta;
            if (medicine.Quantity < 0) medicine.Quantity = 0;

            _dbContext.SaveChanges();

            var action = delta >= 0 ? ActionType.IncreaseQuantity : ActionType.DecreaseQuantity;
            Log(action, performedBy, $"Adjusted quantity by {delta} for {medicine.Name}", medicine.UserProfileId, medicine.Id, delta);
            EvaluatePurchaseSuggestion(medicine, medicine.UserProfileId);
            return medicine;
        }

        public IReadOnlyList<PurchaseListItem> GetPurchaseList(int userProfileId)
        {
            return _dbContext.PurchaseList
                .Include(p => p.Medicine)
                .Where(p => p.UserProfileId == userProfileId && !p.IsResolved)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }

        public void MarkPurchaseItemResolved(int purchaseItemId, string performedBy = "system")
        {
            var item = _dbContext.PurchaseList.FirstOrDefault(p => p.Id == purchaseItemId);
            if (item == null) return;
            item.IsResolved = true;
            _dbContext.SaveChanges();
            Log(ActionType.PurchaseCompleted, performedBy, $"Marked purchase item resolved for {item.MedicineId}", item.UserProfileId, item.MedicineId, null);
        }

        private void EvaluatePurchaseSuggestion(Medicine medicine, int userProfileId)
        {
            var settings = _dbContext.AppSettings.FirstOrDefault(s => s.UserProfileId == userProfileId) ?? _dbContext.AppSettings.FirstOrDefault(s => s.UserProfileId == null);
            var expSoonDays = settings?.ExpiringSoonDays ?? 10;

            var needsRestock = medicine.Quantity < Math.Max(medicine.MinThreshold, settings?.DefaultMinThreshold ?? 1);
            var expiringSoon = (medicine.ExpiryDate.Date - DateTime.Today).Days <= expSoonDays;

            // If needs restock or expiring soon, ensure a purchase suggestion exists (or create one)
            if (needsRestock || expiringSoon)
            {
                var existing = _dbContext.PurchaseList.FirstOrDefault(p => p.MedicineId == medicine.Id && !p.IsResolved);
                if (existing == null)
                {
                    var recommended = Math.Max(medicine.MinThreshold * 2, 1);
                    var reason = needsRestock && expiringSoon
                        ? "Low quantity and expiring soon"
                        : needsRestock ? "Low quantity" : "Expiring soon";
                    var item = new PurchaseListItem
                    {
                        MedicineId = medicine.Id,
                        UserProfileId = userProfileId,
                        RecommendedQuantity = recommended,
                        Reason = reason,
                        IsResolved = false,
                        CreatedAt = DateTime.UtcNow
                    };
                    _dbContext.PurchaseList.Add(item);
                    _dbContext.SaveChanges();

                    Log(ActionType.PurchaseSuggested, "system", $"Suggested purchase for {medicine.Name}: {reason}", userProfileId, medicine.Id, null);
                }
            }
            else
            {
                // If no longer needed, resolve existing suggestions
                var items = _dbContext.PurchaseList.Where(p => p.MedicineId == medicine.Id && !p.IsResolved).ToList();
                if (items.Count > 0)
                {
                    foreach (var it in items)
                    {
                        it.IsResolved = true;
                    }
                    _dbContext.SaveChanges();
                }
            }
        }

        private void Log(ActionType actionType, string performedBy, string details, int? userProfileId, int? medicineId, int? quantityDelta)
        {
            var log = new ActionLog
            {
                ActionType = actionType,
                Timestamp = DateTime.UtcNow,
                PerformedBy = performedBy,
                Details = details,
                UserProfileId = userProfileId,
                MedicineId = medicineId,
                QuantityDelta = quantityDelta
            };
            _dbContext.ActionLogs.Add(log);
            _dbContext.SaveChanges();
        }
    }
}