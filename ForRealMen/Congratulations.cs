using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using DataAccess;

namespace ForRealMen
{
    public partial class Congratulations : Form
    {
        DataAccess.Pozdravleniya OutPutPozdr = new DataAccess.Pozdravleniya();

        public Congratulations()
        {
            InitializeComponent();
        }
        //Перестановка формы
        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void Congratulations_Load(object sender, EventArgs e)
        {
            //Добавляем врачей 
            dataGridView1.DataSource = OutPutPozdr.listPozdr();
            //Видимость полей в гриде
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;

            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 2;
            dataGridView1.Columns[1].FillWeight = 10;
            dataGridView1.Columns[2].FillWeight = 30;


            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Убираем заголовки строк
            //dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;


            btnEdit.Enabled = false;
            btnDel.Enabled = false;
        }

        private void Congratulations_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //Метод на очистку txtbox-ов
        private void ClearTxt()
        {
            textBox1.Text = "  Как вас зовут?";
            textBox2.Text = "  Оставьте свои поздравления";
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                OutPutPozdr.ConnOpen();
                OutPutPozdr.AddPozdr(textBox1.Text, textBox2.Text);
                MessageBox.Show(" Поздравление успешно добавлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Обновление  таблицы
                dataGridView1.DataSource = OutPutPozdr.listPozdr();
                ClearTxt();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавление  поздравления \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                OutPutPozdr.ConnClose();
            }
        }

        private void iconbtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconBtnMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void iconBtnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "  Как вас зовут?")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "  Как вас зовут?";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if(textBox2.Text == "  Оставьте свои поздравления")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "") 
            {
                textBox2.Text = "  Оставьте свои поздравления";
                textBox2.ForeColor = Color.Black;
            }
        }
    }
}
