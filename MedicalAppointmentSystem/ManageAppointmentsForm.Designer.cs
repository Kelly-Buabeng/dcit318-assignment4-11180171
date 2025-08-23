using System;
using System.Windows.Forms;

namespace MedicalAppointmentSystem
{
    partial class ManageAppointmentsForm
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvAppointments;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
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
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            // 
            // dgvAppointments
            // 
            this.dgvAppointments.Location = new System.Drawing.Point(12, 12);
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.Size = new System.Drawing.Size(760, 300);
            this.dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointments.BorderStyle = BorderStyle.None;
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.RowHeadersVisible = false;
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAppointments.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250,250,252);
            this.dgvAppointments.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(6);
            this.dgvAppointments.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvAppointments.EnableHeadersVisualStyles = false;
            this.dgvAppointments.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240,240,245);

            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(12, 320);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 30);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(118, 320);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(224, 320);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(676, 320);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // 
            // ManageAppointmentsForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.dgvAppointments);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);
            this.Text = "Manage Appointments";
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }
}
