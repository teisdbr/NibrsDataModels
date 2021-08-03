using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Item
{
    [XmlRoot("ItemValueAmount", Namespace = Namespaces.niemCore)]
    public class ItemValueAmount
    {
        public ItemValueAmount()
        {
        }

        public ItemValueAmount(int amount)
        {
            Amount = amount.ToString();
        }

        public ItemValueAmount(string amount)
        {
            Amount = amount;
        }

        [XmlElement("Amount", Namespace = Namespaces.niemCore)]
        public string Amount { get; set; }
    }
}