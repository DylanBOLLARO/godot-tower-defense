using Godot;
using System;

[GlobalClass]
public partial class TowerData : Resource
{
    [Export] public string level = null;
    [Export] public Texture2D sprite { get; set;} = null;
}
