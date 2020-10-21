using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerEventStory : ScriptableObject
{
    [SerializeField] private bool _fightBossFire, _fightBossWind, _fightBossWater, _fightBossEarth;
    [SerializeField] private bool _bossFire, _bossWind, _bossWater, _bossEarth;
    [SerializeField] private bool _totemFire, _totemWind, _totemWater, _totemEarth;
    [SerializeField] private Vector3 _posCheckPointDie;

    public void Init()
    {
        _totemEarth = false;
        _totemFire = false;
        _totemWater = false;
        _totemWind = false;
    }

    public void AddTotemFire()
    {
        _totemFire = true;
    }
    public void AddTotemWind()
    {
        _totemWind = true;
    }
    public void AddTotemWater()
    {
        _totemWater = true;
    }
    public void AddTotemEarth()
    {
        _totemEarth = true;
    }

    public Vector3 PosCheckPointDie { get => _posCheckPointDie; set => _posCheckPointDie = value; }
    public bool TotemFire { get => _totemFire; set => _totemFire = value; }
    public bool TotemWind { get => _totemWind; set => _totemWind = value; }
    public bool TotemWater { get => _totemWater; set => _totemWater = value; }
    public bool TotemEarth { get => _totemEarth; set => _totemEarth = value; }
    public bool BossFire { get => _bossFire; set => _bossFire = value; }
    public bool BossWind { get => _bossWind; set => _bossWind = value; }
    public bool BossWater { get => _bossWater; set => _bossWater = value; }
    public bool BossEarth { get => _bossEarth; set => _bossEarth = value; }
    public bool FightBossFire { get => _fightBossFire; set => _fightBossFire = value; }
    public bool FightBossWind { get => _fightBossWind; set => _fightBossWind = value; }
    public bool FightBossWater { get => _fightBossWater; set => _fightBossWater = value; }
    public bool FightBossEarth { get => _fightBossEarth; set => _fightBossEarth = value; }
}
