using System;

namespace CryEngine.Projects.Game.Managers
{
    public interface IManager : IDisposable
    {
        public void Initialize();
        public void Shutdown();
    }
}