using Godot;
using System;

public partial class Menu : Control
{
	public void _OnPlayButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}
}
