namespace Ireport_Rubis
{
    partial class Form_reglage
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
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnVariable = new System.Windows.Forms.Button();
            this.btnChemin = new System.Windows.Forms.Button();
            this.btnFacturation = new System.Windows.Forms.Button();
            this.btnImprimante = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(383, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(387, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Paramètre de l\'application ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(198, 58);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Silver;
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Silver;
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(900, 698);
            this.splitContainer1.SplitterDistance = 389;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.splitContainer2.Location = new System.Drawing.Point(0, 58);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.Silver;
            this.splitContainer2.Panel1.Controls.Add(this.btnVariable);
            this.splitContainer2.Panel1.Controls.Add(this.btnChemin);
            this.splitContainer2.Panel1.Controls.Add(this.btnFacturation);
            this.splitContainer2.Panel1.Controls.Add(this.btnImprimante);
            this.splitContainer2.Size = new System.Drawing.Size(192, 694);
            this.splitContainer2.SplitterDistance = 163;
            this.splitContainer2.TabIndex = 4;
            // 
            // btnVariable
            // 
            this.btnVariable.Location = new System.Drawing.Point(20, 126);
            this.btnVariable.Name = "btnVariable";
            this.btnVariable.Size = new System.Drawing.Size(156, 32);
            this.btnVariable.TabIndex = 11;
            this.btnVariable.Text = "Variable";
            this.btnVariable.UseVisualStyleBackColor = true;
            this.btnVariable.Click += new System.EventHandler(this.btnVariable_Click);
            // 
            // btnChemin
            // 
            this.btnChemin.Location = new System.Drawing.Point(20, 88);
            this.btnChemin.Name = "btnChemin";
            this.btnChemin.Size = new System.Drawing.Size(156, 32);
            this.btnChemin.TabIndex = 8;
            this.btnChemin.Text = "Chemin";
            this.btnChemin.UseVisualStyleBackColor = true;
            this.btnChemin.Click += new System.EventHandler(this.btnChemin_Click);
            // 
            // btnFacturation
            // 
            this.btnFacturation.Location = new System.Drawing.Point(20, 50);
            this.btnFacturation.Name = "btnFacturation";
            this.btnFacturation.Size = new System.Drawing.Size(156, 32);
            this.btnFacturation.TabIndex = 7;
            this.btnFacturation.Text = "Facturation";
            this.btnFacturation.UseVisualStyleBackColor = true;
            this.btnFacturation.Click += new System.EventHandler(this.btnFacturation_Click);
            // 
            // btnImprimante
            // 
            this.btnImprimante.Location = new System.Drawing.Point(20, 12);
            this.btnImprimante.Name = "btnImprimante";
            this.btnImprimante.Size = new System.Drawing.Size(156, 32);
            this.btnImprimante.TabIndex = 0;
            this.btnImprimante.Text = "Imprimante";
            this.btnImprimante.UseVisualStyleBackColor = true;
            this.btnImprimante.Click += new System.EventHandler(this.btnImprimante_Click);
            // 
            // Form_reglage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(1079, 768);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(1500, 800);
            this.MinimumSize = new System.Drawing.Size(863, 500);
            this.Name = "Form_reglage";
            this.Text = "Réglages";
            this.Load += new System.EventHandler(this.Form_reglage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnImprimante;
        private System.Windows.Forms.Button btnFacturation;
        private System.Windows.Forms.Button btnChemin;
        private System.Windows.Forms.Button btnVariable;
    }
}