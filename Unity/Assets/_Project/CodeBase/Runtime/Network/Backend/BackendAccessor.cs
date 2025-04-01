namespace _Project.CodeBase.Runtime.Network.Backend
{
    public class BackendAccessor
    {
        protected readonly BackendSettings BackendSettings;

        public BackendAccessor(BackendSettings backendSettings)
        {
            BackendSettings= backendSettings;
        }

        protected string GetURI(string path)
        {
            return $"http://{BackendSettings.Address}:{BackendSettings.Port}/{path}";
        }
    }
}