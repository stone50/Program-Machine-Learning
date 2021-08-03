using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class MainForm : Form
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.randomizeNetworkButton = new System.Windows.Forms.Button();
            this.sendOutputsCheckBox = new System.Windows.Forms.CheckBox();
            this.windowPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.windowPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(741, 24);
            this.mainMenuStrip.TabIndex = 5;
            // 
            // randomizeNetworkButton
            // 
            this.randomizeNetworkButton.Location = new System.Drawing.Point(26, 70);
            this.randomizeNetworkButton.Name = "randomizeNetworkButton";
            this.randomizeNetworkButton.Size = new System.Drawing.Size(137, 23);
            this.randomizeNetworkButton.TabIndex = 7;
            this.randomizeNetworkButton.Text = "Randomize Network";
            this.randomizeNetworkButton.UseVisualStyleBackColor = true;
            this.randomizeNetworkButton.Click += new System.EventHandler(this.randomizeNetworkButton_Click);
            // 
            // sendOutputsCheckBox
            // 
            this.sendOutputsCheckBox.AutoSize = true;
            this.sendOutputsCheckBox.Location = new System.Drawing.Point(45, 130);
            this.sendOutputsCheckBox.Name = "sendOutputsCheckBox";
            this.sendOutputsCheckBox.Size = new System.Drawing.Size(98, 19);
            this.sendOutputsCheckBox.TabIndex = 9;
            this.sendOutputsCheckBox.Text = "Send Outputs";
            this.sendOutputsCheckBox.UseVisualStyleBackColor = true;
            this.sendOutputsCheckBox.CheckedChanged += new System.EventHandler(this.sendOutputsCheckBox_CheckedChanged);
            // 
            // windowPictureBox
            // 
            this.windowPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.windowPictureBox.Location = new System.Drawing.Point(219, 37);
            this.windowPictureBox.Name = "windowPictureBox";
            this.windowPictureBox.Size = new System.Drawing.Size(480, 270);
            this.windowPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.windowPictureBox.TabIndex = 10;
            this.windowPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(741, 425);
            this.Controls.Add(this.windowPictureBox);
            this.Controls.Add(this.sendOutputsCheckBox);
            this.Controls.Add(this.randomizeNetworkButton);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.windowPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private MenuStrip mainMenuStrip;
        private FolderBrowserDialog folderBrowserDialog;
        private Button randomizeNetworkButton;
        private CheckBox sendOutputsCheckBox;
        private PictureBox windowPictureBox;
    }
}

