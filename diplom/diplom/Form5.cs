using diplom.ta_ble;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace diplom
{
    public partial class Form5 : Form
    {
        SqlDataAdapter adapter = null;
        DataTable Table = null;
        private string Key = "DBstr";
        private SqlConnection Otkr = null;
        SqlCommand command = null;
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var context = new DBpodkl())
            {
                var Users = new User()
                {
                    Login = textBox1.Text,
                    Password = textBox2.Text,
                };
                context.Users.Add(Users);
                context.SaveChanges();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            avtohis();
        }

        private void avtohis()
        {
            string prov = $"select * from Users where Login = @Username and Password = @Password";
            command = new SqlCommand(prov, Otkr);
            command.Parameters.AddWithValue("@Username", textBox1.Text);
            command.Parameters.AddWithValue("@Password", textBox2.Text);
            adapter = new SqlDataAdapter(command);
            Table = new DataTable();
            adapter.Fill(Table);
            if (Table.Rows.Count == 1)
            {
                MessageBox.Show("Вы вошли", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Static.user = textBox1.Text;
            }
            else
                MessageBox.Show("Ошибка", "Ни правильно введен логин или пароль", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Otkr = new SqlConnection(ConfigurationManager.ConnectionStrings[Key].ConnectionString);
        }
    }
}
