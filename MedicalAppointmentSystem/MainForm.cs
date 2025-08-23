using System;
using System.Windows.Forms;

namespace MedicalAppointmentSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnViewDoctors_Click(object sender, EventArgs e)
        {
            using (var doctorForm = new DoctorListForm())
            {
                doctorForm.ShowDialog();
            }
        }

        private void btnBookAppointment_Click(object sender, EventArgs e)
        {
            using (var appointmentForm = new AppointmentForm())
            {
                appointmentForm.ShowDialog();
            }
        }

        private void btnManageAppointments_Click(object sender, EventArgs e)
        {
            using (var manageForm = new ManageAppointmentsForm())
            {
                manageForm.ShowDialog();
            }
        }
    }
}