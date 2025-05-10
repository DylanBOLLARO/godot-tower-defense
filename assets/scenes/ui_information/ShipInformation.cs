using Godot;
using System;

public partial class ShipInformation : Control
{
	public void Initialize(ShipManager ship)
	{
		Label uiInfoLabel = GetNode<Label>("BaseInformation/Label");
		uiInfoLabel.Text = ship.name;

		TextureRect uiInfoSprite = GetNode<TextureRect>("BaseInformation/Sprite");
		uiInfoSprite.Texture = ship.GetNode<Sprite2D>("Sprite2D").Texture;

		VBoxContainer uiInfoStats = GetNode<VBoxContainer>("BaseInformation/Stats");

		Label shipHP = new Label();
		shipHP.Text = $"HP : {ship.HP}";

		uiInfoStats.AddChild(shipHP);
	}
}
