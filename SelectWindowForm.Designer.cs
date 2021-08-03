using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class SelectWindowForm : Form
    {
        //  Required designer variable.
        private System.ComponentModel.IContainer components = null;

        //  Clean up any resources being used.
        //  disposing = true if managed resources should be disposed; otherwise, false.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        //  Required method for Designer support - do not modify
        //  the contents of this method with the code editor.
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SelectWindowForm
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(534, 511);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SelectWindowForm";
            this.Text = "Select Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectWindowFormClosing);
            this.Load += new System.EventHandler(this.SelectWindowForm_Load);
            this.ResumeLayout(false);

        }
        #endregion
    }
}

