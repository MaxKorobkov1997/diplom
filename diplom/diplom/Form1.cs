using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using diplom.ta_ble;
using System.Data.Common;

namespace diplom
{
    public partial class Form1 : Form
    {
        private SqlConnection Otkr = null;
        private DataSet Dataset = null;
        private SqlDataAdapter Data = null;
        private SqlDataAdapter Data1 = null;
        private SqlCommandBuilder SqlBilder = null;
        private string Bd_Naim = "Jurnals";
        private string Key = "DBstr";
        int Delit = 4;
        public bool admin = false;

        public Form1()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Static.user != "Войтти")
                {
                    if (comboBox1.Text == "")
                        MessageBox.Show("Выберите имя");
                    if (comboBox2.Text == "")
                        MessageBox.Show("Выберите факультет");
                    if (comboBox3.Text == "")
                        MessageBox.Show("Выберите социальную группу");
                    else
                    {
                        using (var context = new DBpodkl())
                        {
                            var Joorn = new Jurnal()
                            {
                                Name = comboBox1.Text,
                                Fakultet = comboBox2.Text,
                                VidGr = comboBox3.Text
                            };
                            context.Jurnals.Add(Joorn);
                            context.SaveChanges();
                        }
                        comboBox1.Text = "";
                        otkrit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void otkritie(bool zak)
        {
            try
            {
                if (zak==false)
                    Otkr.Close();
                Otkr.Open();
                Data = new SqlDataAdapter("SELECT Id,Name,Fakultet,VidGr,'Delete' AS [Удалить] FROM " + Bd_Naim, Otkr);
                
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
                zak=true;
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
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var myForm = new Form2();
            myForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var myForm = new Form3();
            myForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var myForm = new Form4();
            myForm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == Delit)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[Delit].Value.ToString();
                    if (task == "Delete" && label4.Text != "user")
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
            catch(Exception ex)
            {
              MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = "user";
            Otkr = new SqlConnection(ConfigurationManager.ConnectionStrings[Key].ConnectionString);
            Combobox("[dbo].[Students] ");
            Combobox("[dbo].[Fakultets] ");
            Combobox("[dbo].[Vids] ");
            otkritie(true);
        }

        private void Combobox( string NameT)
        {
            try
            {
                string sq = "SELECT * FROM "+NameT;
                using (SqlCommand cmd = new SqlCommand(sq, Otkr))
                {
                    cmd.CommandType = CommandType.Text;
                    DataTable table = new DataTable();
                    Data1 = new SqlDataAdapter(cmd);
                    Data1.Fill(table);
                    switch (NameT)
                    {
                        case "[dbo].[Vids] ":
                            comboBox3.DisplayMember = "vid";
                            comboBox3.ValueMember = "Id";
                            comboBox3.DataSource = table;
                            break;
                        case "[dbo].[Fakultets] ":
                            comboBox2.DisplayMember = "Fakultets";
                            comboBox2.ValueMember = "Id";
                            comboBox2.DataSource = table;
                            break;
                        case "[dbo].[Students] ":
                            comboBox1.DisplayMember = "Name";
                            comboBox1.ValueMember = "Id";
                            comboBox1.DataSource = table;
                            break;
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
            otkritie(false);
            label4.Text = Static.user;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            poisk("Name");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            poisk("Fakultet");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            poisk("VidGr");
        }

        private void poisk(string kolonka)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"{kolonka} LIKE '%{textBox3.Text}%'";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var myForm = new Form5();
            myForm.Show();
        }
    }
}