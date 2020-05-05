using System;
using CryEngine.Game.Helpers;
using CryEngine.Game.Logic.Monitoring;

namespace CryEngine.Projects.Game.Managers
{
	public class MonitoringManager : IManager, IGameUpdateReceiver
	{
		private static MonitoringManager _instance;

		private FpsUiHandler _fpsUiHandler;
		private BuildVersionUiHandler _buildVersionUiHandler;

		public void Initialize()
		{
			if(_instance != null || Engine.IsDedicatedServer)
				return;
			
			_fpsUiHandler = new FpsUiHandler();
			_buildVersionUiHandler = new BuildVersionUiHandler();
			

			Engine.EngineUnloading += DestroyUi;
			Engine.EngineReloaded += CreateUi;
			CreateUi();

			GameFramework.RegisterForUpdate(this);
		}

		public void Shutdown()
		{
			_instance?.Dispose();
			_instance = null;
		}

		public virtual void OnUpdate()
		{
			_fpsUiHandler.UpdateOnCanvas();
			_buildVersionUiHandler.UpdateOnCanvas();
		}

		private void CreateUi()
		{
			_fpsUiHandler.CreateOnCanvas();
			_buildVersionUiHandler.CreateOnCanvas();
		}

		private void DestroyUi()
		{
			_fpsUiHandler.DestroyOnCanvas();
			_buildVersionUiHandler.DestroyOnCanvas();
		}

		public void Dispose()
		{
			if(Engine.IsDedicatedServer)
			{
				return;
			}

			GameFramework.UnregisterFromUpdate(this);
		}
	}
}
