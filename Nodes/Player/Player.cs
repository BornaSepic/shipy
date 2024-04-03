using Godot;
using System;

public partial class Player : CharacterBody2D
{
  [Export]
  public Node2D CannonsLeft;

  [Export]
  public Node2D CannonsRight;

  public float Speed = 75.0f;
  public const float RotationSpeed = 0.01f;

  private decimal health = 100;

  private AnimatedSprite2D bodySprite;
  private AnimatedSprite2D sailSprite;
  private PackedScene fireSprite01;


  public override void _Ready()
  {
    base._Ready();
    bodySprite = GetNode<AnimatedSprite2D>("Body");
    sailSprite = GetNode<AnimatedSprite2D>("Sail");

    fireSprite01 = GD.Load<PackedScene>("res://Effects/Fire01/fire_01.tscn");
  }

  public override void _Process(double delta)
  {
    base._Process(delta);

    if (Input.IsActionJustPressed("SHOOT_LEFT"))
    {
      ShootLeftCannons();
    }

    if (Input.IsActionJustPressed("SHOOT_RIGHT"))
    {
      ShootRightCannons();
    }

    int healthAsFrame = (int)Math.Floor(health / 25);

    bodySprite.Frame = healthAsFrame;
    sailSprite.Frame = healthAsFrame;
  }
  public override void _PhysicsProcess(double delta)
  {
    Vector2 velocity = Velocity;
    velocity.Y = Speed;

    if (Input.IsActionPressed("MOVE_RIGHT"))
    {
      Rotate(RotationSpeed);
    }

    if (Input.IsActionPressed("MOVE_LEFT"))
    {
      Rotate(-RotationSpeed);
    }

    Velocity = new Vector2(0, Speed).Rotated(Rotation);
    bool collided = MoveAndSlide();
  }

  private void ShootLeftCannons()
  {

    Godot.Collections.Array<Node> possibleCannons = CannonsLeft.GetChildren();

    for (int i = 0; i < possibleCannons.Count; i++)
    {
      ShipCannon cannon = (ShipCannon)possibleCannons[i];
      cannon.Shoot((uint) CollisionLayers.Enemy);
    }
  }

  private void ShootRightCannons()
  {

    Godot.Collections.Array<Node> possibleCannons = CannonsRight.GetChildren();

    for (int i = 0; i < possibleCannons.Count; i++)
    {
      ShipCannon cannon = (ShipCannon)possibleCannons[i];
      cannon.Shoot((uint) CollisionLayers.Enemy);
    }
  }

  public void TakeDamage(float damage) {
    health -= (decimal)damage;
    if (health <= 0)
    {
      Speed = 0;
    }

    var fireScene = fireSprite01.Instantiate();
    AnimatedSprite2D fireSprite = (AnimatedSprite2D)fireScene;

    Vector2 randomPosition = new Vector2(
      (float)GD.RandRange(-15, 15),
      (float)GD.RandRange(-40, 40)
    );

    fireSprite.GlobalPosition = randomPosition;
    
    AddChild(fireSprite);

    fireSprite.Play("default");
  }
}
