using System;
using System.Windows.Forms;

namespace MedicalAppointmentSystem
{
    partial class AppointmentForm
    {
        private System.ComponentModel.IContainer components = null;

        private ComboBox cmbDoctors;
        private ComboBox cmbPatients;
        private DateTimePicker dtpAppointmentDate;
        private TextBox txtNotes;
        private Button btnBookAppointment;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbDoctors = new System.Windows.Forms.ComboBox();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.dtpAppointmentDate = new System.Windows.Forms.DateTimePicker();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnBookAppointment = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            // 
            // cmbDoctors
            // 
            this.cmbDoctors.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDoctors.Location = new System.Drawing.Point(20, 20);
            this.cmbDoctors.Name = "cmbDoctors";
            this.cmbDoctors.Size = new System.Drawing.Size(300, 23);

            // Doctor label
            var lblDoctor = new System.Windows.Forms.Label();
            lblDoctor.Text = "Doctor:";
            lblDoctor.Location = new System.Drawing.Point(20, 0);
            lblDoctor.AutoSize = true;
            this.Controls.Add(lblDoctor);
            
            // 
            // cmbPatients
            // 
            this.cmbPatients.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPatients.Location = new System.Drawing.Point(20, 60);
            this.cmbPatients.Name = "cmbPatients";
            this.cmbPatients.Size = new System.Drawing.Size(300, 23);

            // Patient label
            var lblPatient = new System.Windows.Forms.Label();
            lblPatient.Text = "Patient:";
            lblPatient.Location = new System.Drawing.Point(20, 40);
            lblPatient.AutoSize = true;
            this.Controls.Add(lblPatient);
            
            // 
            // dtpAppointmentDate
            // 
            this.dtpAppointmentDate.Location = new System.Drawing.Point(20, 100);
            this.dtpAppointmentDate.Name = "dtpAppointmentDate";
            this.dtpAppointmentDate.Size = new System.Drawing.Size(300, 23);

            // Date label
            var lblDate = new System.Windows.Forms.Label();
            lblDate.Text = "Date:";
            lblDate.Location = new System.Drawing.Point(20, 80);
            lblDate.AutoSize = true;
            this.Controls.Add(lblDate);
            
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(20, 140);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(300, 80);
            this.txtNotes.Multiline = true;

            var lblNotes = new System.Windows.Forms.Label();
            lblNotes.Text = "Notes:";
            lblNotes.Location = new System.Drawing.Point(20, 120);
            lblNotes.AutoSize = true;
            this.Controls.Add(lblNotes);
            
            // 
            // btnBookAppointment
            // 
            this.btnBookAppointment.Location = new System.Drawing.Point(20, 235);
            this.btnBookAppointment.Name = "btnBookAppointment";
            this.btnBookAppointment.Size = new System.Drawing.Size(140, 30);
            this.btnBookAppointment.Text = "Book Appointment";
            this.btnBookAppointment.UseVisualStyleBackColor = true;
            this.btnBookAppointment.Click += new System.EventHandler(this.btnBookAppointment_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(180, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 30);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // 
            // AppointmentForm
            // 
            this.ClientSize = new System.Drawing.Size(350, 280);
            this.Controls.Add(this.cmbDoctors);
            this.Controls.Add(this.cmbPatients);
            this.Controls.Add(this.dtpAppointmentDate);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.btnBookAppointment);
            this.Controls.Add(this.btnCancel);
            this.Text = "Book Appointment";
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }
}
