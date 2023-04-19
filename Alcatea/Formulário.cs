using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alcatea
{

    public partial class Formulário : Form
    {
        private bool drag = false, blClientes, blPratos, blPedidos, blEstoque, modificar = false;
        private bool BoolPrato0 = true, BoolPrato1= false, BoolPrato2 = false, BoolPrato3 = false, BoolPrato4 = false, BoolPrato5 = false, BoolPrato6 = false, BoolPrato7 = false;
        Int32 Código, qtdePratos = 0, contPratos = 1;
        private Point startPoint = new Point(0, 0);
        DataSet ds;
        ClasseConexao con;
        List<ComboBox> CBS = new List<ComboBox>();
        List<Label> LCD = new List<Label>();
        List<Label> LPC = new List<Label>();
        private Int32 CodigoFunc;        
        public Formulário()
        {
            InitializeComponent();
            Arrendondar();
            txtPratos_Preco.Text = "0";
            btnAdicionarPrato.Location = new Point(2, 43);
        }
        private void Arrendondar()
        {
            //Cadastro de insumos
            Bunifu.Framework.Lib.Elipse.Apply(ptbNomeInsumo, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnEstoque_Apagar, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnEstoque_Cadastrar, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnEstoque_cdt_voltar, 10);
            Bunifu.Framework.Lib.Elipse.Apply(pcbQtde, 24);
            Bunifu.Framework.Lib.Elipse.Apply(pcbVal, 24);

            //Cadastro de pratos 
            Bunifu.Framework.Lib.Elipse.Apply(pictureBox2, 24);
            Bunifu.Framework.Lib.Elipse.Apply(pictureBox3, 24);
            Bunifu.Framework.Lib.Elipse.Apply(pictureBox4, 24);
            Bunifu.Framework.Lib.Elipse.Apply(pictureBox5, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnPratos_Cadastrar, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnPratos_Apagar, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnPratos_cdt_voltar, 10);

            //Painel de estoque
            Bunifu.Framework.Lib.Elipse.Apply(btnEstoque_cdt, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnEstoque_rbt, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnEstoque_voltar, 10);

            //Painel de pedidos
            Bunifu.Framework.Lib.Elipse.Apply(btnPedidos_cdt, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnPedidos_vrf, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnPedidos_voltar, 10);

            //Reabastecimento de estoque
            Bunifu.Framework.Lib.Elipse.Apply(pictureBox6, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnEstoque_rbt_Cadastrar, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnEstoque_rbt_Apagar, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnEstoque_rbt_voltar, 10);
            Bunifu.Framework.Lib.Elipse.Apply(pictureBox7, 24);

            //Painel de pratos
            Bunifu.Framework.Lib.Elipse.Apply(btnPratos_cdt, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnPratos_vrf, 24);
            Bunifu.Framework.Lib.Elipse.Apply(btnPratos_voltar, 10);

            //Tabela de clientes
            Bunifu.Framework.Lib.Elipse.Apply(btnClientes_voltar, 10);
            Bunifu.Framework.Lib.Elipse.Apply(btnPesquisa_Clientes, 16);
            Bunifu.Framework.Lib.Elipse.Apply(pictureBox8, 16);

            //Tabela de pedidos
            Bunifu.Framework.Lib.Elipse.Apply(btnPedidos_vrf_voltar, 10);
            Bunifu.Framework.Lib.Elipse.Apply(btnPesquisa_Pedidos, 16);
            Bunifu.Framework.Lib.Elipse.Apply(pictureBox9, 16);

            //Tabela de insumos
            Bunifu.Framework.Lib.Elipse.Apply(btnEstoque_vrf_voltar, 10);
            Bunifu.Framework.Lib.Elipse.Apply(btnPesquisa_Estoque, 16);
            Bunifu.Framework.Lib.Elipse.Apply(pictureBox10, 16);
            Bunifu.Framework.Lib.Elipse.Apply(btnEstoque_Modificar, 10);

            //Tabela de pratos
            Bunifu.Framework.Lib.Elipse.Apply(btnPratos_vrf_voltar, 10);
            Bunifu.Framework.Lib.Elipse.Apply(btnPesquisa_Pratos, 16);
            Bunifu.Framework.Lib.Elipse.Apply(pictureBox11, 16);
            Bunifu.Framework.Lib.Elipse.Apply(btnPratos_Modificar, 10);
        }
        private void formatar_grid(DataGridView fdgv, String tipo)
        {
            //permite personalizar o grid
            fdgv.AutoGenerateColumns = false;
            fdgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            fdgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            //altera a cor das linhas alternadas no grid,
            //fdgv.RowsDefaultCellStyle.BackColor = Color.White;
            //fdgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            //altera o nome das colunas
            switch (tipo)
            {
                case "cliente":
                    fdgv.Columns[0].HeaderText = "Código";
                    fdgv.Columns[1].HeaderText = "Nome";
                    fdgv.Columns[2].HeaderText = "Login";
                    fdgv.Columns[3].HeaderText = "Senha";
                    fdgv.Columns[4].HeaderText = "E-mail";
                    fdgv.Columns[5].HeaderText = "Telefone";
                    fdgv.Columns[0].Width = 45;
                    fdgv.Columns[1].Width = 150;
                    fdgv.Columns[2].Width = 80;
                    fdgv.Columns[3].Width = 80;
                    fdgv.Columns[4].Width = 150;
                    fdgv.Columns[5].Width = 80;
                    break;
                case "funcionário":
                    fdgv.Columns[0].HeaderText = "Código";
                    fdgv.Columns[1].HeaderText = "Permissão";
                    fdgv.Columns[2].HeaderText = "Nome";
                    fdgv.Columns[3].HeaderText = "E-mail";
                    fdgv.Columns[4].HeaderText = "Endereço";
                    fdgv.Columns[5].HeaderText = "Telefone";
                    fdgv.Columns[6].HeaderText = "Senha";
                    fdgv.Columns[0].Width = 45;
                    fdgv.Columns[1].Width = 20;
                    fdgv.Columns[2].Width = 150;
                    fdgv.Columns[3].Width = 150;
                    fdgv.Columns[4].Width = 150;
                    fdgv.Columns[5].Width = 80;
                    fdgv.Columns[6].Width = 80;
                    break;
                case "produto":
                    fdgv.Columns[0].HeaderText = "Código";
                    fdgv.Columns[1].HeaderText = "Nome";
                    fdgv.Columns[2].HeaderText = "Preço";
                    fdgv.Columns[3].HeaderText = "Descrição";
                    fdgv.Columns[4].HeaderText = "Tipo de produto";
                    fdgv.Columns[2].DefaultCellStyle.Format = "c";
                    fdgv.Columns[0].Width = 45;
                    fdgv.Columns[1].Width = 150;
                    fdgv.Columns[2].Width = 47;
                    fdgv.Columns[3].Width = 115;
                    fdgv.Columns[4].Width = 100;
                    break;
                case "insumo":
                    fdgv.Columns[0].HeaderText = "Código";
                    fdgv.Columns[1].HeaderText = "Nome";
                    fdgv.Columns[2].HeaderText = "Quantidade";
                    fdgv.Columns[3].HeaderText = "Validade";
                    fdgv.Columns[0].Width = 45;
                    fdgv.Columns[1].Width = 212;
                    fdgv.Columns[2].Width = 100;
                    fdgv.Columns[3].Width = 100;
                    break;
                case "pedido":
                    fdgv.Columns[0].HeaderText = "Código";
                    fdgv.Columns[1].HeaderText = "Cod Mesa";
                    fdgv.Columns[2].HeaderText = "Cod Func";
                    fdgv.Columns[0].Width = 152;
                    fdgv.Columns[1].Width = 152;
                    fdgv.Columns[2].Width = 152;
                    break;
            }
            //formata a coluna para moeda (currency)
            //grid.Columns[3].DefaultCellStyle.Format = "c";
            //ao clicar, seleciona a linha inteira
            fdgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //não permite seleção de multiplas linhas    
            fdgv.MultiSelect = false;
            // exibe vazio no lugar de null
            fdgv.DefaultCellStyle.NullValue = "";
            //Expande a célula automáticamente
            fdgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //alinha à direita os campos moeda
            //grid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        private void atualizarGrid(DataGridView dgv, String comando)
        {
            con = new ClasseConexao();
            ds = new DataSet();
            ds = con.retornarSQL(comando);
            dgv.DataSource = ds.Tables[0];
        }
        private DataSet BuscarDados(String comando)
        {
            con = new ClasseConexao();
            DataSet dts = new DataSet();
            dts = con.retornarSQL(comando);
            return dts;
        }
        private void fecharPanels()
        {
            pnlPrincipal.Visible = false;
            pnlClientes.Visible = false;
            pnlPratos.Visible = false;
            pnlPedidos.Visible = false;
            pnlEstoque.Visible = false;
            pnlPratos_cdt.Visible = false;
            pnlEstoque_cdt.Visible = false;
            pnlPedidos_cdt.Visible = false;
            pnlPratos_vrf.Visible = false;
            pnlEstoque_vrf.Visible = false;
            pnlPedidos_vrf.Visible = false;
            pnlEstoque_rbt.Visible = false;
        }
        private bool Selecionar(bool botao,String a)
        {
            switch (a)
            {
                case "clientes":
                    btnPratos.ForeColor = Color.FromArgb(69, 68, 68);
                    btnPedidos.ForeColor = Color.FromArgb(69, 68, 68);
                    btnEstoque.ForeColor = Color.FromArgb(69, 68, 68);
                    break;
                case "pratos":
                    btnClientes.ForeColor = Color.FromArgb(69, 68, 68);
                    btnPedidos.ForeColor = Color.FromArgb(69, 68, 68);
                    btnEstoque.ForeColor = Color.FromArgb(69, 68, 68);
                    break;
                case "pedidos":
                    btnClientes.ForeColor = Color.FromArgb(69, 68, 68);
                    btnPratos.ForeColor = Color.FromArgb(69, 68, 68);
                    btnEstoque.ForeColor = Color.FromArgb(69, 68, 68);
                    break;
                case "estoque":
                    btnClientes.ForeColor = Color.FromArgb(69, 68, 68);
                    btnPedidos.ForeColor = Color.FromArgb(69, 68, 68);
                    btnPratos.ForeColor = Color.FromArgb(69, 68, 68);
                    break;
            }
            blClientes = false;
            blPratos = false;
            blPedidos = false;
            blEstoque = false;
            return true;            
        }
        private void Conferir(object sender, EventArgs e)
        {
           switch (((Button)sender).Name)
            {
                case "btnClientes":
                    trocarCor((Button)sender, blClientes);
                break;
                case "btnPratos":
                    trocarCor((Button)sender, blPratos);
                break;
                case "btnPedidos":
                    trocarCor((Button)sender, blPedidos);
                break;
                case "btnEstoque":
                    trocarCor((Button)sender, blEstoque);
                break;
            }
        }
        private void trocarCor(Button btn, bool a)
        {            
            if (btn.ForeColor == Color.FromArgb(69, 68, 68))
            {
                btn.ForeColor = Color.White;
            }
            else
            {
                if (!a)
                {
                    btn.ForeColor = Color.FromArgb(69, 68, 68);
                }
            }            
        }
        private void trocarSelecao(PictureBox pcb) {
            pcbClientes.BackColor = Color.FromArgb(210, 42, 37);
            pcbEstoque.BackColor = Color.FromArgb(210, 42, 37);
            pcbPedidos.BackColor = Color.FromArgb(210, 42, 37);
            pcbPratos.BackColor = Color.FromArgb(210, 42, 37);
            pcb.BackColor = Color.White;
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
                Point p3 = new Point(p2.X - this.startPoint.X,p2.Y - this.startPoint.Y);
                this.Location = p3;
            }
        }

        private void BtnPratos_cdt_Click(object sender, EventArgs e)
        {
            fecharPanels();
            pnlPratos_cdt.Visible = true;
        }
        private void BtnEstoque_cdt_Click(object sender, EventArgs e)
        {
            fecharPanels();
            pnlEstoque_cdt.Visible = true;
        }

        private void BtnPedidos_cdt_Click(object sender, EventArgs e)
        {
            fecharPanels();
            pnlPedidos_cdt.Visible = true;
        }

        private void BtnEstoque_vrf_Click(object sender, EventArgs e)
        {
            fecharPanels();
            pnlEstoque_vrf.Visible = true;
        }

        private void BtnPedidos_vrf_Click(object sender, EventArgs e)
        {
            fecharPanels();
            pnlPedidos_vrf.Visible = true;
        }

        private void BtnPratos_vrf_Click(object sender, EventArgs e)
        {
            fecharPanels();
            pnlPratos_vrf.Visible = true;
        }

        private void BtnEstoque_rbt_Click(object sender, EventArgs e)
        {
            fecharPanels();
            pnlEstoque_rbt.Visible = true;
        }

        private void BtnPrincipal_voltar_Click(object sender, EventArgs e)
        {
            fecharPanels();
            pnlPrincipal.Visible = true;
            pcbClientes.BackColor = Color.FromArgb(210, 42, 37);
            pcbEstoque.BackColor = Color.FromArgb(210, 42, 37);
            pcbPedidos.BackColor = Color.FromArgb(210, 42, 37);
            pcbPratos.BackColor = Color.FromArgb(210, 42, 37);
            btnPratos.ForeColor = Color.FromArgb(69, 68, 68);
            btnPedidos.ForeColor = Color.FromArgb(69, 68, 68);
            btnEstoque.ForeColor = Color.FromArgb(69, 68, 68);
            btnClientes.ForeColor = Color.FromArgb(69, 68, 68);
            blClientes = false;
            blPratos = false;
            blPedidos = false;
            blEstoque = false;
        }

        private void BtnPedidos_cdt_voltar_Click(object sender, EventArgs e)
        {
            fecharPanels();
            pnlPedidos.Visible = true;
        }

        private void BtnEstoque_cdt_voltar_Click(object sender, EventArgs e)
        {
            fecharPanels();
            BtnEstoque_Apagar_Click(sender, e);
            pnlEstoque.Visible = true;
            if (modificar)
            {
                modificar = false;
            }
        }

        private void BtnPratos_cdt_voltar_Click(object sender, EventArgs e)
        {
            fecharPanels();
            BtnPratos_Apagar_Click(sender, e);
            pnlPratos.Visible = true;
            if (modificar)
            {
                modificar = false;
            }
        }

        private void PnlPratos_vrf_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlPratos_vrf.Visible)
            {
                atualizarGrid(dgvPratos,"select * from tblProdutos");
                formatar_grid(dgvPratos, "produto");
            }
        }

        private void PnlClientes_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlClientes.Visible)
            {
                atualizarGrid(dgvClientes, "select * from tblClientes");
                formatar_grid(dgvClientes, "cliente");
            }
        }

        private void PnlPedidos_vrf_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlPedidos_vrf.Visible)
            {
                atualizarGrid(dgvPedidos, "select * from tblPedidos");
                formatar_grid(dgvPedidos, "pedido");
            }
        }

        private void PnlEstoque_vrf_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlEstoque_vrf.Visible)
            {
                atualizarGrid(dgvEstoque, "select * from tblInsumos");
                formatar_grid(dgvEstoque, "insumo");
            }
        }

        private void BtnPratos_Apagar_Click(object sender, EventArgs e)
        {
            txtPratos_Nome.Text = null;
            txtPratos_Descricao.Text = null;
            txtPratos_Preco.Text = "0";
            txtPratos_Tipo.Text = "-------------------";
        }

        private void BtnEstoque_rbt_Apagar_Click(object sender, EventArgs e)
        {
            txtRBT_Insumo.Text = null;
            txtRBT_Quantidade.Text = null;
        }

        private void BtnEstoque_Apagar_Click(object sender, EventArgs e)
        {
            txtEstoque_Nome.Text = null;
            txtEstoque_Quantidade.Text = null;
            txtEstoque_Validade.Text = null;
        }
        private void BtnPedidos_Apagar_Click(object sender, EventArgs e)
        {
            txtPedidos_Funcionario.Text = null;
            txtPedidos_Mesa = null;
        }
        private void BtnPratos_Cadastrar_Click(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            if (modificar)
            {
                Int32 tipo = 0;
                switch (txtPratos_Tipo.Text)
                {
                    case "Prato principal":
                        tipo = 1;
                        break;
                    case "Sobremesa":
                        tipo = 2;
                        break;
                    case "Acompanhamento":
                        tipo = 3;
                        break;
                    case "Entrada":
                        tipo = 4;
                        break;
                }
                con.executarSQL("update tblProdutos set NomeProduto = '" + txtPratos_Nome.Text + "' where CodProduto = " + Código);
                con = new ClasseConexao();
                con.executarSQL("update tblProdutos set Preco = " + txtPratos_Preco.Text + " where CodProduto = " + Código);
                con = new ClasseConexao();
                con.executarSQL("update tblProdutos set descricao = '" + txtPratos_Descricao.Text + "' where CodProduto = " + Código);
                con = new ClasseConexao();
                con.executarSQL("update tblProdutos set Tipo_de_Produto = " + tipo + " where CodProduto = " + Código);
                modificar = false;
                Código = 0;
                btnPratos_Cadastrar.Text = "Cadastrar";
                BtnPratos_Apagar_Click(sender, e);
                BtnPratos_vrf_Click(sender, e);
                MessageBox.Show("Cadastro modificado com sucesso!");
            }
            else
            {
                Int32 tipo = 0;
                switch (txtPratos_Tipo.Text)
                {
                    case "Prato principal":
                        tipo = 1;
                        break;
                    case "Sobremesa":
                        tipo = 2;
                        break;
                    case "Acompanhamento":
                        tipo = 3;
                        break;
                    case "Entrada":
                        tipo = 4;
                        break;
                }
                con.executarSQL("insert into tblProdutos values ('" + txtPratos_Nome.Text + "'," + txtPratos_Preco.Text + ",'" + txtPratos_Descricao.Text + "'," + tipo + ")");
                BtnPratos_Apagar_Click(sender, e);
                MessageBox.Show("Cadastro feito com sucesso!");
            }
        }
        private void BtnEstoque_rbt_Cadastrar_Click(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            con.executarSQL("update tblInsumos set QtdInsumo = QtdInsumo + " + txtRBT_Quantidade.Text + " where CodInsumos = " + txtRBT_Insumo.Text);
            //con.executarSQL("update tblInsumos set DataReabastecimento = " + txtReabastecimento_Data.Text + " where CodInsumos = " + txtReabastecimento_Codigo.Text);
            BtnEstoque_rbt_Apagar_Click(sender, e);
            BtnEstoque_vrf_Click(sender, e);
        }

        private void BtnPratos_Modificar_Click(object sender, EventArgs e)
        {

            con = new ClasseConexao();
            modificar = true;
            Código = (Int32)dgvPratos.CurrentRow.Cells[0].Value;
            txtPratos_Nome.Text = (String)dgvPratos.CurrentRow.Cells[1].Value;
            txtPratos_Preco.Text = (String)dgvPratos.CurrentRow.Cells[2].Value;
            txtPratos_Descricao.Text = dgvPratos.CurrentRow.Cells[3].Value.ToString();
            switch ((Int32)dgvPratos.CurrentRow.Cells[4].Value)
            {
                case 1:
                    txtPratos_Tipo.Text = "Prato principal";
                    break;
                case 2:
                    txtPratos_Tipo.Text = "Sobremesa";
                    break;
                case 3:
                    txtPratos_Tipo.Text = "Acompanhamento";
                    break;
                case 4:
                    txtPratos_Tipo.Text = "Entrada";
                    break;
            }
            btnPratos_Cadastrar.Text = "Atualizar";
            BtnPratos_cdt_Click(sender, e);
        }

        private void BtnEstoque_Modificar_Click(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            modificar = true;
            Código = (Int32)dgvEstoque.CurrentRow.Cells[0].Value;
            txtEstoque_Nome.Text = (String)dgvEstoque.CurrentRow.Cells[1].Value;
            txtEstoque_Quantidade.Text = dgvEstoque.CurrentRow.Cells[2].Value.ToString();
            txtEstoque_Validade.Text = dgvEstoque.CurrentRow.Cells[3].Value.ToString();
            btnEstoque_Cadastrar.Text = "Atualizar";
            BtnEstoque_cdt_Click(sender, e);
        }

        private void BtnPesquisa_Pedidos_Click(object sender, EventArgs e)
        {

            try
            {
                atualizarGrid(dgvPedidos, "select * from tblPedidos where CodPedidos = " + txtPesquisa_Pedidos.Text + " or CodMesa = " + txtPesquisa_Pedidos.Text + " or CodFunc = " + txtPesquisa_Pedidos.Text);
            }
            catch (Exception erro)
            {
                try
                {
                    atualizarGrid(dgvPedidos, "select * from tblPedidos");
                }
                catch
                {
                    MessageBox.Show("Erro ao acessar dados");
                }
            }
        }

        private void BtnPesquisa_Estoque_Click(object sender, EventArgs e)
        {

            try
            {
                atualizarGrid(dgvEstoque, "select * from tblInsumos where CodInsumos = " + txtPesquisa_Estoque.Text + " or NomeInsumo like '%" + txtPesquisa_Estoque.Text + "%' or QtdInsumo = " + txtPesquisa_Estoque.Text);
            }
            catch (Exception erro)
            {
                atualizarGrid(dgvEstoque, "select * from tblInsumos where NomeInsumo like '%" + txtPesquisa_Estoque.Text + "%'");
            }
        }

        private void BtnPesquisa_Pratos_Click(object sender, EventArgs e)
        {

            try
            {
                atualizarGrid(dgvPratos, "select * from tblProdutos where CodProduto = '" + txtPesquisa_Pratos.Text + "' or NomeProduto like '%" + txtPesquisa_Pratos.Text + "%' or Preco like $" + txtPesquisa_Pratos.Text + " or descricao like '%" + txtPesquisa_Pratos.Text + "%'");
            }
            catch (Exception erro)
            {
                atualizarGrid(dgvPratos, "select * from tblProdutos where NomeProduto like '%" + txtPesquisa_Pratos.Text + "%' or descricao like '%" + txtPesquisa_Pratos.Text + "%'");
            }
        }
        private void BtnPesquisa_Clientes_Click(object sender, EventArgs e)
        {
            try
            {
                atualizarGrid(dgvClientes, "select * from tblClientes where CodCliente = " + txtPesquisa_Clientes.Text + " or Nome like '%" + txtPesquisa_Clientes.Text + "%' or LoginCliente like '%" + txtPesquisa_Clientes.Text + "%' or senha like '%" + txtPesquisa_Clientes.Text + "%' or email like '%" + txtPesquisa_Clientes.Text + "%' or telefone like '%" + txtPesquisa_Clientes.Text + "%'");
            }
            catch (Exception erro)
            {
                atualizarGrid(dgvClientes, "select * from tblClientes where Nome like '%" + txtPesquisa_Clientes.Text + "%' or LoginCliente like '%" + txtPesquisa_Clientes.Text + "%' or senha like '%" + txtPesquisa_Clientes.Text + "%' or email like '%" + txtPesquisa_Clientes.Text + "%'");
            }
        }

        private void BtnEstoque_Cadastrar_Click(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            if (modificar)
            {
                try
                {                    
                    con.executarSQL("update tblInsumos set NomeInsumo = '" + txtEstoque_Nome.Text + "' where CodInsumos = " + Código);
                    con = new ClasseConexao();
                    con.executarSQL("update tblInsumos set QtdInsumo = " + txtEstoque_Quantidade.Text + " where CodInsumos = " + Código);
                    con = new ClasseConexao();
                    con.executarSQL("update tblInsumos set ValidadeInsumo = '" + txtEstoque_Validade.Text + "' where CodInsumos = " + Código);
                    modificar = false;
                    Código = 0;
                    btnEstoque_Cadastrar.Text = "Cadastrar";
                    BtnEstoque_Apagar_Click(sender, e);
                    BtnEstoque_vrf_Click(sender, e);
                }
                catch (Exception erro)
                {
                    
                }
            }
            else
            {
                try
                {
                    con.executarSQL("insert into tblInsumos values ('" + txtEstoque_Nome.Text + "'," + txtEstoque_Quantidade.Text + ",'" + txtEstoque_Validade.Text + "')");
                    BtnEstoque_Apagar_Click(sender, e);
                    MessageBox.Show("Cadastro feito com sucesso!");
                }
                catch(Exception erro)
                {
                    MessageBox.Show("Não foi possível cadastrar " + erro);
                }
            }
        }
        private void BtnPedidos_Cadastrar_Click(object sender, EventArgs e)
        {
            con.executarSQL("insert into tblProdutos values ('" + txtPedidos_Mesa.Text + "','" + txtPedidos_Funcionario.Text+"'");
            BtnPedidos_Apagar_Click(sender, e);
            Int32 CodPed = Convert.ToInt32(BuscarDados("select max(CodPedido) from tblPedidos"))+1;
            try
            {
                con.executarSQL("insert into tblPedidos_Produtos values ("+CodPed+","+lblCod0.Text+","+txtQntde0+")");
            }
            catch(Exception erro){
                    MessageBox.Show("Não foi possível cadastrar o primeiro prato" + erro);
            }
            try
            {
                con.executarSQL("insert into tblPedidos_Produtos values (" + CodPed + "," + lblCod1.Text + "," + txtQntde1 + ")");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível cadastrar o segundo prato" + erro);
            }
            try
            {
                con.executarSQL("insert into tblPedidos_Produtos values (" + CodPed + "," + lblCod2.Text + "," + txtQntde2 + ")");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível cadastrar o terceiro prato" + erro);
            }
            try
            {
                con.executarSQL("insert into tblPedidos_Produtos values (" + CodPed + "," + lblCod3.Text + "," + txtQntde3 + ")");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível cadastrar o quarto prato" + erro);
            }
            try
            {
                con.executarSQL("insert into tblPedidos_Produtos values (" + CodPed + "," + lblCod4.Text + "," + txtQntde4 + ")");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Não foi possível cadastrar o quinto prato" + erro);
            }
            MessageBox.Show("Pedido realizado com sucesso!");
        }
        private void BtnClientes_Click(object sender, EventArgs e)
        {
            blClientes = Selecionar(blClientes,"clientes");
            trocarSelecao(pcbClientes);
            fecharPanels();
            pnlClientes.Visible = true;
        }

        private void BtnPratos_Click(object sender, EventArgs e)
        {
            blPratos = Selecionar(blPratos,"pratos");
            trocarSelecao(pcbPratos);
            fecharPanels();
            pnlPratos.Visible = true;
        }       

        private void BtnEstoque_Click(object sender, EventArgs e)
        {
            blEstoque = Selecionar(blEstoque,"estoque");
            trocarSelecao(pcbEstoque);
            fecharPanels();
            pnlEstoque.Visible = true;
        }

        private void txtPratos_Preco_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void BtnPedidos_Click(object sender, EventArgs e)
        {
            blPedidos = Selecionar(blEstoque,"pedidos");
            trocarSelecao(pcbPedidos);
            fecharPanels();
            pnlPedidos.Visible = true;
        }      

        private void btnEstoque_cdt_MouseEnter(object sender, EventArgs e)
        {
            pcbEstoque_cdt.BackColor = Color.FromArgb(139, 138, 138);
        }

        private void btnEstoque_cdt_MouseLeave(object sender, EventArgs e)
        {
            pcbEstoque_cdt.BackColor = Color.FromArgb(116, 115, 115);
        }

        private void btnEstoque_rbt_MouseEnter(object sender, EventArgs e)
        {
            pcbEstoque_rbt.BackColor = Color.FromArgb(139, 138, 138);
        }

        private void btnEstoque_rbt_MouseLeave(object sender, EventArgs e)
        {
            pcbEstoque_rbt.BackColor = Color.FromArgb(116, 115, 115);
        }

        private void btnDeletar0_Click(object sender, EventArgs e)
        {
            contPratos--;
            btnDeletar0.Visible = false;
            cbbPrato0.Visible = false;
            pcbPrato0.Visible = false;
            txtQntde0.Visible = false;
            lblCod0.Visible = false;
            lblPreco0.Visible = false;
            Relocalizar(contPratos);
        }
        private void btnPedidos_cdt_MouseEnter(object sender, EventArgs e)
        {
            pcbPedidos_cdt.BackColor = Color.FromArgb(139, 138, 138);
        }

        private void btnPedidos_cdt_MouseLeave(object sender, EventArgs e)
        {
            pcbPedidos_cdt.BackColor = Color.FromArgb(116, 115, 115);
        }

        private void btnDeletar1_Click(object sender, EventArgs e)
        {
            contPratos--;
            btnDeletar1.Visible = false;
            cbbPrato1.Visible = false;
            pcbPrato1.Visible = false;
            txtQntde1.Visible = false;
            lblCod1.Visible = false;
            lblPreco1.Visible = false;
            Relocalizar(contPratos);
        }

        private void btnDeletar2_Click(object sender, EventArgs e)
        {
            contPratos--;
            btnDeletar2.Visible = false;
            cbbPrato2.Visible = false;
            pcbPrato2.Visible = false;
            txtQntde2.Visible = false;
            lblCod2.Visible = false;
            lblPreco2.Visible = false;
            Relocalizar(contPratos);
        }

        private void btnDeletar3_Click(object sender, EventArgs e)
        {            
            contPratos--;
            btnDeletar3.Visible = false;
            cbbPrato3.Visible = false;
            pcbPrato3.Visible = false;
            txtQntde3.Visible = false;
            lblCod3.Visible = false;
            lblPreco3.Visible = false;
            Relocalizar(contPratos);            
        }

        private void btnDeletar4_Click(object sender, EventArgs e)
        {
            contPratos--;
            btnDeletar4.Visible = false;
            cbbPrato4.Visible = false;
            pcbPrato4.Visible = false;
            txtQntde4.Visible = false;
            lblCod4.Visible = false;
            lblPreco4.Visible = false;
            Relocalizar(contPratos);
        }
        private void cbbPrato0_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds = BuscarDados("select CodProduto from tblProdutos where NomeProduto like '" + cbbPrato0.Text + "'");
            lblCod0.Text = ds.ToString();
        }
        private void cbbPrato1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds = BuscarDados("select CodProduto from tblProdutos where NomeProduto like '"+cbbPrato1.Text+"'");
            lblCod1.Text = ds.ToString();
        }

        private void cbbPrato2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds = BuscarDados("select CodProduto from tblProdutos where NomeProduto like '" + cbbPrato2.Text + "'");
            lblCod2.Text = ds.ToString();
        }

        private void cbbPrato3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds = BuscarDados("select CodProduto from tblProdutos where NomeProduto like '" + cbbPrato3.Text + "'");
            lblCod3.Text = ds.ToString();
        }

        private void cbbPrato4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds = BuscarDados("select CodProduto from tblProdutos where NomeProduto like '" + cbbPrato4.Text + "'");
            lblCod4.Text = ds.ToString();
        }

        private void txtQntde0_TextChanged(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            Double pr = (Convert.ToInt32(con.ValorSQL("select Preco from tblProdutos where CodProduto = " + lblCod0.Text + "")));
            lblPreco0.Text = "R$ " + (pr * (Convert.ToInt32(txtQntde0.Text)));
        }

        private void txtQntde1_TextChanged(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            Double pr = (Convert.ToInt32(con.ValorSQL("select Preco from tblProdutos where CodProduto = " + lblCod1.Text + "")));
            lblPreco1.Text = "R$ " + (pr * (Convert.ToInt32(txtQntde1.Text)));
        }

        private void txtQntde2_TextChanged(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            Double pr = (Convert.ToInt32(con.ValorSQL("select Preco from tblProdutos where CodProduto = " + lblCod2.Text + "")));
            lblPreco2.Text = "R$ " + (pr * (Convert.ToInt32(txtQntde2.Text)));
        }

        private void txtQntde3_TextChanged(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            Double pr = (Convert.ToInt32(con.ValorSQL("select Preco from tblProdutos where CodProduto = " + lblCod3.Text + "")));
            lblPreco3.Text = "R$ " + (pr * (Convert.ToInt32(txtQntde3.Text)));
        }

        private void txtQntde4_TextChanged(object sender, EventArgs e)
        {
            con = new ClasseConexao();
            Double pr = (Convert.ToInt32(con.ValorSQL("select Preco from tblProdutos where CodProduto = " + lblCod4.Text + "")));
            lblPreco4.Text = "R$ " + (pr * (Convert.ToInt32(txtQntde4.Text)));
        }
        private void pnlPedidos_cdt_VisibleChanged(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds = BuscarDados("select NomeProduto from tblProdutos");
            cbbPrato0.DataSource = ds;
            cbbPrato1.DataSource = ds;
            cbbPrato2.DataSource = ds;
            cbbPrato3.DataSource = ds;
            cbbPrato4.DataSource = ds;
        }

        private void btnPedidos_vrf_MouseEnter(object sender, EventArgs e)
        {
            pcbPedidos_vrf.BackColor = Color.FromArgb(139, 138, 138);
        }

        private void btnPedidos_vrf_MouseLeave(object sender, EventArgs e)
        {
            pcbPedidos_vrf.BackColor = Color.FromArgb(116, 115, 115);
        }

        private void btnPratos_cdt_MouseEnter(object sender, EventArgs e)
        {
            pcbPratos_cdt.BackColor = Color.FromArgb(139, 138, 138);
        }

        private void btnPratos_cdt_MouseLeave(object sender, EventArgs e)
        {
            pcbPratos_cdt.BackColor = Color.FromArgb(116, 115, 115);
        }

        private void btnPratos_vrf_MouseEnter(object sender, EventArgs e)
        {
            pcbPratos_vrf.BackColor = Color.FromArgb(139, 138, 138);
        }

        private void btnPratos_vrf_MouseLeave(object sender, EventArgs e)
        {
            pcbPratos_vrf.BackColor = Color.FromArgb(116, 115, 115);
        }
        private void btnAdicionarPrato_Click(object sender, EventArgs e)
        {
            if (!btnDeletar0.Visible)
            {
                btnDeletar0.Visible = true;
                cbbPrato0.Visible = true;
                pcbPrato0.Visible = true;
                txtQntde0.Visible = true;
                lblCod0.Visible = true;
                lblPreco0.Visible = true;
                LocalizarPrato(btnDeletar0, cbbPrato0, pcbPrato0, txtQntde0, lblCod0, lblPreco0, contPratos);
            }
            else if (!btnDeletar1.Visible)
            {                
                btnDeletar1.Visible = true;
                cbbPrato1.Visible = true;
                pcbPrato1.Visible = true;
                txtQntde1.Visible = true;
                lblCod1.Visible = true;
                lblPreco1.Visible = true;
                LocalizarPrato(btnDeletar1, cbbPrato1, pcbPrato1, txtQntde1, lblCod1, lblPreco1, contPratos);
            }
            else if(!btnDeletar2.Visible)
            {                
                btnDeletar2.Visible = true;
                cbbPrato2.Visible = true;
                pcbPrato2.Visible = true;
                txtQntde2.Visible = true;
                lblCod2.Visible = true;
                lblPreco2.Visible = true;
                LocalizarPrato(btnDeletar2, cbbPrato2, pcbPrato2, txtQntde2, lblCod2, lblPreco2, contPratos);
            }
            else if (!btnDeletar3.Visible)
            {                
                btnDeletar3.Visible = true;
                cbbPrato3.Visible = true;
                pcbPrato3.Visible = true;
                txtQntde3.Visible = true;
                lblCod3.Visible = true;
                lblPreco3.Visible = true;
                LocalizarPrato(btnDeletar3, cbbPrato3, pcbPrato3, txtQntde3, lblCod3, lblPreco3, contPratos);
            }
            else if (!btnDeletar4.Visible)
            {
                btnDeletar4.Visible = true;
                cbbPrato4.Visible = true;
                pcbPrato4.Visible = true;
                txtQntde4.Visible = true;
                lblCod4.Visible = true;
                lblPreco4.Visible = true;
                LocalizarPrato(btnDeletar4, cbbPrato4, pcbPrato4, txtQntde4, lblCod4, lblPreco4, contPratos);
            }
        }
        private void LocalizarPrato(Button btn, ComboBox cbb, PictureBox pcb, TextBox txt, Label lblC, Label lblP, Int32 cont)
        {
            if (btn.Visible)
            {
                contPratos++;                
                btn.Location = new Point(2, ((cont - 1) * 35) + 43);
                cbb.Location = new Point(40, ((cont - 1) * 35) + 43);
                pcb.Location = new Point(185, ((cont - 1) * 35) + 43);
                txt.Location = new Point(190, ((cont - 1) * 35) + 45);
                lblC.Location = new Point(275, ((cont - 1) * 35) + 43);
                lblP.Location = new Point(344, ((cont - 1) * 35) + 43);
            }
            LocalizarBotão();
        }
        private void LocalizarBotão ()
        {
            btnAdicionarPrato.Location = new Point(2, ((contPratos-1) * 35) + 43);
        }
        private void Relocalizar(Int32 cont)
        {
            Int32 i = 0, j = 0;
            while (i<cont)
            {
                switch (j)
                {
                    case 0:
                        if (btnDeletar0.Visible)
                        {
                            btnDeletar0.Location = new Point(2, ((i - 1) * 35) + 43);
                            cbbPrato0.Location = new Point(40, ((i - 1) * 35) + 43);
                            pcbPrato0.Location = new Point(185, ((i - 1) * 35) + 43);
                            txtQntde0.Location = new Point(190, ((i - 1) * 35) + 45);
                            lblCod0.Location = new Point(275, ((i - 1) * 35) + 43);
                            lblPreco0.Location = new Point(344, ((i - 1) * 35) + 43);
                            i++;
                            LocalizarBotão();
                        }
                        j++;
                        break;
                    case 1:
                        if (btnDeletar1.Visible)
                        {
                            btnDeletar1.Location = new Point(2, ((i - 1) * 35) + 43);
                            cbbPrato1.Location = new Point(40, ((i - 1) * 35) + 43);
                            pcbPrato1.Location = new Point(185, ((i - 1) * 35) + 43);
                            txtQntde1.Location = new Point(190, ((i - 1) * 35) + 45);
                            lblCod1.Location = new Point(275, ((i - 1) * 35) + 43);
                            lblPreco1.Location = new Point(344, ((i - 1) * 35) + 43);
                            i++;
                            LocalizarBotão();
                        }
                        j++;
                        break;
                    case 2:
                        if (btnDeletar2.Visible)
                        {
                            btnDeletar2.Location = new Point(2, ((i - 1) * 35) + 43);
                            cbbPrato2.Location = new Point(40, ((i - 1) * 35) + 43);
                            pcbPrato2.Location = new Point(185, ((i - 1) * 35) + 43);
                            txtQntde2.Location = new Point(190, ((i - 1) * 35) + 45);
                            lblCod2.Location = new Point(275, ((i - 1) * 35) + 43);
                            lblPreco2.Location = new Point(344, ((i - 1) * 35) + 43);
                            i++;
                            LocalizarBotão();
                        }
                        j++;
                        break;
                    case 3:
                        if (btnDeletar3.Visible)
                        {
                            btnDeletar3.Location = new Point(2, ((i - 1) * 35) + 43);
                            cbbPrato3.Location = new Point(40, ((i - 1) * 35) + 43);
                            pcbPrato3.Location = new Point(185, ((i - 1) * 35) + 43);
                            txtQntde3.Location = new Point(190, ((i - 1) * 35) + 45);
                            lblCod3.Location = new Point(275, ((i - 1) * 35) + 43);
                            lblPreco3.Location = new Point(344, ((i - 1) * 35) + 43);
                            i++;
                            LocalizarBotão();
                        }
                        j++;
                        break;
                    case 4:
                        if (btnDeletar4.Visible)
                        {
                            btnDeletar4.Location = new Point(2, ((i - 1) * 35) + 43);
                            cbbPrato4.Location = new Point(40, ((i - 1) * 35) + 43);
                            pcbPrato4.Location = new Point(185, ((i - 1) * 35) + 43);
                            txtQntde4.Location = new Point(190, ((i - 1) * 35) + 45);
                            lblCod4.Location = new Point(275, ((i - 1) * 35) + 43);
                            lblPreco4.Location = new Point(344, ((i - 1) * 35) + 43);
                            i++;
                            LocalizarBotão();
                        }
                        j++;
                        break;
                }
            }
        }
        
        private void TxtCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            char letra = 'a';
            String nums = "";
            ComboBox gtn = (ComboBox)sender;            
            for (Int32 i = gtn.Name.Length; letra != 'n' ;i--)
            {
                letra = gtn.Name[i];
                nums += letra;
                letra = gtn.Name[i - 1];
            }
            DGVped.DataSource = BuscarDados("select CodProduto from tblProdutos where NomeProduto like '" + CBS[Convert.ToInt32(nums)].SelectedItem+"'").Tables[0];// mudar aspas na comparação do nome
            LCD[Convert.ToInt32(nums)].Text = DGVped.Rows[0].Cells[0].Value.ToString();
            DGVped.DataSource = BuscarDados("select Preco from tblProdutos where NomeProduto like '" + CBS[Convert.ToInt32(nums)].SelectedItem + "'").Tables[0];// mudar aspas na comparação do nome
            LPC[Convert.ToInt32(nums)].Text = DGVped.Rows[0].Cells[0].Value.ToString();
        }
        private void txtPratos_Preco_TextChanged(object sender, EventArgs e)
        {
            //Remove previous formatting, or the decimal check will fail including leading zeros
            string value = txtPratos_Preco.Text.Replace(",", "")
                .Replace("$", "").Replace(".", "").TrimStart('0');
            decimal ul;
            //Check we are indeed handling a number
            if (decimal.TryParse(value, out ul))
            {
                ul /= 100;
                //Unsub the event so we don't enter a loop
                txtPratos_Preco.TextChanged -= txtPratos_Preco_TextChanged;
                //Format the text as currency
                txtPratos_Preco.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                txtPratos_Preco.TextChanged += txtPratos_Preco_TextChanged;
                txtPratos_Preco.Select(txtPratos_Preco.Text.Length, 0);
            }
            bool goodToGo = TextisValid(txtPratos_Preco.Text);
            if (!goodToGo)
            {
                txtPratos_Preco.Text = "$0.00";
                txtPratos_Preco.Select(txtPratos_Preco.Text.Length, 0);
            }
        }
        private bool TextisValid(string text)
        {
            Regex money = new Regex(@"^\$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$");
            return money.IsMatch(text);
        }
    }
}
