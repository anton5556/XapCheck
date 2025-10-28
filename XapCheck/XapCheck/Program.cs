using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using XapCheck.Controllers;
using XapCheck.Data;

namespace XapCheck
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Ensure database exists and seed minimal data
            try
            {
                using (var context = new HomePharmacyContext())
                {
                    context.Database.EnsureCreated();
                    var userController = new UserProfileController(context);
                    var profile = userController.GetOrCreateDefaultProfile();
                    userController.EnsureSettingsForUser(profile.Id);
                }
            }
            catch
            {
                // Swallow startup DB exceptions to not block UI; operations will re-attempt later
            }
            Application.Run(new MainForm());
        }
    }
}
