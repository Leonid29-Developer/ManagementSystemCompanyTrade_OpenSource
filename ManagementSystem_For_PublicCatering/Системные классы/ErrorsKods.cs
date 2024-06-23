using System.Collections.Generic;

namespace ManagementSystem_For_PublicCatering
{
    class ErrorsKods
    {
        public static List<ErrorsKods> Errors { get; private set; }

        public string Kod { get; private set; }
        public string Messege { get; private set; }

        public ErrorsKods(string kod, string messege) { Kod = kod; Messege = messege; }

        public static void CreateErrorsKods()
        {
            List<ErrorsKods> Kod = new List<ErrorsKods> { }; Errors = Kod;
            Kod.Add(new ErrorsKods("SQL-148", "Ошибка подключения. База данных не доступна"));
            Kod.Add(new ErrorsKods("SQL-645", "Ошибка подключения. Сервер недоступен"));
            Kod.Add(new ErrorsKods("DE-354", "Ошибка ввода данных. Достигнута максимальная длина"));
            Kod.Add(new ErrorsKods("DE-887", "Ошибка ввода данных. Неверно введены данные"));
        }
    }
}