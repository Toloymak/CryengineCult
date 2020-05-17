namespace CryEngine.Game.Helpers
{
    public class EntityPhysicsHelper
    {
        public void Physicalize(Entity entity, float mass, float height, float airResistance)
        {
            // Physicalize the player as type Living.
            // This physical entity type is specifically implemented for players
            var parameters = new LivingPhysicalizeParams();

            //The player will have settings for the player dimensions and dynamics.
            var playerDimensions = parameters.PlayerDimensions;
            var playerDynamics = parameters.PlayerDynamics;

            parameters.Mass = mass;

            // Prefer usage of a capsule instead of a cylinder
            playerDimensions.UseCapsule = true;

            // Specify the size of our capsule
            playerDimensions.ColliderSize = new Vector3(0.45f, 0.45f, height * 0.25f);

            // Keep pivot at the player's feet (defined in player geometry)
            playerDimensions.PivotHeight = 0.0f;

            // Offset collider upwards
            playerDimensions.ColliderHeight = 1.0f;
            playerDimensions.GroundContactEpsilon = 0.004f;

            playerDynamics.AirControlCoefficient = 0.0f;
            playerDynamics.AirResistance = airResistance;
            playerDynamics.Mass = mass;

            entity.Physics.Physicalize(parameters);
        }
    }
}