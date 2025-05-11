using Godot;
using System;
using System.Collections.Generic;

public partial class TowerManager : Area2D
{
	[Export] private PackedScene _canonBallAsset;

	private Node2D _canonSprite;
	private CollisionShape2D _fovAreaShape;

	private MapManager _mapManager;
	private List<Node2D> _targetInRange;

	private Node2D _currentTarget;

	public string name;



	// tower parameters
	private float _attackRate = 1f;
	private int _attackDamage = 1;
	private float _attackSpeed = 0.25f;
	private float _attackDelay;
	private bool _canBeSelected;


	public override void _Ready()
	{
		_canonSprite = GetNode<Node2D>("Canon");
		_targetInRange = new List<Node2D>();
		_currentTarget = null;
		_attackDelay = _attackRate;
	}

	public override void _Process(double delta)
	{
		QueueRedraw();

		if (_currentTarget != null)
		{
			_canonSprite.LookAt(_currentTarget.Position);

			if (_attackDelay > 0)
			{
				_attackDelay -= (float)delta;
			}
			else
			{
				_FireCannonBall();
				_attackDelay = _attackRate;
			}
		}
	}

	public void Initialize(MapManager mapManager, TowerData data)
	{
		_mapManager = mapManager;

		_attackRate = data.attackRate;
		_attackDamage = data.attackDamage;
		_attackSpeed = data.attackSpeed;
		name = data.name;
		GetNode<Sprite2D>("Base").Texture = data.sprite;

		_fovAreaShape = GetNode<CollisionShape2D>("FOVArea2D/CollisionShape2D");
		((CircleShape2D)_fovAreaShape.Shape).Radius = data.radius;
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

	private void _OnTowerMouseEntered()
	{
		GameManager.SetSelectedCursor();
		_canBeSelected = true;
		_mapManager.SetCanPlaceTower(false);
	}

	private void _OnTowerMouseExited()
	{
		GameManager.SetBaseCursor();
		_canBeSelected = false;
		_mapManager.SetCanPlaceTower(true);
	}

	private void _OnFovArea2dAreaEntered(Area2D area)
	{
		Node2D ship = (Node2D) ((Node)area).GetParent();
		_targetInRange.Add(ship);

		if(_currentTarget == null) _currentTarget = _targetInRange[0];
	}
	
	private void _OnFovArea2dAreaExited(Area2D area)
	{
		Node2D ship = (Node2D) ((Node)area).GetParent();
		_targetInRange.Remove(ship);

		if(_targetInRange.Count > 0) _currentTarget = _targetInRange[0];
		else _currentTarget = null;
	}

	private void _FireCannonBall(){
		CannonBallManager cannonBall = (CannonBallManager) _canonBallAsset.Instantiate();
		cannonBall.Position = Position;

		Vector2 offsettedTarged = _currentTarget.Position + _currentTarget.Transform.X * 50f;
		Vector2 velocity = (offsettedTarged - Position).Normalized();
		cannonBall.Initialize(velocity, _attackDamage, _attackSpeed);
		GetTree().Root.AddChild(cannonBall);
	}

	public override void _Draw()
	{
		if ((TowerManager)_mapManager.Get("_currentSelect") == this){
			DrawCircle(new Vector2(0,0),35f,new Color(0,0,1,1), false, 1, true);
			DrawCircle(new Vector2(0,0),35f,new Color(0,0,1,(float)0.2));
		}
	}
}
