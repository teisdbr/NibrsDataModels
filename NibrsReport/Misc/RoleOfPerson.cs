using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Misc
{
    [XmlRoot("RoleOfPerson", Namespace = Namespaces.niemCore)]
    public class RoleOfPerson
    {
        public RoleOfPerson()
        {
        }

        public RoleOfPerson(string personId)
        {
            PersonId = personId;
        }

        [XmlAttribute("ref", Namespace = Namespaces.niemStructs)]
        public string PersonId { get; set; }
    }
}