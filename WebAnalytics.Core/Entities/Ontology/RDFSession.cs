﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WebAnalytics.Core.Entities.Ontology
{
    [Serializable]
    [XmlType(TypeName = "Session", Namespace = RdfRoot.UsabilityNamespace)]
    public class RdfSession
    {
        public const string DateTimeFormat = "o";

        [XmlAttribute("ID", Form = XmlSchemaForm.Qualified)]
        public string Uid { get; set; }

        [XmlIgnore]
        public DateTimeOffset? StartDateTime { get; set; }

        [XmlAttribute("hasStartDateTime", Form = XmlSchemaForm.Qualified)]
        public string StartDateTimeString
        {
            get => StartDateTime?.ToString(DateTimeFormat);
            set => StartDateTime = string.IsNullOrEmpty(value) ? null : (DateTime?)DateTime.ParseExact(value, DateTimeFormat, CultureInfo.CurrentCulture);
        }

        [XmlIgnore]
        public DateTimeOffset? EndDateTime { get; set; }

        [XmlAttribute("hasEndDateTime", Form = XmlSchemaForm.Qualified)]
        public string EndDateTimeString
        {
            get => EndDateTime?.ToString(DateTimeFormat);
            set => EndDateTime = string.IsNullOrEmpty(value) ? null : (DateTime?)DateTime.ParseExact(value, DateTimeFormat, CultureInfo.CurrentCulture);
        }

        [XmlElement("contains")]
        public RegionList Contains { get; set; }
    }

    [Serializable]
    public class RegionList : BaseRdfCollection
    {
        [XmlElement("Region", Form = XmlSchemaForm.Qualified)]
        public List<RdfRegion> Regions { get; set; }
    }
}
