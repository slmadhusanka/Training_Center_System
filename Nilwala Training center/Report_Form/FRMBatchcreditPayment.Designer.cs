namespace Nilwala_Training_center.Report_Form
{
    partial class FRMBatchcreditPayment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.viwerBatchCreditPayment = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // viwerBatchCreditPayment
            // 
            this.viwerBatchCreditPayment.ActiveViewIndex = -1;
            this.viwerBatchCreditPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viwerBatchCreditPayment.Cursor = System.Windows.Forms.Cursors.Default;
            this.viwerBatchCreditPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viwerBatchCreditPayment.Location = new System.Drawing.Point(0, 0);
            this.viwerBatchCreditPayment.Name = "viwerBatchCreditPayment";
            this.viwerBatchCreditPayment.Size = new System.Drawing.Size(884, 486);
            this.viwerBatchCreditPayment.TabIndex = 0;
            this.viwerBatchCreditPayment.Load += new System.EventHandler(this.viwerBatchCreditPayment_Load);
            // 
            // FRMBatchcreditPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 486);
            this.Controls.Add(this.viwerBatchCreditPayment);
            this.Name = "FRMBatchcreditPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FRMBatchcreditPayment";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FRMBatchcreditPayment_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer viwerBatchCreditPayment;
    }
}