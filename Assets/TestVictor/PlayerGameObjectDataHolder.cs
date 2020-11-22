using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameObjectDataHolder : MonoBehaviour
{
    [SerializeField] GameObjectData _playerMoveScriptData;

    private void Awake()
    {
        _playerMoveScriptData._value = gameObject;
    }
}
