using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Item
{
    [XmlRoot("ItemValueDate", Namespace = Namespaces.niemCore)]
    public class ItemValueDate
    {
        public ItemValueDate()
        {
        }

        public ItemValueDate(string date)
        {
            Date = date;
        }

        [XmlElement("Date", Namespace = Namespaces.niemCore)]
        public string Date { get; set; }
    }
}