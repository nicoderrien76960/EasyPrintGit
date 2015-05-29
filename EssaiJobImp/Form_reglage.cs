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
        UserControlImprimante UCI = new UserControlImprimante();
        
        public Form_reglage()
        {
            InitializeComponent();
            splitContainer2.Panel2Collapsed = true;
            splitContainer2.Panel2.Hide();   
        }
        private void btnImprimante_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1.Controls.Clear();
            splitContainer1.Panel1.Controls.Add(UCI); 
            UCI.dicoProfil = UCI.remplirLB();
        }
    }
}
