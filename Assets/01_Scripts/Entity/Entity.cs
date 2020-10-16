using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private EntityData _entityData;

    [SerializeField] private bool _initializeOnStart;
    [SerializeField] private string _name;
    [SerializeField] private int _lifeMax, _life;
    [SerializeField] private int _damage;

    private void Start()
    {
        if(_initializeOnStart)
        {
            InitializeEntity();
        }
    }

    public void InitializeEntity()
    {
        _name = _entityData.Name;
        _lifeMax = _entityData.LifeMax;
        _life = _lifeMax;
        _damage = _entityData.Damage;
    }

    public void AddLife(int value)
    {
        if (_life + value > _lifeMax)
        {
            _life = _lifeMax;
        }
        else
        {
            _life += value;
        }
    }

    public void LessLife(int value)
    {
        if (_life - value < 0)
        {
            _life = 0;
        }
        else
        {
            _life -= value;
        }
    }

    public string Name { get => _name; set => _name = value; }
    public int LifeMax { get => _lifeMax; set => _lifeMax = value; }
    public int Life { get => _life; set => _life = value; }
    public int Damage { get => _damage; set => _damage = value; }
}
