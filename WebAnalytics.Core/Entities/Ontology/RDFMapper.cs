using System.Collections.Generic;
using System.Linq;

namespace WebAnalytics.Core.Entities.Ontology
{
    public class RdfMapper : IRdfMapper
    {
        public RdfSession MapToRdf(Session session)
        {
            throw new System.NotImplementedException();
        }

        public Session MapFromRdf(RdfSession session)
        {
            throw new System.NotImplementedException();
        }

        public RdfSession[] MapFragmentsToRdf(RecordingFragment[] sessions)
        {
            return sessions.Select(s => new RdfSession()
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
                                        Width = 1920,
                                        Contains = new EventsCollection()
                                        {
                                            Events = s.Frames.Frames.SelectMany(ConvertFrameToRdfEvent).ToList()
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }).ToArray();
        }

        private IEnumerable<RdfEvent> ConvertFrameToRdfEvent(Frame frame, int i)
        {
            if (frame.MouseX.HasValue && frame.MouseY.HasValue)
                yield return new MoveMouseEvent()
                {
                    Name = $"Click_{i}_{frame.Target}_{frame.MouseX}_{frame.MouseY}",
                    InRegionX = frame.MouseX.Value,
                    InRegionY = frame.MouseY.Value
                };
            if (frame.ClickX.HasValue && frame.ClickY.HasValue)
                yield return new RdfSingleClickMouseEvent()
                {
                    Name = $"Click_{i}_{frame.Target}_{frame.MouseX}_{frame.MouseY}",
                    InRegionX = frame.ClickX.Value,
                    InRegionY = frame.ClickY.Value
                };
        }
    }
}