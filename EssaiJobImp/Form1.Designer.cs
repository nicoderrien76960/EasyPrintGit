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
            this.LABELimprimante1 = new System.Windows.Forms.Label();
            this.LABELimprimante2 = new System.Windows.Forms.Label();
            this.LABELimprimante3 = new System.Windows.Forms.Label();
            this.LABELimprimante4 = new System.Windows.Forms.Label();
            this.LABELimprimante5 = new System.Windows.Forms.Label();
            this.LABELimprimante6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dossierSpool = new System.Windows.Forms.Label();
            this.HELP = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tBdureeTimer
            // 
            resources.ApplyResources(this.tBdureeTimer, "tBdureeTimer");
            this.tBdureeTimer.Name = "tBdureeTimer";
            this.tBdureeTimer.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Name = "label1";
            // 
            // tbNbfichier
            // 
            resources.ApplyResources(this.tbNbfichier, "tbNbfichier");
            this.tbNbfichier.Name = "tbNbfichier";
            this.tbNbfichier.ReadOnly = true;
            this.tbNbfichier.TextChanged += new System.EventHandler(this.tbNbfichier_TextChanged);
            // 
            // btnReglage
            // 
            resources.ApplyResources(this.btnReglage, "btnReglage");
            this.btnReglage.Name = "btnReglage";
            this.btnReglage.UseVisualStyleBackColor = true;
            this.btnReglage.Click += new System.EventHandler(this.btnReglage_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnPause
            // 
            resources.ApplyResources(this.btnPause, "btnPause");
            this.btnPause.Name = "btnPause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnReprise
            // 
            resources.ApplyResources(this.btnReprise, "btnReprise");
            this.btnReprise.Name = "btnReprise";
            this.btnReprise.UseVisualStyleBackColor = true;
            this.btnReprise.Click += new System.EventHandler(this.btnReprise_Click);
            // 
            // LABELimprimante1
            // 
            resources.ApplyResources(this.LABELimprimante1, "LABELimprimante1");
            this.LABELimprimante1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante1.Name = "LABELimprimante1";
            // 
            // LABELimprimante2
            // 
            resources.ApplyResources(this.LABELimprimante2, "LABELimprimante2");
            this.LABELimprimante2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante2.Name = "LABELimprimante2";
            // 
            // LABELimprimante3
            // 
            resources.ApplyResources(this.LABELimprimante3, "LABELimprimante3");
            this.LABELimprimante3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante3.Name = "LABELimprimante3";
            // 
            // LABELimprimante4
            // 
            resources.ApplyResources(this.LABELimprimante4, "LABELimprimante4");
            this.LABELimprimante4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante4.Name = "LABELimprimante4";
            // 
            // LABELimprimante5
            // 
            resources.ApplyResources(this.LABELimprimante5, "LABELimprimante5");
            this.LABELimprimante5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante5.Name = "LABELimprimante5";
            // 
            // LABELimprimante6
            // 
            resources.ApplyResources(this.LABELimprimante6, "LABELimprimante6");
            this.LABELimprimante6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.LABELimprimante6.Name = "LABELimprimante6";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.LABELimprimante6);
            this.groupBox1.Controls.Add(this.LABELimprimante5);
            this.groupBox1.Controls.Add(this.LABELimprimante4);
            this.groupBox1.Controls.Add(this.LABELimprimante3);
            this.groupBox1.Controls.Add(this.LABELimprimante2);
            this.groupBox1.Controls.Add(this.LABELimprimante1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // dossierSpool
            // 
            resources.ApplyResources(this.dossierSpool, "dossierSpool");
            this.dossierSpool.BackColor = System.Drawing.SystemColors.Info;
            this.dossierSpool.Name = "dossierSpool";
            // 
            // HELP
            // 
            resources.ApplyResources(this.HELP, "HELP");
            this.HELP.Name = "HELP";
            this.HELP.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pictureBox1.Image = global::Ireport_Rubis.Properties.Resources.logoDEVIS;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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

