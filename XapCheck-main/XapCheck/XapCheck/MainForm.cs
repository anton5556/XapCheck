using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XapCheck.Controllers;
using XapCheck.Data;
using XapCheck.Models;
using System.Collections.Generic;
using System.Drawing.Text;



namespace XapCheck
{
    public partial class MainForm : Form
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        //private HomePharmacyContext _dbContext;
        //private UserProfileController _userController;
        //private MedicineController _medicineController;
        //private  DataGridViewColumn dgvMedicines;
        //private object dgvPurchase;
        //private UserProfile _currentUser;
        //private BindingList<Medicine> _medicinesBinding;
        //private BindingList<PurchaseListItemView> _purchaseBinding;
        //private System.ComponentModel.IContainer components = null;



        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //public MainForm()
        //{


        //    InitializeComponent();

        //    _dbContext = new HomePharmacyContext();
        //    _userController = new UserProfileController(_dbContext);
        //    _medicineController = new MedicineController(_dbContext);

        //    ConfigureGrids();

        //    dgvMedicines.CellFormatting += dgvMedicines_CellFormatting;
        //    dgvMedicines.SelectionChanged += (s, e) => UpdateButtonsEnabled();
        //    dgvMedicines.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) btnEdit_Click(s, EventArgs.Empty); };
        //    dgvMedicines.KeyDown += dgvMedicines_KeyDown;
        //    this.FormClosed += (s, e) => _dbContext?.Dispose();
        //}

        //private void ConfigureGrids()
        //{
        //    dgvMedicines.AutoGenerateColumns = false;
        //    dgvMedicines.Columns.Clear();
        //    dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.Name), HeaderText = "Name" });
        //    dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.Dosage), HeaderText = "Dosage" });
        //    dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.Frequency), HeaderText = "Frequency" });
        //    dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.Quantity), HeaderText = "Qty" });
        //    dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.MinThreshold), HeaderText = "Min" });
        //    dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.ExpiryDate), HeaderText = "Expiry", DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" } });
        //    dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.DaysToExpiry), HeaderText = "Days Left" });

        //    dgvPurchase.AutoGenerateColumns = false;
        //    dgvPurchase.Columns.Clear();
        //    dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItemView.MedicineName), HeaderText = "Medicine" });
        //    dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItemView.RecommendedQuantity), HeaderText = "Recommended" });
        //    dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItemView.Reason), HeaderText = "Reason" });
        //    dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItemView.CreatedAt), HeaderText = "Created", DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" } });
        //}

        //private void MainForm_Load(object sender, EventArgs e)
        //{
        //    _currentUser = _userController.GetOrCreateDefaultProfile();
        //    _userController.EnsureSettingsForUser(_currentUser.Id);
        //    RefreshData();
        //}

        //private void RefreshData()
        //{
        //    try
        //    {
        //        var meds = _medicineController.GetAllForUser(_currentUser.Id);
        //        _medicinesBinding = new BindingList<Medicine>(meds.ToList());
        //        dgvMedicines.DataSource = _medicinesBinding;

        //        var purchase = _medicineController.GetPurchaseList(_currentUser.Id);
        //        var view = purchase.Select(p => new PurchaseListItemView
        //        {
        //            Id = p.Id,
        //            MedicineId = p.MedicineId,
        //            MedicineName = p.Medicine != null ? p.Medicine.Name : $"#{p.MedicineId}",
        //            RecommendedQuantity = p.RecommendedQuantity,
        //            Reason = p.Reason,
        //            CreatedAt = p.CreatedAt
        //        }).ToList();
        //        _purchaseBinding = new BindingList<PurchaseListItemView>(view);
        //        dgvPurchase.DataSource = _purchaseBinding;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, $"Failed to load data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    UpdateButtonsEnabled();
        //}

        //private Medicine GetSelectedMedicine()
        //{
        //    if (dgvMedicines.CurrentRow == null) return null;
        //    return dgvMedicines.CurrentRow.DataBoundItem as Medicine;
        //}

        //private PurchaseListItemView GetSelectedPurchaseItem()
        //{
        //    if (dgvPurchase.CurrentRow == null) return null;
        //    return dgvPurchase.CurrentRow.DataBoundItem as PurchaseListItemView;
        //}

        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    var editor = new MedicineInlineEditorDialog();
        //    if (editor.ShowDialog(this) == DialogResult.OK)
        //    {
        //        try
        //        {
        //            var medicine = editor.Medicine;
        //            _medicineController.AddMedicine(_currentUser.Id, medicine, Environment.UserName);
        //            RefreshData();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(this, $"Failed to add: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //    var selected = GetSelectedMedicine();
        //    if (selected == null) return;

