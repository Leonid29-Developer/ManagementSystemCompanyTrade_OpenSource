using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ManagementSystem_For_PublicCatering
{
    class AddImage
    {
        public AddImage()
        {
            // Сохранение изображения
            string filename = @"S:\Unrelated Programs\2023.06_Cashier\Program110\Cashier\bin\Product Photo\1000100020001.png"; byte[] imageData;
            using (FileStream fs = new FileStream(filename, FileMode.Open)) { imageData = new byte[fs.Length]; fs.Read(imageData, 0, imageData.Length); }

            using (SqlConnection SQL_Connection = new SqlConnection($@"Data Source={Properties.Settings.Default.ServerName};Integrated Security=True"))
            {
                SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                string Request = $"UPDATE [PublicCatering].[dbo].[Product Image] SET [Image] = @ImageData WHERE [Index] = '1000100020001';"; // SQL-запрос
                SQL_Command.Parameters.Add("@ImageData", SqlDbType.Image, 1000000); SQL_Command.Parameters["@ImageData"].Value = imageData;
                SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
            }
        }
    }
}