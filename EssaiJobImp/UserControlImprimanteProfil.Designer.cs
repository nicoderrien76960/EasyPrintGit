namespace Ireport_Rubis
{
    partial class UserControlImprimanteProfil
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
            this.btnValidAjout = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Valider = new System.Windows.Forms.Button();
            this.tBModifImp = new System.Windows.Forms.TextBox();
            this.btn_SupImp = new System.Windows.Forms.Button();
            this.btn_ModifImp = new System.Windows.Forms.Button();
            this.btn_AjoutImp = new System.Windows.Forms.Button();
            this.lBImprimante = new System.Windows.Forms.ListBox();
            this.cBProfil = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnValidAjout
            // 
            this.btnValidAjout.Location = new System.Drawing.Point(269, 311);
            this.btnValidAjout.Name = "btnValidAjout";
            this.btnValidAjout.Size = new System.Drawing.Size(88, 40);
            this.btnValidAjout.TabIndex = 18;
            this.btnValidAjout.Text = "Ok Ajouter";
            this.btnValidAjout.UseVisualStyleBackColor = true;
            this.btnValidAjout.Visible = false;
            this.btnValidAjout.Click += new System.EventHandler(this.btnValidAjout_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Valeur (Ex : Imp204) :";
            this.label2.Visible = false;
            // 
            // btn_Valider
            // 
            this.btn_Valider.Location = new System.Drawing.Point(269, 311);
            this.btn_Valider.Name = "btn_Valider";
            this.btn_Valider.Size = new System.Drawing.Size(88, 40);
            this.btn_Valider.TabIndex = 16;
            this.btn_Valider.Text = "Ok Modifier";
            this.btn_Valider.UseVisualStyleBackColor = true;
            this.btn_Valider.Visible = false;
            this.btn_Valider.Click += new System.EventHandler(this.btn_Valider_Click);
            // 
            // tBModifImp
            // 
            this.tBModifImp.Location = new System.Drawing.Point(126, 311);
            this.tBModifImp.Multiline = true;
            this.tBModifImp.Name = "tBModifImp";
            this.tBModifImp.Size = new System.Drawing.Size(137, 40);
            this.tBModifImp.TabIndex = 15;
            this.tBModifImp.Visible = false;
            // 
            // btn_SupImp
            // 
            this.btn_SupImp.Location = new System.Drawing.Point(323, 193);
            this.btn_SupImp.Name = "btn_SupImp";
            this.btn_SupImp.Size = new System.Drawing.Size(137, 40);
            this.btn_SupImp.TabIndex = 14;
            this.btn_SupImp.Text = "Supprimer";
            this.btn_SupImp.UseVisualStyleBackColor = true;
            this.btn_SupImp.Click += new System.EventHandler(this.btn_SupImp_Click);
            // 
            // btn_ModifImp
            // 
            this.btn_ModifImp.Location = new System.Drawing.Point(167, 193);
            this.btn_ModifImp.Name = "btn_ModifImp";
            this.btn_ModifImp.Size = new System.Drawing.Size(137, 40);
            this.btn_ModifImp.TabIndex = 13;
            this.btn_ModifImp.Text = "Modifier";
            this.btn_ModifImp.UseVisualStyleBackColor = true;
            this.btn_ModifImp.Click += new System.EventHandler(this.btn_ModifImp_Click);
            // 
            // btn_AjoutImp
            // 
            this.btn_AjoutImp.Location = new System.Drawing.Point(15, 193);
            this.btn_AjoutImp.Name = "btn_AjoutImp";
            this.btn_AjoutImp.Size = new System.Drawing.Size(137, 40);
            this.btn_AjoutImp.TabIndex = 12;
            this.btn_AjoutImp.Text = "Ajouter";
            this.btn_AjoutImp.UseVisualStyleBackColor = true;
            this.btn_AjoutImp.Click += new System.EventHandler(this.btn_AjoutImp_Click);
            // 
            // lBImprimante
            // 
            this.lBImprimante.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lBImprimante.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBImprimante.FormattingEnabled = true;
            this.lBImprimante.ItemHeight = 14;
            this.lBImprimante.Location = new System.Drawing.Point(15, 39);
            this.lBImprimante.Name = "lBImprimante";
            this.lBImprimante.Size = new System.Drawing.Size(445, 144);
            this.lBImprimante.TabIndex = 11;
            // 
            // cBProfil
            // 
            this.cBProfil.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBProfil.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBProfil.FormattingEnabled = true;
            this.cBProfil.Location = new System.Drawing.Point(15, 10);
            this.cBProfil.Name = "cBProfil";
            this.cBProfil.Size = new System.Drawing.Size(445, 22);
            this.cBProfil.TabIndex = 10;
            this.cBProfil.SelectedIndexChanged += new System.EventHandler(this.cBProfil_SelectedIndexChanged);
            this.cBProfil.SelectedValueChanged += new System.EventHandler(this.cBProfil_SelectedIndexChanged);
            // 
            // UserControlImprimanteProfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.btnValidAjout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Valider);
            this.Controls.Add(this.tBModifImp);
            this.Controls.Add(this.btn_SupImp);
            this.Controls.Add(this.btn_ModifImp);
            this.Controls.Add(this.btn_AjoutImp);
            this.Controls.Add(this.lBImprimante);
            this.Controls.Add(this.cBProfil);
            this.Name = "UserControlImprimanteProfil";
            this.Size = new System.Drawing.Size(529, 405);
            this.Load += new System.EventHandler(this.UserControlImprimanteProfil_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnValidAjout;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Valider;
        private System.Windows.Forms.TextBox tBModifImp;
        private System.Windows.Forms.Button btn_SupImp;
        private System.Windows.Forms.Button btn_ModifImp;
        private System.Windows.Forms.Button btn_AjoutImp;
        private System.Windows.Forms.ListBox lBImprimante;
        public System.Windows.Forms.ComboBox cBProfil;

    }
}
