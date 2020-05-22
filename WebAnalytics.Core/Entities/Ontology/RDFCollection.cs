using System;
using System.Xml.Serialization;

namespace WebAnalytics.Core.Entities.Ontology
{
    [Serializable]
    [XmlType(Namespace = RdfRoot.UsabilityNamespace)]
    public class BaseRdfCollection
    {
        [XmlAttribute("parseType", Namespace = RdfRoot.RdfNamespace)]
        public string RdfParseType { get; set; } = "Collection";
    }
}