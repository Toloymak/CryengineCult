using System;
using CryEngine;

namespace CryEngine.Game
{
	[EntityComponent(Guid="dd6a490f-7333-8bdb-0fca-88917d44b483")]
	public class TestComponent : EntityComponent
	{
		/// <summary>
		/// Called at the start of the game.
		/// </summary>
		protected override void OnGameplayStart()
		{
		}

		/// <summary>
		/// Called once every frame when the game is running.
		/// </summary>
		/// <param name="frameTime">The time difference between this and the previous frame.</param>
		protected override void OnUpdate(float frameTime)
		{
			
		}
	}
}