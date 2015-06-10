using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EssaiJobImp
{
    public partial class Form_reglage : Form
    {
        public Form_reglage()
        {
            InitializeComponent();
            splitContainer2.Panel2Collapsed = true;
            splitContainer2.Panel2.Hide();   
        }
        private void btnImprimante_Click(object sender, EventArgs e)
        {
            UserControlImprimante UCI = new UserControlImprimante();
            splitContainer1.Panel1.Controls.Clear();
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel1.Controls.Add(UCI); 
            UCI.dicoProfil = UCI.remplirLB();
        }

        private void btnDevis_Click(object sender, EventArgs e)
        {
            UserControlDevis UCD = new UserControlDevis();
            splitContainer1.Panel1.Controls.Clear();
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel1.Controls.Add(UCD);
        }

        private void btnAR_Click(object sender, EventArgs e)
        {
            UserControlAR UCAR = new UserControlAR();
            splitContainer1.Panel1.Controls.Clear();
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel1.Controls.Add(UCAR);
        }

        private void btnBP_Click(object sender, EventArgs e)
        {
            UserControlBP UCBP = new UserControlBP();
            splitContainer1.Panel1.Controls.Clear();
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel1.Controls.Add(UCBP);
        }

        private void btnCF_Click(object sender, EventArgs e)
        {
            UserControlCF UCCF = new UserControlCF();
            splitContainer1.Panel1.Controls.Clear();
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel1.Controls.Add(UCCF);
        }

        private void btnBL_Click(object sender, EventArgs e)
        {
            UserControlBL UCBL = new UserControlBL();
            splitContainer1.Panel1.Controls.Clear();
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel1.Controls.Add(UCBL);
        }

        private void btnFacturation_Click(object sender, EventArgs e)
        {
            UserControlFacturation UCFA = new UserControlFacturation();
            splitContainer1.Panel1.Controls.Clear();
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel1.Controls.Add(UCFA);
        }
    }
}
