using System.Xml.Serialization;
using NibrsModels.Constants;
using NibrsModels.NibrsReport.Misc;

namespace NibrsModels.NibrsReport.ReportHeader
{
    public class ReportingAgency
    {
        public ReportingAgency()
        {
        }

        public ReportingAgency(OrganizationAugmentation orgAugmentation)
        {
            OrgAugmentation = orgAugmentation;
        }

        [XmlElement("OrganizationAugmentation", Namespace = Namespaces.justice)]
        public OrganizationAugmentation OrgAugmentation { get; set; }
    }
}