using Godot;
using System;

[GlobalClass]
public partial class ShipData : Resource
{
    [Export] public float speed { get; set;}
    [Export] public int HP { get; set;}
    [Export] public int reward { get; set;}
    [Export] public Texture2D sprite { get; set;}


    public ShipData()
    {
        speed=1f;
        HP=2;
        reward=5;
        sprite=null;
    }
}
