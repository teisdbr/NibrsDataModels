using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Misc
{
    [XmlRoot("ActivityIdentification", Namespace = Namespaces.niemCore)]
    public class ActivityIdentification
    {
        public ActivityIdentification()
        {
        }

        public ActivityIdentification(string id)
        {
            Id = id;
        }

        [XmlElement("IdentificationID", Namespace = Namespaces.niemCore)]
        public string Id { get; set; }
    }
}