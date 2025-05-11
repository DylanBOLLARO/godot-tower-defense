using Godot;
using System;

public partial class ShipManager : PathFollow2D
{
	private PathFollow2D _pathFollow;


	// ship parameters
	private float _speed = 1f;
	public int HP = 1;
	public Texture2D sprite = null;
	public int reward = 5;
	private MapManager _mapManager;
	private bool _canBeSelected;

	public string name;


	public override void _Ready()
	{
		_pathFollow = GetNode<PathFollow2D>(GetPath());
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseButton eventMouseButton)
		{
			if ((int)eventMouseButton.ButtonIndex == (int)MouseButton.Left && !eventMouseButton.Pressed)
			{
				if (_canBeSelected) _mapManager.SetCurrentSelect(this);
			}
		}
	}

	public void Initialize(MapManager mapManager, ShipData shipData)
	{
		_mapManager = mapManager;
		_speed = shipData.speed;
		HP = shipData.HP;
		reward = shipData.reward;
		name = shipData.name;
		GetNode<Sprite2D>("Sprite2D").Texture = shipData.sprite;
	}

	public override void _Process(double delta)
	{
		QueueRedraw();

		_pathFollow.ProgressRatio  += (float)(delta * _speed * 0.03f);

		if (_pathFollow.ProgressRatio >=1)
		{
			_Pass();
		}
	}

	private void _Pass()
	{
		GameManager.instance.OnShipPassed(this);
		QueueFree();
	}

	private void _OnArea2dBodyEntered(CharacterBody2D body)
	{
		CannonBallManager cannonBall = (CannonBallManager) body;
		_TakeHit(cannonBall.damage);
		cannonBall.QueueFree();
	}

	private void _TakeHit(int damage)
	{
		HP -= damage;
		if (HP <= 0) _Die();
	}

	private void _Die()
	{
		GameManager.instance.OnShipDied(this);
		QueueFree();
	}

	private void _OnMouseEntered()
	{
		GameManager.SetSelectedCursor();
		_canBeSelected = true;
	}

	private void _OnMouseExited()
	{
		GameManager.SetBaseCursor();
		_canBeSelected = false;
	}

	public override void _Draw()
	{
		if ((ShipManager)_mapManager.Get("_currentSelect") == this){
			DrawCircle(new Vector2(0,0),45f,new Color("#00ADB5FF"), false, 1, true);
			DrawCircle(new Vector2(0,0),45f,new Color("#00ADB555"));
		}
	}
}
