using Godot;
using System;

	
public partial class TowerToPlaceManager : Node2D
{
	private static Color _COLOR_VALID = new Color("#4ef544");
	private static Color _COLOR_INVALID = new Color("#f55544");
	private Color _color;
	
	private float _radius = 250f;


	private void _SetRadius(float radius)
	{
		_radius = radius;
	}

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
		DrawCircle(Vector2.Zero, _radius, _color, true, -1, true);
	}

	public void SetTowerData(TowerData data)
	{
		TowerStat towerStats = GameData.GetTowerStatsByLevel(data.level);
		GetNode<Sprite2D>("Base").Texture = data.sprite;
		_SetRadius(towerStats.radius);
	}
}
