using System;
using System.Windows.Forms;
using XapCheck.Models;

namespace XapCheck
{
    public class MedicineInlineEditorDialog : Form
    {
        public Medicine Medicine { get; private set; }
        private Button _okButton;
        private Button _cancelButton;

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
            BuildMinimalUi();
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
            BuildMinimalUi();
        }

        private void BuildMinimalUi()
        {
            Text = "Medicine Editor";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Width = 420;
            Height = 180;

            _okButton = new Button { Text = "OK", DialogResult = DialogResult.OK, Anchor = AnchorStyles.Bottom | AnchorStyles.Right, Left = 210, Top = 100, Width = 80 };
            _cancelButton = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Anchor = AnchorStyles.Bottom | AnchorStyles.Right, Left = 300, Top = 100, Width = 80 };

            Controls.Add(_okButton);
            Controls.Add(_cancelButton);

            AcceptButton = _okButton;
            CancelButton = _cancelButton;
        }
    }
}