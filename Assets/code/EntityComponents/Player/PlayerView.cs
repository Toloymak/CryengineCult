// Copyright 2001-2016 Crytek GmbH / Crytek Group. All rights reserved.

using System;

namespace CryEngine.Game
{
	public class PlayerView
	{
		private readonly Player _player;
		private readonly Entity _cameraPivot;

		public PlayerView(Player player)
		{
			if(player == null)
			{
				throw new ArgumentNullException(nameof(player));
			}
			_player = player;
			_cameraPivot = Entity.Spawn("CameraPivot", Vector3.Zero, Quaternion.Identity, Vector3.One);
		}

		public Quaternion UpdateView(float frameTime, Vector2 rotationDelta)
		{
			_cameraPivot.Position = _player.Entity.Position;
			Quaternion rotation = _cameraPivot.Rotation;
			float yawSpeed = _player.RotationSpeedYaw;
			float pitchSpeed = _player.RotationSpeedPitch;
			float pitchMin = _player.RotationLimitsMinPitch;
			float pitchMax = _player.RotationLimitsMaxPitch;
			float eyeHeight = _player.Height;

			//Invert the rotation to have proper third-person camera-control.
			rotationDelta = -rotationDelta;

			var ypr = rotation.YawPitchRoll;

			ypr.X += rotationDelta.X * yawSpeed;

			float pitchDelta = rotationDelta.Y * pitchSpeed;
			ypr.Y = MathHelpers.Clamp(ypr.Y + pitchDelta, pitchMin, pitchMax);

			ypr.Z = 0;

			rotation.YawPitchRoll = ypr;
			_cameraPivot.Rotation = rotation;
			
			return rotation;
		}

		public void Deinitialize()
		{
			if(_cameraPivot != null)
			{
				_cameraPivot.Remove();
			}
		}
	}
}

