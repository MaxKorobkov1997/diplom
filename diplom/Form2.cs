using diplom.ta_ble;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace diplom
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Static.user != "Гость")
                {
                    using (var context = new DBpodkl())
                    {
                        var stugents = new Student()
                        {
                            Name = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text
                        };
                        context.Students.Add(stugents);
                        context.SaveChanges();
                        otkritie();
                    }
                }
                else
                    MessageBox.Show("Пожалуйста войдите в акаунт");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
                otkritie();
            }
        }

        private void otkritie()
        {
            try
            {
                dataGridView1.Columns.Clear();
                using (var Joorn = new DBpodkl())
                    dataGridView1.DataSource = Joorn.Students.Select(e => new { e.Id, e.Name}).ToList();
                DataGridViewButtonColumn newColumn = new DataGridViewButtonColumn();
                newColumn.HeaderText = "Новый столбец"; // Заголовок
                newColumn.Name = "newColumn"; // Название столбца
                newColumn.Text = "Удалить";
                newColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(newColumn); // Добавляем столбец к DataGridView
                dataGridView1.Columns[1].HeaderText = "Фио";
                dataGridView1.Columns[0].Width = 40;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (MessageBox.Show("Удалить эту строку " + a, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    using (var context = new DBpodkl())
                    {
                        var users = context.Students.Where(o => o.Id == a).FirstOrDefault();
                        context.Students.Remove(users);
                        context.SaveChanges();
                        if (MessageBox.Show("Удалить эту строку " + a, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                            while (true)
                        {
                            var users1 = context.Jurnals.Where(o => o.Name == users.Name).FirstOrDefault();
                            if (users1 == null)
                                break;
                            context.Jurnals.Remove(users1);
                            context.SaveChanges();
                        }
                    }
                }
                otkritie();
            }
            catch
            {
                MessageBox.Show("Ни првильный вопрос");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            otkritie();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
