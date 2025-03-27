namespace _Project.CodeBase.Runtime.Common
{
    public interface IInitializable
    {
        public bool WasInitialized { get; }
        public void InitializeUnit();
    }
}