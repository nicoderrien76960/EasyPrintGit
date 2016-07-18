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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Timer1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tBdureeTimer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNbfichier = new System.Windows.Forms.TextBox();
            this.btnReglage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(147, 162);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(99, 57);
            this.textBox1.TabIndex = 2;
            // 
            // Timer1
            // 
            this.Timer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Timer1.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Timer1.Location = new System.Drawing.Point(12, 162);
            this.Timer1.Name = "Timer1";
            this.Timer1.Size = new System.Drawing.Size(112, 60);
            this.Timer1.TabIndex = 4;
            this.Timer1.Text = "Démarrage";
            this.Timer1.UseVisualStyleBackColor = true;
            this.Timer1.Click += new System.EventHandler(this.Timer1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(147, 243);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(99, 66);
            this.textBox2.TabIndex = 8;
            // 
            // tBdureeTimer
            // 
            this.tBdureeTimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBdureeTimer.Location = new System.Drawing.Point(619, 20);
            this.tBdureeTimer.Name = "tBdureeTimer";
            this.tBdureeTimer.ReadOnly = true;
            this.tBdureeTimer.Size = new System.Drawing.Size(0, 20);
            this.tBdureeTimer.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Highlight;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(274, 267);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "Nombre de fichier travaillé :";
            // 
            // tbNbfichier
            // 
            this.tbNbfichier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNbfichier.Location = new System.Drawing.Point(436, 264);
            this.tbNbfichier.Name = "tbNbfichier";
            this.tbNbfichier.ReadOnly = true;
            this.tbNbfichier.Size = new System.Drawing.Size(49, 20);
            this.tbNbfichier.TabIndex = 18;
            this.tbNbfichier.Text = "0";
            this.tbNbfichier.TextChanged += new System.EventHandler(this.tbNbfichier_TextChanged);
            // 
            // btnReglage
            // 
            this.btnReglage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReglage.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReglage.Location = new System.Drawing.Point(12, 240);
            this.btnReglage.Name = "btnReglage";
            this.btnReglage.Size = new System.Drawing.Size(112, 69);
            this.btnReglage.TabIndex = 19;
            this.btnReglage.Text = "Accès réglage";
            this.btnReglage.UseVisualStyleBackColor = true;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(575, 340);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnReglage);
            this.Controls.Add(this.tbNbfichier);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBdureeTimer);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Timer1);
            this.Controls.Add(this.textBox1);
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

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Timer1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tBdureeTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNbfichier;
        private System.Windows.Forms.Button btnReglage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
    }
}

