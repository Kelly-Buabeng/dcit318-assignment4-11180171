using System;
using System.Windows.Forms;

namespace MedicalAppointmentSystem
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        
        private Button btnViewDoctors;
        private Button btnBookAppointment;
        private Button btnManageAppointments;
        private Label lblTitle;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnViewDoctors = new System.Windows.Forms.Button();
            this.btnBookAppointment = new System.Windows.Forms.Button();
            this.btnManageAppointments = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(100, 30);
            this.lblTitle.Size = new System.Drawing.Size(300, 26);
            this.lblTitle.Text = "Medical Appointment System";
            
            // btnViewDoctors
            this.btnViewDoctors.Location = new System.Drawing.Point(150, 100);
            this.btnViewDoctors.Size = new System.Drawing.Size(200, 40);
            this.btnViewDoctors.Text = "View Doctors";
            this.btnViewDoctors.Click += new System.EventHandler(this.btnViewDoctors_Click);
            
            // btnBookAppointment
            this.btnBookAppointment.Location = new System.Drawing.Point(150, 160);
            this.btnBookAppointment.Size = new System.Drawing.Size(200, 40);
            this.btnBookAppointment.Text = "Book Appointment";
            this.btnBookAppointment.Click += new System.EventHandler(this.btnBookAppointment_Click);
            
            // btnManageAppointments
            this.btnManageAppointments.Location = new System.Drawing.Point(150, 220);
            this.btnManageAppointments.Size = new System.Drawing.Size(200, 40);
            this.btnManageAppointments.Text = "Manage Appointments";
            this.btnManageAppointments.Click += new System.EventHandler(this.btnManageAppointments_Click);
            
            // MainForm
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.btnViewDoctors, this.btnBookAppointment, this.btnManageAppointments});
            this.Text = "Medical Appointment System";
            this.ResumeLayout(false);
        }
    }
}