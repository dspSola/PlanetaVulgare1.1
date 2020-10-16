using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] private Vector3 _position;
    public Vector3 Position { get => _position; set => _position = value; }
}
