using System;
using System.Windows.Forms;
using XapCheck.Models;

namespace XapCheck
{
    internal class MedicineInlineEditorDialog : Form
    {
        public Medicine Medicine { get; private set; }

        public MedicineInlineEditorDialog()
        {
            // Minimal placeholder implementation; real editor can be added later
            Medicine = new Medicine
            {
                Name = string.Empty,
                Dosage = string.Empty,
                Frequency = string.Empty,
                Quantity = 0,
                MinThreshold = 1,
                ExpiryDate = DateTime.Today.AddDays(30),
                PurchaseDate = DateTime.Today
            };
        }

        public MedicineInlineEditorDialog(Medicine selected)
        {
            Medicine = selected ?? new Medicine
            {
                Name = string.Empty,
                Dosage = string.Empty,
                Frequency = string.Empty,
                Quantity = 0,
                MinThreshold = 1,
                ExpiryDate = DateTime.Today.AddDays(30),
                PurchaseDate = DateTime.Today
            };
        }
    }
}