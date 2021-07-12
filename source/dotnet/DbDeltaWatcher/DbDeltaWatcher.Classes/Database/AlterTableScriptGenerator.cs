using System;
using System.Text;
using DbDeltaWatcher.Interfaces.Database;
using DbDeltaWatcher.Interfaces.Database.SchemaProviders;

namespace DbDeltaWatcher.Classes.Database
{
    public class AlterTableScriptGenerator
    {
        private readonly ISqlDialect _sqlDialect;

        public AlterTableScriptGenerator(ISqlDialect sqlDialect)
        {
            _sqlDialect = sqlDialect;
        }

        public string MigrationSql(ISimplifiedTableSchemaChanges differences)
        {
            var result = new StringBuilder();

            foreach (var change in differences.Changes)
            {
                switch (change.TypeOfChange)
                {
                    case SimplifiedTableSchemaChangeEnum.AddColumn:
                        result.AppendLine(_sqlDialect.AddColumnToTable(
                            differences.TableName,
                            change.ColumnSchema));
                        break;
                    case SimplifiedTableSchemaChangeEnum.RemoveColumn:
                        result.AppendLine(_sqlDialect.RemoveColumnFromTable(
                            differences.TableName,
                            change.ColumnSchema));
                        break;
                    case SimplifiedTableSchemaChangeEnum.ChangeDataType:
                        result.AppendLine(_sqlDialect.AlterDataTypeOfColumn(
                            differences.TableName,
                            change.ColumnSchema));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return result.ToString();
        }
    }
}