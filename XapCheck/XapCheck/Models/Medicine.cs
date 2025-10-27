using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
