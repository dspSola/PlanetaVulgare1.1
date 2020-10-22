using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderManager : MonoBehaviour
{
    [SerializeField] private StateMachineAttack _stateMachineAttack;
    [SerializeField] private Entity _playerEntity;
    [SerializeField] private LayerMask _layerMask;

    private void OnTriggerEnter(Collider other)
    {
        if (_stateMachineAttack.IsAnim)
        {
            if (other.gameObject.layer == 9 && other.gameObject.GetComponentInChildren<Entity>() != null)
            {
                other.gameObject.GetComponentInChildren<Entity>().LessLife(_playerEntity.Damage);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
