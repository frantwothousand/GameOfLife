namespace GameOfLifeClient
{
    partial class Launcher
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gamemodeGroup = new System.Windows.Forms.GroupBox();
            this.multiplayerRadio = new System.Windows.Forms.RadioButton();
            this.singleplayer = new System.Windows.Forms.RadioButton();
            this.titleInfo = new System.Windows.Forms.Label();
            this.playerPreferencesGroup = new System.Windows.Forms.GroupBox();
            this.themeLabel = new System.Windows.Forms.Label();
            this.gameThemeList = new System.Windows.Forms.ComboBox();
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.playerName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gamemodeGroup.SuspendLayout();
            this.playerPreferencesGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(535, 160);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // gamemodeGroup
            // 
            this.gamemodeGroup.Controls.Add(this.multiplayerRadio);
            this.gamemodeGroup.Controls.Add(this.singleplayer);
            this.gamemodeGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gamemodeGroup.Location = new System.Drawing.Point(12, 224);
            this.gamemodeGroup.Name = "gamemodeGroup";
            this.gamemodeGroup.Size = new System.Drawing.Size(186, 76);
            this.gamemodeGroup.TabIndex = 1;
            this.gamemodeGroup.TabStop = false;
            this.gamemodeGroup.Text = "Modo de Juego";
            // 
            // multiplayerRadio
            // 
            this.multiplayerRadio.AutoSize = true;
            this.multiplayerRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiplayerRadio.Location = new System.Drawing.Point(7, 47);
            this.multiplayerRadio.Name = "multiplayerRadio";
            this.multiplayerRadio.Size = new System.Drawing.Size(128, 20);
            this.multiplayerRadio.TabIndex = 1;
            this.multiplayerRadio.TabStop = true;
            this.multiplayerRadio.Text = "Varios jugadores";
            this.multiplayerRadio.UseVisualStyleBackColor = true;
            this.multiplayerRadio.CheckedChanged += new System.EventHandler(this.multiplayerRadio_CheckedChanged);
            // 
            // singleplayer
            // 
            this.singleplayer.AutoSize = true;
            this.singleplayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleplayer.Location = new System.Drawing.Point(7, 20);
            this.singleplayer.Name = "singleplayer";
            this.singleplayer.Size = new System.Drawing.Size(120, 20);
            this.singleplayer.TabIndex = 0;
            this.singleplayer.TabStop = true;
            this.singleplayer.Text = "Un solo jugador";
            this.singleplayer.UseVisualStyleBackColor = true;
            this.singleplayer.CheckedChanged += new System.EventHandler(this.singleplayer_CheckedChanged);
            // 
            // titleInfo
            // 
            this.titleInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.titleInfo.AutoSize = true;
            this.titleInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.titleInfo.Font = new System.Drawing.Font("Segoe UI Variable Display", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleInfo.Location = new System.Drawing.Point(12, 186);
            this.titleInfo.Name = "titleInfo";
            this.titleInfo.Size = new System.Drawing.Size(535, 19);
            this.titleInfo.TabIndex = 2;
            this.titleInfo.Text = "El clásico Juego de la Vida, pero con soporte multijugador y uno que otro extra-f" +
    "eature. 🪄";
            this.titleInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerPreferencesGroup
            // 
            this.playerPreferencesGroup.Controls.Add(this.themeLabel);
            this.playerPreferencesGroup.Controls.Add(this.gameThemeList);
            this.playerPreferencesGroup.Controls.Add(this.playerNameLabel);
            this.playerPreferencesGroup.Controls.Add(this.playerName);
            this.playerPreferencesGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerPreferencesGroup.Location = new System.Drawing.Point(12, 316);
            this.playerPreferencesGroup.Name = "playerPreferencesGroup";
            this.playerPreferencesGroup.Size = new System.Drawing.Size(186, 122);
            this.playerPreferencesGroup.TabIndex = 3;
            this.playerPreferencesGroup.TabStop = false;
            this.playerPreferencesGroup.Text = "Preferencias";
            // 
            // themeLabel
            // 
            this.themeLabel.AutoSize = true;
            this.themeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.themeLabel.Location = new System.Drawing.Point(6, 61);
            this.themeLabel.Name = "themeLabel";
            this.themeLabel.Size = new System.Drawing.Size(58, 15);
            this.themeLabel.TabIndex = 3;
            this.themeLabel.Text = "Temática";
            // 
            // gameThemeList
            // 
            this.gameThemeList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameThemeList.FormattingEnabled = true;
            this.gameThemeList.Items.AddRange(new object[] {
            "Zombies",
            "Astronautas"});
            this.gameThemeList.Location = new System.Drawing.Point(6, 80);
            this.gameThemeList.Name = "gameThemeList";
            this.gameThemeList.Size = new System.Drawing.Size(173, 23);
            this.gameThemeList.TabIndex = 2;
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNameLabel.Location = new System.Drawing.Point(6, 18);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(66, 15);
            this.playerNameLabel.TabIndex = 1;
            this.playerNameLabel.Text = "Nickname:";
            // 
            // playerName
            // 
            this.playerName.BackColor = System.Drawing.SystemColors.Window;
            this.playerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerName.Location = new System.Drawing.Point(6, 36);
            this.playerName.Name = "playerName";
            this.playerName.Size = new System.Drawing.Size(173, 21);
            this.playerName.TabIndex = 0;
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 450);
            this.Controls.Add(this.playerPreferencesGroup);
            this.Controls.Add(this.titleInfo);
            this.Controls.Add(this.gamemodeGroup);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Launcher";
            this.Text = "El Juego de la Vida";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gamemodeGroup.ResumeLayout(false);
            this.gamemodeGroup.PerformLayout();
            this.playerPreferencesGroup.ResumeLayout(false);
            this.playerPreferencesGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox gamemodeGroup;
        private System.Windows.Forms.Label titleInfo;
        private System.Windows.Forms.RadioButton singleplayer;
        private System.Windows.Forms.RadioButton multiplayerRadio;
        private System.Windows.Forms.GroupBox playerPreferencesGroup;
        private System.Windows.Forms.Label playerNameLabel;
        private System.Windows.Forms.TextBox playerName;
        private System.Windows.Forms.Label themeLabel;
        private System.Windows.Forms.ComboBox gameThemeList;
    }
}

