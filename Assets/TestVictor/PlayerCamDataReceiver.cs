using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamDataReceiver : MonoBehaviour
{
    [SerializeField] GameObjectData _playerCamData;

    private void Awake()
    {
        _playerCamData._value = this.gameObject;
    }
}
