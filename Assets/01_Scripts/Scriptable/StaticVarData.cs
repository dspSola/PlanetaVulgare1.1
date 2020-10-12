using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVarData : MonoBehaviour
{
    [SerializeField] private float _gravity = -9.81f;

    public float Gravity { get => _gravity; set => _gravity = value; }
}
