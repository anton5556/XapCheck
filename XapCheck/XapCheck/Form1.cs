using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using XapCheck.Controllers;
using XapCheck.Data;
using XapCheck.Models;
using XapCheck.Views;

namespace XapCheck
{
    public partial class Form1 : Form
    {
        private readonly HomePharmacyContext _dbContext;
        private readonly UserProfileController _userController;
        private readonly MedicineController _medicineController;
        private UserProfile _currentUser;

        public Form1()
        {
            InitializeComponent();

            _dbContext = new HomePharmacyContext();
            _userController = new UserProfileController(_dbContext);
            _medicineController = new MedicineController(_dbContext);

            // Configure grids
            dgvMedicines.AutoGenerateColumns = false;
            dgvMedicines.Columns.Clear();
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.Name), HeaderText = "Name" });
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.Dosage), HeaderText = "Dosage" });
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.Quantity), HeaderText = "Qty" });
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.MinThreshold), HeaderText = "Min" });
            dgvMedicines.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Medicine.ExpiryDate), HeaderText = "Expiry" });

            dgvPurchase.AutoGenerateColumns = false;
            dgvPurchase.Columns.Clear();
            dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItem.MedicineId), HeaderText = "MedicineId" });
            dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItem.RecommendedQuantity), HeaderText = "Recommended" });
            dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItem.Reason), HeaderText = "Reason" });
            dgvPurchase.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(PurchaseListItem.CreatedAt), HeaderText = "Created" });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load or create default user
            _currentUser = _userController.GetOrCreateDefaultProfile();
            _userController.EnsureSettingsForUser(_currentUser.Id);
            RefreshData();
        }

        private void RefreshData()
        {
            var meds = _medicineController.GetAllForUser(_currentUser.Id);
            dgvMedicines.DataSource = new BindingList<Medicine>(meds.ToList());

            var purchase = _medicineController.GetPurchaseList(_currentUser.Id);
            dgvPurchase.DataSource = new BindingList<PurchaseListItem>(purchase.ToList());
        }

        private Medicine GetSelectedMedicine()
        {
            if (dgvMedicines.CurrentRow == null) return null;
            return dgvMedicines.CurrentRow.DataBoundItem as Medicine;
        }

        private PurchaseListItem GetSelectedPurchaseItem()
        {
            if (dgvPurchase.CurrentRow == null) return null;
            return dgvPurchase.CurrentRow.DataBoundItem as PurchaseListItem;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editor = new MedicineEditorForm();
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                var medicine = editor.BuildMedicine();
                _medicineController.AddMedicine(_currentUser.Id, medicine, Environment.UserName);
                RefreshData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMedicine();
            if (selected == null) return;
            var editor = new MedicineEditorForm(selected);
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                var updated = editor.BuildMedicine(selected.Id, selected.UserProfileId);
                _medicineController.UpdateMedicine(updated, Environment.UserName);
                RefreshData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMedicine();
            if (selected == null) return;
            var confirm = MessageBox.Show(this, $"Delete {selected.Name}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                _medicineController.DeleteMedicine(selected.Id, Environment.UserName);
                RefreshData();
            }
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMedicine();
            if (selected == null) return;
            _medicineController.AdjustQuantity(selected.Id, +1, Environment.UserName);
            RefreshData();
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMedicine();
            if (selected == null) return;
            _medicineController.AdjustQuantity(selected.Id, -1, Environment.UserName);
            RefreshData();
        }

        private void btnResolve_Click(object sender, EventArgs e)
        {
            var item = GetSelectedPurchaseItem();
            if (item == null) return;
            _medicineController.MarkPurchaseItemResolved(item.Id, Environment.UserName);
            RefreshData();
        }
    }
}
