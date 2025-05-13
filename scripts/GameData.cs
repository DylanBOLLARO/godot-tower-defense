using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;


public class TowerStat
{
    public string name { get; set;} = null;
    public int level { get; set;} = 0 ;
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
            level = 1,
            attackDamage = 1,
            attackRate = 1f,
            attackSpeed = 0.25f,
            radius = 200f,
            cost = 20,
            sprite = (Texture2D)GD.Load("res://assets/towers/cannon_t1.png"),
        },
        new TowerStat
        {
            name = "Cannon",
            level = 2,
            attackDamage = 2,
            attackRate = 0.9f,
            attackSpeed = 0.27f,
            radius = 250f,
            cost = 40,
            sprite = (Texture2D)GD.Load("res://assets/towers/cannon_t2.png"),
        },
        new TowerStat
        {
            name = "Cannon",
            level = 3,
            attackDamage = 3,
            attackRate = 0.8f,
            attackSpeed = 0.30f,
            radius = 320f,
            cost = 80,
            sprite = (Texture2D)GD.Load("res://assets/towers/cannon_t3.png"),
        },
        new TowerStat
        {
            name = "Cannon",
            level = 4,
            attackDamage = 5,
            attackRate = 0.6f,
            attackSpeed = 0.33f,
            radius = 400f,
            cost = 120,
            sprite = (Texture2D)GD.Load("res://assets/towers/cannon_t4.png"),
        },
        new TowerStat
        {
            name = "Cannon",
            level = 5,
            attackDamage = 8,
            attackRate = 0.4f,
            attackSpeed = 0.30f,
            radius = 500,
            cost = 160,
            sprite = (Texture2D)GD.Load("res://assets/towers/cannon_t5.png"),
        },
        new TowerStat
        {
            name = "Cannon",
            level = 6,
            attackDamage = 10,
            attackRate = 0.2f,
            attackSpeed = 0.5f,
            radius = 600f,
            cost = 200,
            sprite = (Texture2D)GD.Load("res://assets/towers/cannon_t6.png"),
        },
    };

    public static TowerStat GetTowerStatsByLevel(int towerLevel){
        TowerStat towerStats = _towerData.FirstOrDefault(t => t.level == towerLevel);
        return towerStats;
    }
}

