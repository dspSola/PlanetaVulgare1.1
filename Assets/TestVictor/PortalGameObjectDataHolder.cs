using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGameObjectDataHolder : MonoBehaviour
{
    [SerializeField] GameObjectData _portalGameObjectData;

    private void Start()
    {
        _portalGameObjectData._value = gameObject;
    }
}
