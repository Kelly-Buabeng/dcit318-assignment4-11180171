using System;
using System.Windows.Forms;

namespace MedicalAppointmentSystem
{
    partial class DoctorListForm
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvDoctors;
        private Button btnClose;

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
            this.dgvDoctors = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();

            // 
            // dgvDoctors
            // 
            this.dgvDoctors.Location = new System.Drawing.Point(12, 12);
            this.dgvDoctors.Name = "dgvDoctors";
            this.dgvDoctors.Size = new System.Drawing.Size(560, 300);
            this.dgvDoctors.ReadOnly = true;
            this.dgvDoctors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoctors.BorderStyle = BorderStyle.None;

            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(472, 320);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // 
            // DoctorListForm
            // 
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.dgvDoctors);
            this.Controls.Add(this.btnClose);
            this.Text = "Doctors";
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }
}
