using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Offense
{
    [XmlRoot("OffenseFactor", Namespace = Namespaces.justice)]
    public class OffenseFactor
    {
        public OffenseFactor()
        {
        }

        public OffenseFactor(string code)
        {
            Code = code;
        }

        [XmlElement("OffenseFactorCode", Namespace = Namespaces.justice)]
        public string Code { get; set; }
    }
}