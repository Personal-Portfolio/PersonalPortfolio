using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml;

namespace PersonalPortfolio.DataProviders.Cbr
{
    [ServiceContract(Namespace = "http://web.cbr.ru/", ConfigurationName = "CbrProvider.DailyInfoSoap")]
    internal interface ICbrClient
    {
        [OperationContract(Action = "http://web.cbr.ru/GetCursOnDateXML", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        Task<XmlNode> GetCursOnDateXMLAsync(DateTime On_date);
    }
}