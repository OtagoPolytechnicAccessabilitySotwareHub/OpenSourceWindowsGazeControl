using System.Windows.Forms;

namespace GazeToolBar
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.bhavSettingMap = new EyeXFramework.Forms.BehaviorMap(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelSaveAndCancel = new System.Windows.Forms.Panel();
            this.pnlCancel = new System.Windows.Forms.Panel();
            this.pnlSave = new System.Windows.Forms.Panel();
            this.pnlPageKeyboard = new System.Windows.Forms.Panel();
            this.pnlLeftClick = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlFKeyHighlight2 = new System.Windows.Forms.Panel();
            this.btClearFKeyLeftClick = new System.Windows.Forms.Button();
            this.pnlFKeyHighlight1 = new System.Windows.Forms.Panel();
            this.btFKeyLeftClick = new System.Windows.Forms.Button();
            this.lbLeft = new System.Windows.Forms.Label();
            this.lbFKeyFeedback = new System.Windows.Forms.Label();
            this.pnlRightClick = new System.Windows.Forms.Panel();
            this.lbRight = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pnlFKeyHighlight3 = new System.Windows.Forms.Panel();
            this.btFKeyRightClick = new System.Windows.Forms.Button();
            this.pnlFKeyHighlight4 = new System.Windows.Forms.Panel();
            this.btClearFKeyRightClick = new System.Windows.Forms.Button();
            this.pnlDoubleClick = new System.Windows.Forms.Panel();
            this.lbDouble = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pnlFKeyHighlight5 = new System.Windows.Forms.Panel();
            this.btFKeyDoubleClick = new System.Windows.Forms.Button();
            this.pnlFKeyHighlight6 = new System.Windows.Forms.Panel();
            this.btClearFKeyDoubleClick = new System.Windows.Forms.Button();
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.lbScroll = new System.Windows.Forms.Label();
            this.pnlFKeyHighlight8 = new System.Windows.Forms.Panel();
            this.btClearFKeyScroll = new System.Windows.Forms.Button();
            this.pnlFKeyHighlight7 = new System.Windows.Forms.Panel();
            this.btFKeyScroll = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pnlFKeyHighlight10 = new System.Windows.Forms.Panel();
            this.btClearFKeyDrapAndDrop = new System.Windows.Forms.Button();
            this.pnlFKeyHighlight9 = new System.Windows.Forms.Panel();
            this.btFKeyDrapAndDrop = new System.Windows.Forms.Button();
            this.bhavGeneralMap = new EyeXFramework.Forms.BehaviorMap(this.components);
            this.bhavZoomMap = new EyeXFramework.Forms.BehaviorMap(this.components);
            this.bhavShortcutMap = new EyeXFramework.Forms.BehaviorMap(this.components);
            this.bhavRearrangeMap = new EyeXFramework.Forms.BehaviorMap(this.components);
            this.bhavCrosshairMap = new EyeXFramework.Forms.BehaviorMap(this.components);
            this.bhavConfirmMap = new EyeXFramework.Forms.BehaviorMap(this.components);
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelSaveAndCancel.SuspendLayout();
            this.pnlCancel.SuspendLayout();
            this.pnlSave.SuspendLayout();
            this.pnlPageKeyboard.SuspendLayout();
            this.pnlLeftClick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlFKeyHighlight2.SuspendLayout();
            this.pnlFKeyHighlight1.SuspendLayout();
            this.pnlRightClick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlFKeyHighlight3.SuspendLayout();
            this.pnlFKeyHighlight4.SuspendLayout();
            this.pnlDoubleClick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.pnlFKeyHighlight5.SuspendLayout();
            this.pnlFKeyHighlight6.SuspendLayout();
            this.pnlScroll.SuspendLayout();
            this.pnlFKeyHighlight8.SuspendLayout();
            this.pnlFKeyHighlight7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.pnlFKeyHighlight10.SuspendLayout();
            this.pnlFKeyHighlight9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
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
            // panelSaveAndCancel
            // 
            this.panelSaveAndCancel.Controls.Add(this.pnlCancel);
            this.panelSaveAndCancel.Controls.Add(this.pnlSave);
            this.panelSaveAndCancel.Location = new System.Drawing.Point(25, 512);
            this.panelSaveAndCancel.Margin = new System.Windows.Forms.Padding(2);
            this.panelSaveAndCancel.Name = "panelSaveAndCancel";
            this.panelSaveAndCancel.Size = new System.Drawing.Size(782, 164);
            this.panelSaveAndCancel.TabIndex = 25;
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
            // pnlSave
            // 
            this.pnlSave.Controls.Add(this.btnSave);
            this.pnlSave.Location = new System.Drawing.Point(5, 2);
            this.pnlSave.Margin = new System.Windows.Forms.Padding(2);
            this.pnlSave.Name = "pnlSave";
            this.pnlSave.Size = new System.Drawing.Size(154, 155);
            this.pnlSave.TabIndex = 22;
            // 
            // pnlPageKeyboard
            // 
            this.pnlPageKeyboard.BackColor = System.Drawing.Color.Black;
            this.pnlPageKeyboard.Controls.Add(this.pnlLeftClick);
            this.pnlPageKeyboard.Controls.Add(this.lbFKeyFeedback);
            this.pnlPageKeyboard.Controls.Add(this.pnlRightClick);
            this.pnlPageKeyboard.Controls.Add(this.pnlDoubleClick);
            this.pnlPageKeyboard.Controls.Add(this.pnlScroll);
            this.pnlPageKeyboard.Location = new System.Drawing.Point(25, 56);
            this.pnlPageKeyboard.Margin = new System.Windows.Forms.Padding(2);
            this.pnlPageKeyboard.Name = "pnlPageKeyboard";
            this.pnlPageKeyboard.Size = new System.Drawing.Size(1708, 435);
            this.pnlPageKeyboard.TabIndex = 26;
            this.pnlPageKeyboard.Visible = false;
            // 
            // pnlLeftClick
            // 
            this.pnlLeftClick.Controls.Add(this.pictureBox1);
            this.pnlLeftClick.Controls.Add(this.pnlFKeyHighlight2);
            this.pnlLeftClick.Controls.Add(this.pnlFKeyHighlight1);
            this.pnlLeftClick.Controls.Add(this.lbLeft);
            this.pnlLeftClick.Location = new System.Drawing.Point(38, 12);
            this.pnlLeftClick.Margin = new System.Windows.Forms.Padding(2);
            this.pnlLeftClick.Name = "pnlLeftClick";
            this.pnlLeftClick.Size = new System.Drawing.Size(195, 533);
            this.pnlLeftClick.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GazeToolBar.Properties.Resources.Left_Click_icon;
            this.pictureBox1.Location = new System.Drawing.Point(19, 22);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 91);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pnlFKeyHighlight2
            // 
            this.pnlFKeyHighlight2.Controls.Add(this.btClearFKeyLeftClick);
            this.pnlFKeyHighlight2.Location = new System.Drawing.Point(19, 339);
            this.pnlFKeyHighlight2.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFKeyHighlight2.Name = "pnlFKeyHighlight2";
            this.pnlFKeyHighlight2.Size = new System.Drawing.Size(154, 155);
            this.pnlFKeyHighlight2.TabIndex = 5;
            // 
            // btClearFKeyLeftClick
            // 
            this.btClearFKeyLeftClick.FlatAppearance.BorderSize = 5;
            this.btClearFKeyLeftClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClearFKeyLeftClick.Font = new System.Drawing.Font("Arial", 24F);
            this.btClearFKeyLeftClick.ForeColor = System.Drawing.Color.White;
            this.btClearFKeyLeftClick.Location = new System.Drawing.Point(2, 2);
            this.btClearFKeyLeftClick.Margin = new System.Windows.Forms.Padding(2);
            this.btClearFKeyLeftClick.Name = "btClearFKeyLeftClick";
            this.btClearFKeyLeftClick.Size = new System.Drawing.Size(150, 150);
            this.btClearFKeyLeftClick.TabIndex = 2;
            this.btClearFKeyLeftClick.Text = "Clear";
            this.btClearFKeyLeftClick.UseVisualStyleBackColor = true;
            this.btClearFKeyLeftClick.Click += new System.EventHandler(this.btClearFKeyLeftClick_Click);
            // 
            // pnlFKeyHighlight1
            // 
            this.pnlFKeyHighlight1.Controls.Add(this.btFKeyLeftClick);
            this.pnlFKeyHighlight1.Location = new System.Drawing.Point(19, 127);
            this.pnlFKeyHighlight1.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFKeyHighlight1.Name = "pnlFKeyHighlight1";
            this.pnlFKeyHighlight1.Size = new System.Drawing.Size(154, 155);
            this.pnlFKeyHighlight1.TabIndex = 4;
            // 
            // btFKeyLeftClick
            // 
            this.btFKeyLeftClick.FlatAppearance.BorderSize = 5;
            this.btFKeyLeftClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFKeyLeftClick.Font = new System.Drawing.Font("Arial", 24F);
            this.btFKeyLeftClick.ForeColor = System.Drawing.Color.White;
            this.btFKeyLeftClick.Location = new System.Drawing.Point(2, 2);
            this.btFKeyLeftClick.Margin = new System.Windows.Forms.Padding(2);
            this.btFKeyLeftClick.Name = "btFKeyLeftClick";
            this.btFKeyLeftClick.Size = new System.Drawing.Size(150, 150);
            this.btFKeyLeftClick.TabIndex = 1;
            this.btFKeyLeftClick.Text = "Set";
            this.btFKeyLeftClick.UseVisualStyleBackColor = true;
            this.btFKeyLeftClick.Click += new System.EventHandler(this.btFKeyLeftClick_Click);
            // 
            // lbLeft
            // 
            this.lbLeft.AutoSize = true;
            this.lbLeft.Font = new System.Drawing.Font("Arial", 18F);
            this.lbLeft.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbLeft.Location = new System.Drawing.Point(64, 502);
            this.lbLeft.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbLeft.Name = "lbLeft";
            this.lbLeft.Size = new System.Drawing.Size(77, 27);
            this.lbLeft.TabIndex = 3;
            this.lbLeft.Text = "label1";
            // 
            // lbFKeyFeedback
            // 
            this.lbFKeyFeedback.AutoSize = true;
            this.lbFKeyFeedback.Font = new System.Drawing.Font("Arial", 18F);
            this.lbFKeyFeedback.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbFKeyFeedback.Location = new System.Drawing.Point(543, 570);
            this.lbFKeyFeedback.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFKeyFeedback.Name = "lbFKeyFeedback";
            this.lbFKeyFeedback.Size = new System.Drawing.Size(77, 27);
            this.lbFKeyFeedback.TabIndex = 5;
            this.lbFKeyFeedback.Text = "label7";
            // 
            // pnlRightClick
            // 
            this.pnlRightClick.Controls.Add(this.lbRight);
            this.pnlRightClick.Controls.Add(this.pictureBox2);
            this.pnlRightClick.Controls.Add(this.pnlFKeyHighlight3);
            this.pnlRightClick.Controls.Add(this.pnlFKeyHighlight4);
            this.pnlRightClick.Location = new System.Drawing.Point(326, 12);
            this.pnlRightClick.Margin = new System.Windows.Forms.Padding(2);
            this.pnlRightClick.Name = "pnlRightClick";
            this.pnlRightClick.Size = new System.Drawing.Size(195, 533);
            this.pnlRightClick.TabIndex = 4;
            // 
            // lbRight
            // 
            this.lbRight.AutoSize = true;
            this.lbRight.Font = new System.Drawing.Font("Arial", 18F);
            this.lbRight.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbRight.Location = new System.Drawing.Point(64, 502);
            this.lbRight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbRight.Name = "lbRight";
            this.lbRight.Size = new System.Drawing.Size(77, 27);
            this.lbRight.TabIndex = 3;
            this.lbRight.Text = "label3";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::GazeToolBar.Properties.Resources.Right_Click_icon;
            this.pictureBox2.Location = new System.Drawing.Point(19, 22);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(155, 91);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pnlFKeyHighlight3
            // 
            this.pnlFKeyHighlight3.Controls.Add(this.btFKeyRightClick);
            this.pnlFKeyHighlight3.Location = new System.Drawing.Point(19, 127);
            this.pnlFKeyHighlight3.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFKeyHighlight3.Name = "pnlFKeyHighlight3";
            this.pnlFKeyHighlight3.Size = new System.Drawing.Size(154, 155);
            this.pnlFKeyHighlight3.TabIndex = 6;
            // 
            // btFKeyRightClick
            // 
            this.btFKeyRightClick.FlatAppearance.BorderSize = 5;
            this.btFKeyRightClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFKeyRightClick.Font = new System.Drawing.Font("Arial", 24F);
            this.btFKeyRightClick.ForeColor = System.Drawing.Color.White;
            this.btFKeyRightClick.Location = new System.Drawing.Point(2, 2);
            this.btFKeyRightClick.Margin = new System.Windows.Forms.Padding(2);
            this.btFKeyRightClick.Name = "btFKeyRightClick";
            this.btFKeyRightClick.Size = new System.Drawing.Size(150, 150);
            this.btFKeyRightClick.TabIndex = 1;
            this.btFKeyRightClick.Text = "Set";
            this.btFKeyRightClick.UseVisualStyleBackColor = true;
            this.btFKeyRightClick.Click += new System.EventHandler(this.btFKeyRightClick_Click);
            // 
            // pnlFKeyHighlight4
            // 
            this.pnlFKeyHighlight4.Controls.Add(this.btClearFKeyRightClick);
            this.pnlFKeyHighlight4.Location = new System.Drawing.Point(19, 339);
            this.pnlFKeyHighlight4.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFKeyHighlight4.Name = "pnlFKeyHighlight4";
            this.pnlFKeyHighlight4.Size = new System.Drawing.Size(154, 155);
            this.pnlFKeyHighlight4.TabIndex = 7;
            // 
            // btClearFKeyRightClick
            // 
            this.btClearFKeyRightClick.FlatAppearance.BorderSize = 5;
            this.btClearFKeyRightClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClearFKeyRightClick.Font = new System.Drawing.Font("Arial", 24F);
            this.btClearFKeyRightClick.ForeColor = System.Drawing.Color.White;
            this.btClearFKeyRightClick.Location = new System.Drawing.Point(2, 2);
            this.btClearFKeyRightClick.Margin = new System.Windows.Forms.Padding(2);
            this.btClearFKeyRightClick.Name = "btClearFKeyRightClick";
            this.btClearFKeyRightClick.Size = new System.Drawing.Size(150, 150);
            this.btClearFKeyRightClick.TabIndex = 2;
            this.btClearFKeyRightClick.Text = "Clear";
            this.btClearFKeyRightClick.UseVisualStyleBackColor = true;
            this.btClearFKeyRightClick.Click += new System.EventHandler(this.btClearFKeyRightClick_Click);
            // 
            // pnlDoubleClick
            // 
            this.pnlDoubleClick.Controls.Add(this.lbDouble);
            this.pnlDoubleClick.Controls.Add(this.pictureBox3);
            this.pnlDoubleClick.Controls.Add(this.pnlFKeyHighlight5);
            this.pnlDoubleClick.Controls.Add(this.pnlFKeyHighlight6);
            this.pnlDoubleClick.Location = new System.Drawing.Point(618, 12);
            this.pnlDoubleClick.Margin = new System.Windows.Forms.Padding(2);
            this.pnlDoubleClick.Name = "pnlDoubleClick";
            this.pnlDoubleClick.Size = new System.Drawing.Size(195, 533);
            this.pnlDoubleClick.TabIndex = 4;
            // 
            // lbDouble
            // 
            this.lbDouble.AutoSize = true;
            this.lbDouble.Font = new System.Drawing.Font("Arial", 18F);
            this.lbDouble.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbDouble.Location = new System.Drawing.Point(64, 502);
            this.lbDouble.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDouble.Name = "lbDouble";
            this.lbDouble.Size = new System.Drawing.Size(77, 27);
            this.lbDouble.TabIndex = 3;
            this.lbDouble.Text = "label4";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::GazeToolBar.Properties.Resources.Double_Click_icon;
            this.pictureBox3.Location = new System.Drawing.Point(21, 22);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(155, 91);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // pnlFKeyHighlight5
            // 
            this.pnlFKeyHighlight5.Controls.Add(this.btFKeyDoubleClick);
            this.pnlFKeyHighlight5.Location = new System.Drawing.Point(19, 127);
            this.pnlFKeyHighlight5.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFKeyHighlight5.Name = "pnlFKeyHighlight5";
            this.pnlFKeyHighlight5.Size = new System.Drawing.Size(154, 155);
            this.pnlFKeyHighlight5.TabIndex = 8;
            // 
            // btFKeyDoubleClick
            // 
            this.btFKeyDoubleClick.FlatAppearance.BorderSize = 5;
            this.btFKeyDoubleClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFKeyDoubleClick.Font = new System.Drawing.Font("Arial", 24F);
            this.btFKeyDoubleClick.ForeColor = System.Drawing.Color.White;
            this.btFKeyDoubleClick.Location = new System.Drawing.Point(2, 2);
            this.btFKeyDoubleClick.Margin = new System.Windows.Forms.Padding(2);
            this.btFKeyDoubleClick.Name = "btFKeyDoubleClick";
            this.btFKeyDoubleClick.Size = new System.Drawing.Size(150, 150);
            this.btFKeyDoubleClick.TabIndex = 1;
            this.btFKeyDoubleClick.Text = "Set";
            this.btFKeyDoubleClick.UseVisualStyleBackColor = true;
            this.btFKeyDoubleClick.Click += new System.EventHandler(this.btFKeyDoubleClick_Click);
            // 
            // pnlFKeyHighlight6
            // 
            this.pnlFKeyHighlight6.Controls.Add(this.btClearFKeyDoubleClick);
            this.pnlFKeyHighlight6.Location = new System.Drawing.Point(19, 339);
            this.pnlFKeyHighlight6.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFKeyHighlight6.Name = "pnlFKeyHighlight6";
            this.pnlFKeyHighlight6.Size = new System.Drawing.Size(154, 155);
            this.pnlFKeyHighlight6.TabIndex = 9;
            // 
            // btClearFKeyDoubleClick
            // 
            this.btClearFKeyDoubleClick.FlatAppearance.BorderSize = 5;
            this.btClearFKeyDoubleClick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClearFKeyDoubleClick.Font = new System.Drawing.Font("Arial", 24F);
            this.btClearFKeyDoubleClick.ForeColor = System.Drawing.Color.White;
            this.btClearFKeyDoubleClick.Location = new System.Drawing.Point(2, 2);
            this.btClearFKeyDoubleClick.Margin = new System.Windows.Forms.Padding(2);
            this.btClearFKeyDoubleClick.Name = "btClearFKeyDoubleClick";
            this.btClearFKeyDoubleClick.Size = new System.Drawing.Size(150, 150);
            this.btClearFKeyDoubleClick.TabIndex = 2;
            this.btClearFKeyDoubleClick.Text = "Clear";
            this.btClearFKeyDoubleClick.UseVisualStyleBackColor = true;
            this.btClearFKeyDoubleClick.Click += new System.EventHandler(this.btClearFKeyDoubleClick_Click);
            // 
            // pnlScroll
            // 
            this.pnlScroll.Controls.Add(this.lbScroll);
            this.pnlScroll.Controls.Add(this.pnlFKeyHighlight8);
            this.pnlScroll.Controls.Add(this.pnlFKeyHighlight7);
            this.pnlScroll.Controls.Add(this.pictureBox4);
            this.pnlScroll.Location = new System.Drawing.Point(911, 12);
            this.pnlScroll.Margin = new System.Windows.Forms.Padding(2);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Size = new System.Drawing.Size(195, 533);
            this.pnlScroll.TabIndex = 4;
            // 
            // lbScroll
            // 
            this.lbScroll.AutoSize = true;
            this.lbScroll.Font = new System.Drawing.Font("Arial", 18F);
            this.lbScroll.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbScroll.Location = new System.Drawing.Point(64, 502);
            this.lbScroll.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbScroll.Name = "lbScroll";
            this.lbScroll.Size = new System.Drawing.Size(77, 27);
            this.lbScroll.TabIndex = 3;
            this.lbScroll.Text = "label5";
            // 
            // pnlFKeyHighlight8
            // 
            this.pnlFKeyHighlight8.Controls.Add(this.btClearFKeyScroll);
            this.pnlFKeyHighlight8.Location = new System.Drawing.Point(19, 339);
            this.pnlFKeyHighlight8.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFKeyHighlight8.Name = "pnlFKeyHighlight8";
            this.pnlFKeyHighlight8.Size = new System.Drawing.Size(154, 155);
            this.pnlFKeyHighlight8.TabIndex = 9;
            // 
            // btClearFKeyScroll
            // 
            this.btClearFKeyScroll.FlatAppearance.BorderSize = 5;
            this.btClearFKeyScroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClearFKeyScroll.Font = new System.Drawing.Font("Arial", 24F);
            this.btClearFKeyScroll.ForeColor = System.Drawing.Color.White;
            this.btClearFKeyScroll.Location = new System.Drawing.Point(2, 2);
            this.btClearFKeyScroll.Margin = new System.Windows.Forms.Padding(2);
            this.btClearFKeyScroll.Name = "btClearFKeyScroll";
            this.btClearFKeyScroll.Size = new System.Drawing.Size(150, 150);
            this.btClearFKeyScroll.TabIndex = 2;
            this.btClearFKeyScroll.Text = "Clear";
            this.btClearFKeyScroll.UseVisualStyleBackColor = true;
            this.btClearFKeyScroll.Click += new System.EventHandler(this.btClearFKeyScroll_Click);
            // 
            // pnlFKeyHighlight7
            // 
            this.pnlFKeyHighlight7.Controls.Add(this.btFKeyScroll);
            this.pnlFKeyHighlight7.Location = new System.Drawing.Point(19, 127);
            this.pnlFKeyHighlight7.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFKeyHighlight7.Name = "pnlFKeyHighlight7";
            this.pnlFKeyHighlight7.Size = new System.Drawing.Size(154, 155);
            this.pnlFKeyHighlight7.TabIndex = 8;
            // 
            // btFKeyScroll
            // 
            this.btFKeyScroll.FlatAppearance.BorderSize = 5;
            this.btFKeyScroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFKeyScroll.Font = new System.Drawing.Font("Arial", 24F);
            this.btFKeyScroll.ForeColor = System.Drawing.Color.White;
            this.btFKeyScroll.Location = new System.Drawing.Point(2, 2);
            this.btFKeyScroll.Margin = new System.Windows.Forms.Padding(2);
            this.btFKeyScroll.Name = "btFKeyScroll";
            this.btFKeyScroll.Size = new System.Drawing.Size(150, 150);
            this.btFKeyScroll.TabIndex = 1;
            this.btFKeyScroll.Text = "Set";
            this.btFKeyScroll.UseVisualStyleBackColor = true;
            this.btFKeyScroll.Click += new System.EventHandler(this.btFKeyScroll_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::GazeToolBar.Properties.Resources.Scroll_icon;
            this.pictureBox4.Location = new System.Drawing.Point(19, 22);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(155, 91);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::GazeToolBar.Properties.Resources.Mic_icon;
            this.pictureBox6.Location = new System.Drawing.Point(20, 22);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(155, 91);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox6.TabIndex = 0;
            this.pictureBox6.TabStop = false;
            // 
            // pnlFKeyHighlight10
            // 
            this.pnlFKeyHighlight10.Controls.Add(this.btClearFKeyDrapAndDrop);
            this.pnlFKeyHighlight10.Location = new System.Drawing.Point(21, 165);
            this.pnlFKeyHighlight10.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFKeyHighlight10.Name = "pnlFKeyHighlight10";
            this.pnlFKeyHighlight10.Size = new System.Drawing.Size(116, 51);
            this.pnlFKeyHighlight10.TabIndex = 9;
            // 
            // btClearFKeyDrapAndDrop
            // 
            this.btClearFKeyDrapAndDrop.FlatAppearance.BorderSize = 5;
            this.btClearFKeyDrapAndDrop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClearFKeyDrapAndDrop.ForeColor = System.Drawing.Color.White;
            this.btClearFKeyDrapAndDrop.Location = new System.Drawing.Point(2, 3);
            this.btClearFKeyDrapAndDrop.Margin = new System.Windows.Forms.Padding(2);
            this.btClearFKeyDrapAndDrop.Name = "btClearFKeyDrapAndDrop";
            this.btClearFKeyDrapAndDrop.Size = new System.Drawing.Size(112, 45);
            this.btClearFKeyDrapAndDrop.TabIndex = 2;
            this.btClearFKeyDrapAndDrop.Text = "Clear";
            this.btClearFKeyDrapAndDrop.UseVisualStyleBackColor = true;
            // 
            // pnlFKeyHighlight9
            // 
            this.pnlFKeyHighlight9.Controls.Add(this.btFKeyDrapAndDrop);
            this.pnlFKeyHighlight9.Location = new System.Drawing.Point(19, 108);
            this.pnlFKeyHighlight9.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFKeyHighlight9.Name = "pnlFKeyHighlight9";
            this.pnlFKeyHighlight9.Size = new System.Drawing.Size(116, 51);
            this.pnlFKeyHighlight9.TabIndex = 8;
            // 
            // btFKeyDrapAndDrop
            // 
            this.btFKeyDrapAndDrop.FlatAppearance.BorderSize = 5;
            this.btFKeyDrapAndDrop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFKeyDrapAndDrop.ForeColor = System.Drawing.Color.White;
            this.btFKeyDrapAndDrop.Location = new System.Drawing.Point(2, 2);
            this.btFKeyDrapAndDrop.Margin = new System.Windows.Forms.Padding(2);
            this.btFKeyDrapAndDrop.Name = "btFKeyDrapAndDrop";
            this.btFKeyDrapAndDrop.Size = new System.Drawing.Size(112, 45);
            this.btFKeyDrapAndDrop.TabIndex = 1;
            this.btFKeyDrapAndDrop.Text = "Set";
            this.btFKeyDrapAndDrop.UseVisualStyleBackColor = true;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(19, 22);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(114, 71);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(711, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Shortcut Keys";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1825, 1181);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlPageKeyboard);
            this.Controls.Add(this.panelSaveAndCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.Shown += new System.EventHandler(this.Settings_Shown);
            this.panelSaveAndCancel.ResumeLayout(false);
            this.pnlCancel.ResumeLayout(false);
            this.pnlSave.ResumeLayout(false);
            this.pnlPageKeyboard.ResumeLayout(false);
            this.pnlPageKeyboard.PerformLayout();
            this.pnlLeftClick.ResumeLayout(false);
            this.pnlLeftClick.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlFKeyHighlight2.ResumeLayout(false);
            this.pnlFKeyHighlight1.ResumeLayout(false);
            this.pnlRightClick.ResumeLayout(false);
            this.pnlRightClick.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlFKeyHighlight3.ResumeLayout(false);
            this.pnlFKeyHighlight4.ResumeLayout(false);
            this.pnlDoubleClick.ResumeLayout(false);
            this.pnlDoubleClick.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.pnlFKeyHighlight5.ResumeLayout(false);
            this.pnlFKeyHighlight6.ResumeLayout(false);
            this.pnlScroll.ResumeLayout(false);
            this.pnlScroll.PerformLayout();
            this.pnlFKeyHighlight8.ResumeLayout(false);
            this.pnlFKeyHighlight7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.pnlFKeyHighlight10.ResumeLayout(false);
            this.pnlFKeyHighlight9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnSave;
        private Button btnCancel;
        private Panel panelSaveAndCancel;
        private EyeXFramework.Forms.BehaviorMap bhavSettingMap;
        private Panel pnlPageKeyboard;
        private Panel pnlLeftClick;
        private Label lbLeft;
        private Button btClearFKeyLeftClick;
        private Button btFKeyLeftClick;
        private PictureBox pictureBox1;
        private Panel pnlRightClick;
        private Label lbRight;
        private Button btClearFKeyRightClick;
        private Button btFKeyRightClick;
        private PictureBox pictureBox2;
        private Panel pnlDoubleClick;
        private Label lbDouble;
        private Button btClearFKeyDoubleClick;
        private Button btFKeyDoubleClick;
        private PictureBox pictureBox3;
        private Panel pnlScroll;
        private Label lbScroll;
        private Button btClearFKeyScroll;
        private Button btFKeyScroll;
        private PictureBox pictureBox4;
        private Button btClearFKeyDrapAndDrop;
        private Button btFKeyDrapAndDrop;
        private PictureBox pictureBox5;
        private Label lbFKeyFeedback;
        private Panel pnlFKeyHighlight3;
        private Panel pnlFKeyHighlight4;
        private Panel pnlFKeyHighlight5;
        private Panel pnlFKeyHighlight6;
        private Panel pnlFKeyHighlight8;
        private Panel pnlFKeyHighlight7;
        private Panel pnlFKeyHighlight10;
        private Panel pnlFKeyHighlight9;
        private Panel pnlFKeyHighlight2;
        private Panel pnlFKeyHighlight1;
        private Panel pnlCancel;
        private Panel pnlSave;
        private EyeXFramework.Forms.BehaviorMap bhavGeneralMap;
        private EyeXFramework.Forms.BehaviorMap bhavZoomMap;
        private EyeXFramework.Forms.BehaviorMap bhavShortcutMap;
        private EyeXFramework.Forms.BehaviorMap bhavRearrangeMap;
        private EyeXFramework.Forms.BehaviorMap bhavCrosshairMap;
        private PictureBox pictureBox6;
        private EyeXFramework.Forms.BehaviorMap bhavConfirmMap;
        private Label label1;
    }
}