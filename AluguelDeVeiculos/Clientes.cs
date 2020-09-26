using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AluguelDeVeiculos
{
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void inícioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Alugueis form1 = new Alugueis();

            this.Hide();
            form1.Show();
        }

        private void cadastraCliente()
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "alugueis";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";

            MySqlConnection connectionBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                connectionBD.Open(); //Abre a conexão com o banco
                //MessageBox.Show("Conexão Aberta!");

                MySqlCommand comandoMySql = connectionBD.CreateCommand(); //Crio um comando SQL
                comandoMySql.CommandText = "INSERT INTO cliente (nome, data_nascimento, cpf, endereco, bairro, cep, cidade, uf) " +
                    "VALUES('" + textNome.Text + "', '" + dateNascimento.Text + "', '" + textCpf.Text + "', '" + textEndereco.Text + "','" + textBairro.Text + "', '" + textCep.Text + "', '" + textCidade.Text + "', '" + comboBoxUf.Text + "')";
                comandoMySql.ExecuteNonQuery();

                connectionBD.Close(); // Fecho a conexão com o banco
                MessageBox.Show("Inserido com sucesso"); //Exibo mensagem de aviso
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível conectar ao servidor!");
                Console.WriteLine(ex.Message);
            }

            atualizarGridClientes();
            textBoxId.Text = "";
            textNome.Text = "";
            dateNascimento.Text = "";
            textCpf.Text = "";
            textEndereco.Text = "";
            textBairro.Text = "";
            textCep.Text = "";
            textCidade.Text = "";
            comboBoxUf.ResetText();
        }

        private void atualizaCliente(int id)
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "alugueis";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";

            MySqlConnection connectionBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                connectionBD.Open(); //Abre a conexão com o banco

                MySqlCommand comandoMySql = connectionBD.CreateCommand(); //Crio um comando SQL
                comandoMySql.CommandText = "UPDATE cliente SET nome = '" + textNome.Text + "', data_nascimento = '" + dateNascimento.Text + "', " +
                    "cpf = '" + textCpf.Text + "', endereco = '" + textEndereco.Text + "', bairro = '" + textBairro.Text + "', cep = '" + textCep.Text + "'," +
                    " cidade = '" + textCidade.Text + "', uf = '" + comboBoxUf.Text + "' WHERE id = '" + id + "'";
                //"VALUES('" + textBoxCpf.Text + "', '" + textBoxPlaca.Text.ToUpper() + "', '" + textBoxKmAtual.Text + "', '" + textBoxKmFinal.Text + "','" + dateTimePickerRetirada.Text + "', '" + dateTimePickerEntrega.Text + "', '" + comboBoxSituacao.Text + "') WHERE id = id";
                comandoMySql.ExecuteNonQuery();

                connectionBD.Close(); // Fecho a conexão com o banco
                MessageBox.Show("Alterado com sucesso"); //Exibo mensagem de aviso
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível conectar ao servidor!");
                Console.WriteLine(ex.Message);
            }

            atualizarGridClientes();
            textBoxId.Text = "";
            textNome.Text = "";
            dateNascimento.Text = "";
            textCpf.Text = "";
            textEndereco.Text = "";
            textBairro.Text = "";
            textCep.Text = "";
            textCidade.Text = "";
            comboBoxUf.ResetText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBoxId.Text != "")
            {
                atualizaCliente(Convert.ToInt32(textBoxId.Text));
            }
            else
            {
                cadastraCliente();
            }
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            atualizarGridClientes();
        }

        private void atualizarGridClientes()
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "alugueis";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";

            MySqlConnection connectionBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                connectionBD.Open();

                MySqlCommand comandoMySql = connectionBD.CreateCommand();
                comandoMySql.CommandText = "SELECT * from cliente"; 
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dataGridViewClientes.Rows.Clear();//FAZ LIMPAR A TABELA

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewClientes.Rows[0].Clone();//FAZ UM CAST E CLONA A LINHA DA TABELA
                    row.Cells[0].Value = reader.GetInt32(0);//ID
                    row.Cells[1].Value = reader.GetString(1);//NOME
                    row.Cells[2].Value = reader.GetString(2);//DATA DE NASCIMENTO
                    row.Cells[3].Value = reader.GetString(3);//CPF
                    row.Cells[4].Value = reader.GetString(4);//ENDEREÇO
                    row.Cells[5].Value = reader.GetString(5);//BAIRRO
                    row.Cells[6].Value = reader.GetString(6);//CEP
                    row.Cells[7].Value = reader.GetString(7);//CIDADE
                    row.Cells[8].Value = reader.GetString(8);//UF
                    dataGridViewClientes.Rows.Add(row);//ADICIONO A LINHA NA TABELA
                }

                connectionBD.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível conectar ao servidor!");
                Console.WriteLine(ex.Message);
            }
        }

        private void dataGridViewClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxId.Text = dataGridViewClientes.SelectedCells[0].Value.ToString();
            textNome.Text = dataGridViewClientes.SelectedCells[1].Value.ToString();
            dateNascimento.Text = dataGridViewClientes.SelectedCells[2].Value.ToString();
            textCpf.Text = dataGridViewClientes.SelectedCells[3].Value.ToString();
            textEndereco.Text = dataGridViewClientes.SelectedCells[4].Value.ToString();
            textBairro.Text = dataGridViewClientes.SelectedCells[5].Value.ToString();
            textCep.Text = dataGridViewClientes.SelectedCells[6].Value.ToString();
            textCidade.Text = dataGridViewClientes.SelectedCells[7].Value.ToString();
            comboBoxUf.SelectedItem = dataGridViewClientes.SelectedCells[8].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridViewClientes.SelectedCells[0].Value.ToString() != "")
            {
                deletaCliente(Convert.ToInt32(dataGridViewClientes.SelectedCells[0].Value.ToString()));
            }
            else
            {
                MessageBox.Show("Selecione um cliente!");
            }
        }

        private void deletaCliente(int id)
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "alugueis";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";

            MySqlConnection connectionBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                connectionBD.Open(); //Abre a conexão com o banco
                //MessageBox.Show("Conexão Aberta!");

                MySqlCommand comandoMySql = connectionBD.CreateCommand(); //Crio um comando SQL
                comandoMySql.CommandText = "DELETE FROM cliente WHERE id = '" + id + "'";
                comandoMySql.ExecuteNonQuery();

                connectionBD.Close(); // Fecho a conexão com o banco
                MessageBox.Show("Deletado com sucesso"); //Exibo mensagem de aviso
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível conectar ao servidor!");
                Console.WriteLine(ex.Message);
            }

            atualizarGridClientes();
        }
    }
    
}
