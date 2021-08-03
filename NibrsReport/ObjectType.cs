using System.Xml.Serialization;
using NibrsModels.Constants;

namespace NibrsModels.NibrsReport
{
    public abstract class ObjectType
    {
        [XmlAttribute("id", Namespace = Namespaces.niemStructs)]
        public virtual string Id {get; set;}
        
        [XmlAttribute("ref", Namespace = Namespaces.niemStructs)]
        public virtual string Ref {get; set;}
        
        [XmlAttribute("metadata", Namespace = Namespaces.niemStructs)]
        public string Metadata {get; set;}
        
        [XmlAttribute("relationshipMetadata", Namespace = Namespaces.niemStructs)]
        public string RelationshipMetadata {get; set;}
    }
}