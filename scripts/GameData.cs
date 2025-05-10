using Godot;
using System;
using System.Collections.Generic;

public partial class GameData : Node2D
{
        public static Dictionary<string, int> TYPES_ENUM = new Dictionary<string, int>
        {
            { "TowerManager", 0 },
            { "ShipManager", 1 }
        };
}
