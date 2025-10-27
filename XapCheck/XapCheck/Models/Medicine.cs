using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace XapCheck.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public int Quantity { get; set; }
        public int MinThreshold { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? TreatmentStart { get; set; }
        public DateTime? TreatmentEnd { get; set; }
        public string Notes { get; set; }

        // Връзка с профил
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        // Domain-calculated helpers (not mapped to DB)
        [NotMapped]
        public bool IsExpired => DateTime.Today > ExpiryDate.Date;

        [NotMapped]
        public int DaysToExpiry => (ExpiryDate.Date - DateTime.Today).Days;

        [NotMapped]
        public bool IsRunningLow => Quantity < MinThreshold;

        [NotMapped]
        public AlertLevel AlertLevel
        {
            get
            {
                if (IsExpired)
                {
                    return AlertLevel.Red;
                }

                // Default window for "expiring soon" when settings are not available
                const int defaultExpiringSoonDays = 10;
                if (DaysToExpiry <= defaultExpiringSoonDays || IsRunningLow)
                {
                    return AlertLevel.Yellow;
                }

                return AlertLevel.Green;
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Dosage}) - Qty: {Quantity}, Exp: {ExpiryDate:yyyy-MM-dd}";
        }
    }
}
