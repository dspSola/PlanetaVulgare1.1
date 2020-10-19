using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderManager : MonoBehaviour
{
    [SerializeField] private Entity _playerEntity;
    [SerializeField] private LayerMask _layerMask;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == _layerMask)
        {
            other.GetComponentInParent<Entity>().LessLife(_playerEntity.Damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
