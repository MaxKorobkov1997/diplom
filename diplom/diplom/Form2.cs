using diplom.ta_ble;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace diplom
{
    public partial class Form2 : Form
    {
        private string Bd_Naim = "Students";
        int Delit = 2;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new DBpodkl())
                {
                    var stugents = new Student()
                    {
                        Name = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text
                    };
                    context.Students.Add(stugents);
                    context.SaveChanges();
                    otkrit();
                }
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
                Static.Otkr = new SqlConnection(ConfigurationManager.ConnectionStrings[Static.Key].ConnectionString);
                Static.Otkr.Open();
                Static.Data2 = new SqlDataAdapter("SELECT Id,Name,'Delete' AS [Удалить] FROM " + Bd_Naim, Static.Otkr);
                Static.SqlBilder2 = new SqlCommandBuilder(Static.Data);
                Static.SqlBilder2.GetDeleteCommand();
                Static.Dataset2 = new DataSet();
                Static.Data2.Fill(Static.Dataset2, Bd_Naim);
                dataGridView1.DataSource = Static.Dataset2.Tables[Bd_Naim];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell Linkcel = new DataGridViewLinkCell();
                    dataGridView1[Delit, i] = Linkcel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void otkrit()
        {
            try
            {
                Static.Dataset2.Tables[Bd_Naim].Clear();
                Static.Data2.Fill(Static.Dataset, Bd_Naim);
                dataGridView1.DataSource = Static.Dataset2.Tables[Bd_Naim];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell Linkcel = new DataGridViewLinkCell();
                    dataGridView1[Delit, i] = Linkcel;
                }
            }
            catch 
            {
                otkritie();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == Delit)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[Delit].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                           DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView1.Rows.RemoveAt(rowIndex);
                            Static.Dataset2.Tables[Bd_Naim].Rows[rowIndex].Delete();
                            Static.Data2.Update(Static.Dataset, Bd_Naim);
                        }
                    }
                }
                otkrit();
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
    }
}
