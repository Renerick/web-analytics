using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Extensions.Caching.Distributed;

namespace WebAnalytics.Core.Entities.Ontology
{
    public class RdfMapper : IRdfMapper
    {
        private IDistributedCache _cache;

        public RdfMapper(IDistributedCache cache)
        {
            _cache = cache;
        }

        public RdfSession[] MapFragmentsToRdf(RecordingFragment[] sessions)
        {
            var serializer = new XmlSerializer(typeof(RdfSession));
            return sessions.Select(s =>
            {
                var result = _cache.GetString(s.SessionId + "_" + s.FragmentId);
                if (result != null) return (RdfSession) serializer.Deserialize(new StringReader(result));

                var rdfSession = new RdfSession()
                {
                    EndDateTime = s.Time,
                    Uid = s.SessionId,
                    Contains = new RegionList()
                    {
                        Regions = new List<RdfRegion>()
                        {
                            new RdfRegion()
                            {
                                Name = "Main",
                                Contains = new VariationsCollection()
                                {
                                    Variations = new List<RdfVariation>()
                                    {
                                        new RdfVariation()
                                        {
                                            Width = s.Frames.Frames[0].Width.HasValue
                                                ? s.Frames.Frames[0].Width.Value
                                                : 0,
                                            Contains = new EventsCollection()
                                            {
                                                Events = s.Frames.Frames
                                                          .SelectMany(
                                                              ConvertFrameToRdfEvent)
                                                          .ToList()
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };

                var stringWriter = new StringWriter();
                serializer.Serialize(stringWriter, rdfSession);
                _cache.SetString(s.SessionId + "_" + s.FragmentId, stringWriter.ToString(),
                    new DistributedCacheEntryOptions() {AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)});
                return rdfSession;
            }).ToArray();
        }

        private IEnumerable<RdfEvent> ConvertFrameToRdfEvent(Frame frame, int i)
        {
            if (frame.MouseX.HasValue && frame.MouseY.HasValue)
                yield return new RdfMoveMouseEvent()
                {
                    Name = $"Click_{i}_{frame.Target}_{frame.MouseX}_{frame.MouseY}",
                    InRegionX = frame.MouseX.Value + frame.ScrollX ?? 0,
                    InRegionY = frame.MouseY.Value + frame.ScrollY ?? 0
                };
            if (frame.ClickX.HasValue && frame.ClickY.HasValue)
                yield return new RdfSingleClickMouseEvent()
                {
                    Name = $"Click_{i}_{frame.Target}_{frame.MouseX}_{frame.MouseY}",
                    InRegionX = frame.ClickX.Value + frame.ScrollX ?? 0,
                    InRegionY = frame.ClickY.Value + frame.ScrollY ?? 0
                };
        }
    }
}