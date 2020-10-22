using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private EntityData _entityData;

    [SerializeField] private bool _initializeOnStart;
    [SerializeField] private string _name;
    [SerializeField] private float _lifeMax, _life, _coefLife, _damage;

    private void Start()
    {
        if(_initializeOnStart)
        {
            InitializeEntity();
        }
    }

    public virtual void InitializeEntity()
    {
        _name = _entityData.Name;
        _lifeMax = _entityData.LifeMax;
        _life = _lifeMax;
        _coefLife = _life / _lifeMax;
        _damage = _entityData.Damage;
    }

    public virtual void AddLife(float value)
    {
        if (_life + value > _lifeMax)
        {
            _life = _lifeMax;
        }
        else
        {
            _life += value;
        }
        _coefLife = _life / _lifeMax;
    }

    public virtual void LessLife(float value)
    {
        if (_life - value < 0)
        {
            _life = 0;
        }
        else
        {
            _life -= value;
        }
        _coefLife = _life / _lifeMax;
    }

    public virtual void UpgradeLife(float value)
    {
        _life += value;
        _lifeMax += value;
    }

    public string Name { get => _name; set => _name = value; }
    public float LifeMax { get => _lifeMax; set => _lifeMax = value; }
    public float Life { get => _life; set => _life = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public EntityData EntityData { get => _entityData; set => _entityData = value; }
    public bool InitializeOnStart { get => _initializeOnStart; set => _initializeOnStart = value; }
    public float CoefLife { get => _coefLife; set => _coefLife = value; }
}
