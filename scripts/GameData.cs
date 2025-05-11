using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;


public class TowerStat
{
    public string name { get; set;} = null;
    public string level { get; set;} = null;
    public float attackRate { get; set;} = 1f;
    public int attackDamage { get; set;} = 1;
    public float attackSpeed { get; set;} = 0.25f;
    public float radius { get; set;} = 400f;
    public int cost { get; set;} = 10;
    public Texture2D sprite { get; set;} = null;

}

public partial class GameData : Node2D
{
    public static System.Collections.Generic.Dictionary<string, int> TYPES_ENUM = new System.Collections.Generic.Dictionary<string, int>
    {
        { "TowerManager", 0 },
        { "ShipManager", 1 }
    };

    private static List<TowerStat> _towerData = new List<TowerStat>
    {
        new TowerStat
        {
            name = "Cannon",
            level = "t1",
            attackDamage = 1,
            attackRate = 1f,
            attackSpeed = 0.25f,
            radius = 200f,
            cost = 20,
            sprite = null,
        },
        new TowerStat
        {
            name = "Cannon",
            level = "t2",
            attackDamage = 2,
            attackRate = 0.9f,
            attackSpeed = 0.27f,
            radius = 250f,
            cost = 50,
            sprite = null,
        },
        new TowerStat
        {
            name = "Cannon",
            level = "t3",
            attackDamage = 3,
            attackRate = 0.8f,
            attackSpeed = 0.30f,
            radius = 320f,
            cost = 75,
            sprite = null,
        },
        new TowerStat
        {
            name = "Cannon",
            level = "t4",
            attackDamage = 5,
            attackRate = 0.6f,
            attackSpeed = 0.33f,
            radius = 400f,
            cost = 100,
            sprite = null,
        },
    };

    public static TowerStat GetTowerStatsByLevel(string towerLevel){
        TowerStat towerStats = _towerData.FirstOrDefault(t => t.level == towerLevel);
        return towerStats;
    }
}

