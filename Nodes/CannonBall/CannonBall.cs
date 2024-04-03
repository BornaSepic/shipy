using Godot;
using System;

public partial class CannonBall : Area2D
{
  public const float Speed = 300.0f;

  public override void _Ready()
  {
    base._Ready();
  }
  public override void _PhysicsProcess(double delta)
  {
    Vector2 velocity = new Vector2(0, -Speed).Rotated(Rotation);
    Position += velocity * (float)delta;

    if (GetOverlappingBodies().Count > 0)
    {
      Godot.Collections.Array<Node2D> overlappingBodies = GetOverlappingBodies();
      for (int i = 0; i < overlappingBodies.Count; i++)
      {
        Node2D collision = overlappingBodies[i];
        GD.Print(collision.GetClass());
        if (collision is Player player)
        {
          player.TakeDamage(25, GlobalPosition);
          QueueFree();
        }
      }
    }
  }

  public void Shoot(Vector2 position, float rotation)
  {
    GlobalPosition = position;
    Rotation = rotation;
  }
}
