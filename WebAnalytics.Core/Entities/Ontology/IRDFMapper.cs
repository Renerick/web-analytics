namespace WebAnalytics.Core.Entities.Ontology
{
    public interface IRdfMapper
    {
        RdfSession[] MapFragmentsToRdf(RecordingFragment[] sessions);
    }
}