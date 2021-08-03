using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport.Misc
{
    [XmlRoot("OrganizationAugmentation", Namespace = Namespaces.justice)]
    public class OrganizationAugmentation
    {
        public OrganizationAugmentation()
        {
        }

        public OrganizationAugmentation(OrganizationORIIdentification orgOriId)
        {
            OrgOriId = orgOriId;
        }

        [XmlElement("OrganizationORIIdentification", Namespace = Namespaces.justice)]
        public OrganizationORIIdentification OrgOriId { get; set; }
    }
}