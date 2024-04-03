using Godot;
using System;

public partial class ShipCannon : Node2D
{
  [Export]
  AnimatedSprite2D Effects;

	public override void _Ready()
	{
    Effects.Visible = false;
    Effects.AnimationFinished += OnAnimationFinished;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

  public void Shoot() {
    var bulletScene = GD.Load<PackedScene>("res://Nodes/CannonBall/cannon_ball.tscn").Instantiate();
    CannonBall bullet = (CannonBall)bulletScene;
    bullet.Shoot(GlobalPosition, GlobalRotation);
    GetTree().Root.AddChild(bullet);
    Effects.Visible = true;

    Effects.Play("shoot");
  }

  private void OnAnimationFinished()
  {
    Effects.Visible = false;
  }
}
