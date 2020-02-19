using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Pract2
{
    class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                InfoSoftware(@"C:\Users\Ильдар\Desktop\input.txt");
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("Error");
            }
        }
        /// <summary>
        /// Этот метод считывает данные с файла в лист
        /// </summary>
        /// <param name="txt">Путь к файлу</param>
        /// <param name="list">Лист для хранения полученных данных</param>
        /// <returns>Лист для хранения полученных данных</returns>
        static List<string> Input(string txt, List<string> list)
        {            
            using (StreamReader sr = new StreamReader(txt, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            return list;
        }
        /// <summary>
        /// Этот метод определяет к какому классу относится данное ПО
        /// </summary>
        /// <param name="array">Лист для хранения данных о ПО</param>
        /// <param name="s">Строка с информации о текущем ПО</param>
        static void ReadInfo(ArrayList array, string s)
        {
            if (s.Substring(0, 9) == "Свободное")
            {
                int i = 0;
                string type = "";
                while (s[i] != ',')
                {
                    type += s[i];
                    i++;
                }
                i++;
                i++;
                string name = "";
                while(s[i] != ',')
                {
                    name += s[i];
                    i++;
                }
                i++;
                i++;
                string manufacturer = "";
                while (i < s.Length)
                {
                    manufacturer += s[i];
                    i++;
                }
                array.Add(new FreeSoft { name = name, manufacturer = manufacturer, type = type });                
            }
            else
            {
                if (s.Substring(0, 18) == "Условно-бесплатное")
                {
                    int i = 0;
                    string type = "";
                    while (s[i] != ',')
                    {
                        type += s[i];
                        i++;
                    }
                    i++;
                    i++;
                    string name = "";
                    while (s[i] != ',')
                    {
                        name += s[i];
                        i++;
                    }
                    i++;
                    i++;
                    string manufacturer = "";
                    while (s[i] != ',')
                    {
                        manufacturer += s[i];
                        i++;
                    }
                    i++;
                    i++;
                    string dateSet = "";
                    while (s[i] != ',')
                    {
                        dateSet += s[i];
                        i++;
                    }
                    i++;
                    i++;
                    string dateTrial = "";
                    while (i < s.Length)
                    {
                        dateTrial += s[i];
                        i++;
                    }
                    array.Add(new FreeCondSoft { name = name, manufacturer = manufacturer, type = type, dateSet = DateTime.Parse(dateSet), dateTrial = Convert.ToInt32(dateTrial) });
                }
                else
                {
                    if (s.Substring(0, 12) == "Коммерческое")
                    {
                        int i = 0;
                        string type = "";
                        while (s[i] != ',')
                        {
                            type += s[i];
                            i++;
                        }
                        i++;
                        i++;
                        string name = "";
                        while (s[i] != ',')
                        {
                            name += s[i];
                            i++;
                        }
                        i++;
                        i++;
                        string manufacturer = "";
                        while (s[i] != ',')
                        {
                            manufacturer += s[i];
                            i++;
                        }
                        i++;
                        i++;
                        string dateSet = "";
                        while (s[i] != ',')
                        {
                            dateSet += s[i];
                            i++;
                        }
                        i++;
                        i++;
                        string dateUsed = "";
                        while (i < s.Length)
                        {
                            dateUsed += s[i];
                            i++;
                        }
                        array.Add(new CommercialSoft { name = name, manufacturer = manufacturer, type = type, dateSet = DateTime.Parse(dateSet), dateUsed = Convert.ToInt32(dateUsed) });
                    }               
                }
            }
        }
        /// <summary>
        /// Этот метод выводит информацию о всех ПО, и выводитсписок ПО допустимых к использованию
        /// </summary>
        /// <param name="txt">Путь к файлу</param>
        static int InfoSoftware(string txt)
        {
            List<string> list = new List<string>();
            list = Input(txt, list);
            int n = Convert.ToInt32(list[0]);
            ArrayList array = new ArrayList();
            for (int i = 1; i < n+1; i++) {
                string line = list[i];
                ReadInfo(array, line);                
            }
            foreach (object cl in array)
            {
                if (cl is FreeSoft)
                {
                    Console.WriteLine(((FreeSoft)cl).type + ' ' + ((FreeSoft)cl).name + ' ' + ((FreeSoft)cl).manufacturer);
                }
                if (cl is FreeCondSoft)
                {
                    Console.WriteLine(((FreeCondSoft)cl).type + ' ' + ((FreeCondSoft)cl).name + ' ' + ((FreeCondSoft)cl).manufacturer + ' ' + ((FreeCondSoft)cl).dateSet.ToShortDateString() + ' ' + ((FreeCondSoft)cl).dateTrial);
                }
                if (cl is CommercialSoft)
                {
                    Console.WriteLine(((CommercialSoft)cl).type + ' ' + ((CommercialSoft)cl).name + ' ' + ((CommercialSoft)cl).manufacturer + ' ' + ((CommercialSoft)cl).dateSet.ToShortDateString() + ' ' + ((CommercialSoft)cl).dateUsed);
                }
            }
            Console.WriteLine("ПО допустимое к использованию:");
            foreach (object cl in array)
            {
                if (cl is FreeSoft)
                {
                    Console.WriteLine(((FreeSoft)cl).name);
                }
                if (cl is FreeCondSoft && (((FreeCondSoft)cl).dateSet).AddDays(((FreeCondSoft)cl).dateTrial) >= DateTime.Today)
                {
                    Console.WriteLine(((FreeCondSoft)cl).name);
                }
                if (cl is CommercialSoft && (((CommercialSoft)cl).dateSet).AddDays(((CommercialSoft)cl).dateUsed) >= DateTime.Today)
                {
                    Console.WriteLine(((CommercialSoft)cl).name);
                }
            }
            return 0;
        }
    }
    /// <summary>
    /// Этот класс содержит тип, имя и производителя ПО
    /// </summary>
    abstract class Software
    {
        public string type { get; set; }
        public string name { get; set; }
        public string manufacturer { get; set; }

    }
    /// <summary>
    /// Этот класс содержит тип, имя и производителя ПО
    /// </summary>
    class FreeSoft : Software
    {
    }
    /// <summary>
    /// Этот класс содержит тип, имя и производителя ПО + дата установки и время работы триал версии
    /// </summary>
    class FreeCondSoft : Software
    {
        public DateTime dateSet { get; set; }
        public int dateTrial { get; set; }
    }
    /// <summary>
    /// Этот класс содержит тип, имя и производителя ПО + дата установки и время работы ПО
    /// </summary>
    class CommercialSoft : Software
    {
        public DateTime dateSet { get; set; }
        public int dateUsed { get; set; }
    }
}
