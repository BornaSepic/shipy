using Godot;
using System;

public partial class EnemyCannon : Cannon
{
  RayCast2D direction;

  Area2D detectionArea;

  Timer shootTimer;

  Player player;
  
  public override void _Ready()
  {
    base._Ready();
    player = GetNode<Player>("/root/World/Player");
    shootTimer = GetNode<Timer>("ShootTimer");
    detectionArea = GetNode<Area2D>("DetectionArea");
    direction = GetNode<RayCast2D>("Direction");
  }

  public override void _Process(double delta)
  {
    LookAt(player.GlobalPosition);

    if(shootTimer.IsStopped() && detectionArea.GetOverlappingBodies().Count > 0)
    {
      Shoot((uint) CollisionLayers.Player);
      shootTimer.Start();
    }
  }
}
