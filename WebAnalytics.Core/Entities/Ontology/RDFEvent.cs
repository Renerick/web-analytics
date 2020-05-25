using System;
using System.Globalization;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WebAnalytics.Core.Entities.Ontology
{
    [Serializable]
    [XmlType(TypeName = "Event", Namespace = RdfRoot.UsabilityNamespace)]
    public class RdfEvent
    {
        public const string DateTimeFormat = "o";

        [XmlIgnore]
        public DateTimeOffset? DateTime { get; set; }

        [XmlAttribute("hasDateTime", Form = XmlSchemaForm.Qualified)]
        public string UtcDateTimeString
        {
            get => DateTime?.ToString(DateTimeFormat);
            set => DateTime = string.IsNullOrEmpty(value)
                ? null
                : (DateTimeOffset?) DateTimeOffset.ParseExact(value, DateTimeFormat, CultureInfo.CurrentCulture);
        }

        [XmlIgnore]
        public int InRegionX { get; set; }

        [XmlAttribute("hasInRegionX", Form = XmlSchemaForm.Qualified)]
        public string InRegionXString
        {
            get => InRegionX.ToString();
            set => InRegionX = int.Parse(value);
        }

        [XmlIgnore]
        public int InRegionY { get; set; }

        [XmlAttribute("hasInRegionY", Form = XmlSchemaForm.Qualified)]
        public string InRegionYString
        {
            get => InRegionY.ToString();
            set =>  InRegionY = int.Parse(value);
        }

        [XmlAttribute("hasName", Form = XmlSchemaForm.Qualified)]
        public string Name { get; set; }
    }

    [XmlType(TypeName = "SingleClickMouseEvent")]
    public class RdfSingleClickMouseEvent: RdfEvent
    {

    }

    [XmlType(TypeName = "MoveMouseEvent")]
    public class RdfMoveMouseEvent: RdfEvent
    {

    }

    [XmlType(TypeName = "CommandEvent")]
    public class RdfCommandEvent : RdfEvent
    {

    }
}