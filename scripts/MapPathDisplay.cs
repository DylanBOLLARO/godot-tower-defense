using Godot;
using System;

public partial class MapPathDisplay : Node2D
{
	private Curve2D _pathCurve;
	private Color _pathColor;
	
	public override void _Ready()
	{
		Path2D path = GetNode<Path2D>("/root/Game/Map/Path2D");
		_pathCurve = path.GetCurve();
		_pathColor = new Color(1,1,1,0.5f);
	}

	public override void _Draw()
	{
		DrawPolyline(_pathCurve.GetBakedPoints(),_pathColor,5f,true);
	}
}
