using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace ManagementSystem_For_PublicCatering
{
    public partial class Menu : Form
    {
        public Menu() => InitializeComponent();

        private void Menu_Load(object sender, EventArgs e)
        {
            // Установка параметров формы
            WindowState = FormWindowState.Maximized;
            FormSettings.StyleSetting(this); FormSettings.DimensionalSettings(this, 1000, 600); Button_ImageShow_Update();
            FillTable.Stage = 1; FillTable.ImageShow = false; FillTable.Tables = Table; FillTable.Catalog_Stage();
        }

        private void Button_ImageShow_Click(object sender, EventArgs e)
        { if (FillTable.ImageShow == true) FillTable.ImageShow = false; else FillTable.ImageShow = true; Button_ImageShow_Update(); FillTable.Catalog_Stage(); }

        private void Button_ImageShow_Update()
        {
            if (FillTable.ImageShow) if (Properties.Settings.Default.FormStyle) Button_ImageShow.BackgroundImage = Properties.Resources.DarkImageShowOn; else Button_ImageShow.BackgroundImage = Properties.Resources.DarkImageShowOn;
            else if (Properties.Settings.Default.FormStyle) Button_ImageShow.BackgroundImage = Properties.Resources.DarkImageShowOff; else Button_ImageShow.BackgroundImage = Properties.Resources.LightImageShowOff;
        }
    }

    class FillTable
    {
        /// <summary>  Элемент управления в который будут вноситься другие отображаемые элементы </summary>
        public static Panel Tables { get; set; }

        /// <summary> Отображение изображения </summary>
        public static bool ImageShow { get; set; }
        /// <summary> Этап погрузки директорий и товаров </summary>
        public static int Stage { get; set; }

        /// <summary>
        /// Номер страницы прогруженных данных
        /// P.S. Страниц больше 1 если на экран не вмещаются все данные из таблицы
        /// </summary>
        static int Page { get; set; }
        static int MaxPage { get; set; }

        static string CategoryName, SubDirectoryName, ProductName;

        static DataTable ProductSize = new DataTable();

        public static void Catalog_Stage()
        {
            Tables.Controls.Clear(); Tables.AutoScroll = true; Tables.SuspendLayout();

            Point SizeF = new Point(250, 250); int ColumnCount = (int)Math.Floor((float)Tables.Width / (SizeF.X + 20.0)), RowCount = (int)Math.Floor((float)Tables.Height / (SizeF.Y + 20.0));
            Point Distance = new Point((int)Math.Floor((float)(Tables.Width - (SizeF.X) * ColumnCount) / (float)(ColumnCount * 2)), (int)Math.Floor((float)(Tables.Height - (SizeF.Y) * RowCount) / (float)(RowCount * 2)));

            Stage_Generation(Tables, SizeF, Distance, new Point(ColumnCount, RowCount));

            Tables.ResumeLayout(true);
        }

        /// <summary> Создание элементов управления для перехода по данным </summary>
        /// <param name="Table"> Элемент управления в который будут вноситься другие отображаемые элементы </param>
        static void Stage_Generation(Panel Table, Point SizeF, Point Distance, Point TableSize)
        {
            DataTable DATA = new DataTable();

            if (Stage == 1) DATA = SQL.SELECT($"EXEC [PublicCatering].[dbo].[Menu_Category]");
            if (Stage == 2) DATA = SQL.SELECT($"EXEC [PublicCatering].[dbo].[Menu_SubCategory] @Category = '{CategoryName}'");
            if (Stage == 3) DATA = SQL.SELECT($"EXEC [PublicCatering].[dbo].[Menu_Product] @SubDirectory = '{SubDirectoryName}'");
            if (Stage == 4) { DATA = SQL.SELECT($"EXEC [PublicCatering].[dbo].[Menu_Product_Details] @ProductName = '{ProductName}'"); ProductSize = DATA; }

            if (Page == 0) { Page = 1; MaxPage = (int)Math.Ceiling((float)DATA.Rows.Count / (float)(TableSize.Y * TableSize.X - 3)); }

            for (int I1 = 0, IndexCount = 0, Index = (Page - 1) * (TableSize.Y * TableSize.X - 3), ControlNumber = 1; I1 < TableSize.Y; I1++) for (int I2 = 0; I2 < TableSize.X; I2++, IndexCount++)

                    if (IndexCount < TableSize.Y * TableSize.X)
                    {
                        Point StartLocation = new Point(Distance.X + Distance.X * 2 * I2 + SizeF.X * I2, Distance.Y + Distance.Y * 2 * I1 + SizeF.Y * I1);

                        if ((IndexCount >= TableSize.Y * TableSize.X - 3) & (IndexCount <= TableSize.Y * TableSize.X - 1))
                        {
                            if (ControlNumber == 1 & Page > 1) Table.Controls.Add(CreateConrols(SizeF, StartLocation, ControlNumber));
                            if (ControlNumber == 2 & Page < MaxPage) Table.Controls.Add(CreateConrols(SizeF, StartLocation, ControlNumber));
                            if (ControlNumber == 3) Table.Controls.Add(CreateConrols(SizeF, StartLocation, ControlNumber));
                            ControlNumber++;
                        }
                        else if (Index < DATA.Rows.Count)
                        {


                            string Named = ""; if (Stage == 4) Named = $"{DATA.Rows[Index][2]}\n{DATA.Rows[Index][3]} гр.  =  {DATA.Rows[Index][4]} руб."; else Named = DATA.Rows[Index][0].ToString();

                            byte[] Image_DATA = new byte[] { }; if (DATA.Rows[Index][1] != DBNull.Value) Image_DATA = (byte[])DATA.Rows[Index][1]; else Image_DATA = null;
                            Table.Controls.Add(CreateProduct(SizeF, StartLocation, $"Stage{Stage}丄[{Named}]", Named, Image_DATA)); Index++;
                        }
                    }
        }

        static Panel CreateConrols(Point SizeF, Point StartLocation, int Number)
        {
            Panel Frame = new Panel { Size = new Size(SizeF.X, SizeF.Y), BackgroundImageLayout = ImageLayout.Stretch, BorderStyle = BorderStyle.FixedSingle, Left = StartLocation.X, Top = StartLocation.Y };
            if (Properties.Settings.Default.FormStyle) Frame.BackgroundImage = Properties.Resources.DarkFrame; else Frame.BackgroundImage = Properties.Resources.LightFrame;

            if (Properties.Settings.Default.FormStyle)
            {
                if (Number == 1) { Frame.BackgroundImage = Properties.Resources.DarkPrev; Frame.Click += PrevPage; };
                if (Number == 2) { Frame.BackgroundImage = Properties.Resources.DarkNext; Frame.Click += NextPage; };
                if (Number == 3) if (Stage == 1) { Frame.BackgroundImage = Properties.Resources.DarkEXIT; Frame.Click += Exit; } else { Frame.BackgroundImage = Properties.Resources.DarkBack; Frame.Click += PrevStage; }
            }
            else
            {
                if (Number == 1) { Frame.BackgroundImage = Properties.Resources.LightPrev; Frame.Click += PrevPage; };
                if (Number == 2) { Frame.BackgroundImage = Properties.Resources.LightNext; Frame.Click += NextPage; };
                if (Number == 3) if (Stage == 1) { Frame.BackgroundImage = Properties.Resources.LightEXIT; Frame.Click += Exit; } else { Frame.BackgroundImage = Properties.Resources.LightBack; Frame.Click += PrevStage; }
            }

            return Frame;
        }

        static Panel CreateProduct(Point SizeF, Point StartLocation, string Named, string Texted, byte[] Imaged)
        {
            Panel Frame = new Panel { Size = new Size(SizeF.X, SizeF.Y), BackgroundImageLayout = ImageLayout.Stretch, BorderStyle = BorderStyle.FixedSingle, Left = StartLocation.X, Top = StartLocation.Y, Name = Named };
            if (Properties.Settings.Default.FormStyle) Frame.BackgroundImage = Properties.Resources.DarkFrame; else Frame.BackgroundImage = Properties.Resources.LightFrame;
            {
                if (ImageShow)
                {
                    PictureBox PictureProduct = new PictureBox { Size = new Size(Frame.Width / 5 * 4, Frame.Height / 5 * 3), BorderStyle = BorderStyle.Fixed3D, BackgroundImageLayout = ImageLayout.Stretch, Left = Frame.Width / 10, Top = Frame.Height / 10, Name = Named };
                    if (Stage < 3) if (Properties.Settings.Default.FormStyle) PictureProduct.BackgroundImage = Properties.Resources.DarkCategory; else PictureProduct.BackgroundImage = Properties.Resources.LightCategory;
                    else if (Imaged == null) PictureProduct.BackgroundImage = Properties.Resources.MissingPhoto; else using (MemoryStream MS = new MemoryStream(Imaged, 0, Imaged.Length)) { MS.Write(Imaged, 0, Imaged.Length); PictureProduct.BackgroundImage = Image.FromStream(MS, true, true); }

                    Label NameProduct = new Label { Size = new Size(Frame.Width / 5 * 4, Frame.Height / 5), BorderStyle = BorderStyle.FixedSingle, Left = Frame.Width / 10, Top = Frame.Height / 10 * 7, Name = Named, Font = new Font("Times New Roman", 12), TextAlign = ContentAlignment.MiddleCenter, Text = Texted };

                    PictureProduct.Click += Product_Panel_Click; NameProduct.Click += Product_Panel_Click; Frame.Controls.Add(PictureProduct); Frame.Controls.Add(NameProduct);
                }
                else
                {
                    Label NameProduct = new Label { Size = new Size(Frame.Width / 5 * 4, Frame.Height / 5 * 4), BorderStyle = BorderStyle.FixedSingle, Left = Frame.Width / 10, Top = Frame.Height / 10, Name = Named, Font = new Font("Times New Roman", 20), TextAlign = ContentAlignment.MiddleCenter, Text = Texted };
                    NameProduct.Click += Product_Panel_Click; Frame.Controls.Add(NameProduct);
                }
            }

            Frame.Click += Product_Panel_Click; return Frame;
        }

        private static void Product_Panel_Click(object sender, EventArgs e)
        {
            var Element_Panel = new Panel(); var Element_PictureBox = new PictureBox(); var Element_Label = new Label(); int KodStage = 0; string[] Named = new string[] { };

            switch (sender.GetType().ToString())
            {
                case "System.Windows.Forms.Panel": Element_Panel = (Panel)sender; KodStage = Element_Panel.Name[5]; Named = Element_Panel.Name.Split('丄'); break;
                case "System.Windows.Forms.PictureBox": Element_PictureBox = (PictureBox)sender; KodStage = Element_PictureBox.Name[5]; Named = Element_PictureBox.Name.Split('丄'); break;
                case "System.Windows.Forms.Label": Element_Label = (Label)sender; KodStage = Element_Label.Name[5]; Named = Element_Label.Name.Split('丄'); break;
            }

            if (KodStage == '1') { Stage = 2; CategoryName = Named[1].Remove(0, 1).Remove(Named[1].Length - 2, 1); Page = 0; Catalog_Stage(); }
            if (KodStage == '2') { Stage = 3; SubDirectoryName = Named[1].Remove(0, 1).Remove(Named[1].Length - 2, 1); Page = 0; Catalog_Stage(); }
            if (KodStage == '3') { Stage = 4; ProductName = Named[1].Remove(0, 1).Remove(Named[1].Length - 2, 1); Page = 0; Catalog_Stage(); }
            if (KodStage == '4')
            {
                string[] Sizez = Named[1].Split('\n'); bool T = false;

                for (int I1 = 0; I1 < CashRegister.PurchaseReceipt_Table.Rows.Count; I1++) if (CashRegister.PurchaseReceipt_Table.Rows[I1][0].ToString() == ProductName & CashRegister.PurchaseReceipt_Table.Rows[I1][1].ToString() == Sizez[0].Remove(0, 1))
                    {
                        CashRegister.PurchaseReceipt_Table.Rows[I1][3] = (Convert.ToInt32(CashRegister.PurchaseReceipt_Table.Rows[I1][3]) + 1).ToString();
                        T = true; break;
                    }

                if (T == false)
                {
                    for (int I1 = 0; I1 < ProductSize.Rows.Count; I1++) if (ProductSize.Rows[I1][0].ToString() == ProductName & ProductSize.Rows[I1][2].ToString() == Sizez[0].Remove(0, 1))
                        { string[] Line = { ProductSize.Rows[I1][0].ToString(), Sizez[0].Remove(0, 1), ProductSize.Rows[I1][4].ToString(), "1" }; CashRegister.PurchaseReceipt_Table = SQL.LocalDB(CashRegister.PurchaseReceipt_Table, Line); break; }
                }

                Stage = 1; Page = 0; Catalog_Stage();
            }
        }

        private static void PrevPage(object sender, EventArgs e) { Page--; Catalog_Stage(); }

        private static void NextPage(object sender, EventArgs e) { Page++; Catalog_Stage(); }

        private static void PrevStage(object sender, EventArgs e) { Stage--; Page = 0; Catalog_Stage(); }

        private static void Exit(object sender, EventArgs e) => Application.OpenForms["Menu"].Close();
    }
}