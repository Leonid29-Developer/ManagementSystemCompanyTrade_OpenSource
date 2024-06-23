using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace ManagementSystem_For_PublicCatering
{
    public partial class Authorization : Form
    {
        public Authorization() => InitializeComponent();

        Thread Time = new Thread(() => Thread.Sleep(2000));

        private void Authorization_Shown(object sender, EventArgs e)
        { Time.Start(); Time.Join(); Main(sender, e); SQL.ConnectionErrorsTest(); }

        private void Main(object sender, EventArgs e)
        {
            // Установка параметров формы
            FormSettings.StyleSetting(this); WindowState = FormWindowState.Maximized;
            if (Properties.Settings.Default.FormStyle) Button_Settings.BackgroundImage = Properties.Resources.DarkSettings; else Button_Settings.BackgroundImage = Properties.Resources.LightSettings;
        }

        private void Button_Settings_Click(object sender, EventArgs e)
        { new ProgramSettingsDevice().ShowDialog(); Main(sender, e); }

        private void TBox_Login_Click(object sender, EventArgs e)
        {
            if (TBox_Login.Text == "Логин") { TBox_Login.Clear(); Label_Login.Visible = true; }
            if (!Properties.Settings.Default.InputType) { TBox_Login.Enabled = false; new MouseInput().Show(); MouseInput.Source = TBox_Login; }
        }
        private void TBox_Password_Click(object sender, EventArgs e)
        {
            if (TBox_Password.Text == "Пароль") { TBox_Password.Clear(); Label_Password.Visible = true; }
            if (!Properties.Settings.Default.InputType) { TBox_Password.Enabled = false; new MouseInput().Show(); MouseInput.Source = TBox_Password; }
        }

        private void TBox_Login_Leave(object sender, EventArgs e)
        { if (TBox_Login.Text == "" & Properties.Settings.Default.InputType) { TBox_Login.Text = "Логин"; Label_Login.Visible = false; } }
        private void TBox_Password_Leave(object sender, EventArgs e)
        { if (TBox_Password.Text == "" & Properties.Settings.Default.InputType) { TBox_Password.Text = "Пароль"; Label_Password.Visible = false; } }

        private void TBox_TextChanged(object sender, EventArgs e)
        { var TB = (TextBox)sender; if (TB.TextLength > TB.MaxLength) MessageBox.Show($"{ErrorsKods.Errors[2].Messege}", $"Ошибка. {ErrorsKods.Errors[2].Kod}", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

        private void Button_Input_Click(object sender, EventArgs e)
        {
            DataTable DATA = SQL.SELECT($"EXEC [PublicCatering].[dbo].[Authentication_ID] @Login = '{TBox_Login.Text}', @Password = '{TBox_Password.Text}'");
            if (DATA.Rows.Count > 0) { new CashRegister().Show(); Hide(); CashRegister.ID = DATA.Rows[0][0].ToString(); } else MessageBox.Show($"{ErrorsKods.Errors[3].Messege}", $"Ошибка. {ErrorsKods.Errors[3].Kod}", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Button_Exit_Click(object sender, EventArgs e)
        { Close(); FormSettings.StyleSetting(this); }

        private void pictureBox1_Click(object sender, EventArgs e) { TBox_Login.Text = "Grodvi"; TBox_Password.Text = "312PcFKQ"; }
    }
}