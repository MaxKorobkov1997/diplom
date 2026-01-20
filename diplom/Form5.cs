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
            avtohis();
            Close();
        }

        private void avtohis()
        {
            try
            {
                using (var context = new DBpodkl())
                {
                    var users = context.Users.Where(o => o.Login == textBox1.Text && o.Password == textBox2.Text);
                    if (users.Count() == 1)
                    {
                        MessageBox.Show("Вы вошли");
                        Static.user = textBox1.Text;
                        form1.label4.Text =Static.user;
                    }
                    else
                        MessageBox.Show("Такого пользоваля нет", "Ошибка", MessageBoxButtons.OK);
                }
            }
            catch(Exception ex)  
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
        }
    }
}
