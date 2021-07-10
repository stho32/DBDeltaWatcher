using Xunit;

namespace DbDeltaWatcher.BL.Tests
{
    public class SchemaDifferenceDetectionTests
    {
        [Fact]
        public void when_source_and_target_are_the_same_no_changes_are_found()
        {
            Assert.True(false);
        }

        [Fact]
        public void when_source_has_more_columns_then_add_is_proposed()
        {
            Assert.True(false);
        }

        [Fact]
        public void when_source_has_less_columns_then_drop_is_proposed()
        {
            Assert.True(false);
        }
    }
}