        //    var editor = new MedicineInlineEditorDialog(selected);
        //    if (editor.ShowDialog(this) == DialogResult.OK)
        //    {
        //        try
        //        {
        //            var updated = editor.Medicine;
        //            updated.Id = selected.Id;
        //            updated.UserProfileId = selected.UserProfileId;
        //            _medicineController.UpdateMedicine(updated, Environment.UserName);
        //            RefreshData();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(this, $"Failed to update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    var selected = GetSelectedMedicine();
        //    if (selected == null) return;
        //    var confirm = MessageBox.Show(this, $"Delete {selected.Name}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //    if (confirm == DialogResult.Yes)
        //    {
        //        try
        //        {
        //            _medicineController.DeleteMedicine(selected.Id, Environment.UserName);
        //            RefreshData();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(this, $"Failed to delete: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        //private void btnIncrease_Click(object sender, EventArgs e)
        //{
        //    var selected = GetSelectedMedicine();
        //    if (selected == null) return;
        //    try
        //    {
        //        _medicineController.AdjustQuantity(selected.Id, +1, Environment.UserName);
        //        RefreshData();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, $"Failed to adjust: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void btnDecrease_Click(object sender, EventArgs e)
        //{
        //    var selected = GetSelectedMedicine();
        //    if (selected == null) return;
        //    try
        //    {
        //        _medicineController.AdjustQuantity(selected.Id, -1, Environment.UserName);
        //        RefreshData();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, $"Failed to adjust: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void btnResolve_Click(object sender, EventArgs e)
        //{
        //    var item = GetSelectedPurchaseItem();
        //    if (item == null) return;
        //    try
        //    {
        //        var purchaseId = item.Id;
        //        _medicineController.MarkPurchaseItemResolved(purchaseId, Environment.UserName);
        //        RefreshData();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, $"Failed to resolve: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void dgvMedicines_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (e.RowIndex < 0) return;
        //    var row = dgvMedicines.Rows[e.RowIndex];
        //    var medicine = row.DataBoundItem as Medicine;
        //    if (medicine == null) return;

        //    switch (medicine.AlertLevel)
        //    {
        //        case AlertLevel.Red:
        //            row.DefaultCellStyle.BackColor = Color.MistyRose;
        //            row.DefaultCellStyle.ForeColor = Color.DarkRed;
        //            break;
        //        case AlertLevel.Yellow:
        //            row.DefaultCellStyle.BackColor = Color.LemonChiffon;
        //            row.DefaultCellStyle.ForeColor = Color.DarkGoldenrod;
        //            break;
        //        default:
        //            row.DefaultCellStyle.BackColor = Color.White;
        //            row.DefaultCellStyle.ForeColor = Color.Black;
        //            break;
        //    }
        //}

        //private void dgvMedicines_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete)
        //    {
        //        btnDelete_Click(sender, EventArgs.Empty);
        //        e.Handled = true;
        //    }
        //    else if (e.KeyCode == Keys.Add || (e.KeyCode == Keys.Oemplus && e.Shift))
        //    {
        //        btnIncrease_Click(sender, EventArgs.Empty);
        //        e.Handled = true;
        //    }
        //    else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
        //    {
        //        btnDecrease_Click(sender, EventArgs.Empty);
        //        e.Handled = true;
        //    }
        //    else if (e.KeyCode == Keys.Enter)
        //    {
        //        btnEdit_Click(sender, EventArgs.Empty);
        //        e.Handled = true;
        //    }
        //}

        //private void UpdateButtonsEnabled()
        //{
        //    var hasSelection = GetSelectedMedicine() != null;
        //    btnEdit.Enabled = hasSelection;
        //    btnDelete.Enabled = hasSelection;
        //    btnIncrease.Enabled = hasSelection;
        //    btnDecrease.Enabled = hasSelection;

        //    var hasPurchase = GetSelectedPurchaseItem() != null;
        //    btnResolve.Enabled = hasPurchase;
        //}

        //private class PurchaseListItemView
        //{
        //    public int Id { get; set; }
        //    public int? MedicineId { get; set; }
        //    public string MedicineName { get; set; }
        //    public int RecommendedQuantity { get; set; }
        //    public string Reason { get; set; }
        //    public DateTime CreatedAt { get; set; }
        //}

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // MainForm
        //    // 
        //    this.ClientSize = new System.Drawing.Size(284, 261);
        //    this.Name = "MainForm";
        //    this.Load += new System.EventHandler(this.MainForm_Load_1);
        //    this.ResumeLayout(false);

        //}

        //private void MainForm_Load_1(object sender, EventArgs e)
        //{

        //}
        private readonly HomePharmacyContext _dbContext;
        private readonly UserProfileController _userController;
        private readonly MedicineController _medicineController;
        private UserProfile _currentUser;
        private BindingList<Medicine> _medicinesBinding;
        private BindingList<PurchaseListItemView> _purchaseBinding;

