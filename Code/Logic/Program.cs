// Copyright 2001-2019 Crytek GmbH / Crytek Group. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using CryEngine.Projects.Game.Managers;

namespace CryEngine.Game
{
	/// <summary>
	/// Add-in entry point will be re-instantiated in runtime, whenever the assembly is updated (e.g. Re-compiled)
	/// </summary>
	public class MyPlugin : ICryEnginePlugin
	{
		public override void Initialize()
		{
			foreach (var manager in GetManagers)
			{
				manager.Initialize();
			}
		}

		public override void Shutdown()
		{
			foreach (var manager in GetManagers)
			{
				manager.Shutdown();
			}
		}

		private IList<IManager> GetManagers =>
			new List<IManager>()
			{
				new MonitoringManager(),
				new KeyControlManager(),
				new CameraManager()
			};
	}
}
