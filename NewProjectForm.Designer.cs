using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class NewProjectForm : Form
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
            this.projectNameTextBox = new System.Windows.Forms.TextBox();
            this.projectNameLabel = new System.Windows.Forms.Label();
            this.projectLocationLabel = new System.Windows.Forms.Label();
            this.projectDirLabel = new System.Windows.Forms.Label();
            this.locationBrowseButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.createButton = new System.Windows.Forms.Button();
            this.windowPictureBox = new System.Windows.Forms.PictureBox();
            this.windowLabel = new System.Windows.Forms.Label();
            this.windowBrowseButton = new System.Windows.Forms.Button();
            this.windowResolutionLabel = new System.Windows.Forms.Label();
            this.windowWidthTextBox = new System.Windows.Forms.TextBox();
            this.windowResolutionBreakLabel = new System.Windows.Forms.Label();
            this.windowHeightTextBox = new System.Windows.Forms.TextBox();
            this.inputNodesLabel = new System.Windows.Forms.Label();
            this.inputNodesCountLabel = new System.Windows.Forms.Label();
            this.inputNodesHelpLabel = new System.Windows.Forms.Label();
            this.middleNodesLabel = new System.Windows.Forms.Label();
            this.middleNodesCountTextBox = new System.Windows.Forms.TextBox();
            this.outputNodesLabel = new System.Windows.Forms.Label();
            this.outputKeyBindsLabel = new System.Windows.Forms.Label();
            this.toggleMouseButtonsButton = new System.Windows.Forms.Button();
            this.toggleArrowKeysButton = new System.Windows.Forms.Button();
            this.toggleNumberKeysButton = new System.Windows.Forms.Button();
            this.toggleLetterKeysButton = new System.Windows.Forms.Button();
            this.toggleKeypadKeysButton = new System.Windows.Forms.Button();
            this.toggleFunctionKeysButton = new System.Windows.Forms.Button();
            this.outputNodesCountLabel = new System.Windows.Forms.Label();
            this.toggleAllButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.windowPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // projectNameTextBox
            // 
            this.projectNameTextBox.Location = new System.Drawing.Point(136, 6);
            this.projectNameTextBox.Name = "projectNameTextBox";
            this.projectNameTextBox.Size = new System.Drawing.Size(235, 23);
            this.projectNameTextBox.TabIndex = 0;
            this.projectNameTextBox.Text = "New Project";
            this.projectNameTextBox.TextChanged += new System.EventHandler(this.projectNameTextBox_TextChanged);
            // 
            // projectNameLabel
            // 
            this.projectNameLabel.Location = new System.Drawing.Point(12, 6);
            this.projectNameLabel.Name = "projectNameLabel";
            this.projectNameLabel.Size = new System.Drawing.Size(79, 23);
            this.projectNameLabel.TabIndex = 1;
            this.projectNameLabel.Text = "Project Name";
            this.projectNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // projectLocationLabel
            // 
            this.projectLocationLabel.Location = new System.Drawing.Point(12, 48);
            this.projectLocationLabel.Name = "projectLocationLabel";
            this.projectLocationLabel.Size = new System.Drawing.Size(53, 23);
            this.projectLocationLabel.TabIndex = 2;
            this.projectLocationLabel.Text = "Location";
            this.projectLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // projectDirLabel
            // 
            this.projectDirLabel.AutoEllipsis = true;
            this.projectDirLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectDirLabel.Location = new System.Drawing.Point(136, 48);
            this.projectDirLabel.MaximumSize = new System.Drawing.Size(235, 0);
            this.projectDirLabel.Name = "projectDirLabel";
            this.projectDirLabel.Size = new System.Drawing.Size(235, 23);
            this.projectDirLabel.TabIndex = 3;
            this.projectDirLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // locationBrowseButton
            // 
            this.locationBrowseButton.Location = new System.Drawing.Point(377, 48);
            this.locationBrowseButton.Name = "locationBrowseButton";
            this.locationBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.locationBrowseButton.TabIndex = 4;
            this.locationBrowseButton.Text = "Browse";
            this.locationBrowseButton.UseVisualStyleBackColor = true;
            this.locationBrowseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(393, 500);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(97, 23);
            this.createButton.TabIndex = 5;
            this.createButton.Text = "Create Project";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // windowPictureBox
            // 
            this.windowPictureBox.BackColor = System.Drawing.Color.Black;
            this.windowPictureBox.Location = new System.Drawing.Point(136, 90);
            this.windowPictureBox.Name = "windowPictureBox";
            this.windowPictureBox.Size = new System.Drawing.Size(235, 132);
            this.windowPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.windowPictureBox.TabIndex = 6;
            this.windowPictureBox.TabStop = false;
            // 
            // windowLabel
            // 
            this.windowLabel.Location = new System.Drawing.Point(12, 90);
            this.windowLabel.Name = "windowLabel";
            this.windowLabel.Size = new System.Drawing.Size(53, 23);
            this.windowLabel.TabIndex = 7;
            this.windowLabel.Text = "Window";
            // 
            // windowBrowseButton
            // 
            this.windowBrowseButton.Location = new System.Drawing.Point(377, 199);
            this.windowBrowseButton.Name = "windowBrowseButton";
            this.windowBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.windowBrowseButton.TabIndex = 8;
            this.windowBrowseButton.Text = "Browse";
            this.windowBrowseButton.UseVisualStyleBackColor = true;
            this.windowBrowseButton.Click += new System.EventHandler(this.windowBrowseButton_Click);
            // 
            // windowResolutionLabel
            // 
            this.windowResolutionLabel.Location = new System.Drawing.Point(12, 241);
            this.windowResolutionLabel.Name = "windowResolutionLabel";
            this.windowResolutionLabel.Size = new System.Drawing.Size(63, 23);
            this.windowResolutionLabel.TabIndex = 9;
            this.windowResolutionLabel.Text = "Resolution";
            this.windowResolutionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // windowWidthTextBox
            // 
            this.windowWidthTextBox.Location = new System.Drawing.Point(136, 241);
            this.windowWidthTextBox.Name = "windowWidthTextBox";
            this.windowWidthTextBox.Size = new System.Drawing.Size(71, 23);
            this.windowWidthTextBox.TabIndex = 10;
            this.windowWidthTextBox.Text = "1";
            this.windowWidthTextBox.TextChanged += new System.EventHandler(this.windowWidthTextBox_TextChanged);
            // 
            // windowResolutionBreakLabel
            // 
            this.windowResolutionBreakLabel.Location = new System.Drawing.Point(213, 241);
            this.windowResolutionBreakLabel.Name = "windowResolutionBreakLabel";
            this.windowResolutionBreakLabel.Size = new System.Drawing.Size(23, 23);
            this.windowResolutionBreakLabel.TabIndex = 11;
            this.windowResolutionBreakLabel.Text = "x";
            this.windowResolutionBreakLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // windowHeightTextBox
            // 
            this.windowHeightTextBox.Location = new System.Drawing.Point(242, 242);
            this.windowHeightTextBox.Name = "windowHeightTextBox";
            this.windowHeightTextBox.Size = new System.Drawing.Size(71, 23);
            this.windowHeightTextBox.TabIndex = 12;
            this.windowHeightTextBox.Text = "1";
            this.windowHeightTextBox.TextChanged += new System.EventHandler(this.windowHeightTextBox_TextChanged);
            // 
            // inputNodesLabel
            // 
            this.inputNodesLabel.Location = new System.Drawing.Point(12, 283);
            this.inputNodesLabel.Name = "inputNodesLabel";
            this.inputNodesLabel.Size = new System.Drawing.Size(79, 23);
            this.inputNodesLabel.TabIndex = 13;
            this.inputNodesLabel.Text = "Input Nodes";
            this.inputNodesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // inputNodesCountLabel
            // 
            this.inputNodesCountLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputNodesCountLabel.Location = new System.Drawing.Point(136, 283);
            this.inputNodesCountLabel.Name = "inputNodesCountLabel";
            this.inputNodesCountLabel.Size = new System.Drawing.Size(71, 23);
            this.inputNodesCountLabel.TabIndex = 14;
            this.inputNodesCountLabel.Text = "1";
            this.inputNodesCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // inputNodesHelpLabel
            // 
            this.inputNodesHelpLabel.Location = new System.Drawing.Point(213, 283);
            this.inputNodesHelpLabel.Name = "inputNodesHelpLabel";
            this.inputNodesHelpLabel.Size = new System.Drawing.Size(100, 23);
            this.inputNodesHelpLabel.TabIndex = 15;
            this.inputNodesHelpLabel.Text = "(Width x Height)";
            this.inputNodesHelpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // middleNodesLabel
            // 
            this.middleNodesLabel.Location = new System.Drawing.Point(12, 325);
            this.middleNodesLabel.Name = "middleNodesLabel";
            this.middleNodesLabel.Size = new System.Drawing.Size(86, 23);
            this.middleNodesLabel.TabIndex = 16;
            this.middleNodesLabel.Text = "Middle Nodes";
            this.middleNodesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // middleNodesCountTextBox
            // 
            this.middleNodesCountTextBox.Location = new System.Drawing.Point(136, 324);
            this.middleNodesCountTextBox.Name = "middleNodesCountTextBox";
            this.middleNodesCountTextBox.Size = new System.Drawing.Size(71, 23);
            this.middleNodesCountTextBox.TabIndex = 17;
            this.middleNodesCountTextBox.Text = "0";
            this.middleNodesCountTextBox.TextChanged += new System.EventHandler(this.middleNodesCountTextBox_TextChanged);
            // 
            // outputNodesLabel
            // 
            this.outputNodesLabel.Location = new System.Drawing.Point(12, 367);
            this.outputNodesLabel.Name = "outputNodesLabel";
            this.outputNodesLabel.Size = new System.Drawing.Size(86, 23);
            this.outputNodesLabel.TabIndex = 18;
            this.outputNodesLabel.Text = "Output Nodes";
            this.outputNodesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // outputKeyBindsLabel
            // 
            this.outputKeyBindsLabel.Location = new System.Drawing.Point(12, 409);
            this.outputKeyBindsLabel.Name = "outputKeyBindsLabel";
            this.outputKeyBindsLabel.Size = new System.Drawing.Size(100, 23);
            this.outputKeyBindsLabel.TabIndex = 20;
            this.outputKeyBindsLabel.Text = "Output Key Binds";
            this.outputKeyBindsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggleMouseButtonsButton
            // 
            this.toggleMouseButtonsButton.Location = new System.Drawing.Point(136, 409);
            this.toggleMouseButtonsButton.Name = "toggleMouseButtonsButton";
            this.toggleMouseButtonsButton.Size = new System.Drawing.Size(133, 23);
            this.toggleMouseButtonsButton.TabIndex = 22;
            this.toggleMouseButtonsButton.Text = "Toggle Mouse Buttons";
            this.toggleMouseButtonsButton.UseVisualStyleBackColor = true;
            this.toggleMouseButtonsButton.Click += new System.EventHandler(this.toggleMouseButtonsButton_Click);
            // 
            // toggleArrowKeysButton
            // 
            this.toggleArrowKeysButton.Location = new System.Drawing.Point(275, 409);
            this.toggleArrowKeysButton.Name = "toggleArrowKeysButton";
            this.toggleArrowKeysButton.Size = new System.Drawing.Size(112, 23);
            this.toggleArrowKeysButton.TabIndex = 23;
            this.toggleArrowKeysButton.Text = "Toggle Arrow Keys";
            this.toggleArrowKeysButton.UseVisualStyleBackColor = true;
            this.toggleArrowKeysButton.Click += new System.EventHandler(this.toggleArrowKeysButton_Click);
            // 
            // toggleNumberKeysButton
            // 
            this.toggleNumberKeysButton.Location = new System.Drawing.Point(393, 409);
            this.toggleNumberKeysButton.Name = "toggleNumberKeysButton";
            this.toggleNumberKeysButton.Size = new System.Drawing.Size(125, 23);
            this.toggleNumberKeysButton.TabIndex = 24;
            this.toggleNumberKeysButton.Text = "Toggle Number Keys";
            this.toggleNumberKeysButton.UseVisualStyleBackColor = true;
            this.toggleNumberKeysButton.Click += new System.EventHandler(this.toggleNumberKeysButton_Click);
            // 
            // toggleLetterKeysButton
            // 
            this.toggleLetterKeysButton.Location = new System.Drawing.Point(275, 438);
            this.toggleLetterKeysButton.Name = "toggleLetterKeysButton";
            this.toggleLetterKeysButton.Size = new System.Drawing.Size(112, 23);
            this.toggleLetterKeysButton.TabIndex = 25;
            this.toggleLetterKeysButton.Text = "Toggle Letter Keys";
            this.toggleLetterKeysButton.UseVisualStyleBackColor = true;
            this.toggleLetterKeysButton.Click += new System.EventHandler(this.toggleLetterKeysButton_Click);
            // 
            // toggleKeypadKeysButton
            // 
            this.toggleKeypadKeysButton.Location = new System.Drawing.Point(393, 438);
            this.toggleKeypadKeysButton.Name = "toggleKeypadKeysButton";
            this.toggleKeypadKeysButton.Size = new System.Drawing.Size(125, 23);
            this.toggleKeypadKeysButton.TabIndex = 26;
            this.toggleKeypadKeysButton.Text = "Toggle Keypad Keys";
            this.toggleKeypadKeysButton.UseVisualStyleBackColor = true;
            this.toggleKeypadKeysButton.Click += new System.EventHandler(this.toggleKeypadKeysButton_Click);
            // 
            // toggleFunctionKeysButton
            // 
            this.toggleFunctionKeysButton.Location = new System.Drawing.Point(136, 438);
            this.toggleFunctionKeysButton.Name = "toggleFunctionKeysButton";
            this.toggleFunctionKeysButton.Size = new System.Drawing.Size(133, 23);
            this.toggleFunctionKeysButton.TabIndex = 27;
            this.toggleFunctionKeysButton.Text = "Toggle Function Keys";
            this.toggleFunctionKeysButton.UseVisualStyleBackColor = true;
            this.toggleFunctionKeysButton.Click += new System.EventHandler(this.toggleFunctionKeysButton_Click);
            // 
            // outputNodesCountLabel
            // 
            this.outputNodesCountLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputNodesCountLabel.Location = new System.Drawing.Point(136, 367);
            this.outputNodesCountLabel.Name = "outputNodesCountLabel";
            this.outputNodesCountLabel.Size = new System.Drawing.Size(71, 23);
            this.outputNodesCountLabel.TabIndex = 28;
            this.outputNodesCountLabel.Text = "0";
            this.outputNodesCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toggleAllButton
            // 
            this.toggleAllButton.Location = new System.Drawing.Point(524, 409);
            this.toggleAllButton.Name = "toggleAllButton";
            this.toggleAllButton.Size = new System.Drawing.Size(75, 23);
            this.toggleAllButton.TabIndex = 29;
            this.toggleAllButton.Text = "Toggle All";
            this.toggleAllButton.UseVisualStyleBackColor = true;
            this.toggleAllButton.Click += new System.EventHandler(this.toggleAllButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(524, 438);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 30;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // NewProjectForm
            // 
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 42);
            this.ClientSize = new System.Drawing.Size(669, 567);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.toggleAllButton);
            this.Controls.Add(this.outputNodesCountLabel);
            this.Controls.Add(this.toggleFunctionKeysButton);
            this.Controls.Add(this.toggleKeypadKeysButton);
            this.Controls.Add(this.toggleLetterKeysButton);
            this.Controls.Add(this.toggleNumberKeysButton);
            this.Controls.Add(this.toggleArrowKeysButton);
            this.Controls.Add(this.toggleMouseButtonsButton);
            this.Controls.Add(this.outputKeyBindsLabel);
            this.Controls.Add(this.outputNodesLabel);
            this.Controls.Add(this.middleNodesCountTextBox);
            this.Controls.Add(this.middleNodesLabel);
            this.Controls.Add(this.inputNodesHelpLabel);
            this.Controls.Add(this.inputNodesCountLabel);
            this.Controls.Add(this.inputNodesLabel);
            this.Controls.Add(this.windowHeightTextBox);
            this.Controls.Add(this.windowResolutionBreakLabel);
            this.Controls.Add(this.windowWidthTextBox);
            this.Controls.Add(this.windowResolutionLabel);
            this.Controls.Add(this.windowBrowseButton);
            this.Controls.Add(this.windowLabel);
            this.Controls.Add(this.windowPictureBox);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.locationBrowseButton);
            this.Controls.Add(this.projectDirLabel);
            this.Controls.Add(this.projectLocationLabel);
            this.Controls.Add(this.projectNameLabel);
            this.Controls.Add(this.projectNameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NewProjectForm";
            this.Text = "New Project";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewProjectFormClosing);
            this.Load += new System.EventHandler(this.NewProjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.windowPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private TextBox projectNameTextBox;
        private Label projectNameLabel;
        private Label projectLocationLabel;
        private Label projectDirLabel;
        private Button locationBrowseButton;
        private FolderBrowserDialog folderBrowserDialog;
        private Button createButton;
        private PictureBox windowPictureBox;
        private Label windowLabel;
        private Button windowBrowseButton;
        private Label windowResolutionLabel;
        private TextBox windowWidthTextBox;
        private Label windowResolutionBreakLabel;
        private TextBox windowHeightTextBox;
        private Label inputNodesLabel;
        private Label inputNodesCountLabel;
        private Label inputNodesHelpLabel;
        private Label middleNodesLabel;
        private TextBox middleNodesCountTextBox;
        private Label outputNodesLabel;
        private Label outputKeyBindsLabel;
        private Button toggleMouseButtonsButton;
        private Button toggleArrowKeysButton;
        private Button toggleNumberKeysButton;
        private Button toggleLetterKeysButton;
        private Button toggleKeypadKeysButton;
        private Button toggleFunctionKeysButton;
        private Label outputNodesCountLabel;
        private Button toggleAllButton;
        private Button resetButton;
    }
}

