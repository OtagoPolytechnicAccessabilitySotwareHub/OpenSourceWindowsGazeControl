namespace GazeToolBar
{
    partial class SettingsCrosshairPage
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
            this.components = new System.ComponentModel.Container();
            this.bhavCrosshairMap = new EyeXFramework.Forms.BehaviorMap(this.components);
            this.pictureBoxCrosshairPreview = new System.Windows.Forms.PictureBox();
            this.panelCrosshairSelection = new System.Windows.Forms.Panel();
            this.panelCrosshairHolder = new System.Windows.Forms.Panel();
            this.pnlCrosshairUpButton = new System.Windows.Forms.Panel();
            this.buttonCrosshairUp = new System.Windows.Forms.Button();
            this.trackBarCrosshair = new System.Windows.Forms.TrackBar();
            this.pnlCrosshairDownButton = new System.Windows.Forms.Panel();
            this.buttonCrosshairDown = new System.Windows.Forms.Button();
            this.labCrosshairType = new System.Windows.Forms.Label();
            this.panelSaveAndCancel = new System.Windows.Forms.Panel();
            this.pnlCancel = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlSave = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCrosshairPage = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCrosshairPreview)).BeginInit();
            this.panelCrosshairSelection.SuspendLayout();
            this.panelCrosshairHolder.SuspendLayout();
            this.pnlCrosshairUpButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCrosshair)).BeginInit();
            this.pnlCrosshairDownButton.SuspendLayout();
            this.panelSaveAndCancel.SuspendLayout();
            this.pnlCancel.SuspendLayout();
            this.pnlSave.SuspendLayout();
            this.pnlCrosshairPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxCrosshairPreview
            // 
            this.pictureBoxCrosshairPreview.BackColor = System.Drawing.Color.White;
            this.pictureBoxCrosshairPreview.Location = new System.Drawing.Point(621, 260);
            this.pictureBoxCrosshairPreview.Name = "pictureBoxCrosshairPreview";
            this.pictureBoxCrosshairPreview.Size = new System.Drawing.Size(150, 150);
            this.pictureBoxCrosshairPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCrosshairPreview.TabIndex = 15;
            this.pictureBoxCrosshairPreview.TabStop = false;
            // 
            // panelCrosshairSelection
            // 
            this.panelCrosshairSelection.BackColor = System.Drawing.Color.Transparent;
            this.panelCrosshairSelection.Controls.Add(this.panelCrosshairHolder);
            this.panelCrosshairSelection.Controls.Add(this.labCrosshairType);
            this.panelCrosshairSelection.Location = new System.Drawing.Point(26, 36);
            this.panelCrosshairSelection.Name = "panelCrosshairSelection";
            this.panelCrosshairSelection.Size = new System.Drawing.Size(1347, 165);
            this.panelCrosshairSelection.TabIndex = 14;
            // 
            // panelCrosshairHolder
            // 
            this.panelCrosshairHolder.BackColor = System.Drawing.Color.Transparent;
            this.panelCrosshairHolder.Controls.Add(this.pnlCrosshairUpButton);
            this.panelCrosshairHolder.Controls.Add(this.trackBarCrosshair);
            this.panelCrosshairHolder.Controls.Add(this.pnlCrosshairDownButton);
            this.panelCrosshairHolder.Location = new System.Drawing.Point(115, 2);
            this.panelCrosshairHolder.Margin = new System.Windows.Forms.Padding(2);
            this.panelCrosshairHolder.Name = "panelCrosshairHolder";
            this.panelCrosshairHolder.Size = new System.Drawing.Size(1232, 160);
            this.panelCrosshairHolder.TabIndex = 22;
            // 
            // pnlCrosshairUpButton
            // 
            this.pnlCrosshairUpButton.Controls.Add(this.buttonCrosshairUp);
            this.pnlCrosshairUpButton.Location = new System.Drawing.Point(1076, 2);
            this.pnlCrosshairUpButton.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCrosshairUpButton.Name = "pnlCrosshairUpButton";
            this.pnlCrosshairUpButton.Size = new System.Drawing.Size(154, 155);
            this.pnlCrosshairUpButton.TabIndex = 23;
            // 
            // buttonCrosshairUp
            // 
            this.buttonCrosshairUp.BackColor = System.Drawing.Color.Transparent;
            this.buttonCrosshairUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCrosshairUp.Font = new System.Drawing.Font("SimSun", 72F, System.Drawing.FontStyle.Bold);
            this.buttonCrosshairUp.ForeColor = System.Drawing.Color.White;
            this.buttonCrosshairUp.Location = new System.Drawing.Point(2, 2);
            this.buttonCrosshairUp.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCrosshairUp.Name = "buttonCrosshairUp";
            this.buttonCrosshairUp.Size = new System.Drawing.Size(150, 150);
            this.buttonCrosshairUp.TabIndex = 13;
            this.buttonCrosshairUp.Text = "+";
            this.buttonCrosshairUp.UseVisualStyleBackColor = false;
            this.buttonCrosshairUp.Click += new System.EventHandler(this.buttonCrosshairUp_Click);
            // 
            // trackBarCrosshair
            // 
            this.trackBarCrosshair.BackColor = System.Drawing.Color.Black;
            this.trackBarCrosshair.Location = new System.Drawing.Point(170, 58);
            this.trackBarCrosshair.Maximum = 8;
            this.trackBarCrosshair.Name = "trackBarCrosshair";
            this.trackBarCrosshair.Size = new System.Drawing.Size(896, 45);
            this.trackBarCrosshair.TabIndex = 12;
            this.trackBarCrosshair.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarCrosshair.Value = 5;
            this.trackBarCrosshair.ValueChanged += new System.EventHandler(this.trackBarCrosshair_ValueChanged);
            // 
            // pnlCrosshairDownButton
            // 
            this.pnlCrosshairDownButton.Controls.Add(this.buttonCrosshairDown);
            this.pnlCrosshairDownButton.Location = new System.Drawing.Point(2, 2);
            this.pnlCrosshairDownButton.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCrosshairDownButton.Name = "pnlCrosshairDownButton";
            this.pnlCrosshairDownButton.Size = new System.Drawing.Size(154, 155);
            this.pnlCrosshairDownButton.TabIndex = 22;
            // 
            // buttonCrosshairDown
            // 
            this.buttonCrosshairDown.BackColor = System.Drawing.Color.Transparent;
            this.buttonCrosshairDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCrosshairDown.Font = new System.Drawing.Font("SimSun", 72F, System.Drawing.FontStyle.Bold);
            this.buttonCrosshairDown.ForeColor = System.Drawing.Color.White;
            this.buttonCrosshairDown.Location = new System.Drawing.Point(2, 2);
            this.buttonCrosshairDown.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCrosshairDown.Name = "buttonCrosshairDown";
            this.buttonCrosshairDown.Size = new System.Drawing.Size(150, 150);
            this.buttonCrosshairDown.TabIndex = 14;
            this.buttonCrosshairDown.Text = "-";
            this.buttonCrosshairDown.UseVisualStyleBackColor = false;
            this.buttonCrosshairDown.Click += new System.EventHandler(this.buttonCrosshairDown_Click);
            // 
            // labCrosshairType
            // 
            this.labCrosshairType.AutoSize = true;
            this.labCrosshairType.Font = new System.Drawing.Font("Arial", 18F);
            this.labCrosshairType.ForeColor = System.Drawing.Color.White;
            this.labCrosshairType.Location = new System.Drawing.Point(3, 60);
            this.labCrosshairType.Name = "labCrosshairType";
            this.labCrosshairType.Size = new System.Drawing.Size(114, 54);
            this.labCrosshairType.TabIndex = 11;
            this.labCrosshairType.Text = "Crosshair\r\nType";
            // 
            // panelSaveAndCancel
            // 
            this.panelSaveAndCancel.Controls.Add(this.pnlCancel);
            this.panelSaveAndCancel.Controls.Add(this.pnlSave);
            this.panelSaveAndCancel.Location = new System.Drawing.Point(323, 565);
            this.panelSaveAndCancel.Margin = new System.Windows.Forms.Padding(2);
            this.panelSaveAndCancel.Name = "panelSaveAndCancel";
            this.panelSaveAndCancel.Size = new System.Drawing.Size(782, 164);
            this.panelSaveAndCancel.TabIndex = 35;
            // 
            // pnlCancel
            // 
            this.pnlCancel.Controls.Add(this.btnCancel);
            this.pnlCancel.Location = new System.Drawing.Point(626, 2);
            this.pnlCancel.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCancel.Name = "pnlCancel";
            this.pnlCancel.Size = new System.Drawing.Size(154, 155);
            this.pnlCancel.TabIndex = 22;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.BorderSize = 3;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 24F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(2, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 150);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlSave
            // 
            this.pnlSave.Controls.Add(this.btnSave);
            this.pnlSave.Location = new System.Drawing.Point(5, 2);
            this.pnlSave.Margin = new System.Windows.Forms.Padding(2);
            this.pnlSave.Name = "pnlSave";
            this.pnlSave.Size = new System.Drawing.Size(154, 155);
            this.pnlSave.TabIndex = 22;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.BorderSize = 3;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(2, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 150);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(502, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(687, 91);
            this.label1.TabIndex = 36;
            this.label1.Text = "Crosshair Settings";
            // 
            // pnlCrosshairPage
            // 
            this.pnlCrosshairPage.BackColor = System.Drawing.Color.Transparent;
            this.pnlCrosshairPage.Controls.Add(this.panelCrosshairSelection);
            this.pnlCrosshairPage.Controls.Add(this.pictureBoxCrosshairPreview);
            this.pnlCrosshairPage.Location = new System.Drawing.Point(12, 196);
            this.pnlCrosshairPage.Name = "pnlCrosshairPage";
            this.pnlCrosshairPage.Size = new System.Drawing.Size(1465, 342);
            this.pnlCrosshairPage.TabIndex = 37;
            // 
            // SettingsCrosshairPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1740, 777);
            this.ControlBox = false;
            this.Controls.Add(this.pnlCrosshairPage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelSaveAndCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsCrosshairPage";
            this.ShowInTaskbar = false;
            this.Text = "0.";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SettingsCrosshairPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCrosshairPreview)).EndInit();
            this.panelCrosshairSelection.ResumeLayout(false);
            this.panelCrosshairSelection.PerformLayout();
            this.panelCrosshairHolder.ResumeLayout(false);
            this.panelCrosshairHolder.PerformLayout();
            this.pnlCrosshairUpButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCrosshair)).EndInit();
            this.pnlCrosshairDownButton.ResumeLayout(false);
            this.panelSaveAndCancel.ResumeLayout(false);
            this.pnlCancel.ResumeLayout(false);
            this.pnlSave.ResumeLayout(false);
            this.pnlCrosshairPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private EyeXFramework.Forms.BehaviorMap bhavCrosshairMap;
        private System.Windows.Forms.PictureBox pictureBoxCrosshairPreview;
        private System.Windows.Forms.Panel panelCrosshairSelection;
        private System.Windows.Forms.Panel panelCrosshairHolder;
        private System.Windows.Forms.Panel pnlCrosshairUpButton;
        private System.Windows.Forms.Button buttonCrosshairUp;
        private System.Windows.Forms.TrackBar trackBarCrosshair;
        private System.Windows.Forms.Panel pnlCrosshairDownButton;
        private System.Windows.Forms.Button buttonCrosshairDown;
        private System.Windows.Forms.Label labCrosshairType;
        private System.Windows.Forms.Panel panelSaveAndCancel;
        private System.Windows.Forms.Panel pnlCancel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlSave;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlCrosshairPage;
    }
}