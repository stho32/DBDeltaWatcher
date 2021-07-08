namespace DbDeltaWatcher.BL.Tests
{
    public interface ICreateTableStatementGeneratorTests
    {
        void can_generate_an_empty_create_table_statement();
        void can_add_code_for_a_primary_key_column();
        void the_code_for_the_testtable_is_generated_correctly();
    }
}