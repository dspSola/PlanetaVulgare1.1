using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector3 _position;
    [SerializeField] private float _lifeCoef;

    public Vector3 Position { get => _position; set => _position = value; }
    public Transform Transform { get => _transform; set => _transform = value; }
    public float LifeCoef { get => _lifeCoef; set => _lifeCoef = value; }
}
