using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ManagementSystem_For_PublicCatering
{
    internal class SQL
    {
        /// <summary> Переменная - Успешное подключение </summary>
        public static bool Successful_Сonnection { get; private set; }

        /// <summary> Тест на ошибки подключения к серверу и базам данных </summary>
        public static void ConnectionErrorsTest()
        {
            // Проверка подключения к серверу
            if (ServerСonnectionVerification())
                // Проверка подключения к базе данных
                if (DataBaseСonnectionVerification("PublicCatering")) { MessageBox.Show("Сервер и база данных успешно проверены", "", MessageBoxButtons.OK, MessageBoxIcon.Information); Successful_Сonnection = true; }
                else { MessageBox.Show($"{ErrorsKods.Errors[0].Messege}", $"Ошибка. {ErrorsKods.Errors[0].Kod}", MessageBoxButtons.OK, MessageBoxIcon.Error); Successful_Сonnection = false; }
            else { MessageBox.Show($"{ErrorsKods.Errors[1].Messege}", $"Ошибка. {ErrorsKods.Errors[1].Kod}", MessageBoxButtons.OK, MessageBoxIcon.Error); Successful_Сonnection = false; }
        }

        /// <summary> Проверка подключения к серверу </summary>
        private static bool ServerСonnectionVerification()
        {
            try { SqlConnection Connection = new SqlConnection($@"Data Source={Properties.Settings.Default.ServerName};Integrated Security=True"); Connection.Open(); return true; }
            catch { return false; }
        }

        /// <summary> Проверка подключения к базе данных </summary>
        private static bool DataBaseСonnectionVerification(string DataBase)
        {
            try { SqlConnection Connection = new SqlConnection($@"Data Source={Properties.Settings.Default.ServerName};" + $"Initial Catalog=\"{DataBase}\";Integrated Security=True"); Connection.Open(); return true; }
            catch { return false; }
        }

        /// <summary> Исполнение хранимой процедуры для получения таблицы согласно запросу Select </summary>
        public static DataTable SELECT(string SQL_Command)
        {
            using (SqlConnection Connection = new SqlConnection($@"Data Source={Properties.Settings.Default.ServerName};Integrated Security=True"))
            {
                Connection.Open(); DataTable DATA = new DataTable();
                using (SqlCommand CMD = new SqlCommand(SQL_Command, Connection)) using (SqlDataReader Reader = CMD.ExecuteReader()) DATA.Load(Reader);
                Connection.Close(); return DATA;
            }
        }

        /// <summary> Создание локальной таблицы по предоставленным столбцам </summary>
        public static DataTable LocalDB(string[] Columns)
        { DataTable Data = new DataTable(); foreach (string Column in Columns) Data.Columns.Add(Column); return Data; }

        /// <summary> Добавление данных в локальную таблицу </summary>
        public static DataTable LocalDB(DataTable Data, string[] Line)
        { Data.Rows.Add(Line); return Data; }
    }
}