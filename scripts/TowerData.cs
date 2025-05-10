using Godot;
using System;

[GlobalClass]
public partial class TowerData : Resource
{
    [Export] public float attackRate { get; set;} = 1f;
    [Export] public int attackDamage { get; set;} = 1;
    [Export] public float attackSpeed { get; set;} = 0.25f;
    [Export] public float radius { get; set;} = 400f;
    [Export] public int cost { get; set;} = 10;
    [Export] public Texture2D sprite { get; set;} = null;
    [Export] public string name { get; set;} = null;

}
