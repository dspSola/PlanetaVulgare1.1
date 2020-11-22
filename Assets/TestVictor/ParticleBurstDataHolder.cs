using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurstDataHolder : MonoBehaviour
{
    [SerializeField] GameObjectData _gameObjectData;
    private void Awake()
    {
        _gameObjectData._value = gameObject;
    }
}
