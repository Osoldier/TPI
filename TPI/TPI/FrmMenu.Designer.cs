namespace TPI
{
    partial class FrmMenu
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblIp = new System.Windows.Forms.Label();
            this.tbxIP = new System.Windows.Forms.TextBox();
            this.btnJoin = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.tbxNickname = new System.Windows.Forms.TextBox();
            this.lblPseudo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.BackColor = System.Drawing.Color.Transparent;
            this.lblIp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIp.ForeColor = System.Drawing.Color.White;
            this.lblIp.Location = new System.Drawing.Point(101, 12);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(95, 20);
            this.lblIp.TabIndex = 0;
            this.lblIp.Text = "Adresse IP: ";
            // 
            // tbxIP
            // 
            this.tbxIP.Location = new System.Drawing.Point(202, 12);
            this.tbxIP.Name = "tbxIP";
            this.tbxIP.Size = new System.Drawing.Size(100, 20);
            this.tbxIP.TabIndex = 1;
            this.tbxIP.Text = "225.0.0.1";
            this.tbxIP.TextChanged += new System.EventHandler(this.tbxIP_TextChanged);
            // 
            // btnJoin
            // 
            this.btnJoin.BackColor = System.Drawing.Color.Black;
            this.btnJoin.BackgroundImage = global::TPI.Properties.Resources.btnjoin;
            this.btnJoin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnJoin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoin.ForeColor = System.Drawing.Color.White;
            this.btnJoin.Location = new System.Drawing.Point(18, 78);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(173, 69);
            this.btnJoin.TabIndex = 4;
            this.btnJoin.Text = "Rejoindre";
            this.btnJoin.UseVisualStyleBackColor = false;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCreate.BackgroundImage = global::TPI.Properties.Resources.btncreate;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(202, 78);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(173, 69);
            this.btnCreate.TabIndex = 5;
            this.btnCreate.Text = "Créer";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // tbxNickname
            // 
            this.tbxNickname.Location = new System.Drawing.Point(202, 38);
            this.tbxNickname.Name = "tbxNickname";
            this.tbxNickname.Size = new System.Drawing.Size(100, 20);
            this.tbxNickname.TabIndex = 3;
            this.tbxNickname.TextChanged += new System.EventHandler(this.tbxNickname_TextChanged);
            // 
            // lblPseudo
            // 
            this.lblPseudo.AutoSize = true;
            this.lblPseudo.BackColor = System.Drawing.Color.Transparent;
            this.lblPseudo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPseudo.ForeColor = System.Drawing.Color.White;
            this.lblPseudo.Location = new System.Drawing.Point(101, 38);
            this.lblPseudo.Name = "lblPseudo";
            this.lblPseudo.Size = new System.Drawing.Size(75, 20);
            this.lblPseudo.TabIndex = 2;
            this.lblPseudo.Text = "Pseudo : ";
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::TPI.Properties.Resources.back_10;
            this.ClientSize = new System.Drawing.Size(387, 166);
            this.Controls.Add(this.tbxNickname);
            this.Controls.Add(this.lblPseudo);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.tbxIP);
            this.Controls.Add(this.lblIp);
            this.Name = "FrmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SquareRunners - Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.TextBox tbxIP;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox tbxNickname;
        private System.Windows.Forms.Label lblPseudo;
    }
}

