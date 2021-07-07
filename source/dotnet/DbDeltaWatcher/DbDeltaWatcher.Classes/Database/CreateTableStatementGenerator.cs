using System.Collections.Generic;
using System.Text;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database
{
    public class CreateTableStatementGenerator
    {
        private readonly ISqlDialect _sqlDialect;

        public CreateTableStatementGenerator(ISqlDialect sqlDialect)
        {
            _sqlDialect = sqlDialect;
        }
        
        public string Generate(string tableName, ISimplifiedColumnSchema[] simplifiedColumnSchemata)
        {
            var result = new StringBuilder();
            result.AppendLine(_sqlDialect.CreateTableStart(tableName));

            if (simplifiedColumnSchemata.Length > 0)
            {
                var columnDefinitionRows = new List<string>();

                for (var i = 0; i < simplifiedColumnSchemata.Length; i++)
                {
                    var columnSchema = simplifiedColumnSchemata[i];
                    columnDefinitionRows.Add("    " + _sqlDialect.ColumnDefinition(columnSchema));
                }

                result.AppendJoin(",\n", columnDefinitionRows);
                result.AppendLine("");
            }
            
            result.AppendLine(_sqlDialect.CreateTableEnd());
            return result.ToString();
        }
    }
}