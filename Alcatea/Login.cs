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
    public partial class Login : Form
    {
        DataSet ds;
        ClasseConexao con;
        private bool drag = false; // determine if we should be moving the form
        private Point startPoint = new Point(0, 0); // also for the moving
        public Login()
        {
            InitializeComponent();
            Arrendondar();
        }
        private void Arrendondar()
        {
            Bunifu.Framework.Lib.Elipse.Apply(btnEntrar, 40);
            Bunifu.Framework.Lib.Elipse.Apply(txtNome, 35);
            Bunifu.Framework.Lib.Elipse.Apply(txtSenha, 35);
            Bunifu.Framework.Lib.Elipse.Apply(btnSenha, 20);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }    

        void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            this.drag = false;
        }

        void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            this.startPoint = e.Location;
            this.drag = true;
        }

        void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.drag)
            { // if we should be dragging it, we need to figure out some movement
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.startPoint.X,
                                     p2.Y - this.startPoint.Y);
                this.Location = p3;
            }
        }
        private void atualizarGrid(DataGridView dgv, String comando)
        {
            con = new ClasseConexao();
            ds = new DataSet();
            ds = con.retornarSQL(comando);
            dgv.DataSource = ds.Tables[0];
        }
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                con = new ClasseConexao();
                String s = "EXEC usp_cFunc '" + txtNome.Text + "', '" + txtSenha.Text + "'";
                atualizarGrid(dataGridView1,s);
                if (dataGridView1.Rows[0].Cells[0].Value.ToString() == "Funcionário pode ser logado pois os dados conferem")
                {
                    Loading frm = new Loading();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("O nome ou senha estão incorretos");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível conectar ao banco. "+erro);
            }
        }

        private void txtNome_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtNome.Text == "L O G I N")
            {
                txtNome.Text = "";
            }
        }        
        private void txtNome_Leave(object sender, EventArgs e)
        {
            if (txtNome.Text == "")
            {
                txtNome.Text = "L O G I N";
            }
        }
        private void txtSenha_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtSenha.Text == "S E N H A")
            {
                txtSenha.Text = "";
                txtSenha.UseSystemPasswordChar = true;
            }
        }
        private void txtSenha_Leave(object sender, EventArgs e)
        {
            if (txtSenha.Text == "")
            {
                txtSenha.Text = "S E N H A";
                txtSenha.UseSystemPasswordChar = false;
            }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            Loading frm = new Loading();
            frm.Show();
            this.Hide();
        }
    }
}
