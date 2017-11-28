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
            this.btnPause = new System.Windows.Forms.Button();
            this.btnReprise = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LABELimprimante1 = new System.Windows.Forms.Label();
            this.LABELimprimante2 = new System.Windows.Forms.Label();
            this.LABELimprimante3 = new System.Windows.Forms.Label();
            this.LABELimprimante4 = new System.Windows.Forms.Label();
            this.LABELimprimante5 = new System.Windows.Forms.Label();
            this.LABELimprimante6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dossierSpool = new System.Windows.Forms.Label();
            this.HELP = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tBdureeTimer
            // 
            this.tBdureeTimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBdureeTimer.Location = new System.Drawing.Point(12, 445);
            this.tBdureeTimer.Name = "tBdureeTimer";
            this.tBdureeTimer.ReadOnly = true;
            this.tBdureeTimer.Size = new System.Drawing.Size(260, 20);
            this.tBdureeTimer.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 480);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "Nombre de fichier travaillé :";
            // 
            // tbNbfichier
            // 
            this.tbNbfichier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNbfichier.Location = new System.Drawing.Point(174, 477);
            this.tbNbfichier.Name = "tbNbfichier";
            this.tbNbfichier.ReadOnly = true;
            this.tbNbfichier.Size = new System.Drawing.Size(55, 20);
            this.tbNbfichier.TabIndex = 18;
            this.tbNbfichier.Text = "0";
            this.tbNbfichier.TextChanged += new System.EventHandler(this.tbNbfichier_TextChanged);
            // 
            // btnReglage
            // 
            this.btnReglage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReglage.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReglage.Location = new System.Drawing.Point(251, 142);
            this.btnReglage.Name = "btnReglage";
            this.btnReglage.Size = new System.Drawing.Size(100, 50);
            this.btnReglage.TabIndex = 19;
            this.btnReglage.Text = "Accès réglage";
            this.btnReglage.UseVisualStyleBackColor = true;
            this.btnReglage.Click += new System.EventHandler(this.btnReglage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(294, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(314, 34);
            this.label2.TabIndex = 20;
            this.label2.Text = "Ireport Rubis V 3.1b1";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(15, 142);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(100, 50);
            this.btnPause.TabIndex = 22;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnReprise
            // 
            this.btnReprise.Location = new System.Drawing.Point(129, 143);
            this.btnReprise.Name = "btnReprise";
            this.btnReprise.Size = new System.Drawing.Size(100, 50);
            this.btnReprise.TabIndex = 24;
            this.btnReprise.Text = "Reprise";
            this.btnReprise.UseVisualStyleBackColor = true;
            this.btnReprise.Click += new System.EventHandler(this.btnReprise_Click);
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
            // LABELimprimante1
            // 
            this.LABELimprimante1.AutoSize = true;
            this.LABELimprimante1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LABELimprimante1.Location = new System.Drawing.Point(23, 28);
            this.LABELimprimante1.Name = "LABELimprimante1";
            this.LABELimprimante1.Size = new System.Drawing.Size(105, 15);
            this.LABELimprimante1.TabIndex = 26;
            this.LABELimprimante1.Text = "Imprimante 1 :";
            // 
            // LABELimprimante2
            // 
            this.LABELimprimante2.AutoSize = true;
            this.LABELimprimante2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LABELimprimante2.Location = new System.Drawing.Point(23, 55);
            this.LABELimprimante2.Name = "LABELimprimante2";
            this.LABELimprimante2.Size = new System.Drawing.Size(105, 15);
            this.LABELimprimante2.TabIndex = 27;
            this.LABELimprimante2.Text = "Imprimante 2 :";
            // 
            // LABELimprimante3
            // 
            this.LABELimprimante3.AutoSize = true;
            this.LABELimprimante3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LABELimprimante3.Location = new System.Drawing.Point(23, 85);
            this.LABELimprimante3.Name = "LABELimprimante3";
            this.LABELimprimante3.Size = new System.Drawing.Size(105, 15);
            this.LABELimprimante3.TabIndex = 28;
            this.LABELimprimante3.Text = "Imprimante 3 :";
            // 
            // LABELimprimante4
            // 
            this.LABELimprimante4.AutoSize = true;
            this.LABELimprimante4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LABELimprimante4.Location = new System.Drawing.Point(23, 114);
            this.LABELimprimante4.Name = "LABELimprimante4";
            this.LABELimprimante4.Size = new System.Drawing.Size(105, 15);
            this.LABELimprimante4.TabIndex = 29;
            this.LABELimprimante4.Text = "Imprimante 4 :";
            // 
            // LABELimprimante5
            // 
            this.LABELimprimante5.AutoSize = true;
            this.LABELimprimante5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante5.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LABELimprimante5.Location = new System.Drawing.Point(23, 141);
            this.LABELimprimante5.Name = "LABELimprimante5";
            this.LABELimprimante5.Size = new System.Drawing.Size(105, 15);
            this.LABELimprimante5.TabIndex = 30;
            this.LABELimprimante5.Text = "Imprimante 5 :";
            // 
            // LABELimprimante6
            // 
            this.LABELimprimante6.AutoSize = true;
            this.LABELimprimante6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante6.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LABELimprimante6.Location = new System.Drawing.Point(23, 169);
            this.LABELimprimante6.Name = "LABELimprimante6";
            this.LABELimprimante6.Size = new System.Drawing.Size(105, 15);
            this.LABELimprimante6.TabIndex = 31;
            this.LABELimprimante6.Text = "Imprimante 6 :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LABELimprimante6);
            this.groupBox1.Controls.Add(this.LABELimprimante5);
            this.groupBox1.Controls.Add(this.LABELimprimante4);
            this.groupBox1.Controls.Add(this.LABELimprimante3);
            this.groupBox1.Controls.Add(this.LABELimprimante2);
            this.groupBox1.Controls.Add(this.LABELimprimante1);
            this.groupBox1.Location = new System.Drawing.Point(12, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 210);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // dossierSpool
            // 
            this.dossierSpool.AutoSize = true;
            this.dossierSpool.BackColor = System.Drawing.SystemColors.Info;
            this.dossierSpool.Location = new System.Drawing.Point(471, 203);
            this.dossierSpool.Name = "dossierSpool";
            this.dossierSpool.Size = new System.Drawing.Size(78, 13);
            this.dossierSpool.TabIndex = 33;
            this.dossierSpool.Text = "Dossier Spool :";
            // 
            // HELP
            // 
            this.HELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HELP.Location = new System.Drawing.Point(474, 231);
            this.HELP.Multiline = true;
            this.HELP.Name = "HELP";
            this.HELP.Size = new System.Drawing.Size(486, 198);
            this.HELP.TabIndex = 34;
            this.HELP.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.HELP);
            this.Controls.Add(this.dossierSpool);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnReprise);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReglage);
            this.Controls.Add(this.tbNbfichier);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBdureeTimer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Ireport Rubis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnReprise;
        private System.Windows.Forms.Label LABELimprimante1;
        private System.Windows.Forms.Label LABELimprimante2;
        private System.Windows.Forms.Label LABELimprimante3;
        private System.Windows.Forms.Label LABELimprimante4;
        private System.Windows.Forms.Label LABELimprimante5;
        private System.Windows.Forms.Label LABELimprimante6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label dossierSpool;
        private System.Windows.Forms.TextBox HELP;
    }
}

