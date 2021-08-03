using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Offense
{
    [XmlRoot("OffenseEntryPoint", Namespace = Namespaces.justice)]
    public class OffenseEntryPoint
    {
        public OffenseEntryPoint()
        {
        }

        public OffenseEntryPoint(string passagePointMethodCode)
        {
            PassagePointMethodCode = passagePointMethodCode;
        }

        [XmlElement("PassagePointMethodCode", Namespace = Namespaces.justice)]
        public string PassagePointMethodCode { get; set; }
    }
}