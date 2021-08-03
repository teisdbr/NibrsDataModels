using System.Xml.Serialization;
using NibrsModels.Constants;
using NibrsModels.NibrsReport.Misc;

namespace NibrsModels.NibrsReport.MessageMetadatas
{
    [XmlRoot("MessageSubmittingOrganization", Namespace = Namespaces.cjis)]
    public class MessageSubmittingOrganization
    {
        [XmlElement("OrganizationAugmentation", Namespace = Namespaces.justice, Order = 1)]
        public OrganizationAugmentation OrganizationAugmentation { get; set; }
    }
}