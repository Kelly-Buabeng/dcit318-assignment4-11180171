using System;
using System.Data;
using System.Windows.Forms;

namespace MedicalAppointmentSystem
{
    public partial class AppointmentForm : Form
    {
        public AppointmentForm()
        {
            InitializeComponent();
            LoadDoctors();
            LoadPatients();
        }

        private void LoadDoctors()
        {
            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
                {
                    string query = "SELECT DoctorID, FullName + ' - ' + Specialty as DoctorInfo FROM Doctors WHERE Availability = 1 ORDER BY FullName";

                    Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand(query, connection);
                    connection.Open();

                    Microsoft.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();

                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    cmbDoctors.DataSource = dataTable;
                    cmbDoctors.DisplayMember = "DoctorInfo";
                    cmbDoctors.ValueMember = "DoctorID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPatients()
        {
            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
                {
                    string query = "SELECT PatientID, FullName FROM Patients ORDER BY FullName";

                    Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand(query, connection);
                    connection.Open();

                    Microsoft.Data.SqlClient.SqlDataReader reader = command.ExecuteReader();

                    var dataTable = new DataTable();
                    dataTable.Load(reader);

                    cmbPatients.DataSource = dataTable;
                    cmbPatients.DisplayMember = "FullName";
                    cmbPatients.ValueMember = "PatientID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patients: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBookAppointment_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                BookAppointment();
            }
        }

        private bool ValidateInput()
        {
            if (cmbDoctors.SelectedValue == null)
            {
                MessageBox.Show("Please select a doctor.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbPatients.SelectedValue == null)
            {
                MessageBox.Show("Please select a patient.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var appointmentDate = dtpAppointmentDate?.Value ?? DateTime.Now;
            if (appointmentDate < DateTime.Now)
            {
                MessageBox.Show("Appointment date cannot be in the past.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BookAppointment()
        {
            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Appointments (DoctorID, PatientID, AppointmentDate, Notes) 
                                   VALUES (@DoctorID, @PatientID, @AppointmentDate, @Notes)";

                    Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand(query, connection);

                    int doctorId = Convert.ToInt32(cmbDoctors.SelectedValue ?? 0);
                    int patientId = Convert.ToInt32(cmbPatients.SelectedValue ?? 0);
                    DateTime appointmentDate = dtpAppointmentDate?.Value ?? DateTime.Now;
                    string notes = txtNotes?.Text ?? string.Empty;

                    command.Parameters.AddWithValue("@DoctorID", doctorId);
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                    command.Parameters.AddWithValue("@Notes", notes);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Appointment booked successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error booking appointment: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            if (cmbDoctors.DataSource != null)
            {
                cmbDoctors.SelectedIndex = -1;
            }
            else
            {
                cmbDoctors.SelectedItem = null;
            }

            if (cmbPatients.DataSource != null)
            {
                cmbPatients.SelectedIndex = -1;
            }
            else
            {
                cmbPatients.SelectedItem = null;
            }

            if (dtpAppointmentDate != null) dtpAppointmentDate.Value = DateTime.Now;
            if (txtNotes != null) txtNotes.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}