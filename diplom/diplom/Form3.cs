using diplom.ta_ble;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace diplom
{
    public partial class Form3 : Form
    {
        private string Bd_Naim = "Fakultets";
        int Delit = 2;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new DBpodkl())
                {
                    var fakultet = new Fakultet()
                    {
                        Fakultets = textBox1.Text
                    };
                    context.Fakultets.Add(fakultet);
                    context.SaveChanges();
                    otkrit();
                }
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
                Static.Otkr3 = new SqlConnection(ConfigurationManager.ConnectionStrings[Static.Key].ConnectionString);
                Static.Otkr3.Open();
                Static.Data3 = new SqlDataAdapter("SELECT Id,Fakultets,'Delete' AS [Удалить] FROM " + Bd_Naim, Static.Otkr);
                Static.SqlBilder3 = new SqlCommandBuilder(Static.Data);
                Static.SqlBilder3.GetDeleteCommand();
                Static.Dataset3 = new DataSet();
                Static.Data3.Fill(Static.Dataset3, Bd_Naim);
                dataGridView1.DataSource = Static.Dataset3.Tables[Bd_Naim];
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
                Static.Dataset3.Tables[Bd_Naim].Clear();
                Static.Data3.Fill(Static.Dataset3, Bd_Naim);
                dataGridView1.DataSource = Static.Dataset3.Tables[Bd_Naim];
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
                            Static.Dataset3.Tables[Bd_Naim].Rows[rowIndex].Delete();
                            Static.Data3.Update(Static.Dataset3, Bd_Naim);
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

        private void Form3_Load(object sender, EventArgs e)
        {
            otkritie();
        }
    }
}
