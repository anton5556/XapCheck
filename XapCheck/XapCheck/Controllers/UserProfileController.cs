using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using XapCheck.Data;
using XapCheck.Models;

namespace XapCheck.Controllers
{
    public class UserProfileController
    {
        private readonly HomePharmacyContext _dbContext;

        public UserProfileController(HomePharmacyContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public UserProfile GetOrCreateDefaultProfile()
        {
            var profile = _dbContext.UserProfiles.FirstOrDefault(p => p.Name == "Default");
            if (profile != null)
            {
                return profile;
            }

            profile = new UserProfile
            {
                Name = "Default"
            };
            _dbContext.UserProfiles.Add(profile);
            _dbContext.SaveChanges();
            return profile;
        }

        public AppSetting EnsureSettingsForUser(int? userProfileId)
        {
            var settings = _dbContext.AppSettings.FirstOrDefault(s => s.UserProfileId == userProfileId);
            if (settings != null)
            {
                return settings;
            }

            settings = new AppSetting
            {
                UserProfileId = userProfileId,
                ExpiringSoonDays = 10,
                EnablePopupNotifications = true,
                Theme = "Light",
                DefaultMinThreshold = 1,
                CreatedAt = DateTime.UtcNow
            };
            _dbContext.AppSettings.Add(settings);
            _dbContext.SaveChanges();
            return settings;
        }
    }
}
