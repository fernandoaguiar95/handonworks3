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
    public partial class Alugueis : Form
    {
        public Alugueis()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void veículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Veiculos form2 = new Veiculos();
            this.Hide();
            form2.Show();
        }

        private void inícioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Alugueis form1 = new Alugueis();

            this.Hide();
            form1.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clientes form3 = new Clientes();

            this.Hide();
            form3.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cadastraAluguel()
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
                comandoMySql.CommandText = "INSERT INTO alugueis (cpf_cliente, placa_veiculo, km_atual, km_final, data_retirada, data_entrega, situacao) " +
                    "VALUES('" + textBoxCpf.Text + "', '" + textBoxPlaca.Text.ToUpper() + "', '" + textBoxKmAtual.Text + "', '" + textBoxKmFinal.Text + "','" + dateTimePickerRetirada.Text + "', '" + dateTimePickerEntrega.Text + "', '" + comboBoxSituacao.Text + "')";
                comandoMySql.ExecuteNonQuery();

                connectionBD.Close(); // Fecho a conexão com o banco
                MessageBox.Show("Inserido com sucesso"); //Exibo mensagem de aviso
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível conectar ao servidor!");
                Console.WriteLine(ex.Message);
            }

            atualizaGridAlugueis();
            textBoxId.Text = "";
            textBoxCpf.Text = "";
            textBoxPlaca.Text = "";
            textBoxKmAtual.Text = "";
            textBoxKmFinal.Text = "";
            dateTimePickerRetirada.Text = "";
            dateTimePickerEntrega.Text = "";
            comboBoxSituacao.ResetText();

        }

        private void atualizaCadastro(int id)
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
                comandoMySql.CommandText = "UPDATE alugueis SET cpf_cliente = '" + textBoxCpf.Text + "', placa_veiculo = '" + textBoxPlaca.Text.ToUpper() + "', km_atual = '" + textBoxKmAtual.Text + "', km_final = '" + textBoxKmFinal.Text + "', data_retirada = '" + dateTimePickerRetirada.Text + "', data_entrega = '" + dateTimePickerEntrega.Text + "', situacao = '" + comboBoxSituacao.Text + "' WHERE id = '" + id +"'";
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

            atualizaGridAlugueis();
            textBoxId.Text = "";
            textBoxCpf.Text = "";
            textBoxPlaca.Text = "";
            textBoxKmAtual.Text = "";
            textBoxKmFinal.Text = "";
            dateTimePickerRetirada.Text = "";
            dateTimePickerEntrega.Text = "";
            comboBoxSituacao.ResetText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxId.Text != "")
            {
                atualizaCadastro(Convert.ToInt32(textBoxId.Text)); 
            }
            else
            {
                cadastraAluguel();
            }
        }

        private void Alugueis_Load(object sender, EventArgs e)
        {
            atualizaGridAlugueis();
        }

        public void atualizaGridAlugueis()
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
                comandoMySql.CommandText = "SELECT * from alugueis"; //Traz todo mundo da tabela autor
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dataGridViewAlugueis.Rows.Clear();//FAZ LIMPAR A TABELA

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewAlugueis.Rows[0].Clone();//FAZ UM CAST E CLONA A LINHA DA TABELA
                    row.Cells[0].Value = reader.GetInt32(0);//ID
                    row.Cells[1].Value = reader.GetString(1);//CPF

                    try
                    {
                        string cpf_cliente = reader.GetString(1);
                        MySqlConnection connectionCliente = new MySqlConnection(conexaoBD.ToString());
                        connectionCliente.Open();

                        MySqlCommand comandoCliente = connectionCliente.CreateCommand();
                        comandoCliente.CommandText = "select nome from cliente where cpf = ?cpf_cliente";
                        comandoCliente.Parameters.AddWithValue("?cpf_cliente", cpf_cliente);
                        MySqlDataReader cliente = comandoCliente.ExecuteReader();
                        while (cliente.Read())
                        {
                            row.Cells[2].Value = cliente.GetString(0);//NOME
                        }

                        connectionCliente.Close();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível conectar ao servidor!");
                        Console.WriteLine(ex.Message);
                    }

                    try
                    {
                        string placa_veiculo = reader.GetString(2);
                        MySqlConnection connectionVeiculo = new MySqlConnection(conexaoBD.ToString());
                        connectionVeiculo.Open();

                        MySqlCommand comandoVeiculo = connectionVeiculo.CreateCommand();
                        comandoVeiculo.CommandText = "select modelo from veiculos where placa = ?placa_veiculo";
                        comandoVeiculo.Parameters.AddWithValue("?placa_veiculo", placa_veiculo);
                        MySqlDataReader veiculo = comandoVeiculo.ExecuteReader();
                        while (veiculo.Read())
                        {
                            row.Cells[3].Value = veiculo.GetString(0);//PLACA
                        }

                        connectionVeiculo.Close();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não foi possível conectar ao servidor!");
                        Console.WriteLine(ex.Message);
                    }

                    row.Cells[4].Value = reader.GetString(2);
                    row.Cells[5].Value = reader.GetString(3);
                    row.Cells[6].Value = reader.GetString(4);
                    row.Cells[7].Value = reader.GetString(5);
                    row.Cells[8].Value = reader.GetString(6);
                    row.Cells[9].Value = reader.GetString(7);
                    dataGridViewAlugueis.Rows.Add(row);
                }

                connectionBD.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível conectar ao servidor!");
                Console.WriteLine(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxId.Text = dataGridViewAlugueis.SelectedCells[0].Value.ToString();
            textBoxCpf.Text = dataGridViewAlugueis.SelectedCells[1].Value.ToString();
            textBoxPlaca.Text = dataGridViewAlugueis.SelectedCells[4].Value.ToString();
            textBoxKmAtual.Text = dataGridViewAlugueis.SelectedCells[5].Value.ToString();
            textBoxKmFinal.Text = dataGridViewAlugueis.SelectedCells[6].Value.ToString();
            dateTimePickerRetirada.Text = dataGridViewAlugueis.SelectedCells[7].Value.ToString();
            dateTimePickerEntrega.Text = dataGridViewAlugueis.SelectedCells[8].Value.ToString();
            comboBoxSituacao.Text = dataGridViewAlugueis.SelectedCells[9].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewAlugueis.SelectedCells[0].Value.ToString() != "")
            {
                deletaAluguel(Convert.ToInt32(dataGridViewAlugueis.SelectedCells[0].Value.ToString()));
            }
            else
            {
                MessageBox.Show("Selecione um aluguel!");
            }
        }

        private void deletaAluguel(int id)
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
                comandoMySql.CommandText = "DELETE FROM alugueis WHERE id = '" + id + "'";
                comandoMySql.ExecuteNonQuery();

                connectionBD.Close(); // Fecho a conexão com o banco
                MessageBox.Show("Deletado com sucesso"); //Exibo mensagem de aviso
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível conectar ao servidor!");
                Console.WriteLine(ex.Message);
            }

            atualizaGridAlugueis();
        }
    }
}
