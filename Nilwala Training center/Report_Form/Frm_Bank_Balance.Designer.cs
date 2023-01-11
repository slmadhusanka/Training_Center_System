namespace Inventory_Control_System
{
    partial class Frm_Bank_Balance
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
            this.Cmb_Bank = new System.Windows.Forms.ComboBox();
            this.By_Bank = new DevExpress.XtraEditors.RadioGroup();
            this.PickerDateTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.PickerDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Cmb_Document_ID = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.by_Document_ID = new DevExpress.XtraEditors.RadioGroup();
            this.Cmb_User = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.By_User = new DevExpress.XtraEditors.RadioGroup();
            this.Cmb_Bank_Account_Vice = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.By_Account_Vice = new DevExpress.XtraEditors.RadioGroup();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Bank_ID = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CrystalReVie_Bank_Bal = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.LgDisplayName = new System.Windows.Forms.Label();
            this.LgUser = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.By_Bank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.by_Document_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.By_User.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.By_Account_Vice.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cmb_Bank
            // 
            this.Cmb_Bank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Bank.Enabled = false;
            this.Cmb_Bank.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_Bank.FormattingEnabled = true;
            this.Cmb_Bank.Location = new System.Drawing.Point(155, 70);
            this.Cmb_Bank.Name = "Cmb_Bank";
            this.Cmb_Bank.Size = new System.Drawing.Size(173, 25);
            this.Cmb_Bank.TabIndex = 2;
            this.Cmb_Bank.SelectedIndexChanged += new System.EventHandler(this.Cmb_Bank_SelectedIndexChanged);
            this.Cmb_Bank.Click += new System.EventHandler(this.Cmb_Bank_Click);
            // 
            // By_Bank
            // 
            this.By_Bank.Location = new System.Drawing.Point(19, 39);
            this.By_Bank.Name = "By_Bank";
            this.By_Bank.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.By_Bank.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.By_Bank.Properties.Appearance.Options.UseBackColor = true;
            this.By_Bank.Properties.Appearance.Options.UseFont = true;
            this.By_Bank.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.By_Bank.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All Banks"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "By Bank")});
            this.By_Bank.Size = new System.Drawing.Size(322, 66);
            this.By_Bank.TabIndex = 179;
            this.By_Bank.SelectedIndexChanged += new System.EventHandler(this.By_Bank_SelectedIndexChanged);
            // 
            // PickerDateTo
            // 
            this.PickerDateTo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerDateTo.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PickerDateTo.Location = new System.Drawing.Point(252, 26);
            this.PickerDateTo.Name = "PickerDateTo";
            this.PickerDateTo.Size = new System.Drawing.Size(97, 22);
            this.PickerDateTo.TabIndex = 169;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(236, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 166;
            this.label4.Text = ":";
            // 
            // PickerDateFrom
            // 
            this.PickerDateFrom.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerDateFrom.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PickerDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PickerDateFrom.Location = new System.Drawing.Point(75, 26);
            this.PickerDateFrom.Name = "PickerDateFrom";
            this.PickerDateFrom.Size = new System.Drawing.Size(97, 22);
            this.PickerDateFrom.TabIndex = 168;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(205, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 164;
            this.label3.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 165;
            this.label1.Text = "From";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(24, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 17);
            this.label7.TabIndex = 180;
            this.label7.Text = "Bank Vice";
            // 
            // Cmb_Document_ID
            // 
            this.Cmb_Document_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Document_ID.Enabled = false;
            this.Cmb_Document_ID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_Document_ID.FormattingEnabled = true;
            this.Cmb_Document_ID.Location = new System.Drawing.Point(155, 340);
            this.Cmb_Document_ID.Name = "Cmb_Document_ID";
            this.Cmb_Document_ID.Size = new System.Drawing.Size(174, 25);
            this.Cmb_Document_ID.TabIndex = 178;
            this.Cmb_Document_ID.Click += new System.EventHandler(this.Cmb_Document_ID_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Maroon;
            this.label8.Location = new System.Drawing.Point(25, 300);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 17);
            this.label8.TabIndex = 177;
            this.label8.Text = "Document ID Vice..";
            // 
            // by_Document_ID
            // 
            this.by_Document_ID.Location = new System.Drawing.Point(20, 309);
            this.by_Document_ID.Name = "by_Document_ID";
            this.by_Document_ID.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.by_Document_ID.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.by_Document_ID.Properties.Appearance.Options.UseBackColor = true;
            this.by_Document_ID.Properties.Appearance.Options.UseFont = true;
            this.by_Document_ID.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.by_Document_ID.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All Documents"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "ID Vice")});
            this.by_Document_ID.Size = new System.Drawing.Size(322, 66);
            this.by_Document_ID.TabIndex = 176;
            this.by_Document_ID.SelectedIndexChanged += new System.EventHandler(this.by_Document_ID_SelectedIndexChanged);
            // 
            // Cmb_User
            // 
            this.Cmb_User.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_User.Enabled = false;
            this.Cmb_User.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_User.FormattingEnabled = true;
            this.Cmb_User.Location = new System.Drawing.Point(155, 249);
            this.Cmb_User.Name = "Cmb_User";
            this.Cmb_User.Size = new System.Drawing.Size(174, 25);
            this.Cmb_User.TabIndex = 175;
            this.Cmb_User.Click += new System.EventHandler(this.Cmb_User_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(24, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 17);
            this.label6.TabIndex = 174;
            this.label6.Text = "Added User Vice";
            // 
            // By_User
            // 
            this.By_User.Location = new System.Drawing.Point(19, 218);
            this.By_User.Name = "By_User";
            this.By_User.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.By_User.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.By_User.Properties.Appearance.Options.UseBackColor = true;
            this.By_User.Properties.Appearance.Options.UseFont = true;
            this.By_User.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.By_User.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All Users"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "By User")});
            this.By_User.Size = new System.Drawing.Size(322, 66);
            this.By_User.TabIndex = 173;
            this.By_User.SelectedIndexChanged += new System.EventHandler(this.By_User_SelectedIndexChanged);
            // 
            // Cmb_Bank_Account_Vice
            // 
            this.Cmb_Bank_Account_Vice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Bank_Account_Vice.Enabled = false;
            this.Cmb_Bank_Account_Vice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cmb_Bank_Account_Vice.FormattingEnabled = true;
            this.Cmb_Bank_Account_Vice.Location = new System.Drawing.Point(155, 160);
            this.Cmb_Bank_Account_Vice.Name = "Cmb_Bank_Account_Vice";
            this.Cmb_Bank_Account_Vice.Size = new System.Drawing.Size(174, 25);
            this.Cmb_Bank_Account_Vice.TabIndex = 172;
            this.Cmb_Bank_Account_Vice.SelectedIndexChanged += new System.EventHandler(this.Cmb_Bank_Account_Vice_SelectedIndexChanged);
            this.Cmb_Bank_Account_Vice.Click += new System.EventHandler(this.Cmb_Bank_Account_Vice_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(24, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 17);
            this.label5.TabIndex = 170;
            this.label5.Text = "Bank & Account Vice..";
            // 
            // By_Account_Vice
            // 
            this.By_Account_Vice.Location = new System.Drawing.Point(19, 128);
            this.By_Account_Vice.Name = "By_Account_Vice";
            this.By_Account_Vice.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.By_Account_Vice.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.By_Account_Vice.Properties.Appearance.Options.UseBackColor = true;
            this.By_Account_Vice.Properties.Appearance.Options.UseFont = true;
            this.By_Account_Vice.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.By_Account_Vice.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All Bank Accounts"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "By Account Num")});
            this.By_Account_Vice.Size = new System.Drawing.Size(322, 66);
            this.By_Account_Vice.TabIndex = 5;
            this.By_Account_Vice.SelectedIndexChanged += new System.EventHandler(this.By_Account_Vice_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 14.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(776, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(193, 25);
            this.label9.TabIndex = 171;
            this.label9.Text = "Cash Deposit Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(58, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 167;
            this.label2.Text = ":";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.Bank_ID);
            this.groupBox2.Controls.Add(this.Cmb_Bank);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.By_Bank);
            this.groupBox2.Controls.Add(this.Cmb_Document_ID);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.by_Document_ID);
            this.groupBox2.Controls.Add(this.Cmb_User);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.By_User);
            this.groupBox2.Controls.Add(this.Cmb_Bank_Account_Vice);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.By_Account_Vice);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(23, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 446);
            this.groupBox2.TabIndex = 175;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter by...";
            // 
            // Bank_ID
            // 
            this.Bank_ID.AutoSize = true;
            this.Bank_ID.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bank_ID.Location = new System.Drawing.Point(155, 50);
            this.Bank_ID.Name = "Bank_ID";
            this.Bank_ID.Size = new System.Drawing.Size(49, 13);
            this.Bank_ID.TabIndex = 170;
            this.Bank_ID.Text = "Bank_ID";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(217, 390);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 30);
            this.button1.TabIndex = 171;
            this.button1.Text = "Load The Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PickerDateTo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.PickerDateFrom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(23, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 71);
            this.groupBox1.TabIndex = 174;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date Range...";
            // 
            // CrystalReVie_Bank_Bal
            // 
            this.CrystalReVie_Bank_Bal.ActiveViewIndex = -1;
            this.CrystalReVie_Bank_Bal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CrystalReVie_Bank_Bal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CrystalReVie_Bank_Bal.Cursor = System.Windows.Forms.Cursors.Default;
            this.CrystalReVie_Bank_Bal.Location = new System.Drawing.Point(400, 136);
            this.CrystalReVie_Bank_Bal.Name = "CrystalReVie_Bank_Bal";
            this.CrystalReVie_Bank_Bal.Size = new System.Drawing.Size(891, 460);
            this.CrystalReVie_Bank_Bal.TabIndex = 173;
            this.CrystalReVie_Bank_Bal.ToolPanelWidth = 233;
            // 
            // LgDisplayName
            // 
            this.LgDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgDisplayName.AutoSize = true;
            this.LgDisplayName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgDisplayName.ForeColor = System.Drawing.Color.Black;
            this.LgDisplayName.Location = new System.Drawing.Point(1126, 41);
            this.LgDisplayName.Name = "LgDisplayName";
            this.LgDisplayName.Size = new System.Drawing.Size(105, 21);
            this.LgDisplayName.TabIndex = 32;
            this.LgDisplayName.Text = "DispalyName";
            // 
            // LgUser
            // 
            this.LgUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LgUser.AutoSize = true;
            this.LgUser.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LgUser.ForeColor = System.Drawing.Color.Black;
            this.LgUser.Location = new System.Drawing.Point(1126, 11);
            this.LgUser.Name = "LgUser";
            this.LgUser.Size = new System.Drawing.Size(94, 21);
            this.LgUser.TabIndex = 31;
            this.LgUser.Text = "HideUserID";
            this.LgUser.Visible = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox7.Image = global::Nilwala_Training_center.Properties.Resources.Account_and_Control;
            this.pictureBox7.Location = new System.Drawing.Point(988, 34);
            this.pictureBox7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(35, 30);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 23;
            this.pictureBox7.TabStop = false;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(1023, 41);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(96, 21);
            this.label28.TabIndex = 24;
            this.label28.Text = "Login User :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(15, 27);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(0, 24);
            this.label17.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(120, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(190, 25);
            this.label15.TabIndex = 0;
            this.label15.Text = "Cash Deposit Details";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label16.Location = new System.Drawing.Point(161, 41);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(131, 17);
            this.label16.TabIndex = 0;
            this.label16.Text = "Bank Balance details";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.LgDisplayName);
            this.panel2.Controls.Add(this.LgUser);
            this.panel2.Controls.Add(this.pictureBox7);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Location = new System.Drawing.Point(0, -6);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1377, 71);
            this.panel2.TabIndex = 172;
            // 
            // Frm_Bank_Balance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 608);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CrystalReVie_Bank_Bal);
            this.Controls.Add(this.panel2);
            this.Name = "Frm_Bank_Balance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bank Balance Report";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Bank_Balance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.By_Bank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.by_Document_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.By_User.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.By_Account_Vice.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Cmb_Bank;
        private DevExpress.XtraEditors.RadioGroup By_Bank;
        private System.Windows.Forms.DateTimePicker PickerDateTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker PickerDateFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox Cmb_Document_ID;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.RadioGroup by_Document_ID;
        private System.Windows.Forms.ComboBox Cmb_User;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.RadioGroup By_User;
        private System.Windows.Forms.ComboBox Cmb_Bank_Account_Vice;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.RadioGroup By_Account_Vice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label Bank_ID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer CrystalReVie_Bank_Bal;
        public System.Windows.Forms.Label LgDisplayName;
        public System.Windows.Forms.Label LgUser;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel2;
    }
}