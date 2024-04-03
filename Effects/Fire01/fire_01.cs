using Godot;
using System;

public partial class fire_01 : AnimatedSprite2D
{

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
    LookAt(GlobalPosition + Vector2.Right);
	}
}
