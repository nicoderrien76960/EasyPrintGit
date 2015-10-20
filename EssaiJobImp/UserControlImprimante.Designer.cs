namespace EssaiJobImp
{
    partial class UserControlImprimante
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.tbAjoutImprimante = new System.Windows.Forms.TextBox();
            this.btnValiderAjoutPro = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAjoutProfil = new System.Windows.Forms.TextBox();
            this.btnSupProfil = new System.Windows.Forms.Button();
            this.btnAjoutProfil = new System.Windows.Forms.Button();
            this.lBProfil = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 14);
            this.label4.TabIndex = 20;
            this.label4.Text = "Nom Imprimante :";
            this.label4.Visible = false;
            // 
            // tbAjoutImprimante
            // 
            this.tbAjoutImprimante.Location = new System.Drawing.Point(139, 257);
            this.tbAjoutImprimante.Multiline = true;
            this.tbAjoutImprimante.Name = "tbAjoutImprimante";
            this.tbAjoutImprimante.Size = new System.Drawing.Size(131, 40);
            this.tbAjoutImprimante.TabIndex = 19;
            this.tbAjoutImprimante.Visible = false;
            // 
            // btnValiderAjoutPro
            // 
            this.btnValiderAjoutPro.Location = new System.Drawing.Point(13, 305);
            this.btnValiderAjoutPro.Name = "btnValiderAjoutPro";
            this.btnValiderAjoutPro.Size = new System.Drawing.Size(106, 40);
            this.btnValiderAjoutPro.TabIndex = 17;
            this.btnValiderAjoutPro.Text = "Valider";
            this.btnValiderAjoutPro.UseVisualStyleBackColor = true;
            this.btnValiderAjoutPro.Visible = false;
            this.btnValiderAjoutPro.Click += new System.EventHandler(this.btnValiderAjoutPro_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 14);
            this.label3.TabIndex = 18;
            this.label3.Text = "Nom du profil (Code) :";
            this.label3.Visible = false;
            // 
            // tbAjoutProfil
            // 
            this.tbAjoutProfil.Location = new System.Drawing.Point(13, 257);
            this.tbAjoutProfil.Multiline = true;
            this.tbAjoutProfil.Name = "tbAjoutProfil";
            this.tbAjoutProfil.Size = new System.Drawing.Size(106, 40);
            this.tbAjoutProfil.TabIndex = 16;
            this.tbAjoutProfil.Visible = false;
            // 
            // btnSupProfil
            // 
            this.btnSupProfil.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupProfil.Location = new System.Drawing.Point(175, 162);
            this.btnSupProfil.Name = "btnSupProfil";
            this.btnSupProfil.Size = new System.Drawing.Size(183, 39);
            this.btnSupProfil.TabIndex = 15;
            this.btnSupProfil.Text = "Supprimer";
            this.btnSupProfil.UseVisualStyleBackColor = true;
            this.btnSupProfil.Click += new System.EventHandler(this.btnSupProfil_Click);
            // 
            // btnAjoutProfil
            // 
            this.btnAjoutProfil.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjoutProfil.Location = new System.Drawing.Point(13, 162);
            this.btnAjoutProfil.Name = "btnAjoutProfil";
            this.btnAjoutProfil.Size = new System.Drawing.Size(156, 39);
            this.btnAjoutProfil.TabIndex = 14;
            this.btnAjoutProfil.Text = "Ajouter Profil";
            this.btnAjoutProfil.UseVisualStyleBackColor = true;
            this.btnAjoutProfil.Click += new System.EventHandler(this.btnAjoutProfil_Click);
            // 
            // lBProfil
            // 
            this.lBProfil.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lBProfil.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBProfil.FormattingEnabled = true;
            this.lBProfil.ItemHeight = 14;
            this.lBProfil.Location = new System.Drawing.Point(13, 11);
            this.lBProfil.MultiColumn = true;
            this.lBProfil.Name = "lBProfil";
            this.lBProfil.Size = new System.Drawing.Size(345, 144);
            this.lBProfil.TabIndex = 13;
            this.lBProfil.SelectedIndexChanged += new System.EventHandler(this.lBProfil_SelectedIndexChanged);
            // 
            // UserControlImprimante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbAjoutImprimante);
            this.Controls.Add(this.btnValiderAjoutPro);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbAjoutProfil);
            this.Controls.Add(this.btnSupProfil);
            this.Controls.Add(this.btnAjoutProfil);
            this.Controls.Add(this.lBProfil);
            this.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserControlImprimante";
            this.Size = new System.Drawing.Size(1163, 664);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbAjoutImprimante;
        private System.Windows.Forms.Button btnValiderAjoutPro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAjoutProfil;
        private System.Windows.Forms.Button btnSupProfil;
        private System.Windows.Forms.Button btnAjoutProfil;
        private System.Windows.Forms.ListBox lBProfil;
    }
}
