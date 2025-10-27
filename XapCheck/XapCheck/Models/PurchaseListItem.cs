using System;

namespace XapCheck.Models
{
    public class PurchaseListItem
    {
        public int Id { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        public int? UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int RecommendedQuantity { get; set; }
        public string Reason { get; set; }

        public bool IsResolved { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
