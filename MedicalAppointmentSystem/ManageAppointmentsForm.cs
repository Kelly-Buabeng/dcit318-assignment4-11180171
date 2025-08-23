using System;
using System.Data;
using System.Windows.Forms;

namespace MedicalAppointmentSystem
{
    public partial class ManageAppointmentsForm : Form
    {
        public ManageAppointmentsForm()
        {
            InitializeComponent();
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            try
            {
                using (var connection = new Microsoft.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MedicalDBConnection"].ConnectionString))
                {
                    string query = @"SELECT a.AppointmentID, d.FullName as Doctor, p.FullName as Patient, 
                                   a.AppointmentDate, a.Notes 
                                   FROM Appointments a 
                                   INNER JOIN Doctors d ON a.DoctorID = d.DoctorID 
                                   INNER JOIN Patients p ON a.PatientID = p.PatientID 
                                   ORDER BY a.AppointmentDate";

                    var adapter = new Microsoft.Data.SqlClient.SqlDataAdapter(query, connection);
                    var ds = new DataSet();
                    adapter.Fill(ds, "Appointments");
                    var table = ds.Tables["Appointments"];

                    if (dgvAppointments != null)
                    {
                        dgvAppointments.DataSource = table;
                        dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvAppointments != null && dgvAppointments.SelectedRows.Count > 0)
            {
                UpdateAppointment();
            }
            else
            {
                MessageBox.Show("Please select an appointment to update.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateAppointment()
        {
            try
            {
                if (dgvAppointments == null || dgvAppointments.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an appointment to update.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataGridViewRow selectedRow = dgvAppointments.SelectedRows[0];

                DataGridViewCell? appointmentCell = selectedRow.Cells["AppointmentDate"];
                object? appointmentObj = appointmentCell?.Value;
                DateTime appointmentDate;
                if (appointmentObj == null || !DateTime.TryParse(appointmentObj.ToString(), out appointmentDate))
                {
                    appointmentDate = DateTime.Now;
                }

                DataGridViewCell? notesCell = selectedRow.Cells["Notes"];
                object? notesObj = notesCell?.Value;
                string notes = notesObj?.ToString() ?? string.Empty;

                DataGridViewCell? idCell = selectedRow.Cells["AppointmentID"];
                object? idObj = idCell?.Value;
                if (idObj == null || !int.TryParse(idObj.ToString(), out int appointmentId))
                {
                    MessageBox.Show("Selected appointment has no valid ID.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Use DB update
                using (var connection = new Microsoft.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MedicalDBConnection"].ConnectionString))
                {
                    string updateQuery = @"UPDATE Appointments SET AppointmentDate = @AppointmentDate, Notes = @Notes WHERE AppointmentID = @AppointmentID";
                    var cmd = new Microsoft.Data.SqlClient.SqlCommand(updateQuery, connection);
                    cmd.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                     {
                         MessageBox.Show("Appointment updated successfully!", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information);
                         LoadAppointments();
                     }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating appointment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAppointments != null && dgvAppointments.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this appointment?", 
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DeleteAppointment();
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment to delete.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteAppointment()
        {
            try
            {
                if (dgvAppointments == null || dgvAppointments.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an appointment to delete.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataGridViewRow selectedRow = dgvAppointments.SelectedRows[0];
                DataGridViewCell? idCell = selectedRow.Cells["AppointmentID"];
                object? idObj = idCell?.Value;
                if (idObj == null || !int.TryParse(idObj.ToString(), out int appointmentId))
                {
                    MessageBox.Show("Selected appointment has no valid ID.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var connection = new Microsoft.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MedicalDBConnection"].ConnectionString))
                {
                    string deleteQuery = "DELETE FROM Appointments WHERE AppointmentID = @AppointmentID";
                    var cmd = new Microsoft.Data.SqlClient.SqlCommand(deleteQuery, connection);
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                     {
                         MessageBox.Show("Appointment deleted successfully!", "Success",
                             MessageBoxButtons.OK, MessageBoxIcon.Information);
                         LoadAppointments();
                     }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting appointment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAppointments();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvAppointments_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Enable update button when cell is edited
            if (btnUpdate != null) btnUpdate.Enabled = true;
        }
    }
}