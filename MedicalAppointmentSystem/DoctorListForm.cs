using System;
using System.Data;
using System.Windows.Forms;

namespace MedicalAppointmentSystem
{
    public partial class DoctorListForm : Form
    {
        public DoctorListForm()
        {
            InitializeComponent();
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager
                    .ConnectionStrings["MedicalDBConnection"].ConnectionString;

                using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
                {
                    string query = "SELECT DoctorID, FullName, Specialty, CASE WHEN Availability = 1 THEN 'Available' ELSE 'Not Available' END as Status, FullName + ' - ' + Specialty as DoctorInfo FROM Doctors ORDER BY FullName";

                    Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand(query, connection);
                    connection.Open();

                    Microsoft.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    dgvDoctors.DataSource = dataTable;
                    dgvDoctors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // optional UI tweaks
                    if (dgvDoctors.Columns.Contains("DoctorID")) dgvDoctors.Columns["DoctorID"].Visible = false;
                    if (dgvDoctors.Columns.Contains("Status")) dgvDoctors.Columns["Status"].HeaderText = "Availability";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}