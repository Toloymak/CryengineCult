// Copyright 2001-2016 Crytek GmbH / Crytek Group. All rights reserved.

using System.Diagnostics;
using CryEngine.Game.Helpers;
using CryEngine.Game.Intefaces;
using CryEngine.Game.Weapons;
using CryEngine.Projects.Game.Storage;

namespace CryEngine.Game
{
	[EntityComponent(Guid = "2d0518e2-022a-a22f-4195-62c7eb1be031")]
	public class Player : EntityComponent, IUnit
	{
		public enum GeometrySlots
		{
			ThirdPerson = 0
		}

		private const string PlayerGeometryUrl = "Objects/Characters/SampleCharacter/thirdperson.cdf";

		public Vector2 Movement = Vector2.Zero;
		public Vector2 RotationMovement = Vector2.Zero;

		[SerializeValue]
		private float _mass = 90.0f;
		[SerializeValue]
		private float _height = 0.935f;
		[SerializeValue]
		private float _airResistance = 0.0f;

		// The ActionHandler is marked as SerializeValue so the old reference to the ActionHandler can be disposed after a reload.
		[SerializeValue]
		private DefaultCharacterAnimator _animator;

		private PlayerView _playerView;
		private PlayerAnimations _animations;

		private PlayerActionHandler _playerActionHandler;

		public BaseWeapon Weapon { get; private set; }

		/// <summary>
		/// Mass of the player entity in kg.
		/// </summary>
		[EntityProperty(EntityPropertyType.Primitive, "Mass of the player entity in kg.")]
		public float Mass
		{
			get
			{
				return _mass;
			}
			set
			{
				_mass = value;
				Physicalize();
			}
		}

		/// <summary>
		/// The air-resistance of the player. Higher air-resistance will make the player float when falling down.
		/// </summary>
		[EntityProperty(EntityPropertyType.Primitive, "Air resistance of the player entity. Higher air-resistance will make the player float when falling down.")]
		public float AirResistance
		{
			get
			{
				return _airResistance;
			}
			set
			{
				_airResistance = value;
				Physicalize();
			}
		}

		/// <summary>
		/// The eye-height of the player.
		/// </summary>
		[EntityProperty(EntityPropertyType.Primitive, "The eye-height of the player.")]
		public float Height
		{
			get
			{
				return _height;
			}
			set
			{
				_height = value;
				Physicalize();
			}
		}

		/// <summary>
		/// Strength of the per-frame impulse when holding inputs
		/// </summary>
		/// <value>The move impulse strength.</value>
		[EntityProperty(EntityPropertyType.Primitive, "Speed of the player in meters per second.")]
		public float MoveSpeed { get; set; } = 20.5f;

		/// <summary>
		/// Speed at which the player rotates entity yaw
		/// </summary>
		/// <value>The rotation speed yaw.</value>
		[EntityProperty(EntityPropertyType.Primitive, "Speed at which the player rotates entity yaw")]
		public float RotationSpeedYaw { get; set; } = 0.002f;

		/// <summary>
		/// Speed at which the player rotates entity pitch
		/// </summary>
		/// <value>The rotation speed pitch.</value>
		[EntityProperty(EntityPropertyType.Primitive, "Speed at which the player rotates entity pitch")]
		public float RotationSpeedPitch { get; set; } = 0.002f;

		/// <summary>
		/// Minimum entity pitch limit
		/// </summary>
		/// <value>The rotation limits minimum pitch.</value>
		[EntityProperty(EntityPropertyType.Primitive, "Minimum entity pitch limit")]
		public float RotationLimitsMinPitch { get; set; } = -0.84f;

		/// <summary>
		/// Maximum entity pitch limit
		/// </summary>
		/// <value>The rotation limits max pitch.</value>
		[EntityProperty(EntityPropertyType.Primitive, "Maximum entity pitch limit")]
		public float RotationLimitsMaxPitch { get; set; } = 1.5f;

		protected override void OnInitialize()
		{
			base.OnInitialize();

			WorldStorage.Player = this;
			
			//Add the required components first.
			_animator = Entity.GetOrCreateComponent<DefaultCharacterAnimator>();

			// Assign the player's geometry so it shows the right model when it's dragged into the level.
			_animator.CharacterGeometry = PlayerGeometryUrl;

			PrepareRigidbody();
		}

		protected override void OnGameplayStart()
		{
			base.OnGameplayStart();

			if (WorldStorage.Player == null)
			{
				WorldStorage.Player = this;
			}

			// Make sure we didn't lose the reference to the component when the Sandbox jumped in and out of game-mode.
			if(_animator == null)
			{
				_animator = Entity.GetOrCreateComponent<DefaultCharacterAnimator>();
			}

			//Set up the player animations, but only if the game is being played.
			_animations = new PlayerAnimations(this, _animator);

			// Now create the physical representation of the entity
			PrepareRigidbody();

			_playerView = new PlayerView(this);

			InitializeInput();

			PrepareWeapon();
		}

		protected override void OnRemove()
		{
			base.OnRemove();
			_playerActionHandler?.Dispose();
		}

		protected override void OnEditorGameModeChange(bool enterGame)
		{
			base.OnEditorGameModeChange(enterGame);

			if(!enterGame)
			{
				_playerView?.Deinitialize();
				_playerView = null;
			}
		}

		private void InitializeInput()
		{
			_playerActionHandler?.Dispose();
			_playerActionHandler = new PlayerActionHandler();
		}

		protected override void OnUpdate(float frameTime)
		{
			base.OnUpdate(frameTime);

			var cameraRotation = _playerView == null ? Camera.Rotation : _playerView.UpdateView(frameTime, RotationMovement);
			RotationMovement = Vector2.Zero;
			_animations?.UpdateAnimationState(frameTime, cameraRotation);

			UpdateMovement(frameTime);
		}

		private void UpdateMovement(float frameTime)
		{
			var entity = Entity;
			var physicalEntity = entity.Physics;
			if(physicalEntity == null)
			{
				return;
			}

			var movement = new Vector3(Movement);

			//Transform the movement to camera-space
			movement = Camera.TransformDirection(movement);

			//Transforming it could have caused the movement to point up or down, so we flatten the z-axis to remove the height.
			movement.Z = 0.0f;
			movement = movement.Normalized;

			// Only dispatch the impulse to physics if one was provided
			if(movement.LengthSquared > 0.0f)
			{
				var status = physicalEntity.GetStatus<LivingStatus>();
				if(status.IsFlying)
				{
					//If we're not touching the ground we're not going to send any more move actions.
					return;
				}

				// Multiply by frame time to keep consistent across machines
				movement *= MoveSpeed * frameTime;

				physicalEntity.Move(movement);
			}
		}


		private void SetPlayerModel()
		{
			var entity = Entity;

			// Load the third person model
			var slot = entity.LoadCharacter((int)GeometrySlots.ThirdPerson, PlayerGeometryUrl);
			if(slot != (int)GeometrySlots.ThirdPerson)
			{
				Log.Always("Error");
			}
		}

		private void PrepareWeapon()
		{
			Weapon = new Rifle();
		}

		private void PrepareRigidbody()
		{
			var physicsEntity = Entity.Physics;
			if(physicsEntity == null)
			{
				return;
			}

			// Create the physical representation of the entity
			Physicalize();
		}

		private void Physicalize()
		{
			new EntityPhysicsHelper().Physicalize(Entity, Mass, Height, AirResistance);
		}
	}
}

