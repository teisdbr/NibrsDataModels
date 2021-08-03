using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Item
{
    [XmlRoot("ItemStatus", Namespace = Namespaces.niemCore)]
    public class ItemStatus
    {
        public ItemStatus()
        {
        }

        public ItemStatus(string code)
        {
            Code = code;
        }

        [XmlElement("ItemStatusCode", Namespace = Namespaces.cjis)]
        public string Code { get; set; }
    }
}