namespace CryEngine.Game.Logic.Monitoring
{
    public interface IMonitoringUiHandler
    {
        void UpdateOnCanvas();
        void CreateOnCanvas();
        void DestroyOnCanvas();
    }
}