﻿using diplom.ta_ble;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace diplom
{
    public partial class Form2 : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
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
                    using (var context = new DBpodkl())
                    {
                        var stugents = new Student()
                        {
                            Name = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text
                        };
                        context.Students.Add(stugents);
                        context.SaveChanges();
                        otkritie();
                    }
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
                using (var Joorn = new DBpodkl())
                    dataGridView1.DataSource = Joorn.Students.Select(e => new { e.Id, e.Name}).ToList();
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
                int a = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (MessageBox.Show("Удалить эту строку " + a, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    using (var context = new DBpodkl())
                    {
                        var users = context.Students.Where(o => o.Id == a).FirstOrDefault();
                        context.Students.Remove(users);
                        context.SaveChanges();
                    }
                }
                otkritie();
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }
    }
}
