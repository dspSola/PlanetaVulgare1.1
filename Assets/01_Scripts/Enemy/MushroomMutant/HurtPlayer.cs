using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    [SerializeField] EnemyEntityData _mushroomEntityData;
    //[SerializeField] PlayerEntityData _playerEntityData;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_player))
        {
            other.GetComponentInChildren<PlayerEntity>().LessLife(_mushroomEntityData.Damage);
            Debug.Log("player touché!!!");
        }
    }

    string _player = "PlayerColl";
}
