using System;

namespace XapCheck.Models
{
    // Application or per-profile settings (nullable UserProfileId => global)
    public class AppSetting
    {
        public int Id { get; set; }

        public int? UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int ExpiringSoonDays { get; set; } = 10;
        public bool EnablePopupNotifications { get; set; } = true;
        public string Theme { get; set; } = "Light"; // Light | Dark
        public int DefaultMinThreshold { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
