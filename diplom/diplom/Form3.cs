using diplom.ta_ble;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace diplom
{
    public partial class Form3 : Form
    {
        private SqlConnection Otkr = null;
        private DataSet Dataset = null;
        private SqlDataAdapter Data = null;
        private SqlCommandBuilder SqlBilder = null;
        private string Bd_Naim = "Fakultets";
        private string Key = "DBstr";
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
                Otkr = new SqlConnection(ConfigurationManager.ConnectionStrings[Key].ConnectionString);
                Otkr.Open();
                Data = new SqlDataAdapter("SELECT Id,Fakultets,'Delete' AS [Удалить] FROM " + Bd_Naim, Otkr);
                SqlBilder = new SqlCommandBuilder(Data);
                SqlBilder.GetDeleteCommand();
                Dataset = new DataSet();
                Data.Fill(Dataset, Bd_Naim);
                dataGridView1.DataSource = Dataset.Tables[Bd_Naim];
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
                Dataset.Tables[Bd_Naim].Clear();
                Data.Fill(Dataset, Bd_Naim);
                dataGridView1.DataSource = Dataset.Tables[Bd_Naim];
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
                            Dataset.Tables[Bd_Naim].Rows[rowIndex].Delete();
                            Data.Update(Dataset, Bd_Naim);
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
