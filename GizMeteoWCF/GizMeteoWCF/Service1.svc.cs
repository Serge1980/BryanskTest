using GizMeteoWCF.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GizMeteoWCF
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        CWtrContext db = new CWtrContext();

        public List<DateTime> GetDates()
        {
            List<DateTime> listOfDates = db.Database.SqlQuery<DateTime>("SELECT Date FROM city_weather group by Date").ToList();

            return listOfDates;
        }

        public List<CityWether> GetTempListOfCities(DateTime date)
        {
            List<CityWether> rezult = db.CityWeather.Where(x=>x.Date.Year==date.Year && x.Date.Month==date.Month && x.Date.Day==x.Date.Day && x.Date.Hour==date.Hour)
                .Select(x => new CityWether { Id = x.Id, Name=x.Cityes.City,Temp = x.Temp }).ToList();

            return rezult;
        }
    }
}
