namespace CryEngine.Game.Intefaces
{
    public interface IUnit
    {
        float Mass { get; set; }
        float AirResistance { get; set; }
        float Height { get; set; }
        float MoveSpeed { get; set; }
        float RotationSpeedYaw { get; set; }
        float RotationSpeedPitch { get; set; }
        float RotationLimitsMinPitch { get; set; }
        float RotationLimitsMaxPitch { get; set; }
    }
}