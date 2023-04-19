using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Alcatea
{
    class ClasseConexao
    {

        SqlConnection conexao = new SqlConnection();

        private SqlConnection conectar()
        {
            try
            {
                String strConexao = "Password=etesp; Persist Security Info=True; User ID=sa; Initial Catalog=DatabaseAlcatea; Data Source=" + Environment.MachineName;
                conexao.ConnectionString = strConexao;
                conexao.Open();
                return conexao;
            }
            catch (Exception erro)
            {
                desconectar();
                return null;
            }
        }

        public void desconectar()
        {
            try
            {
                if ((conexao.State == ConnectionState.Open))
                {
                    conexao.Close();
                    conexao.Dispose();
                    conexao = null;
                }
            }
            catch (Exception erro) { }
        }

        public DataSet retornarSQL(String comando_sql)
        {
            try
            {
                conectar();
                SqlDataAdapter adaptador = new SqlDataAdapter(comando_sql, conexao);
                DataSet ds = new DataSet();
                adaptador.Fill(ds);
                return ds;
            }
            catch (Exception erro)
            {
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public object ValorSQL(String comando_sql)
        {
            try
            {
                conectar();                
                SqlCommand lSqlCommand;
                lSqlCommand = new SqlCommand();
                lSqlCommand.CommandText = comando_sql;
                lSqlCommand.CommandType = CommandType.Text;                
                object result = lSqlCommand.ExecuteScalar();
                return result;
            }
            catch (Exception erro)
            {
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public void executarSQL(String comando_sql)
        {
            try
            {
                conectar();
                SqlCommand comand = new SqlCommand(comando_sql, conexao);
                comand.ExecuteNonQuery();
            }
            catch (Exception erro)
            { 

            }
            finally
            {
                desconectar();
            }
        }


    }
}
