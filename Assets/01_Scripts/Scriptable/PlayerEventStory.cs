using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerEventStory : ScriptableObject
{
    [SerializeField] private bool _bossFire, _bossWind, _bossWater, _bossEarth;
    [SerializeField] private bool _totemFire, _totemWind, _totemWater, _totemEarth;
    [SerializeField] private int _cptBossWin;
    [SerializeField] private Vector3 _posCheckPointDie;

    public void Init()
    {
        _totemEarth = true;
        _totemFire = true;
        _totemWater = true;
        _totemWind = true;
        _bossFire = false;
        _bossWind = false;
        _bossWater = false;
        _bossEarth = false;
        _cptBossWin = 0;
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

    public void WinBossFire()
    {
        _bossFire = true;
        _cptBossWin++;
    }
    public void WinBossWind()
    {
        _bossWind = true;
        _cptBossWin++;
    }
    public void WinBossWater()
    {
        _bossWater = true;
        _cptBossWin++;
    }
    public void WinBossEarth()
    {
        _bossEarth = true;
        _cptBossWin++;
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
    public int CptBossWin { get => _cptBossWin; set => _cptBossWin = value; }
}
