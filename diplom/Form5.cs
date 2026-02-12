using diplom.ta_ble;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace diplom
{
    public partial class Form5 : Form
    {
        Form1 form1;
        public Form5(Form1 owner)
        {
            InitializeComponent();
            form1 = owner;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var context = new DBpodkl())
            {
                var users = context.Users.Where(o => o.Login == textBox1.Text && o.Password == textBox2.Text).ToList();
                if (users.Count() > 0)
                {
                    MessageBox.Show("Вы вошли");
                    Static.user = textBox1.Text;
                    form1.label4.Text = Static.user;
                    Close();
                }
                else
                    MessageBox.Show("Такого пользоваля нет", "Ошибка", MessageBoxButtons.OK);
            }
            
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            using (var context = new DBpodkl())
            {
                var users = context.Vids.Select(o => new { o.Id, o.vid }).ToList();
                if (users.Count() < 1) button1.Enabled = true;
                else button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (DBpodkl context = new DBpodkl())
            {
                User users = new User()
                {
                    Login = textBox1.Text,
                    Password = textBox2.Text,
                };
                context.Users.Add(users);
                context.SaveChanges();
            }
        }
    }
}
