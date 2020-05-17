using System;
using CryEngine.Projects.Game.Storage;

namespace CryEngine.Game
{
    public class PlayerActionHandler : IDisposable
    {
        [SerializeValue]
        private ActionHandler _actionHandler;
        
        private const string InputActionMapUrl = "Libs/config/defaultprofile.xml";
        private const string InputActionMapName = "player";
        
        private bool InverseVerticalRotation { get; set; } = false;
        
        public PlayerActionHandler()
        {
            _actionHandler?.Dispose();
            _actionHandler = new ActionHandler(InputActionMapUrl, InputActionMapName);
            
            _actionHandler?.Dispose();
            _actionHandler = new ActionHandler(InputActionMapUrl, InputActionMapName);

            //Movement
            _actionHandler.AddHandler("moveforward", OnMoveForward);
            _actionHandler.AddHandler("moveback", OnMoveBack);
            _actionHandler.AddHandler("moveright", OnMoveRight);
            _actionHandler.AddHandler("moveleft", OnMoveLeft);

            //Mouse movement
            _actionHandler.AddHandler("mouse_rotateyaw", OnMoveMouseX);
            _actionHandler.AddHandler("mouse_rotatepitch", OnMoveMouseY);
        }

        private void OnMoveForward(string name, InputState state, float value)
        {
	        if (state == InputState.Pressed)
	        {
		        WorldStorage.Player.Movement.Y = 1.0f;
	        }
	        else if (state == InputState.Released)
	        {
		        //The movement only needs to be stopped when the player is still moving forward.
		        if (WorldStorage.Player.Movement.Y > 0.0f)
		        {
			        WorldStorage.Player.Movement.Y = 0.0f;
		        }
	        }
        }

        private void OnMoveBack(string name, InputState state, float value)
        {
	        if (state == InputState.Pressed)
	        {
		        WorldStorage.Player.Movement.Y = -1.0f;
	        }
	        else if (state == InputState.Released)
	        {
		        //The movement only needs to be stopped when the player is still moving back.
		        if (WorldStorage.Player.Movement.Y < 0.0f)
		        {
			        WorldStorage.Player.Movement.Y = 0.0f;
		        }
	        }
        }

        private void OnMoveRight(string name, InputState state, float value)
        {
	        if (state == InputState.Pressed)
	        {
		        WorldStorage.Player.Movement.X = 1.0f;
	        }
	        else if (state == InputState.Released)
	        {
		        //The movement only needs to be stopped when the player is still moving right.
		        if (WorldStorage.Player.Movement.X > 0.0f)
		        {
			        WorldStorage.Player.Movement.X = 0.0f;
		        }
	        }
        }

        private void OnMoveLeft(string name, InputState state, float value)
        {
	        if (state == InputState.Pressed)
	        {
		        WorldStorage.Player.Movement.X = -1.0f;
	        }
	        else if (state == InputState.Released)
	        {
		        //The movement only needs to be stopped when the player is still moving left.
		        if (WorldStorage.Player.Movement.X < 0.0f)
		        {
			        WorldStorage.Player.Movement.X = 0.0f;
		        }
	        }
        }

        private void OnMoveMouseX(string name, InputState state, float value)
        {
	        //If for some reason another state than Changed is received, it will be ignored.
	        if (state != InputState.Changed)
	        {
		        return;
	        }

	        WorldStorage.Player.RotationMovement.X += value;
        }

        private void OnMoveMouseY(string name, InputState state, float value)
        {
	        //If for some reason another state than Changed is received, it will be ignored.
	        if (state != InputState.Changed)
	        {
		        return;
	        }

	        if (InverseVerticalRotation)
	        {
		        value = -value;
	        }

	        WorldStorage.Player.RotationMovement.Y += value;
        }

        public void Dispose()
        {
	        _actionHandler?.Dispose();
        }
    }
}