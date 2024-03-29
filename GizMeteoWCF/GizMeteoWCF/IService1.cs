﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GizMeteoWCF
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<CityWether> GetTempListOfCities(DateTime date);

        [OperationContract]
        List<DateTime> GetDates();

        // TODO: Добавьте здесь операции служб
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class CityWether
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Temp { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
