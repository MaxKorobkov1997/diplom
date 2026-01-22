using diplom.Database_management;
using System;
using System.Collections;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
                        otkritie1();
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

        private void otkritie1()
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = otkritie_tb.Otk_jurnal();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Enabled = false;

            Form2 f = new Form2();
            f.FormClosed += SecondForm2_FormClosed;
            f.Show();
        }

        private void SecondForm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Активируем главную форму обратно
            Enabled = true;

            // Отписываемся от события
            Form2 secondForm = (Form2)sender;
            secondForm.FormClosed -= SecondForm2_FormClosed;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Enabled = false;

            Form3 f = new Form3();
            f.FormClosed += SecondForm3_FormClosed;
            f.Show();
        }

        private void SecondForm3_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Активируем главную форму обратно
            Enabled = true;

            // Отписываемся от события
            Form3 secondForm = (Form3)sender;
            secondForm.FormClosed -= SecondForm3_FormClosed;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Enabled = false;

            Form4 f = new Form4();
            f.FormClosed += SecondForm4_FormClosed;
            f.Show();
        }

        private void SecondForm4_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Активируем главную форму обратно
            Enabled = true;

            // Отписываемся от события
            Form4 secondForm = (Form4)sender;
            secondForm.FormClosed -= SecondForm4_FormClosed;
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
                otkritie1();
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
            otkritie1();
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
            otkritie1();
            label4.Text = Static.user;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Enabled = false;

            Form5 f = new Form5(this);
            f.FormClosed += SecondForm5_FormClosed;
            f.Show();
        }

        private void SecondForm5_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Активируем главную форму обратно
            Enabled = true;

            // Отписываемся от события
            Form5 secondForm = (Form5)sender;
            secondForm.FormClosed -= SecondForm4_FormClosed;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            SearchAndHighlightRows(textBox3.Text);
        }

        private void SearchAndHighlightRows(string searchText)
        {
            dataGridView1.CurrentCell = null;
            // 1. Предварительная очистка выделения и проверка входных данных
            // Используем foreach для более чистого синтаксиса
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = false;
            }

            // Если строка поиска пуста, нет смысла продолжать
            if (string.IsNullOrEmpty(searchText))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    row.Visible =true;
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
                    row.Visible = true;
                }
                else
                {
                        row.Visible = false;
                }
            }
        }
    }
}