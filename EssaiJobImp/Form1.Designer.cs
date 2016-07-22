namespace Ireport_Rubis
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tBdureeTimer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNbfichier = new System.Windows.Forms.TextBox();
            this.btnReglage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LABELimprimante0 = new System.Windows.Forms.Label();
            this.LABELimprimante1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tBdureeTimer
            // 
            this.tBdureeTimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBdureeTimer.Location = new System.Drawing.Point(12, 236);
            this.tBdureeTimer.Name = "tBdureeTimer";
            this.tBdureeTimer.ReadOnly = true;
            this.tBdureeTimer.Size = new System.Drawing.Size(247, 20);
            this.tBdureeTimer.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "Nombre de fichier travaillé :";
            // 
            // tbNbfichier
            // 
            this.tbNbfichier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNbfichier.Location = new System.Drawing.Point(180, 265);
            this.tbNbfichier.Name = "tbNbfichier";
            this.tbNbfichier.ReadOnly = true;
            this.tbNbfichier.Size = new System.Drawing.Size(79, 20);
            this.tbNbfichier.TabIndex = 18;
            this.tbNbfichier.Text = "0";
            this.tbNbfichier.TextChanged += new System.EventHandler(this.tbNbfichier_TextChanged);
            // 
            // btnReglage
            // 
            this.btnReglage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReglage.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnReglage.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReglage.Location = new System.Drawing.Point(12, 145);
            this.btnReglage.Name = "btnReglage";
            this.btnReglage.Size = new System.Drawing.Size(120, 51);
            this.btnReglage.TabIndex = 19;
            this.btnReglage.Text = "Accès réglage";
            this.btnReglage.UseVisualStyleBackColor = false;
            this.btnReglage.Click += new System.EventHandler(this.btnReglage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(416, 33);
            this.label2.TabIndex = 20;
            this.label2.Text = "Ireport Rubis Facturation V1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Image = global::Ireport_Rubis.Properties.Resources.logoDEVIS;
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(284, 87);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(537, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "IRAMLOW  dossier dans  HKEY_LOCALE_MACHINE\\SYSTEM\\CurrentControleSet\\Control\\Prin" +
    "t\\IRAM01LOW";
            // 
            // LABELimprimante0
            // 
            this.LABELimprimante0.AutoSize = true;
            this.LABELimprimante0.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LABELimprimante0.Location = new System.Drawing.Point(177, 147);
            this.LABELimprimante0.Name = "LABELimprimante0";
            this.LABELimprimante0.Size = new System.Drawing.Size(97, 13);
            this.LABELimprimante0.TabIndex = 23;
            this.LABELimprimante0.Text = "Imprimante LDP0 : ";
            // 
            // LABELimprimante1
            // 
            this.LABELimprimante1.AutoSize = true;
            this.LABELimprimante1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LABELimprimante1.Location = new System.Drawing.Point(177, 183);
            this.LABELimprimante1.Name = "LABELimprimante1";
            this.LABELimprimante1.Size = new System.Drawing.Size(97, 13);
            this.LABELimprimante1.TabIndex = 24;
            this.LABELimprimante1.Text = "Imprimante LDP1 : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(576, 338);
            this.Controls.Add(this.LABELimprimante1);
            this.Controls.Add(this.LABELimprimante0);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReglage);
            this.Controls.Add(this.tbNbfichier);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBdureeTimer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Ireport Rubis Facturation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tBdureeTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNbfichier;
        private System.Windows.Forms.Button btnReglage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LABELimprimante0;
        private System.Windows.Forms.Label LABELimprimante1;
    }
}

