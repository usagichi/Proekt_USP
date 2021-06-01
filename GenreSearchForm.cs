using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MovieSearchSystem
{
    public partial class GenreSearchForm : Form
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;
        private static string genreNameVar;

        public GenreSearchForm()
        {
            InitializeComponent();
        }
        
        private void GenreSearchForm_Load(object sender, EventArgs e)
        {
            label1.BackColor = System.Drawing.Color.Transparent;

            dataGridView1.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //back button
            Genre returnForm = new Genre();
            returnForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //search code
            try
            {
                MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
                


                MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT moviedatabase.film.title,moviedatabase.film.year FROM moviedatabase.genre  LEFT Join moviedatabase.film ON moviedatabase.film.id_genre=moviedatabase.genre.id_genre WHERE genre.name_genre='{textBox1.Text.Trim()}' ", connection);


                

               


                connection.Open();
                DataSet ds = new DataSet();
                adapter.Fill(ds, "film");
                dataGridView1.DataSource = ds.Tables["film"];

                dataGridView1.Columns[0].HeaderCell.Value = "Movie Name";
                dataGridView1.Columns[1].HeaderCell.Value = "Movie Year";

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dataGridView1.DefaultCellStyle.BackColor = Color.Yellow;
        }

        
    }
}
