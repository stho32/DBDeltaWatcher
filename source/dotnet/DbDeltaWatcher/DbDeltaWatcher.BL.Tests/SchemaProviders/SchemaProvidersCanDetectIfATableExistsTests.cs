using DbDeltaWatcher.BL.Tests.Mocks;
using Xunit;

namespace DbDeltaWatcher.BL.Tests.SchemaProviders
{
    public class SchemaProvidersCanDetectIfATableExistsTests
    {
        [Fact]
        public void When_the_table_exists_it_returns_true()
        {
            var schemaProvider = new MockSchemaProvider(new[] { "Brian" });
            Assert.True(schemaProvider.TableExists("Brian"));
        }

        [Fact]
        public void When_the_table_does_not_exist_it_returns_false()
        {
            var schemaProvider = new MockSchemaProvider(new[] { "Brian" });
            Assert.False(schemaProvider.TableExists("Rudi"));
        }
    }
}