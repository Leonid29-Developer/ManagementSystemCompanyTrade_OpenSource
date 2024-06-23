using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ManagementSystem_For_PublicCatering
{
    public partial class CashRegister : Form
    {
        public CashRegister() => InitializeComponent();

        public static string ID { get; set; }

        public static DataTable PurchaseReceipt_Table { get; set; }

        private void CashRegister_Load(object sender, EventArgs e)
        {
            // Установка параметров формы
            StyleUpdate();
            Button_Payment.Location = new Point(Button_Payment.Location.X - 110, Button_Payment.Location.Y); Button_Cancel.Location = new Point(Button_Cancel.Location.X - 90, Button_Payment.Location.Y);
            WindowState = FormWindowState.Maximized; FormSettings.DimensionalSettings(this, 950, 800);

            string[] Tab = { "Name", "Size", "Price", "Quantity" }; PurchaseReceipt_Table = SQL.LocalDB(Tab);
        }

        /// <summary> Установка параметров формы </summary>
        private void StyleUpdate()
        {
            FormSettings.StyleSetting(this);

            if (!Properties.Settings.Default.FormStyle)
            {
                Button_Menu.BackgroundImage = Properties.Resources.LightMenu; Button_Discount.BackgroundImage = Properties.Resources.LightDiscount; Button_Payment.BackgroundImage = Properties.Resources.LightPayment;
                Button_Cancel.BackgroundImage = Properties.Resources.LightFrame; Button_Settings.BackgroundImage = Properties.Resources.LightSettings;

                for (int i = 0; i <= 24; i++) Controls["Button_PopularProducts_" + i].BackgroundImage = Properties.Resources.LightFrame; for (int i = 0; i <= 5; i++) Controls["Button_PopularService_" + i].BackgroundImage = Properties.Resources.LightFrame;
            }
            else
            {
                Button_Menu.BackgroundImage = Properties.Resources.DarkMenu; Button_Discount.BackgroundImage = Properties.Resources.DarkDiscount; Button_Payment.BackgroundImage = Properties.Resources.DarkPayment;
                Button_Cancel.BackgroundImage = Properties.Resources.DarkFrame; Button_Settings.BackgroundImage = Properties.Resources.DarkSettings;

                for (int i = 0; i <= 24; i++) Controls["Button_PopularProducts_" + i].BackgroundImage = Properties.Resources.DarkFrame; for (int i = 0; i <= 5; i++) Controls["Button_PopularService_" + i].BackgroundImage = Properties.Resources.DarkFrame;
            }
        }

        private void CashRegister_FormClosed(object sender, FormClosedEventArgs e) => Application.OpenForms["Authorization"].Show();

        private void Button_Menu_Click(object sender, EventArgs e) => new Menu().ShowDialog();

        private void Button_Discount_Click(object sender, EventArgs e) { }

        private void Button_Payment_Click(object sender, EventArgs e) => new PurchaseReceipt().ShowDialog();

        private void Button_Cancel_Click(object sender, EventArgs e) => new RemovalOperations().ShowDialog();

        private void Button_Settings_Click(object sender, EventArgs e) { new ProgramSettingsDevice().ShowDialog(); StyleUpdate(); }

        private void Button_PopularProducts_Click(object sender, EventArgs e) { }

        private void Button_PopularService_Click(object sender, EventArgs e) { }

        private void Button_Close_Click(object sender, EventArgs e) => Close();
    }
}