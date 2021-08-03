using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.MessageMetadatas
{
    [XmlRoot("MessageIdentification", Namespace = Namespaces.cjis)]
    public class MessageIdentification
    {
        [XmlElement("IdentificationID", Namespace = Namespaces.niemCore, Order = 1)]
        public string IdentificationId { get; set; }
    }
}