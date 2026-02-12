using diplom.Database_management;
using diplom.ta_ble;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace diplom
{
    public partial class Form2 : Form
    {
        string pathpasp, pathsoclic, pasp, soclic;
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
                    string path;
                    path = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text;
                    add_bd.Add_student(path, pasp, soclic);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    try
                    {
                        File.Copy(pathpasp, "documents"+"/"+ path +"/"+ pasp);
                        if (pathsoclic!= "")
                            File.Copy(pathsoclic, "documents" + "/" + path +"/"+ soclic);
                    }
                    catch { }
                    otkritie();
                    
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
                dataGridView1.DataSource = otkritie_tb.otk_student();
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
                if (Static.user != "Гость")
                if (((DataGridView)sender).Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    int a = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    if (MessageBox.Show("Удалить эту строку " + a, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                        DialogResult.Yes)
                    {
                        Delit.Delit_student(a);
                        if (MessageBox.Show("Удалить эту строку " + a + " в таблице журнал", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                            while (true)
                            {
                                using (var context = new DBpodkl())
                                {
                                    var users1 = context.Jurnals.Where(o => o.Id_Neme == a).FirstOrDefault();
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

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.Font = new Font("Microsoft Sans Serif", 14);
            otkritie();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                soclic = Path.GetFileName(openFileDialog.FileName);
                pathsoclic = openFileDialog.FileName;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pasp = Path.GetFileName(openFileDialog.FileName);
                pathpasp = openFileDialog.FileName;
            }
        }
    }
}
