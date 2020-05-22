using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WebAnalytics.Core.Entities.Ontology
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class RdfRegion
    {
        [XmlAttribute("hasName", Form = XmlSchemaForm.Qualified)]
        public string Name { get; set; }

        [XmlElement("contains", IsNullable = true)]
        public VariationsCollection Contains { get; set; }
    }

    [Serializable]
    public class VariationsCollection : BaseRdfCollection
    {
        [XmlElement("Variation", Namespace = RdfRoot.UsabilityNamespace)]
        public List<RdfVariation> Variations { get; set; }
    }
}
