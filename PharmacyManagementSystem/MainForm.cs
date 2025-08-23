using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class MainForm : Form
    {
        private string connectionString;

        public MainForm()
        {
            InitializeComponent();
            connectionString = System.Configuration.ConfigurationManager
                .ConnectionStrings["PharmacyDBConnection"].ConnectionString;
            LoadMedicines();
        }

        private void LoadMedicines()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("GetAllMedicines", connection) { CommandType = CommandType.StoredProcedure })
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvMedicines.DataSource = dt;
                        dgvMedicines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading medicines: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddMedicine_Click(object sender, EventArgs e)
        {
            if (!ValidateMedicineInput()) return;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("AddMedicine", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@Name", txtName.Text);
                    command.Parameters.AddWithValue("@Category", txtCategory.Text);
                    command.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
                    command.Parameters.AddWithValue("@Quantity", int.Parse(txtQuantity.Text));
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Medicine added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    LoadMedicines();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding medicine: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateMedicineInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("Enter name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (string.IsNullOrWhiteSpace(txtCategory.Text)) { MessageBox.Show("Enter category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0) { MessageBox.Show("Enter valid price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (!int.TryParse(txtQuantity.Text, out int q) || q < 0) { MessageBox.Show("Enter valid quantity.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            return true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("SearchMedicine", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@SearchTerm", txtSearch.Text);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvMedicines.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            if (dgvMedicines.SelectedRows.Count == 0) { MessageBox.Show("Select a row.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            try
            {
                var row = dgvMedicines.SelectedRows[0];
                int id = Convert.ToInt32(row.Cells["MedicineID"].Value);
                int qty = Convert.ToInt32(row.Cells["Quantity"].Value);
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("UpdateStock", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@MedicineID", id);
                    command.Parameters.AddWithValue("@Quantity", qty);
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Stock updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMedicines();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecordSale_Click(object sender, EventArgs e)
        {
            if (dgvMedicines.SelectedRows.Count == 0) { MessageBox.Show("Select a row.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            var row = dgvMedicines.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["MedicineID"].Value);
            string name = row.Cells["Name"].Value.ToString();
            int current = Convert.ToInt32(row.Cells["Quantity"].Value);
            string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter qty to sell for {name} (current {current}):", "Record Sale", "1");
            if (!int.TryParse(input, out int sell) || sell <= 0) { MessageBox.Show("Enter valid qty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("RecordSale", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("@MedicineID", id);
                    command.Parameters.AddWithValue("@QuantitySold", sell);
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Sale recorded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMedicines();
                }
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                MessageBox.Show(ex.Message, "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error recording sale: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            LoadMedicines();
            txtSearch.Clear();
        }

        private void ClearInputFields()
        {
            txtName.Clear();
            txtCategory.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
        }

        private void dgvMedicines_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMedicines.Columns[e.ColumnIndex].Name == "Quantity") btnUpdateStock.Enabled = true;
        }
    }
}