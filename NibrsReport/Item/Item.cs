using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using NibrsModels.Constants;
using NibrsModels.Utility;

namespace NibrsModels.NibrsReport.Item
{
    [XmlRoot("Item", Namespace = Namespaces.niemCore)]
    public class Item : ObjectType, IComparable
    {
        [XmlElement("ItemStatus", Namespace = Namespaces.niemCore, Order = 1)]
        public ItemStatus Status { get; set; }

        [XmlElement("ItemValue", Namespace = Namespaces.niemCore, Order = 2)]
        public ItemValue Value { get; set; }

        [XmlElement("ItemCategoryNIBRSPropertyCategoryCode", Namespace = Namespaces.justice, Order = 3)]
        public string NibrsPropertyCategoryCode { get; set; }

        [XmlElement("ItemQuantity", Namespace = Namespaces.niemCore, Order = 4)]
        public string Quantity { get; set; }
        
        [XmlIgnore]
        public string ItemSequenceNumber => Id != null ? Regex.Match(Id, @"\d+")?.Value : Id;

        public Item() { }

        public Item(ItemStatus status, ItemValue value, string nibrsPropertyCategoryCode, int quantity, string id = null)
        {
            this.Status = status;
            this.Value = value;
            this.NibrsPropertyCategoryCode = nibrsPropertyCategoryCode;
            this.Quantity = quantity.ToString();
            this.Id = id;
        }

        public Item(string statusCode, string valueAmount, string valueDate, string nibrsPropCategCode, string quantity, string id = null)
        {
            this.Status = new ItemStatus(statusCode);
            if ((valueAmount != null || valueDate != null))
                this.Value = new ItemValue(valueAmount, valueDate);
            this.NibrsPropertyCategoryCode = nibrsPropCategCode;

            // Ignore quntity if ItemStatusCode is not Stolen or Recovered
            if (statusCode == ItemStatusCode.STOLEN.NibrsCode() || statusCode == ItemStatusCode.RECOVERED.NibrsCode())
            this.Quantity = quantity;
            this.Id = id;
        }

        public int CompareTo(object b)
        {
            if (b == null)
                throw new ArgumentNullException();

            var otherItem = b as Item;
            if (otherItem != null)
                return Convert.ToInt32(Value.ValueAmount.Amount) - Convert.ToInt32(otherItem.Value.ValueAmount.Amount);
            throw new ArgumentException("Object is not an Item.");
        }
    }
}