using System;

namespace CryEngine.Projects.Game.Managers
{
    public interface IManager : IDisposable
    {
        void Initialize();
        void Shutdown();
    }
}