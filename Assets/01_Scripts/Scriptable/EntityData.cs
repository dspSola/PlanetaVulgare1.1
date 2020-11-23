using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EntityData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _lifeMax;
    [SerializeField] private float _damage;

    public string Name { get => _name; set => _name = value; }
    public float LifeMax { get => _lifeMax; set => _lifeMax = value; }
    public float Damage { get => _damage; set => _damage = value; }
}
