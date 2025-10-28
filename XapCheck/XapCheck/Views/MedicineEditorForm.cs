using System;
using System.Drawing;
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
        private DateTimePicker dtpTreatmentStart;
        private DateTimePicker dtpTreatmentEnd;
        private TextBox txtFrequency;
        private TextBox txtNotes;
        private Button btnOk;
        private Button btnCancel;
        private Label lblStatus;
        private ErrorProvider errorProvider;

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
            this.Height = 520;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 12, Padding = new Padding(10), AutoSize = true };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));

            txtName = new TextBox();
            txtDosage = new TextBox();
            txtFrequency = new TextBox();
            numQuantity = new NumericUpDown { Minimum = 0, Maximum = 100000, Value = 1 };
            numMin = new NumericUpDown { Minimum = 0, Maximum = 100000, Value = 1 };
            dtpExpiry = new DateTimePicker { Format = DateTimePickerFormat.Short };
            dtpPurchase = new DateTimePicker { Format = DateTimePickerFormat.Short };
            dtpTreatmentStart = new DateTimePicker { Format = DateTimePickerFormat.Short, ShowCheckBox = true };
            dtpTreatmentEnd = new DateTimePicker { Format = DateTimePickerFormat.Short, ShowCheckBox = true };
            txtNotes = new TextBox { Multiline = true, Height = 80, ScrollBars = ScrollBars.Vertical };
            btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel };
            lblStatus = new Label { AutoSize = true, ForeColor = Color.Gray };
            errorProvider = new ErrorProvider();
            errorProvider.ContainerControl = this;

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
            layout.Controls.Add(new Label { Text = "Treatment Start", AutoSize = true }, 0, 7);
            layout.Controls.Add(dtpTreatmentStart, 1, 7);
            layout.Controls.Add(new Label { Text = "Treatment End", AutoSize = true }, 0, 8);
            layout.Controls.Add(dtpTreatmentEnd, 1, 8);
            layout.Controls.Add(new Label { Text = "Notes", AutoSize = true }, 0, 9);
            layout.Controls.Add(txtNotes, 1, 9);
            layout.Controls.Add(lblStatus, 0, 10);
            layout.SetColumnSpan(lblStatus, 2);
            layout.Controls.Add(panelButtons, 0, 11);
            layout.SetColumnSpan(panelButtons, 2);

            this.Controls.Add(layout);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;

            // Default dates
            dtpPurchase.Value = DateTime.Today;
            dtpExpiry.Value = DateTime.Today.AddMonths(6);

            // Events
            numQuantity.ValueChanged += (s, e) => UpdateStatus();
            numMin.ValueChanged += (s, e) => UpdateStatus();
            dtpExpiry.ValueChanged += (s, e) => UpdateStatus();
            dtpTreatmentStart.ValueChanged += (s, e) => UpdateStatus();
            dtpTreatmentEnd.ValueChanged += (s, e) => UpdateStatus();

            btnOk.Click += (s, e) =>
            {
                var valid = ValidateForm();
                if (!valid) this.DialogResult = DialogResult.None;
            };

            UpdateStatus();
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
                TreatmentStart = dtpTreatmentStart.Checked ? (DateTime?)dtpTreatmentStart.Value.Date : null,
                TreatmentEnd = dtpTreatmentEnd.Checked ? (DateTime?)dtpTreatmentEnd.Value.Date : null,
                Notes = txtNotes.Text?.Trim()
            };
        }

        private bool ValidateForm()
        {
            errorProvider.Clear();
            var ok = true;
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider.SetError(txtName, "Name is required");
                ok = false;
            }

            if (dtpTreatmentStart.Checked && dtpTreatmentEnd.Checked && dtpTreatmentStart.Value.Date > dtpTreatmentEnd.Value.Date)
            {
                errorProvider.SetError(dtpTreatmentEnd, "End date must be after start date");
                ok = false;
            }

            if (dtpPurchase.Value.Date > dtpExpiry.Value.Date)
            {
                errorProvider.SetError(dtpExpiry, "Expiry must be on/after purchase date");
                ok = false;
            }

            return ok;
        }

        private void UpdateStatus()
        {
            const int defaultExpiringSoonDays = 10;
            var parts = new System.Collections.Generic.List<string>();

            if (numQuantity.Value < numMin.Value)
            {
                parts.Add("Low quantity");
            }

            var daysToExpiry = (dtpExpiry.Value.Date - DateTime.Today).Days;
            if (daysToExpiry < 0)
            {
                parts.Add("Expired");
            }
            else if (daysToExpiry <= defaultExpiringSoonDays)
            {
                parts.Add("Expiring soon");
            }

            if (dtpTreatmentStart.Checked && dtpTreatmentEnd.Checked && dtpTreatmentStart.Value.Date > dtpTreatmentEnd.Value.Date)
            {
                parts.Add("Treatment dates invalid");
            }

            if (parts.Count == 0)
            {
                lblStatus.ForeColor = Color.ForestGreen;
                lblStatus.Text = "Status: OK";
            }
            else if (parts.Contains("Expired") || parts.Contains("Treatment dates invalid"))
            {
                lblStatus.ForeColor = Color.Firebrick;
                lblStatus.Text = "Status: " + string.Join(", ", parts);
            }
            else
            {
                lblStatus.ForeColor = Color.DarkGoldenrod;
                lblStatus.Text = "Status: " + string.Join(", ", parts);
            }
        }
    }
}
