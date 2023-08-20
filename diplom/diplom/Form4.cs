using diplom.ta_ble;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace diplom
{
    public partial class Form4 : Form
    {
        private string Bd_Naim = "Vids";
        int Delit = 2;
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new DBpodkl())
                {
                    var vid = new Vid()
                    {
                        vid = textBox1.Text
                    };
                    context.Vids.Add(vid);
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
                Static.Otkr4 = new SqlConnection(ConfigurationManager.ConnectionStrings[Static.Key].ConnectionString);
                Static.Otkr4.Open();
                Static.Data4 = new SqlDataAdapter("SELECT Id,vid,'Delete' AS [Удалить] FROM " + Bd_Naim, Static.Otkr4);
                Static.SqlBilder4 = new SqlCommandBuilder(Static.Data4);
                Static.SqlBilder4.GetDeleteCommand();
                Static.Dataset4 = new DataSet();
                Static.Data4.Fill(Static.Dataset4, Bd_Naim);
                dataGridView1.DataSource = Static.Dataset4.Tables[Bd_Naim];
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
                Static.Dataset4.Tables[Bd_Naim].Clear();
                Static.Data4.Fill(Static.Dataset4, Bd_Naim);
                dataGridView1.DataSource = Static.Dataset4.Tables[Bd_Naim];
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
                            Static.Dataset4.Tables[Bd_Naim].Rows[rowIndex].Delete();
                            Static.Data4.Update(Static.Dataset4, Bd_Naim);
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

        private void Form4_Load(object sender, EventArgs e)
        {
            otkritie();
        }
    }
}
