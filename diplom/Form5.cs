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
        Form1 form1;
        public Form5(Form1 owner)
        {
            InitializeComponent();
            form1 = owner;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            avtohis();
        }

        private void avtohis()
        {
            try
            {
                using (var context = new DBpodkl())
                {
                    var users = context.Users.Where(o => o.Login == textBox1.Text && o.Password == textBox2.Text);
                    if (users != null)
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
