using Godot;
using System;

public partial class GameManager : Node2D
{
	public static GameManager instance;

	[Export] private PackedScene _towerInformation;
	[Export] private PackedScene _shipInformation;

	private Label _coinsLabel;
	private Label _livesLabel;
	private int _coins = 100;
	private int _lives = 50;
	private Control _ui_choice_of_towers;

	public override void _Ready()
	{
		instance = this;

		_ui_choice_of_towers = GetNode<Control>("/root/Game/CanvasLayer/UI/VBoxContainer/TowersData");
		_coinsLabel = GetNode<Label>("CanvasLayer/UI/HBoxContainer/CoinsBG/HBoxContainer/CoinsLabel");
		_livesLabel = GetNode<Label>("CanvasLayer/UI/HBoxContainer/LivesBG/HBoxContainer/LivesLabel");

		_UpdateUI();
	}
	private void _UpdateUI()
	{
		_coinsLabel.Text = $"{_coins}";
		_livesLabel.Text = $"{_lives}";
	}
	
	public void UpdateInformation(Node2D data)
	{
		foreach (Node child in _ui_choice_of_towers.GetChildren())
		{
			child.QueueFree();
		}

		if (data == null) return;

		if (GameData.TYPES_ENUM.TryGetValue(data.GetType().Name, out int value))
		{
			switch (value)
			{
				case 0:
					TowerManager tower = (TowerManager)data;
					Node towerInformation = _towerInformation.Instantiate();
					((TowerInformation)towerInformation).Initialize(tower);
					_ui_choice_of_towers.AddChild(towerInformation);
					break;

				case 1:
					ShipManager ship = (ShipManager)data;
					Node shipInformation = _shipInformation.Instantiate();
					((ShipInformation)shipInformation).Initialize(ship);
					_ui_choice_of_towers.AddChild(shipInformation);
					break;

				default:
					break;
			}
		}
	}

	public bool BuyTower(int cost)
	{
		if (_coins < cost) return false;

		_coins -= cost;
		_UpdateUI();
		return true;

	}
	
	public void OnShipPassed(ShipManager ship)
	{
		_lives -= ship.HP;
		_UpdateUI();
	}

	public bool CanBuyTower(int cost)
	{
		return _coins >= cost;
	}

	public void OnShipDied(ShipManager ship)
	{
		_coins += ship.reward;
		_UpdateUI();
	}

	public static void SetSelectedCursor()
	{
        var arrow = ResourceLoader.Load("res://assets/art/crosshair_b.png");
        Input.SetCustomMouseCursor(arrow);
	}

	public static void SetBaseCursor()
	{
        var arrow = ResourceLoader.Load("res://assets/art/cursor_g.png");
        Input.SetCustomMouseCursor(arrow);
	}
}
