using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WebAnalytics.Core.Entities.Ontology
{
    [XmlType(TypeName = "Variation", Namespace = RdfRoot.UsabilityNamespace)]
    public class RdfVariation
    {
        [XmlAttribute("hasWidth", Form = XmlSchemaForm.Qualified)]
        public double Width { get; set; }

        [XmlAttribute("hasHeight", Form = XmlSchemaForm.Qualified)]
        public double Height { get; set; }

        [XmlAttribute("hasName", Form = XmlSchemaForm.Qualified)]
        public string Name { get; set; }

        [XmlIgnore]
        public byte[] Data { get; set; }

        [XmlElement("hasImage")]
        public XmlCDataSection DataSection
        {
            get
            {
                var dataString = Data == null ? string.Empty : Convert.ToBase64String(Data);
                return new XmlDocument().CreateCDataSection(dataString);
            }
            set
            {
                var dataString = value.Value;
                Data = Convert.FromBase64String(dataString);
            }
        }

        [XmlElement("contains")]
        public EventsCollection Contains { get; set; }
    }

    [Serializable]
    public class EventsCollection : BaseRdfCollection
    {
        [XmlElement("SingleClickMouseEvent", typeof(RdfSingleClickMouseEvent))]
        [XmlElement("MoveMouseEvent", typeof(RdfMoveMouseEvent))]
        [XmlElement("CommandEvent", typeof(RdfCommandEvent))]
        public List<RdfEvent> Events { get; set; }
    }
}