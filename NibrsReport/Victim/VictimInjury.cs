using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Victim
{
    [XmlRoot("VictimInjury", Namespace = Namespaces.niemCore)]
    public class VictimInjury
    {
        public VictimInjury()
        {
        }

        public VictimInjury(string categoryCode)
        {
            CategoryCode = categoryCode;
        }

        [XmlElement("InjuryCategoryCode", Namespace = Namespaces.justice)]
        public string CategoryCode { get; set; }
    }
}