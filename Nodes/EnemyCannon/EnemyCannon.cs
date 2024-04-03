using Godot;
using System;

public partial class EnemyCannon : Node2D
{
  [Export]
  RayCast2D rayCast;

  [Export]
  Area2D detectionArea;

  [Export]
  Timer shootTimer;

  [Export]
  AnimatedSprite2D Effects;
  Player player;
  

  public override void _Ready()
  {
    Effects.Visible = false;
    Effects.AnimationFinished += OnAnimationFinished;
    player = GetNode<Player>("/root/World/Player");
  }

  public override void _Process(double delta)
  {
    LookAt(player.GlobalPosition);

    if(shootTimer.IsStopped() && detectionArea.GetOverlappingBodies().Count > 0)
    {
      Shoot();
    }
  }

  public void Shoot()
  {
    var bulletScene = GD.Load<PackedScene>("res://Nodes/CannonBall/cannon_ball.tscn").Instantiate();
    CannonBall bullet = (CannonBall)bulletScene;

    bullet.Shoot(GlobalPosition, rayCast.GlobalRotation + Mathf.Pi);
    GetTree().Root.AddChild(bullet);
    Effects.Visible = true;

    Effects.Play("shoot");
    shootTimer.Start();
  }

  private void OnAnimationFinished()
  {
    Effects.Visible = false;
  }
}
