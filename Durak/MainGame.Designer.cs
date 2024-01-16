namespace Durak
{
    partial class frmMainGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainGame));
            this.grpPlayerHand = new System.Windows.Forms.GroupBox();
            this.grpComputerHand = new System.Windows.Forms.GroupBox();
            this.grpStats = new System.Windows.Forms.GroupBox();
            this.btnClearPlayerData = new System.Windows.Forms.Button();
            this.lblTiesValue = new System.Windows.Forms.Label();
            this.lblTies = new System.Windows.Forms.Label();
            this.lblLossesValue = new System.Windows.Forms.Label();
            this.lblLosses = new System.Windows.Forms.Label();
            this.lblWinsValue = new System.Windows.Forms.Label();
            this.lblWins = new System.Windows.Forms.Label();
            this.lblGamesPlayedValue = new System.Windows.Forms.Label();
            this.lblGamesPlayed = new System.Windows.Forms.Label();
            this.lblAttackerValue = new System.Windows.Forms.Label();
            this.lblDefenderValue = new System.Windows.Forms.Label();
            this.lblDefender = new System.Windows.Forms.Label();
            this.lblAttacker = new System.Windows.Forms.Label();
            this.btnEndAttack = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.lblMessages = new System.Windows.Forms.Label();
            this.pnlField = new System.Windows.Forms.Panel();
            this.grpStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPlayerHand
            // 
            this.grpPlayerHand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPlayerHand.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grpPlayerHand.BackgroundImage = global::Durak.Properties.Resources.oak_wood;
            this.grpPlayerHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grpPlayerHand.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grpPlayerHand.Location = new System.Drawing.Point(271, 372);
            this.grpPlayerHand.Name = "grpPlayerHand";
            this.grpPlayerHand.Size = new System.Drawing.Size(676, 141);
            this.grpPlayerHand.TabIndex = 3;
            this.grpPlayerHand.TabStop = false;
            this.grpPlayerHand.Text = "Player Hand";
            // 
            // grpComputerHand
            // 
            this.grpComputerHand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpComputerHand.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grpComputerHand.BackgroundImage = global::Durak.Properties.Resources.oak_wood;
            this.grpComputerHand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grpComputerHand.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grpComputerHand.Location = new System.Drawing.Point(271, 12);
            this.grpComputerHand.Name = "grpComputerHand";
            this.grpComputerHand.Size = new System.Drawing.Size(676, 131);
            this.grpComputerHand.TabIndex = 4;
            this.grpComputerHand.TabStop = false;
            this.grpComputerHand.Text = "Computer Hand";
            // 
            // grpStats
            // 
            this.grpStats.BackgroundImage = global::Durak.Properties.Resources.oak_wood;
            this.grpStats.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grpStats.Controls.Add(this.btnClearPlayerData);
            this.grpStats.Controls.Add(this.lblTiesValue);
            this.grpStats.Controls.Add(this.lblTies);
            this.grpStats.Controls.Add(this.lblLossesValue);
            this.grpStats.Controls.Add(this.lblLosses);
            this.grpStats.Controls.Add(this.lblWinsValue);
            this.grpStats.Controls.Add(this.lblWins);
            this.grpStats.Controls.Add(this.lblGamesPlayedValue);
            this.grpStats.Controls.Add(this.lblGamesPlayed);
            this.grpStats.Controls.Add(this.lblAttackerValue);
            this.grpStats.Controls.Add(this.lblDefenderValue);
            this.grpStats.Controls.Add(this.lblDefender);
            this.grpStats.Controls.Add(this.lblAttacker);
            this.grpStats.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grpStats.Location = new System.Drawing.Point(12, 12);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(200, 164);
            this.grpStats.TabIndex = 6;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "Statistics";
            // 
            // btnClearPlayerData
            // 
            this.btnClearPlayerData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearPlayerData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClearPlayerData.Location = new System.Drawing.Point(8, 136);
            this.btnClearPlayerData.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearPlayerData.Name = "btnClearPlayerData";
            this.btnClearPlayerData.Size = new System.Drawing.Size(187, 20);
            this.btnClearPlayerData.TabIndex = 34;
            this.btnClearPlayerData.Text = "Clear Player Data";
            this.btnClearPlayerData.UseVisualStyleBackColor = true;
            this.btnClearPlayerData.Click += new System.EventHandler(this.btnClearPlayerData_Click);
            // 
            // lblTiesValue
            // 
            this.lblTiesValue.AutoSize = true;
            this.lblTiesValue.BackColor = System.Drawing.Color.Transparent;
            this.lblTiesValue.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblTiesValue.Location = new System.Drawing.Point(38, 110);
            this.lblTiesValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTiesValue.Name = "lblTiesValue";
            this.lblTiesValue.Size = new System.Drawing.Size(0, 13);
            this.lblTiesValue.TabIndex = 33;
            this.lblTiesValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTies
            // 
            this.lblTies.AutoSize = true;
            this.lblTies.BackColor = System.Drawing.Color.Transparent;
            this.lblTies.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblTies.Location = new System.Drawing.Point(5, 110);
            this.lblTies.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTies.Name = "lblTies";
            this.lblTies.Size = new System.Drawing.Size(30, 13);
            this.lblTies.TabIndex = 32;
            this.lblTies.Text = "Ties:";
            this.lblTies.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblLossesValue
            // 
            this.lblLossesValue.AutoSize = true;
            this.lblLossesValue.BackColor = System.Drawing.Color.Transparent;
            this.lblLossesValue.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblLossesValue.Location = new System.Drawing.Point(141, 85);
            this.lblLossesValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLossesValue.Name = "lblLossesValue";
            this.lblLossesValue.Size = new System.Drawing.Size(0, 13);
            this.lblLossesValue.TabIndex = 31;
            // 
            // lblLosses
            // 
            this.lblLosses.AutoSize = true;
            this.lblLosses.BackColor = System.Drawing.Color.Transparent;
            this.lblLosses.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblLosses.Location = new System.Drawing.Point(95, 85);
            this.lblLosses.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLosses.Name = "lblLosses";
            this.lblLosses.Size = new System.Drawing.Size(43, 13);
            this.lblLosses.TabIndex = 30;
            this.lblLosses.Text = "Losses:";
            // 
            // lblWinsValue
            // 
            this.lblWinsValue.AutoSize = true;
            this.lblWinsValue.BackColor = System.Drawing.Color.Transparent;
            this.lblWinsValue.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblWinsValue.Location = new System.Drawing.Point(45, 85);
            this.lblWinsValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWinsValue.Name = "lblWinsValue";
            this.lblWinsValue.Size = new System.Drawing.Size(0, 13);
            this.lblWinsValue.TabIndex = 29;
            // 
            // lblWins
            // 
            this.lblWins.AutoSize = true;
            this.lblWins.BackColor = System.Drawing.Color.Transparent;
            this.lblWins.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblWins.Location = new System.Drawing.Point(5, 85);
            this.lblWins.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWins.Name = "lblWins";
            this.lblWins.Size = new System.Drawing.Size(37, 13);
            this.lblWins.TabIndex = 28;
            this.lblWins.Text = "Wins: ";
            // 
            // lblGamesPlayedValue
            // 
            this.lblGamesPlayedValue.AutoSize = true;
            this.lblGamesPlayedValue.BackColor = System.Drawing.Color.Transparent;
            this.lblGamesPlayedValue.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblGamesPlayedValue.Location = new System.Drawing.Point(87, 63);
            this.lblGamesPlayedValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGamesPlayedValue.Name = "lblGamesPlayedValue";
            this.lblGamesPlayedValue.Size = new System.Drawing.Size(0, 13);
            this.lblGamesPlayedValue.TabIndex = 27;
            // 
            // lblGamesPlayed
            // 
            this.lblGamesPlayed.AutoSize = true;
            this.lblGamesPlayed.BackColor = System.Drawing.Color.Transparent;
            this.lblGamesPlayed.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblGamesPlayed.Location = new System.Drawing.Point(5, 63);
            this.lblGamesPlayed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGamesPlayed.Name = "lblGamesPlayed";
            this.lblGamesPlayed.Size = new System.Drawing.Size(78, 13);
            this.lblGamesPlayed.TabIndex = 26;
            this.lblGamesPlayed.Text = "Games Played:";
            // 
            // lblAttackerValue
            // 
            this.lblAttackerValue.AutoSize = true;
            this.lblAttackerValue.BackColor = System.Drawing.Color.Transparent;
            this.lblAttackerValue.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblAttackerValue.Location = new System.Drawing.Point(71, 17);
            this.lblAttackerValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAttackerValue.Name = "lblAttackerValue";
            this.lblAttackerValue.Size = new System.Drawing.Size(0, 13);
            this.lblAttackerValue.TabIndex = 25;
            // 
            // lblDefenderValue
            // 
            this.lblDefenderValue.AutoSize = true;
            this.lblDefenderValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDefenderValue.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblDefenderValue.Location = new System.Drawing.Point(71, 40);
            this.lblDefenderValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDefenderValue.Name = "lblDefenderValue";
            this.lblDefenderValue.Size = new System.Drawing.Size(0, 13);
            this.lblDefenderValue.TabIndex = 24;
            // 
            // lblDefender
            // 
            this.lblDefender.AutoSize = true;
            this.lblDefender.BackColor = System.Drawing.Color.Transparent;
            this.lblDefender.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblDefender.Location = new System.Drawing.Point(5, 40);
            this.lblDefender.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDefender.Name = "lblDefender";
            this.lblDefender.Size = new System.Drawing.Size(57, 13);
            this.lblDefender.TabIndex = 23;
            this.lblDefender.Text = "Defender: ";
            // 
            // lblAttacker
            // 
            this.lblAttacker.AutoSize = true;
            this.lblAttacker.BackColor = System.Drawing.Color.Transparent;
            this.lblAttacker.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblAttacker.Location = new System.Drawing.Point(5, 17);
            this.lblAttacker.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAttacker.Name = "lblAttacker";
            this.lblAttacker.Size = new System.Drawing.Size(53, 13);
            this.lblAttacker.TabIndex = 22;
            this.lblAttacker.Text = "Attacker: ";
            // 
            // btnEndAttack
            // 
            this.btnEndAttack.Location = new System.Drawing.Point(110, 328);
            this.btnEndAttack.Name = "btnEndAttack";
            this.btnEndAttack.Size = new System.Drawing.Size(97, 23);
            this.btnEndAttack.TabIndex = 7;
            this.btnEndAttack.Text = "End Attack";
            this.btnEndAttack.UseVisualStyleBackColor = true;
            this.btnEndAttack.Click += new System.EventHandler(this.btnEndAttack_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(12, 328);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(97, 23);
            this.btnAccept.TabIndex = 8;
            this.btnAccept.Text = "Accept Cards";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(12, 357);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(195, 23);
            this.btnNewGame.TabIndex = 9;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // lblError
            // 
            this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(268, 329);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(679, 23);
            this.lblError.TabIndex = 10;
            this.lblError.Text = "INCORRECT MOVE";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMessages
            // 
            this.lblMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessages.BackColor = System.Drawing.Color.Transparent;
            this.lblMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblMessages.Location = new System.Drawing.Point(268, 153);
            this.lblMessages.Name = "lblMessages";
            this.lblMessages.Size = new System.Drawing.Size(679, 23);
            this.lblMessages.TabIndex = 11;
            this.lblMessages.Text = "GAME MESSAGES";
            this.lblMessages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlField
            // 
            this.pnlField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlField.BackColor = System.Drawing.Color.Transparent;
            this.pnlField.Location = new System.Drawing.Point(271, 185);
            this.pnlField.Name = "pnlField";
            this.pnlField.Size = new System.Drawing.Size(676, 141);
            this.pnlField.TabIndex = 12;
            // 
            // frmMainGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Durak.Properties.Resources.Felt;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(959, 525);
            this.Controls.Add(this.pnlField);
            this.Controls.Add(this.lblMessages);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnEndAttack);
            this.Controls.Add(this.grpStats);
            this.Controls.Add(this.grpComputerHand);
            this.Controls.Add(this.grpPlayerHand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(975, 564);
            this.Name = "frmMainGame";
            this.Text = "Durak";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainGame_FormClosing);
            this.Load += new System.EventHandler(this.frmMainGame_Load);
            this.grpStats.ResumeLayout(false);
            this.grpStats.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpPlayerHand;
        private System.Windows.Forms.GroupBox grpComputerHand;
        private System.Windows.Forms.GroupBox grpStats;
        private System.Windows.Forms.Button btnClearPlayerData;
        private System.Windows.Forms.Label lblTiesValue;
        private System.Windows.Forms.Label lblTies;
        private System.Windows.Forms.Label lblLossesValue;
        private System.Windows.Forms.Label lblLosses;
        private System.Windows.Forms.Label lblWinsValue;
        private System.Windows.Forms.Label lblWins;
        private System.Windows.Forms.Label lblGamesPlayedValue;
        private System.Windows.Forms.Label lblGamesPlayed;
        private System.Windows.Forms.Label lblAttackerValue;
        private System.Windows.Forms.Label lblDefenderValue;
        private System.Windows.Forms.Label lblDefender;
        private System.Windows.Forms.Label lblAttacker;
        private System.Windows.Forms.Button btnEndAttack;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblMessages;
        private System.Windows.Forms.Panel pnlField;
    }
}

