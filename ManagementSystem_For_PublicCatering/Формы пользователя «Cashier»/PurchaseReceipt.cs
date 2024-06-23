using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ManagementSystem_For_PublicCatering
{
    public partial class PurchaseReceipt : Form
    {
        public PurchaseReceipt() => InitializeComponent();

        private void PurchaseReceipt_Load(object sender, EventArgs e)
        {
            Location = new Point(Screen.PrimaryScreen.Bounds.Size.Width / 3, 10);

            //Установка параметров формы
            FormSettings.StyleSetting(this);
            Receipt();
        }

        private void Receipt()
        {
            if (CashRegister.PurchaseReceipt_Table.Rows.Count == 0) Height = 290;
            else if (CashRegister.PurchaseReceipt_Table.Rows.Count <= 6) Height = 140 + 150 * CashRegister.PurchaseReceipt_Table.Rows.Count; else Height = 1040;

            //Очистка таблицы-вывод
            Table.BringToFront(); Table.Controls.Clear(); Table.AutoScroll = false; Table.HorizontalScroll.Enabled = false; Table.AutoScroll = true;
            Table.SuspendLayout(); Table.MouseWheel += new MouseEventHandler(this_MouseWheel);

            //Заполнение данных
            {
                //Создание Panel - Оглавление
                {
                    Panel PName = new Panel { Size = new Size(Table.Width, 100), Margin = new Padding(0) };

                    //Создание Label с текстом "Квитанция о покупке"
                    {
                        Label lab = new Label()
                        {
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Times New Roman", 22),
                            Text = "Квитанция о покупке",
                            Size = new Size(PName.Width, PName.Height),
                            BorderStyle = BorderStyle.FixedSingle
                        };

                        PName.Controls.Add(lab);
                    }

                    Table.Controls.Add(PName);
                }

                for (int I1 = 0; I1 < CashRegister.PurchaseReceipt_Table.Rows.Count; I1++)
                {
                    //Создание Panel - Товар
                    {
                        Panel Product = new Panel
                        {
                            Size = new Size(Table.Width + 1, 150),
                            Margin = new Padding(0)
                        };

                        //Создание Label с наименованием товара
                        {
                            Label lab = new Label()
                            {
                                TextAlign = ContentAlignment.MiddleLeft,
                                Font = new Font("Times New Roman", 20),
                                Text = CashRegister.PurchaseReceipt_Table.Rows[I1][0].ToString(),
                                Size = new Size((int)((double)Product.Width * 2 / 3), (int)((double)Product.Height * 2 / 3)),
                                BorderStyle = BorderStyle.FixedSingle
                            };

                            Product.Controls.Add(lab);
                        }

                        //Создание Panel - Рамка
                        {
                            Panel Frame = new Panel
                            {
                                Margin = new Padding(0),
                                Top = (int)((double)Product.Height * 2 / 3),
                                Size = new Size((int)((double)Product.Width * 2 / 3), (int)((double)Product.Height / 3)),
                                BorderStyle = BorderStyle.FixedSingle
                            };

                            //Создание Label с размерам товара
                            {
                                Label lab = new Label()
                                {
                                    TextAlign = ContentAlignment.MiddleLeft,
                                    Font = new Font("Times New Roman", 16),
                                    Text = CashRegister.PurchaseReceipt_Table.Rows[I1][1].ToString(),
                                    Size = new Size((int)((double)Product.Width / 3), (int)((double)Product.Height / 3))
                                };

                                Frame.Controls.Add(lab);
                            }

                            //Создание Label с количеством товара
                            {
                                Label lab = new Label()
                                {
                                    TextAlign = ContentAlignment.MiddleRight,
                                    Font = new Font("Times New Roman", 16),
                                    Text = CashRegister.PurchaseReceipt_Table.Rows[I1][3] + " x " + CashRegister.PurchaseReceipt_Table.Rows[I1][2] + " ₽",
                                    Left = (int)((double)Product.Width / 3),
                                    Size = new Size((int)((double)Product.Width / 3), (int)((double)Product.Height / 3))
                                };

                                Frame.Controls.Add(lab);
                            }

                            Product.Controls.Add(Frame);
                        }

                        //Создание Label с общей суммой товара
                        {
                            Label lab = new Label()
                            {
                                TextAlign = ContentAlignment.MiddleCenter,
                                Font = new Font("Times New Roman", 18),
                                Left = (int)((double)Product.Width * 2 / 3),
                                Size = new Size((int)((double)Product.Width / 3), Product.Height),
                                BorderStyle = BorderStyle.FixedSingle
                            };

                            double Price = Convert.ToDouble(CashRegister.PurchaseReceipt_Table.Rows[I1][3]) * Convert.ToDouble(CashRegister.PurchaseReceipt_Table.Rows[I1][2]);
                            lab.Text = "ИТОГО:\n" + Price + " ₽";

                            Product.Controls.Add(lab);
                        }

                        Table.Controls.Add(Product);
                    }
                }

                //Создание Panel - Вывод
                {
                    Panel PName = new Panel { Size = new Size(Table.Width, 100), Margin = new Padding(0), BorderStyle = BorderStyle.FixedSingle };

                    //Создание Label с текстом "Общая сумма к оплате"
                    {
                        double All_Price = 0;

                        for (int I1 = 0; I1 < CashRegister.PurchaseReceipt_Table.Rows.Count; I1++) All_Price += Convert.ToDouble(CashRegister.PurchaseReceipt_Table.Rows[I1][3]) * Convert.ToDouble(CashRegister.PurchaseReceipt_Table.Rows[I1][2]);

                        Label lab = new Label()
                        {
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Times New Roman", 20),
                            Text = "Общая сумма к оплате: " + All_Price + " ₽",
                            Size = new Size(PName.Width, PName.Height)
                        };

                        PName.Controls.Add(lab);
                    }

                    Table.Controls.Add(PName);
                }

                //Создание Panel - Закрыть
                {
                    Panel Close = new Panel { Size = new Size(Table.Width, 84), Margin = new Padding(0), BorderStyle = BorderStyle.FixedSingle };

                    //Создание Label с текстом "Закрыть"
                    {
                        Label lab = new Label()
                        {
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Times New Roman", 20),
                            Text = "Закрыть",
                            Name = "Close",
                            Size = new Size(Close.Width, Close.Height)
                        };

                        lab.Click += FormClose_Click; Close.Controls.Add(lab);
                    }

                    Close.Click += FormClose_Click; Table.Controls.Add(Close);
                }
            }
            Table.ResumeLayout(true);
        }

        //Прогрузка данных при использовании Scroll
        private void Table_Scroll(object sender, ScrollEventArgs e) { Table.Refresh(); Table.Refresh(); }
        void this_MouseWheel(object sender, MouseEventArgs e) { Table.Refresh(); Table.Refresh(); }

        private void FormClose_Click(object sender, EventArgs e) => this.Close();
    }
}