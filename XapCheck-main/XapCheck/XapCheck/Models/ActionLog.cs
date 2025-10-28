using System;

namespace XapCheck.Models
{
    public class ActionLog
    {
        public int Id { get; set; }
        public ActionType ActionType { get; set; }
        public DateTime Timestamp { get; set; }
        public string PerformedBy { get; set; }
        public string Details { get; set; }

        // Optional relationships to provide context for the action
        public int? UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int? MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        // Quantity delta for inventory adjustments (positive or negative)
        public int? QuantityDelta { get; set; }
    }
}
