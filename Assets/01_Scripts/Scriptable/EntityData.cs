using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EntityData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _lifeMax;
    [SerializeField] private int _damage;

    public string Name { get => _name; set => _name = value; }
    public int LifeMax { get => _lifeMax; set => _lifeMax = value; }
    public int Damage { get => _damage; set => _damage = value; }
}
