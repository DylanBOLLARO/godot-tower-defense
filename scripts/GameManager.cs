using Godot;
using System;

public partial class GameManager : Node2D
{
	public static GameManager instance;


	private Label _coinsLabel;
	private Label _livesLabel;
	private int _coins = 20;
	private int _lives = 50;

	public override void _Ready()
	{
		instance = this;

		_coinsLabel = GetNode<Label>("CanvasLayer/UI/CoinsBG/HBoxContainer/CoinsLabel");
		_livesLabel = GetNode<Label>("CanvasLayer/UI/LivesBG/HBoxContainer/LivesLabel");

		_UpdateUI();
	}
	private void _UpdateUI()
	{
		_coinsLabel.Text = $"{_coins}";
		_livesLabel.Text = $"{_lives}";
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

}
