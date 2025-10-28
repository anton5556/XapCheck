using System;
using System.Windows.Forms;
using XapCheck.Models;

namespace XapCheck.Views
{
    public class MedicineEditorForm : Form
    {
        private TextBox txtName;
        private TextBox txtDosage;
        private NumericUpDown numQuantity;
        private NumericUpDown numMin;
        private DateTimePicker dtpExpiry;
        private DateTimePicker dtpPurchase;
        private TextBox txtFrequency;
        private TextBox txtNotes;
        private Button btnOk;
        private Button btnCancel;

        public MedicineEditorForm()
        {
            InitializeUi();
        }

        public MedicineEditorForm(Medicine existing) : this()
        {
            if (existing != null)
            {
                txtName.Text = existing.Name;
                txtDosage.Text = existing.Dosage;
                txtFrequency.Text = existing.Frequency;
                numQuantity.Value = existing.Quantity;
                numMin.Value = existing.MinThreshold;
                dtpExpiry.Value = existing.ExpiryDate == default(DateTime) ? DateTime.Today : existing.ExpiryDate;
                dtpPurchase.Value = existing.PurchaseDate == default(DateTime) ? DateTime.Today : existing.PurchaseDate;
                txtNotes.Text = existing.Notes;
            }
        }

        private void InitializeUi()
        {
            this.Text = "Medicine";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Width = 400;
            this.Height = 380;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 9, Padding = new Padding(10), AutoSize = true };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));

            txtName = new TextBox();
            txtDosage = new TextBox();
            txtFrequency = new TextBox();
            numQuantity = new NumericUpDown { Minimum = 0, Maximum = 100000, Value = 1 };
            numMin = new NumericUpDown { Minimum = 0, Maximum = 100000, Value = 1 };
            dtpExpiry = new DateTimePicker { Format = DateTimePickerFormat.Short };
            dtpPurchase = new DateTimePicker { Format = DateTimePickerFormat.Short };
            txtNotes = new TextBox { Multiline = true, Height = 80, ScrollBars = ScrollBars.Vertical };
            btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel };

            var panelButtons = new FlowLayoutPanel { FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill, AutoSize = true };
            panelButtons.Controls.Add(btnOk);
            panelButtons.Controls.Add(btnCancel);

            layout.Controls.Add(new Label { Text = "Name", AutoSize = true }, 0, 0);
            layout.Controls.Add(txtName, 1, 0);
            layout.Controls.Add(new Label { Text = "Dosage", AutoSize = true }, 0, 1);
            layout.Controls.Add(txtDosage, 1, 1);
            layout.Controls.Add(new Label { Text = "Frequency", AutoSize = true }, 0, 2);
            layout.Controls.Add(txtFrequency, 1, 2);
            layout.Controls.Add(new Label { Text = "Quantity", AutoSize = true }, 0, 3);
            layout.Controls.Add(numQuantity, 1, 3);
            layout.Controls.Add(new Label { Text = "Min Threshold", AutoSize = true }, 0, 4);
            layout.Controls.Add(numMin, 1, 4);
            layout.Controls.Add(new Label { Text = "Expiry Date", AutoSize = true }, 0, 5);
            layout.Controls.Add(dtpExpiry, 1, 5);
            layout.Controls.Add(new Label { Text = "Purchase Date", AutoSize = true }, 0, 6);
            layout.Controls.Add(dtpPurchase, 1, 6);
            layout.Controls.Add(new Label { Text = "Notes", AutoSize = true }, 0, 7);
            layout.Controls.Add(txtNotes, 1, 7);
            layout.Controls.Add(panelButtons, 0, 8);
            layout.SetColumnSpan(panelButtons, 2);

            this.Controls.Add(layout);

            btnOk.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show(this, "Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                }
            };
        }

        public Medicine BuildMedicine(int id = 0, int? userProfileId = null)
        {
            return new Medicine
            {
                Id = id,
                UserProfileId = userProfileId ?? 0,
                Name = txtName.Text?.Trim(),
                Dosage = txtDosage.Text?.Trim(),
                Frequency = txtFrequency.Text?.Trim(),
                Quantity = (int)numQuantity.Value,
                MinThreshold = (int)numMin.Value,
                ExpiryDate = dtpExpiry.Value.Date,
                PurchaseDate = dtpPurchase.Value.Date,
                Notes = txtNotes.Text?.Trim()
            };
        }
    }
}
