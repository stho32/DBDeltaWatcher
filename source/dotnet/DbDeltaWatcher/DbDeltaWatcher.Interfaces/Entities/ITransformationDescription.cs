namespace DbDeltaWatcher.Interfaces.Entities
{
    public interface ITransformationDescription
    {
        string OnDeletedRow { get; }
        string OnAddedRow { get; }
        string OnChangedRow { get; }
    }
}