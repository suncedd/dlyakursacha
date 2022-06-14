using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VeloMEN_project
{
    public partial class Forma : Form
    {
        public static string User = ""; // Переменная для опознавания пользователя в системе
        public Forma()
        {
            //// ИНИЦИАЛИЗАЦИЯ ////
            InitializeComponent();
            string conn = @"Data Source=PK306NEW-7\SQLEXPRESS;Initial Catalog=VeloMEN;Integrated Security=True"; // Строка подключения
            SqlFunctions.openConnection(conn); // Открытие подключения
            //SqlFunctions.closeConnection(); // Закрытие подключения
            
        }


        private void Forma_Load(object sender, EventArgs e)
        {
            // Код смещения таб контрола при запуске приложения
            System.Drawing.Size size = new System.Drawing.Size(1,1);
            LIST_interface.ItemSize = (size);
            Password.UseSystemPasswordChar = true;
        }
         // КОД КНОПКИ СКРЫТИЯ ПАРОЛЯ //
        private void Star_pass_Click(object sender, EventArgs e)
        {
            if(Password.UseSystemPasswordChar == true)
            {
                Password.UseSystemPasswordChar = false;
            }
            else
            {
                Password.UseSystemPasswordChar = true;
            }
        }
        // КОД РАБОТЫ С КОЛОР ДИАЛОГОМ //
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            string Red = ""+colorDialog1.Color.R;
            string Green = "" + colorDialog1.Color.G;
            string Blue = "" + colorDialog1.Color.B;
            string Argb = ""+ colorDialog1.Color.ToArgb();

            int convArgb = Convert.ToInt32(Argb);

            Color argbCOLOR = Color.FromArgb(convArgb);

            pictureBox4.BackColor = (argbCOLOR);
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            string Red = "" + colorDialog1.Color.R;
            string Green = "" + colorDialog1.Color.G;
            string Blue = "" + colorDialog1.Color.B;
            string Argb = "" + colorDialog1.Color.ToArgb();

            int convArgb = Convert.ToInt32(Argb);

            Color argbCOLOR = Color.FromArgb(convArgb);

            pictureBox7.BackColor = (argbCOLOR);
        }
        /////////////////////////////////////////////////////////

        // КОД ГЕНЕРАЦИИ КОДА ИЗ 4 СИМВОЛОВ ДЛЯ ВЕЛОТРАНСПОРТА //
        private void button19_Click(object sender, EventArgs e)
        {

            string abc = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890"; //набор символов
            int kol = 4; // кол-во символов
            string result = "";
            Random rnd = new Random();
            int lng = 36;
            for (int i = 0; i < kol; i++)
            {
                result += abc[rnd.Next(lng)];
            }
            string CODS = result;

            VeloS.marka = textBox6.Text;
            VeloS.model = textBox5.Text;
            VeloS.color = pictureBox7.BackColor.ToArgb().ToString();
            VeloS.stat = comboBox5.Text;
            VeloS.pricez = numericUpDown6.Value.ToString();
            VeloS.pricea = numericUpDown5.Value.ToString();
            VeloS.prices = numericUpDown4.Value.ToString();
            VeloS.foto = VeloS.foto;
            VeloS.code = result;

            if (VeloS.marka != "" & VeloS.model != "" & VeloS.color != "" & VeloS.stat != "" & VeloS.pricez != "" & VeloS.pricea != "" & VeloS.prices != "" & VeloS.foto != "" & VeloS.code != "")
            {
                SqlFunctions.AddVelo(VeloS.marka, VeloS.model, VeloS.color, VeloS.stat, VeloS.pricez, VeloS.pricea, VeloS.prices, VeloS.foto, VeloS.code);
                MessageBox.Show(
 "Велотранспорт успешно добавлен",
 "Внимание",
 MessageBoxButtons.OK,
 MessageBoxIcon.Information,
 MessageBoxDefaultButton.Button1);
                VeloS.marka = "";
                VeloS.model = "";
                VeloS.color = "";
                VeloS.stat = "";
                VeloS.pricez = "";
                VeloS.pricea = "";
                VeloS.prices = "";
                VeloS.code = "";
            }
            else
            {
                MessageBox.Show(
"Возможно вы ввели не все данные\n" +
"Попробуйте еще раз или обратитесь к системному администратору...",
"Ошибка",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1);
            }
            

        }
        ///////////////////////////////////////////////////////////////////////

        ////// Команды перехода по страницам //////
        private void Vhod_Click(object sender, EventArgs e)
        {
            try
            {
                if (Login.Text == "Direktor" & Password.Text == "VeloMen")
                {
                    MessageBox.Show(
"Вход разрешен",
"Добро пожаловать",
MessageBoxButtons.OK,
MessageBoxIcon.Information,
MessageBoxDefaultButton.Button1);
                    LIST_interface.SelectTab(Direktor_Menu);
                }
                else
                {
                    User = SqlFunctions.get($"SELECT ID_Кассира FROM Кассиры WHERE Логин = '{Login.Text}' AND Пароль = '{Password.Text}'");
                    if (User != "")
                    {
                        MessageBox.Show(
    "Вход разрешен",
    "Добро пожаловать",
    MessageBoxButtons.OK,
    MessageBoxIcon.Information,
    MessageBoxDefaultButton.Button1);
                        LIST_interface.SelectTab(Menu_Kassa);
                    }
                    else
                    {
                        MessageBox.Show(
    "Введены неверные данные!",
    "Ошибка",
    MessageBoxButtons.OK,
    MessageBoxIcon.Error,
    MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch
            {
                MessageBox.Show(
"Возможно вы не подключены к базе\n" +
"Обратитесь с Системному администратору!",
"Ошибка",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1);
            }
        }
                

        
        private void button1_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(ArendaMenu);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Password.Text = "";
            Login.Text = "";
            User = "";

            LIST_interface.SelectTab(Aftoriz_List);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Sklad);


            dataGridView1.DataSource = SqlFunctions.GetAllInventoryAsDataTable("SELECT * FROM Велотранспорт");

            // Смена ширины столбцов таблицы
            //dataGridView1.RowHeadersWidth = 220;
            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 165;
            DataGridViewColumn column2 = dataGridView1.Columns[1];
            column2.Width = 130;
            DataGridViewColumn column3 = dataGridView1.Columns[2];
            column3.Width = 180;
            DataGridViewColumn column4 = dataGridView1.Columns[3];
            column4.Width = 180;
            DataGridViewColumn column5 = dataGridView1.Columns[4];
            column5.Width = 180;
            DataGridViewColumn column6 = dataGridView1.Columns[5];
            column6.Width = 180;
            DataGridViewColumn column7 = dataGridView1.Columns[6];
            column7.Width = 210;
            DataGridViewColumn column8 = dataGridView1.Columns[7];
            column8.Width = 185;
            DataGridViewColumn column9 = dataGridView1.Columns[8];
            column9.Width = 180;
            DataGridViewColumn column10 = dataGridView1.Columns[9];
            column10.Width = 180;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Menu_Kassa);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(VeloADD);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                VeloS.id = (dataGridView1.SelectedRows[0].Cells[0].Value).ToString();
                VeloS.marka = (dataGridView1.SelectedRows[0].Cells[1].Value).ToString();
                VeloS.model = (dataGridView1.SelectedRows[0].Cells[2].Value).ToString();
                VeloS.color = (dataGridView1.SelectedRows[0].Cells[3].Value).ToString();
                VeloS.stat = (dataGridView1.SelectedRows[0].Cells[4].Value).ToString();
                VeloS.pricez = (dataGridView1.SelectedRows[0].Cells[5].Value).ToString();
                VeloS.pricea = (dataGridView1.SelectedRows[0].Cells[6].Value).ToString();
                VeloS.prices = (dataGridView1.SelectedRows[0].Cells[7].Value).ToString();
                VeloS.foto = (dataGridView1.SelectedRows[0].Cells[8].Value).ToString();
                VeloS.code = (dataGridView1.SelectedRows[0].Cells[9].Value).ToString();
                LIST_interface.SelectTab(VeloMenu);

                int colorVM = Convert.ToInt32(VeloS.color);
                Color argbCOLOR2 = Color.FromArgb(colorVM);
                pictureBox4.BackColor = argbCOLOR2;

                textBox1.Text = VeloS.marka;
                textBox2.Text = VeloS.model;
                numericUpDown1.Value = Convert.ToInt32(VeloS.pricez);
                numericUpDown2.Value = Convert.ToInt32(VeloS.pricea);
                numericUpDown3.Value = Convert.ToInt32(VeloS.prices);
                comboBox6.Text = VeloS.stat;
                label154.Text = "" + VeloS.code;
                pictureBox3.ImageLocation = savePath + "\\" + VeloS.foto;
            }
            catch
            {
                MessageBox.Show(
"Вы невыбрали велотранспорт!",
"Ошибка",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1);
            }
           
        }
        private void button10_Click(object sender, EventArgs e)
        {
            VeloS.marka = "";
            VeloS.model = "";
            VeloS.color = "";
            VeloS.stat = "";
            VeloS.pricez = "";
            VeloS.pricea = "";
            VeloS.prices = "";
            VeloS.foto = "";
            VeloS.code = "";
            dataGridView1.DataSource = SqlFunctions.GetAllInventoryAsDataTable("SELECT * FROM Велотранспорт");
            LIST_interface.SelectTab(Sklad);
        }
        private void button20_Click(object sender, EventArgs e)
        {
            VeloS.marka = "";
            VeloS.model = "";
            VeloS.color = "";
            VeloS.stat = "";
            VeloS.pricez = "";
            VeloS.pricea = "";
            VeloS.prices = "";
            VeloS.foto = "";
            VeloS.code = "";
            dataGridView1.DataSource = SqlFunctions.GetAllInventoryAsDataTable("SELECT * FROM Велотранспорт");
            LIST_interface.SelectTab(Sklad);
        }
        private void button12_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Menu_Kassa);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView6.DataSource = SqlFunctions.GetAllInventoryAsDataTable("SELECT * FROM Клиенты");

            DataGridViewColumn column = dataGridView6.Columns[0];
            column.Width = 165;
            DataGridViewColumn column2 = dataGridView6.Columns[1];
            column2.Width = 130;
            DataGridViewColumn column3 = dataGridView6.Columns[2];
            column3.Width = 180;
            DataGridViewColumn column4 = dataGridView6.Columns[3];
            column4.Width = 180;
            DataGridViewColumn column5 = dataGridView6.Columns[4];
            column5.Width = 180;
            DataGridViewColumn column6 = dataGridView6.Columns[5];
            column6.Width = 180;
            DataGridViewColumn column7 = dataGridView6.Columns[6];
            column7.Width = 210;
            DataGridViewColumn column8 = dataGridView6.Columns[7];
            column8.Width = 300;


            LIST_interface.SelectTab(Client_Baze);
        }
        private void button16_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Menu_Kassa);
        }
        private void button24_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Client_Baze);
        }
        private void button18_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Client_ADD);
        }
        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                ClientS.id = (dataGridView6.SelectedRows[0].Cells[0].Value).ToString();
                ClientS.fam = (dataGridView6.SelectedRows[0].Cells[1].Value).ToString();
                ClientS.nam = (dataGridView6.SelectedRows[0].Cells[2].Value).ToString();
                ClientS.otc = (dataGridView6.SelectedRows[0].Cells[3].Value).ToString();
                ClientS.tel = (dataGridView6.SelectedRows[0].Cells[4].Value).ToString();
                ClientS.serp = (dataGridView6.SelectedRows[0].Cells[5].Value).ToString();
                ClientS.nomp = (dataGridView6.SelectedRows[0].Cells[6].Value).ToString();
                ClientS.adr = (dataGridView6.SelectedRows[0].Cells[7].Value).ToString();

                LIST_interface.SelectTab(ClientMenu);

                textBox16.Text = ClientS.fam;
                textBox15.Text = ClientS.nam;
                textBox13.Text = ClientS.otc;
                numericUpDown10.Value = Convert.ToInt32(ClientS.serp);
                numericUpDown7.Value = Convert.ToInt32(ClientS.nomp);
                textBox12.Text = ClientS.adr;
                textBox14.Text = ClientS.tel;
            }
            catch
            {
                MessageBox.Show(
"Вы невыбрали клиента!",
"Ошибка",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1);
            }
           

        }
        private void button27_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Client_Baze);
        }
        private void button15_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(NewArend);
        }
        private void button31_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(ArendaMenu);
        }
        private void button34_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(ArendaMenu);
        }
        private void button14_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(ArendaBAZA);
        }
        private void button29_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Arend_ITOG);
        }
        private void button39_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(NewArend);
        }
        private void button35_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Dogovor_Revork);
        }
        private void button40_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(ArendaBAZA);
        }
        private void button25_Click(object sender, EventArgs e)
        {
            Password.Text = "";
            Login.Text = "";
            LIST_interface.SelectTab(Aftoriz_List);
        }
        private void button43_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Direktor_Dogovor);
        }
        private void button42_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Director_Kassa);
        }
        private void button51_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Director_Kassa);
        }
        private void button49_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Director_Kassa_Add);
        }
        private void button45_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Direktor_Menu);
        }
        private void button47_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Direktor_Menu);
        }
        // выход
        private void button22_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button53_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Director_Kassa);
        }
        private void button48_Click(object sender, EventArgs e)
        {
            LIST_interface.SelectTab(Director_Kassa_Revork);
        }


        /////////////////////////////////////////////////////////

        // Загрузка фото велотранспорта
        public static string savePath = Path.GetFullPath(@"C:\Users\usersql\Desktop\Программа Курсовой (Старцев ИС-19)\VeloMEN_project\VeloMEN_project\Velo_Foto");
        private void button21_Click(object sender, EventArgs e)
        {
            

            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Изображения|*.png|*.jpeg|*.jpg";
            if (OPF.ShowDialog() == DialogResult.OK)
            {

                string fileName = Path.GetFileName(OPF.FileName);
                string savePath2 = savePath + "\\" + fileName;
                //File.Copy(OPF.FileName, savePath, true);
                try
                {
                    VeloS.foto = fileName;
                    Bitmap image = new Bitmap(OPF.FileName);

                    Bitmap newSizeImage = new Bitmap(image, new Size(400, 400));
                    newSizeImage.Save(savePath,
                    System.Drawing.Imaging.ImageFormat.Jpeg);

                    pictureBox8.ImageLocation = $@"{savePath2}";
                }
                catch
                {
                    MessageBox.Show(
"Не удалось загрузить изображене",
"Ошибка",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1,
MessageBoxOptions.DefaultDesktopOnly);
                }

            }
            else
            {
                MessageBox.Show(
"Изображение не выбрано!",
"Внимание",
MessageBoxButtons.OK,
MessageBoxIcon.Warning,
MessageBoxDefaultButton.Button1,
MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        // Кнопка Удаления Велотранспорта из базы
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                VeloS.id = (dataGridView1.SelectedRows[0].Cells[0].Value).ToString();
                VeloS.marka = (dataGridView1.SelectedRows[0].Cells[1].Value).ToString();
                VeloS.model = (dataGridView1.SelectedRows[0].Cells[2].Value).ToString();
                VeloS.color = (dataGridView1.SelectedRows[0].Cells[3].Value).ToString();
                VeloS.stat = (dataGridView1.SelectedRows[0].Cells[4].Value).ToString();
                VeloS.pricez = (dataGridView1.SelectedRows[0].Cells[5].Value).ToString();
                VeloS.pricea = (dataGridView1.SelectedRows[0].Cells[6].Value).ToString();
                VeloS.prices = (dataGridView1.SelectedRows[0].Cells[7].Value).ToString();
                VeloS.foto = (dataGridView1.SelectedRows[0].Cells[8].Value).ToString();
                VeloS.code = (dataGridView1.SelectedRows[0].Cells[9].Value).ToString();

                SqlFunctions.DeliteVelo(VeloS.id);

                MessageBox.Show(
"Вы удалили велотранспорт!",
"Внимание",
MessageBoxButtons.OK,
MessageBoxIcon.Information,
MessageBoxDefaultButton.Button1);

                dataGridView1.DataSource = SqlFunctions.GetAllInventoryAsDataTable("SELECT * FROM Велотранспорт");

            }
            catch
            {
                MessageBox.Show(
"Вы невыбрали велотранспорт!",
"Ошибка",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1);
            }





        }

        private void button11_Click(object sender, EventArgs e)
        {
            VeloS.marka = textBox1.Text;
            VeloS.model = textBox2.Text;
            VeloS.color = pictureBox4.BackColor.ToArgb().ToString();
            VeloS.stat = comboBox6.Text;
            VeloS.pricez = numericUpDown1.Value.ToString();
            VeloS.pricea = numericUpDown2.Value.ToString();
            VeloS.prices = numericUpDown3.Value.ToString();
            VeloS.foto = VeloS.foto;

            if (VeloS.marka != "" & VeloS.model != "" & VeloS.color != "" & VeloS.stat != "" & VeloS.pricez != "" & VeloS.pricea != "" & VeloS.prices != "" & VeloS.foto != "" & VeloS.code != "")
            {
                SqlFunctions.ApdeitVelo(VeloS.marka, VeloS.model, VeloS.color, VeloS.stat, VeloS.pricez, VeloS.pricea, VeloS.prices, VeloS.foto, VeloS.id);
                
                MessageBox.Show(
"Данные успешно обновлены",
"Внимание",
MessageBoxButtons.OK,
MessageBoxIcon.Information,
MessageBoxDefaultButton.Button1);
            

                VeloS.marka = "";
                VeloS.model = "";
                VeloS.color = "";
                VeloS.stat = "";
                VeloS.pricez = "";
                VeloS.pricea = "";
                VeloS.prices = "";
                VeloS.code = "";
            }
            else
            {
                MessageBox.Show(
"Возможно вы ввели не все данные\n" +
"Попробуйте еще раз или обратитесь к системному администратору...",
"Ошибка",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Изображения|*.png|*.jpeg|*.jpg";
            if (OPF.ShowDialog() == DialogResult.OK)
            {

                string fileName = Path.GetFileName(OPF.FileName);
               string savePath2 = savePath + "\\" + fileName;
                //File.Copy(OPF.FileName, savePath, true);
                try
                {
                    VeloS.foto = fileName;
                    Bitmap image = new Bitmap(OPF.FileName);

                    Bitmap newSizeImage = new Bitmap(image, new Size(400, 400));
                    newSizeImage.Save(savePath,
                    System.Drawing.Imaging.ImageFormat.Jpeg);

                    pictureBox3.ImageLocation = $@"{savePath2}";
                }
                catch
                {
                    MessageBox.Show(
"Не удалось загрузить изображене",
"Ошибка",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1,
MessageBoxOptions.DefaultDesktopOnly);
                }

            }
            else
            {
                MessageBox.Show(
"Изображение не выбрано!",
"Внимание",
MessageBoxButtons.OK,
MessageBoxIcon.Warning,
MessageBoxDefaultButton.Button1,
MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //ClientS.id = textBox9.Text;
            ClientS.fam = textBox9.Text;
            ClientS.nam = textBox8.Text;
            ClientS.otc = textBox10.Text;
            ClientS.tel = textBox7.Text;
            ClientS.serp = numericUpDown9.Value.ToString();
            ClientS.nomp = numericUpDown8.Value.ToString();
            ClientS.adr = textBox11.Text;

            if (ClientS.fam != "" & ClientS.nam != "" & ClientS.otc != "" & ClientS.tel != "" & ClientS.serp != "" & ClientS.nomp != "" & ClientS.adr != "")
            {
                SqlFunctions.AddClient(ClientS.fam ,  ClientS.nam ,  ClientS.otc ,  ClientS.tel ,  ClientS.serp ,  ClientS.nomp ,  ClientS.adr );
                dataGridView6.DataSource = SqlFunctions.GetAllInventoryAsDataTable("SELECT * FROM Клиенты");
                MessageBox.Show(
 "Клиент успешно добавлен",
 "Внимание",
 MessageBoxButtons.OK,
 MessageBoxIcon.Information,
 MessageBoxDefaultButton.Button1);
                ClientS.id = "";
                ClientS.fam = "";
                ClientS.nam = "";
                ClientS.otc = "";
                ClientS.tel = "";
                ClientS.serp = "";
                ClientS.nomp = "";
                ClientS.adr = "";
            }
            else
            {
                MessageBox.Show(
"Возможно вы ввели не все данные\n" +
"Попробуйте еще раз или обратитесь к системному администратору...",
"Ошибка",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1);
            }


        }

        private void button26_Click(object sender, EventArgs e)
        {
            ClientS.fam = "";
            ClientS.nam = "";
            ClientS.otc = "";
            ClientS.serp = "";
            ClientS.nomp = "";
            ClientS.adr = "";
            ClientS.tel = "";

            ClientS.fam = textBox16.Text;
            ClientS.nam = textBox15.Text;
            ClientS.otc = textBox13.Text;
            ClientS.serp = Convert.ToString(numericUpDown10.Value);
            ClientS.nomp = Convert.ToString(numericUpDown7.Value);
            ClientS.adr = textBox12.Text;
            ClientS.tel = textBox14.Text;

            if (ClientS.fam != "" & ClientS.nam != "" & ClientS.otc != "" & ClientS.tel != "" & ClientS.serp != "" & ClientS.nomp != "" & ClientS.adr != "")
            {
                SqlFunctions.ApdeitClient(ClientS.id, ClientS.fam, ClientS.nam, ClientS.otc, ClientS.tel, ClientS.serp, ClientS.nomp, ClientS.adr);
                MessageBox.Show(
 "Данные клиента успешно обновлены",
 "Внимание",
 MessageBoxButtons.OK,
 MessageBoxIcon.Information,
 MessageBoxDefaultButton.Button1);
                ClientS.fam = "";
                ClientS.nam = "";
                ClientS.otc = "";
                ClientS.serp = "";
                ClientS.nomp = "";
                ClientS.adr = "";
                ClientS.tel = "";
                dataGridView6.DataSource = SqlFunctions.GetAllInventoryAsDataTable("SELECT * FROM Клиенты");
            }
            else
            {
                MessageBox.Show(
"Возможно вы ввели не все данные\n" +
"Попробуйте еще раз или обратитесь к системному администратору...",
"Ошибка",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1);
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                ClientS.id = (dataGridView6.SelectedRows[0].Cells[0].Value).ToString();

                SqlFunctions.DeliteClient(ClientS.id);
                MessageBox.Show(
 "Клиент успешно удален!",
 "Внимание",
 MessageBoxButtons.OK,
 MessageBoxIcon.Information,
 MessageBoxDefaultButton.Button1);
                dataGridView6.DataSource = SqlFunctions.GetAllInventoryAsDataTable("SELECT * FROM Клиенты");
            }
            catch
            {
                MessageBox.Show(
"Вы невыбрали клиента!",
"Ошибка",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1);
            }
        }
    }
}
