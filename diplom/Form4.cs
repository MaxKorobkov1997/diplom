using diplom.Database_management;
using System;
using System.Data;
using System.Drawing;
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
                    add_bd.Add_vid(textBox1.Text);
                    otkritie();
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
                dataGridView1.DataSource = otkritie_tb.otk_vidgr();
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
                if(Static.user != "Гость")
                if (((DataGridView)sender).Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    int a = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    if (MessageBox.Show("Удалить эту строку " + a, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                        DialogResult.Yes)
                    {
                        Delit.Delit_vidgr(a);
                        if (MessageBox.Show("Удалить эту строку " + a + " в таблице журнал", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                            while (true)
                            {
                                using (var context = new DBpodkl())
                                {
                                    var users1 = context.Jurnals.Where(o => o.Id_VidGr == a).FirstOrDefault();
                                    if (users1 == null)
                                        break;
                                    Delit.Delit_jurnal(users1.Id);
                                }
                            }

                    }
                    otkritie();
                }
            }
            catch
            {
                MessageBox.Show("Ни првильный вопрос");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView1.Font = new Font("Microsoft Sans Serif", 14);
            otkritie();
        }
    }
}
