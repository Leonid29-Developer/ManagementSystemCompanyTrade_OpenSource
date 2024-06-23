using System;
using System.Windows.Forms;

namespace ManagementSystem_For_PublicCatering
{
    internal static class Program
    {
        /// <summary> Главная точка входа для приложения. </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ErrorsKods.CreateErrorsKods();
            Application.Run(new Authorization());
            //Application.Run(new CashRegister());
            //Application.Run(new Menu());
            //new AddImage();
        }
    }
}