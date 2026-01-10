using diplom.ta_ble;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace diplom
{
    public partial class Form4 : Form
    {
        public Form4()
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
                        var vid = new Vid()
                        {
                            vid = textBox1.Text
                        };
                        context.Vids.Add(vid);
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
                    dataGridView1.DataSource = Joorn.Vids.Select(e => new { e.Id, e.vid }).ToList();
                DataGridViewButtonColumn newColumn = new DataGridViewButtonColumn();
                newColumn.HeaderText = "Новый столбец"; // Заголовок
                newColumn.Name = "newColumn"; // Название столбца
                newColumn.Text = "Удалить";
                newColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(newColumn); // Добавляем столбец к DataGridView
                dataGridView1.Columns[1].HeaderText = "Группа";
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
                        var users = context.Vids.Where(o => o.Id == a).FirstOrDefault();
                        context.Vids.Remove(users);
                        context.SaveChanges();
                        if (MessageBox.Show("Удалить эту строку " + a, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                            while (true)
                            {
                                var users1 = context.Jurnals.Where(o => o.Fakultet == users.vid).FirstOrDefault();
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

        private void Form4_Load(object sender, EventArgs e)
        {
            otkritie();
        }
    }
}