        public MainForm()
        {
            InitializeComponent();

            _dbContext = new HomePharmacyContext();
            _userController = new UserProfileController(_dbContext);
            _medicineController = new MedicineController(_dbContext);

            ConfigureGrids();

            dgvMedicines.CellFormatting += dgvMedicines_CellFormatting;
            dgvMedicines.SelectionChanged += (s, e) => UpdateButtonsEnabled();
            dgvMedicines.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) btnEdit_Click(s, EventArgs.Empty); };
            dgvMedicines.KeyDown += dgvMedicines_KeyDown;
            this.FormClosed += (s, e) => _dbContext?.Dispose();
        }

        private void ConfigureGrids()
        {
            dgvMedicines.AutoGenerateColumns = false;
            dgvMedicines.Columns.Clear();
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.Name), HeaderText = "Name" });
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.Dosage), HeaderText = "Dosage" });
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.Frequency), HeaderText = "Frequency" });
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.Quantity), HeaderText = "Qty" });
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.MinThreshold), HeaderText = "Min" });
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.ExpiryDate), HeaderText = "Expiry", DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" } });
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.DaysToExpiry), HeaderText = "Days Left" });

            dgvPurchase.AutoGenerateColumns = false;
            dgvPurchase.Columns.Clear();
            dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItemView.MedicineName), HeaderText = "Medicine" });
            dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItemView.RecommendedQuantity), HeaderText = "Recommended" });
            dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItemView.Reason), HeaderText = "Reason" });
            dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItemView.CreatedAt), HeaderText = "Created", DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" } });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _currentUser = _userController.GetOrCreateDefaultProfile();
            _userController.EnsureSettingsForUser(_currentUser.Id);
            RefreshData();
        }

        private void RefreshData()
        {
            try
            {
                var meds = _medicineController.GetAllForUser(_currentUser.Id);
                _medicinesBinding = new BindingList<Medicine>(meds.ToList());
                dgvMedicines.DataSource = _medicinesBinding;

                var purchase = _medicineController.GetPurchaseList(_currentUser.Id);
                var view = purchase.Select(p => new PurchaseListItemView
                {
                    Id = p.Id,
                    MedicineId = p.MedicineId,
                    MedicineName = p.Medicine != null ? p.Medicine.Name : $"#{p.MedicineId}",
                    RecommendedQuantity = p.RecommendedQuantity,
                    Reason = p.Reason,
                    CreatedAt = p.CreatedAt
                }).ToList();
                _purchaseBinding = new BindingList<PurchaseListItemView>(view);
                dgvPurchase.DataSource = _purchaseBinding;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to load data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateButtonsEnabled();
        }

        private Medicine GetSelectedMedicine()
        {
            if (dgvMedicines.CurrentRow == null) return null;
            return dgvMedicines.CurrentRow.DataBoundItem as Medicine;
        }

        private PurchaseListItemView GetSelectedPurchaseItem()
        {
            if (dgvPurchase.CurrentRow == null) return null;
            return dgvPurchase.CurrentRow.DataBoundItem as PurchaseListItemView;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editor = new MedicineInlineEditorDialog();
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var medicine = editor.Medicine;
                    _medicineController.AddMedicine(_currentUser.Id, medicine, Environment.UserName);
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Failed to add: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMedicine();
            if (selected == null) return;

            var editor = new MedicineInlineEditorDialog(selected);
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var updated = editor.Medicine;
                    updated.Id = selected.Id;
                    updated.UserProfileId = selected.UserProfileId;
                    _medicineController.UpdateMedicine(updated, Environment.UserName);
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Failed to update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMedicine();
            if (selected == null) return;
            var confirm = MessageBox.Show(this, $"Delete {selected.Name}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _medicineController.DeleteMedicine(selected.Id, Environment.UserName);
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Failed to delete: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMedicine();
            if (selected == null) return;
            try
            {
                _medicineController.AdjustQuantity(selected.Id, +1, Environment.UserName);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to adjust: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMedicine();
            if (selected == null) return;
            try
            {
                _medicineController.AdjustQuantity(selected.Id, -1, Environment.UserName);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to adjust: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResolve_Click(object sender, EventArgs e)
        {
            var item = GetSelectedPurchaseItem();
            if (item == null) return;
            try
            {
                var purchaseId = item.Id;
                _medicineController.MarkPurchaseItemResolved(purchaseId, Environment.UserName);
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to resolve: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMedicines_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvMedicines.Rows[e.RowIndex];
            var medicine = row.DataBoundItem as Medicine;
            if (medicine == null) return;

            switch (medicine.AlertLevel)
            {
                case AlertLevel.Red:
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    break;
                case AlertLevel.Yellow:
                    row.DefaultCellStyle.BackColor = Color.LemonChiffon;
                    row.DefaultCellStyle.ForeColor = Color.DarkGoldenrod;
                    break;
                default:
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    break;
            }
        }

        private void dgvMedicines_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Add || (e.KeyCode == Keys.Oemplus && e.Shift))
            {
                btnIncrease_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                btnDecrease_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnEdit_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void UpdateButtonsEnabled()
        {
            var hasSelection = GetSelectedMedicine() != null;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
            btnIncrease.Enabled = hasSelection;
            btnDecrease.Enabled = hasSelection;

            var hasPurchase = GetSelectedPurchaseItem() != null;
            btnResolve.Enabled = hasPurchase;
        }

        private class PurchaseListItemView
        {
            public int Id { get; set; }
            public int? MedicineId { get; set; }
            public string MedicineName { get; set; }
            public int RecommendedQuantity { get; set; }
            public string Reason { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
}
