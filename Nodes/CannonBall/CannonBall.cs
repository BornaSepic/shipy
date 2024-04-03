using Godot;
using System;

public partial class CannonBall : Area2D
{

  public const float Speed = 300.0f;
  private AnimatedSprite2D explosionEffect;
  private bool isExploding = false;


  public override void _Ready()
  {
    base._Ready();
    explosionEffect = GetNode<AnimatedSprite2D>("ExplosionEffect");
    explosionEffect.AnimationFinished += OnAnimationFinished;
  }
  public override void _PhysicsProcess(double delta)
  {
    if (isExploding)
    {
      return;
    }
    Vector2 velocity = new Vector2(0, -Speed).Rotated(Rotation);
    Position += velocity * (float)delta;

    if (GetOverlappingBodies().Count > 0)
    {
      Godot.Collections.Array<Node2D> overlappingBodies = GetOverlappingBodies();
      for (int i = 0; i < overlappingBodies.Count; i++)
      {
        Node2D collision = overlappingBodies[i];

        if (collision is Player player)
        {
          player.TakeDamage(25);
          isExploding = true;
          explosionEffect.Visible = true;
          explosionEffect.Play("explode");
        }
      }
    }
  }

  public void Shoot(Vector2 position, float rotation, uint targetLayer)
  {
    GlobalPosition = position;
    Rotation = rotation;
    CollisionMask = targetLayer;
  }

  private void OnAnimationFinished()
  {
    explosionEffect.Visible = false;
    QueueFree();
  }
}
