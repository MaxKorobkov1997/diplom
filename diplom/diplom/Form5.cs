using diplom.ta_ble;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace diplom
{
    public partial class Form5 : Form
    {
        
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
            Static.command5 = new SqlCommand(prov, Static.Otkr);
            Static.command5.Parameters.AddWithValue("@Username", textBox1.Text);
            Static.command5.Parameters.AddWithValue("@Password", textBox2.Text);
            Static.Adapter5 = new SqlDataAdapter(Static.command5);
            Static.Table5 = new DataTable();
            Static.Adapter5.Fill(Static.Table5);
            if (Static.Table5.Rows.Count == 1)
            {
                MessageBox.Show("Вы вошли", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Static.user = textBox1.Text;
            }
            else
                MessageBox.Show("Ошибка", "Ни правильно введен логин или пароль", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Static.Otkr = new SqlConnection(ConfigurationManager.ConnectionStrings[Static.Key].ConnectionString);
        }
    }
}
