using Godot;
using System;

	
public partial class TowerToPlaceManager : Node2D
{
	private static Color _COLOR_VALID = new Color("#4ef544");
	private static Color _COLOR_INVALID = new Color("#f55544");
	private Color _color;
	
	public float radius = 250f;

	public override void _Ready()
	{
		Modulate = new Color("#FFF4");
	}

	public void SetValid(bool isValid)
	{
		_color = isValid ? _COLOR_VALID : _COLOR_INVALID;
		QueueRedraw();
	}

	public override void _Draw()
	{
		DrawCircle(Vector2.Zero, radius, _color, true, -1, true);
	}
}
