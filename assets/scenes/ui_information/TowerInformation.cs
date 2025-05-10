using Godot;
using System;

public partial class TowerInformation : Control
{
	public void Initialize(TowerManager tower)
	{
		Label uiInfoLabel = GetNode<Label>("BaseInformation/Label");
		uiInfoLabel.Text = tower.name;

		TextureRect uiInfoSprite = GetNode<TextureRect>("BaseInformation/Sprite");
		uiInfoSprite.Texture = tower.GetNode<Sprite2D>("Base").Texture;

		VBoxContainer uiInfoStats = GetNode<VBoxContainer>("BaseInformation/Stats");

		Label towerDamage = new Label();
		towerDamage.Text = $"Damage : {tower.Get("_attackDamage")}";
		uiInfoStats.AddChild(towerDamage);

		Label towerRate = new Label();
		towerRate.Text = $"Rate : {tower.Get("_attackRate")}";
		uiInfoStats.AddChild(towerRate);
	}
}
