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

namespace MovieSearchSystem
{
    public partial class Actor : Form
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;
        private static string genreNameVar;

        public Actor()
        {
            server = "localhost";
            database = "moviedatabase";
            uid = "root";
            password = "";

            string connString;
            connString = $"SERVER={server}; DATABASE={database};UID={uid};PASSWORD={password};";
            conn = new MySqlConnection(connString);

            InitializeComponent();
        }

        private void Actor_Load(object sender, EventArgs e)
        {
            ShowInfo();
            label1.BackColor = System.Drawing.Color.Transparent;
            //label1.Size = new System.Drawing.Size(900, 26);
            //label1.Font = new Font("Arial", 20, FontStyle.Regular);
            dataGridView1.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please fill an actor name!");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");



                string insertQueary = $"INSERT INTO moviedatabase.actor(name_actor) VALUES('{textBox1.Text}')";

                connection.Open();
                MySqlCommand command = new MySqlCommand(insertQueary, connection);


                try
                {
                    command.ExecuteNonQuery();
                    //ShowInfo();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                connection.Close();
                ShowInfo();

            }
        }
            private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please fill in an actor!");
            }
            else
            {


                MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");

                string updateQuery = "UPDATE moviedatabase.actor SET actor.name_actor= '" + textBox1.Text + "' WHERE genre.id_actor=" + genreNameVar;

                connection.Open();
                try
                {
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("DATA UPDATED");
                    }
                    else
                    {
                        MessageBox.Show("Data NOT UPDATED");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
                ShowInfo();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 zaglavna = new Form1();
            zaglavna.Show();
            this.Hide();
        }

        private void ShowInfo()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
                MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT name_actor FROM moviedatabase.actor", connection);

                connection.Open();
                DataSet ds = new DataSet();
                adapter.Fill(ds, "actor");

                dataGridView1.DataSource = ds.Tables["actor"];
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dataGridView1.DefaultCellStyle.BackColor = Color.Yellow;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string ime = dataGridView1.SelectedRows[0].Cells[0].Value + string.Empty;
                //string telefon = dataGridView1.SelectedRows[0].Cells[2].Value + string.Empty;

                textBox1.Text = ime;
            }
            getData1();
        }

        private void getData1()
        {
            DataTable dt = new DataTable();
            MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM moviedatabase.actor WHERE name_actor='" + textBox1.Text + "'", connection); // textBox1 ----> полето по което търсиш ID-то, например janr

            connection.Open();
            DataSet ds = new DataSet();
            // adapter.Fill(ds, "stoka");
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                genreNameVar = dt.Rows[0]["id_actor"].ToString(); //textBox3 -> там където искаш да се възуализира ID-то / или го съхраняваш в променлива (стринг е) ---- kod_na_stoka -> ID колоната в БД (при вас може да е janr_id)

            }
            connection.Close();
        }

        private void deleteFilm(String actorID)
        {


            MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");

            try
            {

                string deleteQuery = "DELETE FROM moviedatabase.film WHERE film.id_actor='" + actorID + "'";
                connection.Open();
                MySqlCommand command = new MySqlCommand(deleteQuery, connection);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("ACTOR FROM FILMS DELETED");
                }
                else
                {
                    MessageBox.Show("ACTOR FROM FILMS NOT DELETED");
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*if (textBox1.Text == "")
            {
                MessageBox.Show("Please fill all the fields");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");

                try
                {
                    string deleteQuery = "DELETE FROM moviedatabase.actor WHERE actor.id_actor= " + genreNameVar;
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(deleteQuery, connection);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Actor DELETED");
                    }
                    else
                    {
                        MessageBox.Show("Actor NOT DELETED");
                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
                ShowInfo();

            }*/

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please fill all the fields");
            }
            else
            {

                MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");

                try
                {

                    MySqlDataReader reader = null;

                    //взимаме ID-то на елемента, който искаме да изтрием
                    string SelectedRowID = $"SELECT id_actor FROM moviedatabase.actor WHERE moviedatabase.actor.name_actor='" + textBox1.Text + "'";
                    MySqlCommand command2 = new MySqlCommand(SelectedRowID, connection);

                    connection.Close();

                    connection.Open();
                    reader = command2.ExecuteReader();
                    if (reader.Read())
                    {
                        //записваме в стринг ID-то
                        SelectedRowID = reader.GetValue(0).ToString();//works!!!
                    }

                    connection.Close();
                    connection.Open();
                    //трием по ID
                    string deleteQuery = "DELETE FROM moviedatabase.actor WHERE actor.id_actor= '" + SelectedRowID + "'";

                    MySqlCommand command = new MySqlCommand(deleteQuery, connection);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Actor DELETED");
                        deleteFilm(SelectedRowID);
                    }
                    else
                    {
                        MessageBox.Show("Actor NOT DELETED");
                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
                ShowInfo();
            }
        }
    }
}
