using diplom.ta_ble;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace diplom
{
    public partial class Form3 : Form
    {
        public Form3()
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
                        var fakultet = new Fakultet()
                        {
                            Fakultets = textBox1.Text
                        };
                        context.Fakultets.Add(fakultet);
                        context.SaveChanges();
                        otkritie();
                    }
                }
                else
                    MessageBox.Show("Пожалуйста войдите в акаунт");
            }
            catch
            {
                otkritie();
            }
        }
        private void otkritie()
        {
            try
            {
                dataGridView1.Columns.Clear();
                using (var Joorn = new DBpodkl())
                    dataGridView1.DataSource = Joorn.Fakultets.Select(e => new { e.Id, e.Fakultets }).ToList();
                DataGridViewButtonColumn newColumn = new DataGridViewButtonColumn();
                newColumn.HeaderText = "Новый столбец"; // Заголовок
                newColumn.Text = "Удалить";
                newColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(newColumn); // Добавляем столбец к DataGridView
                dataGridView1.Columns[1].HeaderText = "Факультет";
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
                        var users = context.Fakultets.Where(o => o.Id == a).FirstOrDefault();
                        context.Fakultets.Remove(users);
                        context.SaveChanges();
                        MessageBox.Show(users.Fakultets);
                        if (MessageBox.Show("Удалить эту строку " + a, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                        {
                            while (true) {
                                var users1 = context.Jurnals.Where(o => o.Fakultet == users.Fakultets).FirstOrDefault();
                                if (users1 == null)
                                    break;
                                context.Jurnals.Remove(users1);
                                context.SaveChanges();
                            }
                            
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

        private void Form3_Load(object sender, EventArgs e)
        {
            otkritie();
        }
    }
}
