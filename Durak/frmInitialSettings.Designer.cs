namespace Durak
{
    partial class frmInitialSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInitialSettings));
            this.btnStart = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblEnterName = new System.Windows.Forms.Label();
            this.rdo20 = new System.Windows.Forms.RadioButton();
            this.rdo36 = new System.Windows.Forms.RadioButton();
            this.rdo52 = new System.Windows.Forms.RadioButton();
            this.lblDeckAmount = new System.Windows.Forms.Label();
            this.cboFrontType = new System.Windows.Forms.ComboBox();
            this.cboBackType = new System.Windows.Forms.ComboBox();
            this.lblFrontType = new System.Windows.Forms.Label();
            this.lblBackType = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tltInitialSettings = new System.Windows.Forms.ToolTip(this.components);
            this.pbFront = new CardBox.CardBox();
            this.pbBack = new CardBox.CardBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(378, 225);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(66, 47);
            this.btnStart.TabIndex = 27;
            this.btnStart.Text = "Start!";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(161, 255);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(188, 20);
            this.txtName.TabIndex = 26;
            this.txtName.Text = "Player1";
            // 
            // lblEnterName
            // 
            this.lblEnterName.AutoSize = true;
            this.lblEnterName.BackColor = System.Drawing.Color.Transparent;
            this.lblEnterName.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnterName.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblEnterName.Location = new System.Drawing.Point(48, 252);
            this.lblEnterName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEnterName.Name = "lblEnterName";
            this.lblEnterName.Size = new System.Drawing.Size(107, 21);
            this.lblEnterName.TabIndex = 25;
            this.lblEnterName.Text = "Enter Name:";
            // 
            // rdo20
            // 
            this.rdo20.AutoSize = true;
            this.rdo20.BackColor = System.Drawing.Color.Transparent;
            this.rdo20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdo20.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.rdo20.Location = new System.Drawing.Point(306, 209);
            this.rdo20.Margin = new System.Windows.Forms.Padding(2);
            this.rdo20.Name = "rdo20";
            this.rdo20.Size = new System.Drawing.Size(45, 24);
            this.rdo20.TabIndex = 24;
            this.rdo20.Text = "20";
            this.rdo20.UseVisualStyleBackColor = false;
            // 
            // rdo36
            // 
            this.rdo36.AutoSize = true;
            this.rdo36.BackColor = System.Drawing.Color.Transparent;
            this.rdo36.Checked = true;
            this.rdo36.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdo36.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.rdo36.Location = new System.Drawing.Point(231, 209);
            this.rdo36.Margin = new System.Windows.Forms.Padding(2);
            this.rdo36.Name = "rdo36";
            this.rdo36.Size = new System.Drawing.Size(45, 24);
            this.rdo36.TabIndex = 23;
            this.rdo36.TabStop = true;
            this.rdo36.Text = "36";
            this.rdo36.UseVisualStyleBackColor = false;
            // 
            // rdo52
            // 
            this.rdo52.AutoSize = true;
            this.rdo52.BackColor = System.Drawing.Color.Transparent;
            this.rdo52.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdo52.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.rdo52.Location = new System.Drawing.Point(156, 209);
            this.rdo52.Margin = new System.Windows.Forms.Padding(2);
            this.rdo52.Name = "rdo52";
            this.rdo52.Size = new System.Drawing.Size(45, 24);
            this.rdo52.TabIndex = 22;
            this.rdo52.Text = "52";
            this.rdo52.UseVisualStyleBackColor = false;
            // 
            // lblDeckAmount
            // 
            this.lblDeckAmount.AutoSize = true;
            this.lblDeckAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblDeckAmount.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeckAmount.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblDeckAmount.Location = new System.Drawing.Point(34, 208);
            this.lblDeckAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDeckAmount.Name = "lblDeckAmount";
            this.lblDeckAmount.Size = new System.Drawing.Size(123, 21);
            this.lblDeckAmount.TabIndex = 21;
            this.lblDeckAmount.Text = "Deck Amount:";
            // 
            // cboFrontType
            // 
            this.cboFrontType.FormattingEnabled = true;
            this.cboFrontType.Items.AddRange(new object[] {
            "Classic",
            "Modern"});
            this.cboFrontType.Location = new System.Drawing.Point(161, 157);
            this.cboFrontType.Margin = new System.Windows.Forms.Padding(2);
            this.cboFrontType.Name = "cboFrontType";
            this.cboFrontType.Size = new System.Drawing.Size(97, 21);
            this.cboFrontType.TabIndex = 20;
            this.cboFrontType.SelectedIndexChanged += new System.EventHandler(this.cboFrontType_SelectedIndexChanged);
            // 
            // cboBackType
            // 
            this.cboBackType.FormattingEnabled = true;
            this.cboBackType.Items.AddRange(new object[] {
            "Lunch",
            "Pineapples",
            "Autumn",
            "Tropics",
            "Darth Vader"});
            this.cboBackType.Location = new System.Drawing.Point(161, 112);
            this.cboBackType.Margin = new System.Windows.Forms.Padding(2);
            this.cboBackType.Name = "cboBackType";
            this.cboBackType.Size = new System.Drawing.Size(97, 21);
            this.cboBackType.TabIndex = 18;
            this.cboBackType.SelectedIndexChanged += new System.EventHandler(this.cboBackType_SelectedIndexChanged);
            // 
            // lblFrontType
            // 
            this.lblFrontType.AutoSize = true;
            this.lblFrontType.BackColor = System.Drawing.Color.Transparent;
            this.lblFrontType.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrontType.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblFrontType.Location = new System.Drawing.Point(16, 159);
            this.lblFrontType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFrontType.Name = "lblFrontType";
            this.lblFrontType.Size = new System.Drawing.Size(139, 21);
            this.lblFrontType.TabIndex = 19;
            this.lblFrontType.Text = "Card Front Type:";
            // 
            // lblBackType
            // 
            this.lblBackType.AutoSize = true;
            this.lblBackType.BackColor = System.Drawing.Color.Transparent;
            this.lblBackType.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackType.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblBackType.Location = new System.Drawing.Point(16, 109);
            this.lblBackType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBackType.Name = "lblBackType";
            this.lblBackType.Size = new System.Drawing.Size(141, 21);
            this.lblBackType.TabIndex = 17;
            this.lblBackType.Text = "Card Back Type: ";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblSubtitle.Location = new System.Drawing.Point(130, 61);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(224, 30);
            this.lblSubtitle.TabIndex = 16;
            this.lblSubtitle.Text = "Set up your game";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Stencil", 25.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblTitle.Location = new System.Drawing.Point(66, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(357, 42);
            this.lblTitle.TabIndex = 15;
            this.lblTitle.Text = "WELCOME TO DURAK";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbFront
            // 
            this.pbFront.Back = 1;
            this.pbFront.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.pbFront.FaceUp = false;
            this.pbFront.Front = 1;
            this.pbFront.Location = new System.Drawing.Point(378, 95);
            this.pbFront.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbFront.Name = "pbFront";
            this.pbFront.Rank = Cards.CardRank.Ace;
            this.pbFront.Size = new System.Drawing.Size(76, 110);
            this.pbFront.Suit = Cards.CardSuit.Spades;
            this.pbFront.TabIndex = 29;
            this.pbFront.Click += new System.EventHandler(this.pbFront_Click);
            // 
            // pbBack
            // 
            this.pbBack.Back = 1;
            this.pbBack.CardOrientation = System.Windows.Forms.Orientation.Vertical;
            this.pbBack.FaceUp = false;
            this.pbBack.Front = 1;
            this.pbBack.Location = new System.Drawing.Point(280, 95);
            this.pbBack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbBack.Name = "pbBack";
            this.pbBack.Rank = Cards.CardRank.Ace;
            this.pbBack.Size = new System.Drawing.Size(76, 110);
            this.pbBack.Suit = Cards.CardSuit.Hearts;
            this.pbBack.TabIndex = 28;
            this.pbBack.Click += new System.EventHandler(this.pbBack_Click);
            // 
            // frmInitialSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Durak.Properties.Resources.settingsBackground;
            this.ClientSize = new System.Drawing.Size(480, 318);
            this.Controls.Add(this.pbFront);
            this.Controls.Add(this.pbBack);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblEnterName);
            this.Controls.Add(this.rdo20);
            this.Controls.Add(this.rdo36);
            this.Controls.Add(this.rdo52);
            this.Controls.Add(this.lblDeckAmount);
            this.Controls.Add(this.cboFrontType);
            this.Controls.Add(this.cboBackType);
            this.Controls.Add(this.lblFrontType);
            this.Controls.Add(this.lblBackType);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInitialSettings";
            this.Text = "Durak";
            this.Load += new System.EventHandler(this.frmInitialSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblEnterName;
        private System.Windows.Forms.RadioButton rdo20;
        private System.Windows.Forms.RadioButton rdo36;
        private System.Windows.Forms.RadioButton rdo52;
        private System.Windows.Forms.Label lblDeckAmount;
        private System.Windows.Forms.ComboBox cboFrontType;
        private System.Windows.Forms.ComboBox cboBackType;
        private System.Windows.Forms.Label lblFrontType;
        private System.Windows.Forms.Label lblBackType;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolTip tltInitialSettings;
        private CardBox.CardBox pbBack;
        private CardBox.CardBox pbFront;
    }
}