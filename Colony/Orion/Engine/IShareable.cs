namespace Orion.Engine
{
    public interface IShareable
    {
        Cache Cache { get; set; }
        void BuildHierarchy();
    }
}
