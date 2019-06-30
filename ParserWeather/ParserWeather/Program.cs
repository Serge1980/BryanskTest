using CsQuery;
using ParserWeather.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserWeather
{
    class Program
    {
        static void Main(string[] args)
        {
            CWtrContext db = new CWtrContext();
            string url = "https://yandex.ru/pogoda/region/225";

            if (db.Cityes.Count()==0)
            {
                FillDbCities(url);
            }

            ParseTemperature(url);

            Console.WriteLine("Все процессы завершены");
            Console.ReadLine();
        }

        static private void FillDbCities(string url)
        {
            CWtrContext db = new CWtrContext();

            CQ dom = new CQ();
            dom = CQ.CreateFromUrl(url);

            CQ CityModuls = dom[".place-list"];

            foreach (var modul in CityModuls)
            {
                CQ Modul = modul.Render();

                CQ cityListCQ = Modul["a"];

                List<Cityes> listCities = new List<Cityes>();

                for (int i = 0; i < cityListCQ.Count(); i++)
                {

                    CQ name = cityListCQ[i].InnerText;
                    Console.WriteLine(name.ToString());
                    Cityes city = new Cityes();
                    city.City = name.ToString();
                    listCities.Add(city);

                }

                db.Cityes.AddRange(listCities);
                db.SaveChanges();

            }
        }

        static private void ParseTemperature(string url)
        {
            CWtrContext db = new CWtrContext();

            CQ dom = new CQ();
            dom = CQ.CreateFromUrl(url);

            CQ CityModuls = dom[".place-list"];

            List<City_Weather> listTemp = new List<City_Weather>();

            //Заранее объявим дату - чтобы была одинакова для всех записей спарсенных в этом цикле-по ней будем выцеплять данные сервисом.
            DateTime date = DateTime.Now;

            foreach (var modul in CityModuls)
            {
                CQ Modul = modul.Render();

                CQ tcListCQ = Modul["li"];

                for (int i = 0; i < tcListCQ.Count(); i++)
                {
                    CQ item = tcListCQ[i].Render();

                    CQ t1 = item.Find(".place-list__item-temp");
                    CQ t2 = item.Find(".place-list__item-name");

                    string temp = t1.Text();
                    string name = t2.Text();

                    City_Weather ctWtr= new City_Weather();

                    ctWtr.CityId = db.Cityes.FirstOrDefault(x => x.City.Equals(name)).Id;
                    ctWtr.Date = date;
                    ctWtr.Temp = temp;

                    listTemp.Add(ctWtr);

                    //Console.WriteLine(temp+" "+ name);
                    //Console.WriteLine("3333");
                }
            }

            db.CityWeather.AddRange(listTemp);
            db.SaveChanges();
        }

    }
}