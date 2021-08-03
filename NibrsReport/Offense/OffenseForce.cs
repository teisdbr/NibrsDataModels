using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Offense
{
    [XmlRoot("OffenseForce", Namespace = Namespaces.justice)]
    public class OffenseForce
    {
        public OffenseForce()
        {
        }

        public OffenseForce(string categoryCode)
        {
            CategoryCode = categoryCode;
        }

        [XmlElement("ForceCategoryCode", Namespace = Namespaces.justice)]
        public string CategoryCode { get; set; }
    }
}