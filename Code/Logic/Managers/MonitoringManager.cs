using System;
using CryEngine.Game.Helpers;
using CryEngine.Game.Logic.Monitoring;

namespace CryEngine.Projects.Game.Managers
{
	public class MonitoringManager : IManager, IGameUpdateReceiver
	{
		private static MonitoringManager _instance;

		private FpsUiLogic _fpsUiLogic;
		private BuildVersionUiLogic _buildVersionUiLogic;

		public void Initialize()
		{
			if(_instance != null || Engine.IsDedicatedServer)
				return;
			
			_fpsUiLogic = new FpsUiLogic();
			_buildVersionUiLogic = new BuildVersionUiLogic();
			

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
			_fpsUiLogic.UpdateOnCanvas();
			_buildVersionUiLogic.UpdateOnCanvas();
		}

		private void CreateUi()
		{
			_fpsUiLogic.CreateOnCanvas();
			_buildVersionUiLogic.CreateOnCanvas();
		}

		private void DestroyUi()
		{
			_fpsUiLogic.DestroyOnCanvas();
			_buildVersionUiLogic.DestroyOnCanvas();
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
