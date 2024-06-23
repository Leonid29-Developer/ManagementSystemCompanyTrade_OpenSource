using System;
using System.Windows.Forms;

namespace ManagementSystem_For_PublicCatering
{
    public partial class ProgramSettingsDevice : Form
    {
        public ProgramSettingsDevice() => InitializeComponent();

        private void ProgramSettingsDevice_Load(object sender, EventArgs e)
        {
            // Установка параметров формы
            FormSettings.StyleSetting(this);
            if (!Properties.Settings.Default.FormStyle) { PB_Entry1.BackgroundImage = Properties.Resources.LightTouchpad1; PB_Entry2.BackgroundImage = Properties.Resources.LightTouchpad2; }
            else { PB_Entry1.BackgroundImage = Properties.Resources.DarkTouchpad1; PB_Entry2.BackgroundImage = Properties.Resources.DarkTouchpad2; }

            WindowState = FormWindowState.Maximized;
            TB_ServerName.Text = Properties.Settings.Default.ServerName;
            TBar_LightOrDark.Value = Convert.ToInt32(!Properties.Settings.Default.FormStyle); TBar_KeyboardOrMouse.Value = Convert.ToInt32(!Properties.Settings.Default.InputType);
        }

        private void Button_ServerName_Click(object sender, EventArgs e)
        { Properties.Settings.Default.ServerName = TB_ServerName.Text; Properties.Settings.Default.Save(); SQL.ConnectionErrorsTest(); }

        private void TBar_LightOrDark_Scroll(object sender, EventArgs e)
        { Properties.Settings.Default.FormStyle = !Convert.ToBoolean(TBar_LightOrDark.Value); Properties.Settings.Default.Save(); ProgramSettingsDevice_Load(sender, e); }

        private void TBar_KeyboardOrMouse_Scroll(object sender, EventArgs e)
        { Properties.Settings.Default.InputType = !Convert.ToBoolean(TBar_KeyboardOrMouse.Value); Properties.Settings.Default.Save(); }

        private void TB_ServerName_Click(object sender, EventArgs e)
        { if (!Properties.Settings.Default.InputType) { TB_ServerName.Enabled = false; new MouseInput().Show(); MouseInput.Source = TB_ServerName; } }

        private void Button_Close_Click(object sender, EventArgs e) => Close();
    }
}