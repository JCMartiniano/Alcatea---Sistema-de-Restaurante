using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alcatea
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value = progressBar1.Value + 2;
            }
            if (progressBar1.Value == 100)
            {
                Formulário frmPrincipal = new Formulário();
                frmPrincipal.Show(); 
                timer1.Stop();       
                this.Hide();         
            }
        }
    }
}
