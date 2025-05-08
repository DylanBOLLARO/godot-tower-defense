using Godot;
using System;

public partial class MapManager : Node2D
{
	[Export] private PackedScene _shipAsset;
	[Export] private PackedScene _towerAsset;
	[Export] private PackedScene _towerButtonAsset;
	[Export] private ShipData[] _shipData;
	[Export] private TowerData[] _towerData;


	private Path2D _path;
	private bool _isBuilding;


	private TileMapLayer _groundTilemap;
	private float _cellRound;
	private Vector2 _cellOffset;

	private TowerToPlaceManager _towerToPlace;

	private bool _towerHasValidPlacement;
	private bool _canPlaceTower;
	private TowerData _towerToPlaceData;

	public override void _Ready()
	{
		_path = GetNode<Path2D>("Path2D");
		_towerToPlace = GetNode<TowerToPlaceManager>("Tower-ToPlace");
		_groundTilemap = GetNode<TileMapLayer>("Tilemaps/Ground");
		_cellRound = _groundTilemap.TileSet.TileSize.X;
		_cellOffset = 0.5f * new Vector2(_cellRound,_cellRound);
		SetIsBuilding(false);

		// dynamically connect UI tower buttons to placing logic
		Control towersButton = GetNode<Control>("/root/Game/CanvasLayer/UI/Towers");

		foreach (TowerData data in _towerData)
		{
			Control c = (Control)_towerButtonAsset.Instantiate();
			c.GetNode<TextureRect>("VBoxContainer/Control/Base").Texture = data.sprite;
			c.GetNode<Label>("VBoxContainer/HBoxContainer/CoinsLabel").Text = $"{data.cost}";

			c.Connect(Button.SignalName.Pressed, Callable.From(() => _OnTowerButtonMousePressed(data)));
			
			c.Connect(Button.SignalName.MouseEntered, Callable.From(_OnTowerButtonMouseEntered));
			c.Connect(Button.SignalName.MouseExited, Callable.From(_OnTowerButtonMouseExited));
			towersButton.AddChild(c);
		}
	}


	private void _OnEnemySpawnTimerTimeout()
	{
		Node ship = _shipAsset.Instantiate();

		Random random = new Random();
		ShipData data = _shipData[random.Next(0,_shipData.Length)];

		((ShipManager)ship).Initialize(data);
		_path.AddChild(ship);
	}

	private Vector2 _RoundPositionToTilmap(Vector2 p)
	{
		return (p/_cellRound).Floor() * _cellRound + _cellOffset;
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseButton eventMouseButton && (int)eventMouseButton.ButtonIndex == 1 && !eventMouseButton.Pressed){

			if (_towerHasValidPlacement && _isBuilding && _canPlaceTower)
			{
				if (GameManager.instance.BuyTower(_towerToPlaceData.cost))
					_PlaceTower(_RoundPositionToTilmap(GetGlobalMousePosition()));
			}
		}
		else if (@event is InputEventMouseMotion eventMouseMotion)
		{
			Vector2 mousePosition = GetGlobalMousePosition();
			_towerToPlace.Position = _RoundPositionToTilmap(mousePosition);

			// check tower has valid placement
			Vector2I cellPos = _groundTilemap.LocalToMap(_towerToPlace.Position);
			_towerHasValidPlacement = _groundTilemap.GetCellSourceId(cellPos) != -1;
			_towerToPlace.SetValid(_towerHasValidPlacement);
		}
	}

    private void _PlaceTower(Vector2 position)
    {
		Node2D tower = (Node2D) _towerAsset.Instantiate();
		tower.Position = position;
		AddChild(tower);
		((TowerManager)tower).Initialize(this, _towerToPlaceData);
		SetIsBuilding(false);
    }

    public void SetIsBuilding(bool isBuilding)
    {
		_isBuilding = isBuilding;

		if(isBuilding && _canPlaceTower) _towerToPlace.Show();
		else _towerToPlace.Hide();
    }

	public void SetCanPlaceTower(bool canPlaceTower)
	{
		_canPlaceTower = canPlaceTower;

		if (canPlaceTower && _isBuilding) ((CanvasItem)_towerToPlace).Show();
		else ((CanvasItem)_towerToPlace).Hide();
	}

	private void _OnTowerButtonMousePressed(TowerData data)
	{
		if (GameManager.instance.CanBuyTower(data.cost))
		{
			_towerToPlace.SetTowerData(data);
			_towerToPlaceData = data;
			SetIsBuilding(true);
		}
	}

	private void _OnTowerButtonMouseEntered()
	{
		SetCanPlaceTower(false);
	}

	private void _OnTowerButtonMouseExited()
	{
		SetCanPlaceTower(true);
	}
}
