using diplom.Database_management;
using diplom.ta_ble;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace diplom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Static.user != "Гость")
                {
                    if (comboBox1.Text == "")
                        MessageBox.Show("Выберите имя");
                    if (comboBox2.Text == "")
                        MessageBox.Show("Выберите факультет");
                    if (comboBox3.Text == "")
                        MessageBox.Show("Выберите социальную группу");
                    else
                    {
                        add_bd.Add_jurnal(comboBox1.Text, comboBox2.Text, comboBox3.Text);
                        otkritie();
                    }
                }
                else
                    MessageBox.Show("Пожалуйста войдите в акаунт");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void otkritie()
        {
            try
            {
                dataGridView1.Columns.Clear();
                using (DBpodkl Joorn = new DBpodkl())
                    dataGridView1.DataSource = Joorn.Jurnals.Select(e => new { e.Id, e.Name,e.Id_Neme, e.Fakultet,e.Id_Fakultet, e.VidGr,e.Id_VidGr }).ToList();
                DataGridViewButtonColumn newColumn = new DataGridViewButtonColumn();
                newColumn.HeaderText = "Новый столбец"; // Заголовок
                newColumn.Name = "newColumn"; // Название столбца
                newColumn.Text = "Удалить";
                newColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(newColumn); // Добавляем столбец к DataGridView
                dataGridView1.Columns[1].HeaderText = "Имя";
                dataGridView1.Columns[2].HeaderText = "id";
                dataGridView1.Columns[3].HeaderText = "Факультет";
                dataGridView1.Columns[4].HeaderText = "Id факультета";
                dataGridView1.Columns[5].HeaderText = "Группа";
                dataGridView1.Columns[6].HeaderText = "Id ввида группы";
                dataGridView1.Columns[0].Width = 40;
            }
            catch
            {
                if (MessageBox.Show("Создать пользоватедя Login = \"maks\",Password = \"123\",", "Ошибка",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                    using (DBpodkl context = new DBpodkl())
                    {
                        User users = new User()
                        {
                            Login = "maks",
                            Password = "123",
                        };
                        context.Users.Add(users);
                        context.SaveChanges();
                    }
                else
                    Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            new Form3().Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Form4().Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (MessageBox.Show("Удалить эту строку " + a, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    Delit.Delit_jurnal(a);
                }
                otkritie();
            }
            catch(Exception ex)
            {
              MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = Static.user;
            Combobox("[dbo].[Students] ");
            Combobox("[dbo].[Fakultets] ");
            Combobox("[dbo].[Vids] ");
            otkritie();
            dataGridView1.Font = new Font("Microsoft Sans Serif", 14);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Combobox( string NameT)
        {
            try
            {
                comboBox3.Items.Clear();
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                using (DBpodkl Joorn = new DBpodkl())
                {
                    var lengths = Joorn.Vids.Select(e => new { e.Id, e.vid }).ToList();
                    foreach (var length in lengths)
                    {
                        comboBox3.Items.Add(length.vid.ToString());
                    }                    
                    var lengths1 = Joorn.Fakultets.Select(e => new { e.Fakultets }).ToList();
                    foreach (var length in lengths1)
                    {
                        comboBox2.Items.Add(length.Fakultets.ToString());
                    }
                    var lengths2 = Joorn.Students.Select(e => new { e.Name }).ToList();
                    foreach (var length in lengths2)
                    {
                        comboBox1.Items.Add(length.Name.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Combobox("[dbo].[Students] ");
            Combobox("[dbo].[Fakultets] ");
            Combobox("[dbo].[Vids] ");
            otkritie();
            label4.Text = Static.user;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new Form5(this).Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    dataGridView1.Rows[i].Selected = false;
            //    for (int j = 0; j < dataGridView1.Columns.Count; j++)
            //    {
            //        if (textBox3.Text != "")
            //        {
            //            if (dataGridView1.Rows[i].Cells[j].Value.ToString().IndexOf(textBox3.Text, StringComparison.OrdinalIgnoreCase) != -1)
            //            {
            //                dataGridView1.Rows[i].Selected = true;
            //                break;
            //            }
            //        }
            //    }
            //}
            SearchAndHighlightRows(textBox3.Text);
        }

        private void SearchAndHighlightRows(string searchText)
        {
            // 1. Предварительная очистка выделения и проверка входных данных
            // Используем foreach для более чистого синтаксиса
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = false;
            }

            // Если строка поиска пуста, нет смысла продолжать
            if (string.IsNullOrEmpty(searchText))
            {
                return;
            }

            // 2. Основной цикл поиска
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Пропускаем последнюю "новую" строку, если она отображается пользователю
                if (row.IsNewRow) continue;

                bool foundInRow = false;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    // 3. Улучшенная проверка: убеждаемся, что значение ячейки не равно null
                    if (cell.Value != null)
                    {
                        // 4. Используем ToLowerInvariant() для сравнения без учета регистра
                        // (это более эффективно, чем создание нового StringComparison)
                        string cellValue = cell.Value.ToString();
                        if (cellValue.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            foundInRow = true;
                            break; // Нашли совпадение в этой строке, можно идти к следующей строке
                        }
                    }
                }

                if (foundInRow)
                {
                    row.Selected = true;
                    // Дополнительно: можно прокрутить к первой найденной строке
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                }
            }
        }
    }
}