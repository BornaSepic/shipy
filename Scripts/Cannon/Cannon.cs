using Godot;
using System;

public partial class Cannon : Node2D
{
  AnimatedSprite2D effects;
  PackedScene cannonBallScene;

  public override void _Ready()
  {
    effects = GetNode<AnimatedSprite2D>("FireEffects");
    cannonBallScene = GD.Load<PackedScene>("res://Nodes/CannonBall/cannon_ball.tscn");


    effects.Visible = false;
    effects.AnimationFinished += OnAnimationFinished;
  }

  public override void _Process(double delta)
  {
  }

  public void Shoot(uint targetLayer)
  {
    CannonBall cannonBallInstance = (CannonBall)cannonBallScene.Instantiate();
    cannonBallInstance.Shoot(GlobalPosition, GlobalRotation + Mathf.Pi / 2, targetLayer);
    GetTree().Root.AddChild(cannonBallInstance);
    PlayShootAnimation();
  }

  private void OnAnimationFinished()
  {
    effects.Visible = false;
  }

  private void PlayShootAnimation()
  {
    effects.Visible = true;
    effects.Play("shoot");
  }
}
