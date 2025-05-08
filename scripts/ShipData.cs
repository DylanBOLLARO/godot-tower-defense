using Godot;
using System;

[GlobalClass]
public partial class ShipData : Resource
{
    [Export] public float speed { get; set;} = 1f;
    [Export] public int HP { get; set;} = 2;
    [Export] public int reward { get; set;} = 5;
    [Export] public Texture2D sprite { get; set;} = null;
}
