using System.Xml.Serialization;
using NibrsModels.Constants;
using NibrsModels.NibrsReport.MessageMetadatas;

namespace NibrsModels.NibrsReport
{
    [XmlRoot("MessageMetadata", Namespace = Namespaces.cjis)]
    public class MessageMetadata : INibrsSerializable

    {
        [XmlElement("MessageDateTime", Namespace = Namespaces.cjis, Order = 1)]
        public string MessageDateTime { get; set; }

        [XmlElement("MessageIdentification", Namespace = Namespaces.cjis, Order = 2)]
        public MessageIdentification MessageIdentification { get; set; }


        [XmlElement("MessageImplementationVersion", Namespace = Namespaces.cjis, Order = 3)]
        public float Version { get; set; }


        [XmlElement("MessageSubmittingOrganization", Namespace = Namespaces.cjis, Order = 4)]
        public MessageSubmittingOrganization MessageSubmittingOrganization { get; set; }
    }
}