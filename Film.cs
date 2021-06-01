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
    public partial class Film : Form
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;
        private static string filmNameVar;

        private string big_combo;

        public Film()
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

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 zaglavna = new Form1();
            zaglavna.Show();
            this.Hide();
        }

        private void Film_Load(object sender, EventArgs e)
        {
            ShowInfo();
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            label3.BackColor = System.Drawing.Color.Transparent;
            label4.BackColor = System.Drawing.Color.Transparent;
            label5.BackColor = System.Drawing.Color.Transparent;
            label6.BackColor = System.Drawing.Color.Transparent;
            label7.BackColor = System.Drawing.Color.Transparent;
            actors();
            genres();
            dataGridView1.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        private void ShowInfo()
        {

            try
            {
                MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
                MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT moviedatabase.film.title,moviedatabase.film.year,moviedatabase.genre.name_genre,moviedatabase.film.director,moviedatabase.actor.name_actor,moviedatabase.film.description,moviedatabase.film.duration"+
                    " FROM moviedatabase.film JOIN moviedatabase.genre ON moviedatabase.genre.id_genre=moviedatabase.film.id_genre " +
                    $"JOIN moviedatabase.actor ON moviedatabase.actor.id_actor=moviedatabase.film.id_actor", connection);

                connection.Open();
                DataSet ds = new DataSet();
                adapter.Fill(ds, "film");

                dataGridView1.DataSource = ds.Tables["film"];
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dataGridView1.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
         
        }


        private void getData1()
        {
            DataTable dt = new DataTable();
            MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM moviedatabase.film WHERE title='" + textBox1.Text + "'", connection); // textBox1 ----> полето по което търсиш ID-то, например janr

            connection.Open();
            DataSet ds = new DataSet();
            // adapter.Fill(ds, "stoka");
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                filmNameVar = dt.Rows[0]["id_film"].ToString(); //textBox3 -> там където искаш да се възуализира ID-то / или го съхраняваш в променлива (стринг е) ---- kod_na_stoka -> ID колоната в БД (при вас може да е janr_id)

            }
            connection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

                if (dataGridView1.SelectedRows.Count > 0)
                {
                    string title = dataGridView1.SelectedRows[0].Cells[0].Value + string.Empty;
                    string year = dataGridView1.SelectedRows[0].Cells[1].Value + string.Empty;
                    //string avtor = dataGridView1.SelectedRows[0].Cells[2].Value + string.Empty;

                    string genre = comboBox1.SelectedText = dataGridView1.CurrentRow.Cells[2].Value.ToString();

                    string director = dataGridView1.SelectedRows[0].Cells[3].Value + string.Empty;
                    // string izdatelstvo = dataGridView1.SelectedRows[0].Cells[4].Value + string.Empty;
                    string actor = comboBox1.SelectedText = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    string description = dataGridView1.SelectedRows[0].Cells[5].Value + string.Empty;
                    string duration = dataGridView1.SelectedRows[0].Cells[6].Value + string.Empty;

                // string janr = dataGridView1.SelectedRows[0].Cells[5].Value + string.Empty;

                    textBox1.Text = title;
                    textBox2.Text = year;
                    comboBox1.Text = genre;
                    textBox3.Text = director;
                    comboBox2.Text = actor;
                    textBox4.Text = description;
                    textBox5.Text = duration;
            }

                getData1();
        }

        private void actors()
        {
            string constring = "datasource=localhost;port=3306;username=root;password=";

            string Query = "SELECT * FROM moviedatabase.actor";

            MySqlConnection conDatabase = new MySqlConnection(constring);

            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDatabase);
            MySqlDataReader myReader;

            try
            {
                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();
                while (myReader.Read())
                {
                    string sname = myReader.GetString("name_actor");
                    comboBox2.Items.Add(sname);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void genres()
        {
            string constring = "datasource=localhost;port=3306;username=root;password=";

            string Query = "SELECT * FROM moviedatabase.genre";

            MySqlConnection conDatabase = new MySqlConnection(constring);

            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDatabase);
            MySqlDataReader myReader;

            try
            {
                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();
                while (myReader.Read())
                {
                    string sname = myReader.GetString("name_genre");
                    comboBox1.Items.Add(sname);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private string getActorID(string actor)
        {
            MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");

            MySqlDataReader reader = null;

            string SelectedRowID = $"SELECT id_actor FROM moviedatabase.actor WHERE moviedatabase.actor.name_actor='" + actor + "'";
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
            return SelectedRowID;
        }

        private string getGenreID(string genre)
        {
            MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");

            MySqlDataReader reader = null;

            //взимаме ID-то на елемента, който искаме да изтрием
            string SelectedRowID = $"SELECT id_genre FROM moviedatabase.genre WHERE moviedatabase.genre.name_genre='" + genre + "'";
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
            return SelectedRowID;
        }

        //INSERT
        private void button1_Click(object sender, EventArgs e)
        {

            String genre = comboBox1.Text;//жанр
            String leadActor = comboBox2.Text;//главен актьор

            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedItem == null || textBox3.Text == "" || comboBox2.SelectedItem == null || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please fill all the fields");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");

                string genreID = getGenreID(genre);
                string actorID = getActorID(leadActor);
                
                string insertQueary = $"INSERT INTO moviedatabase.film(title,year,id_genre,director,id_actor,description,duration) VALUES('{textBox1.Text}','{textBox2.Text}','{genreID}','{textBox3.Text}','{actorID}','{textBox4.Text}','{textBox5.Text}')"; // int id

                connection.Open();
                MySqlCommand command = new MySqlCommand(insertQueary, connection);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                connection.Close();
                ShowInfo();
            }

        }


        private string get_Combo(string little_combo)
        {
            DataTable dt = new DataTable();
            MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM moviedatabase.genre WHERE name_genre='" + little_combo + "'", connection); // textBox2 ----> полето по което търсиш ID-то, например janr

            connection.Open();
            DataSet ds = new DataSet();
            // adapter.Fill(ds, "stoka");
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                big_combo = dt.Rows[0]["id_genre"].ToString(); //textBox3 -> там където искаш да се възуализира ID-то / или го съхраняваш в променлива (стринг е) ---- kod_na_stoka -> ID колоната в БД (при вас може да е janr_id)

            }
            connection.Close();
            return big_combo;
        }

        private void button2_Click(object sender, EventArgs e)
        {


            String genre = comboBox1.Text;//жанр
            String leadActor = comboBox2.Text;//главен актьор

            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.SelectedItem == null || textBox3.Text == "" || comboBox2.SelectedItem == null || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please fill all the fields");
            }
            else
            {
                string genreID = getGenreID(genre);
                string actorID = getActorID(leadActor);

                MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");

                string updateQuery = "UPDATE moviedatabase.film SET film.title='" + textBox1.Text + "',film.year='" + textBox2.Text + "',film.id_genre= '" + genreID + "',film.director='" + textBox3.Text + "',film.id_actor='" + actorID + "',film.description= '" + textBox4.Text + "', film.duration= '" + textBox5.Text + "' WHERE film.id_film=" + filmNameVar;
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
                        MessageBox.Show("DATA NOT UPDATED");
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

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == ""|| textBox2.Text == ""|| comboBox1.SelectedItem == null || textBox3.Text == "" || comboBox2.SelectedItem == null || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Please fill all the fields");
                }
            else
            {

                MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");

                try
                {
                    string deleteQuery = "DELETE FROM moviedatabase.film WHERE film.id_film= " + filmNameVar;
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(deleteQuery, connection);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Film DELETED");
                    }
                    else
                    {
                        MessageBox.Show("Film NOT DELETED");
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
