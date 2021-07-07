namespace DbDeltaWatcher.BL.IntegrationTests.Interfaces
{
    public interface IDatabaseSupportTests
    {
        void a_connection_can_be_established();
        void the_schemaprovider_does_its_job();
        void the_schemaprovider_can_grab_a_table_structure();
    }
}