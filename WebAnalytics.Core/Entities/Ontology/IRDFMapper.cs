namespace WebAnalytics.Core.Entities.Ontology
{
    public interface IRdfMapper
    {
        RdfSession MapToRdf(Session session);

        Session MapFromRdf(RdfSession session);

        RdfSession[] MapFragmentsToRdf(RecordingFragment[] sessions);
    }
}