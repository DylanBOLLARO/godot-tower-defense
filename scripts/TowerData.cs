using Godot;
using System;

[GlobalClass]
public partial class TowerData : Resource
{
    [Export] public int level = 0;
    [Export] public Texture2D sprite { get; set;} = null;
}
