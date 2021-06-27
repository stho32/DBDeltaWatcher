using DbDeltaWatcher.Interfaces.Entities;

namespace DbDeltaWatcher.Classes.Entities
{
    public class TransformationDescription : ITransformationDescription
    {
        public string OnDeletedRow { get; }
        public string OnAddedRow { get; }
        public string OnChangedRow { get; }

        public TransformationDescription(string onDeletedRow, string onAddedRow, string onChangedRow)
        {
            OnDeletedRow = onDeletedRow;
            OnAddedRow = onAddedRow;
            OnChangedRow = onChangedRow;
        }
    }
}