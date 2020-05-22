using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebAnalytics.Core.Entities.Ontology
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = RdfRoot.RdfNamespace, ElementName = "RDF")]
    public class RdfRoot
    {
        [XmlIgnore]
        public const string UsabilityNamespace = "https://w3id.org/usability#";

        [XmlIgnore]
        public const string RdfNamespace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";

        [XmlElement(Namespace = UsabilityNamespace)]
        public List<RdfSession> Session { get; set; }

        private XmlSerializerNamespaces _namespaces;

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces
        {
            get
            {
                if (_namespaces == null)
                {
                    _namespaces = new XmlSerializerNamespaces();
                    _namespaces.Add("us", RdfRoot.UsabilityNamespace);
                    _namespaces.Add("rdf", RdfRoot.RdfNamespace);
                }

                return _namespaces;
            }
            set => _namespaces = value;
        }
    }
}