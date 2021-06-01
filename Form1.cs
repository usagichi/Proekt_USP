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
    public partial class Form1 : Form
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;

        public Form1()
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

        private void Form1_Load(object sender, EventArgs e)
        {
         

            
        }

        

        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Genre genreForm = new Genre();
            genreForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Film filmForm = new Film();
            filmForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Actor actorForm = new Actor();
            actorForm.Show();
            this.Hide();
        }
    }
}
