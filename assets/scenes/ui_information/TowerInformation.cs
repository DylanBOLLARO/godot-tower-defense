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
		uiInfoStats.MouseFilter = MouseFilterEnum.Pass;

		Label towerDamage = new Label();
		towerDamage.Text = $"Damage : {tower.Get("_attackDamage")}";
		uiInfoStats.AddChild(towerDamage);

		Label towerRate = new Label();
		towerRate.Text = $"Rate : {Math.Round((double)tower.Get("_attackRate"), 2)}";
		uiInfoStats.AddChild(towerRate);

		int towerLevel = ((TowerStat)tower.GetTowerStats()).level;
		TowerStat nextUpgrade = GameData.GetTowerStatsByLevel(towerLevel+1);

		if (nextUpgrade != null)
		{
			PackedScene scene = GD.Load<PackedScene>("res://assets/scenes/ui_information/upgrade_tower_button.tscn");
			TextureButton instance = (TextureButton)scene.Instantiate();
			instance.GetNode<TextureRect>("Sprite").Texture = nextUpgrade.sprite;

			instance.GetNode<VBoxContainer>("Stats").MouseFilter = MouseFilterEnum.Pass;

			Label d = new Label();
			d.Text = $"Damage : {nextUpgrade.attackDamage}";
			instance.GetNode<VBoxContainer>("Stats").AddChild(d);

			Label r = new Label();
			r.Text = $"Rate : {Math.Round((double)nextUpgrade.attackRate, 2)}";
			instance.GetNode<VBoxContainer>("Stats").AddChild(r);

			uiInfoStats.AddChild(instance);

			instance.Connect(TextureButton.SignalName.Pressed, Callable.From(tower.UpgradeLevelOfTower));
		}
	}
}
