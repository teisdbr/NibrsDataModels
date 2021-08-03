using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Person
{
    [XmlRoot("PersonAugmentation", Namespace = Namespaces.justice)]
    public class PersonAugmentation
    {
        public PersonAugmentation()
        {
        }

        public PersonAugmentation(string ageCode)
        {
            AgeCode = ageCode;
        }

        [XmlElement("PersonAgeCode", Namespace = Namespaces.cjisNibrs)]
        public string AgeCode { get; set; }
    }
}