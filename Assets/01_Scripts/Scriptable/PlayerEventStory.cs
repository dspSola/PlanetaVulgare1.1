using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerEventStory : ScriptableObject
{
    [SerializeField] private bool _gameExist, _playerPassPortal;
    [SerializeField] private bool _bossFire, _bossWind, _bossWater, _bossEarth, _ultimeBoss;
    [SerializeField] private bool _totemFire, _totemWind, _totemWater, _totemEarth;
    [SerializeField] private int _cptBossWin;
    [SerializeField] private Vector3 _posSave, _posCheckPointDie;

    [SerializeField] private EntityData _playerEntityData;

    public void Init()
    {
        _totemEarth = false;
        _totemFire = false;
        _totemWater = false;
        _totemWind = false;
        _bossFire = false;
        _bossWind = false;
        _bossWater = false;
        _bossEarth = false;
        _cptBossWin = 0;

        _posSave = Vector3.zero;
        _posCheckPointDie = Vector3.zero;

        _playerPassPortal = false;

        _playerEntityData.LifeMax = 100;
    }

    public void NewGame()
    {
        _gameExist = true;
        Init();
    }

    public void Continue()
    {
        _posCheckPointDie = Vector3.zero;
    }

    public void SavePos(Vector3 posPlayer)
    {
        _posSave = posPlayer;
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
    public void WinBossUltime()
    {
        _ultimeBoss = true;
        _cptBossWin++;
    }

    public void ReloadScene()
    {
        if (_totemFire && !_bossFire)
        {
            _totemFire = false;
        }
        if (_totemEarth && !_bossEarth)
        {
            _totemEarth = false;
        }
        if (_totemWater && !_bossWater)
        {
            _totemWater = false;
        }
        if (_totemWind && !_bossWind)
        {
            _totemWind = false;
        }
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
    public bool GameExist { get => _gameExist; set => _gameExist = value; }
    public Vector3 PosSave { get => _posSave; set => _posSave = value; }
    public bool PlayerPassPortal { get => _playerPassPortal; set => _playerPassPortal = value; }
}
