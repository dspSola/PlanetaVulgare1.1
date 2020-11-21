using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimeBossHand : MonoBehaviour
{
    [SerializeField] private UltimeBossEntity _bossEntity;
    [SerializeField] private bool _canDamageCac, _touchPlayer;

    public bool CanDamageCac { get => _canDamageCac; set => _canDamageCac = value; }
    public bool TouchPlayer { get => _touchPlayer; set => _touchPlayer = value; }

    private void Start()
    {
        SetActiveCollider(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ici : " + other.name);
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            Debug.Log("Ici Player Collider : " + other.name);
            if (_canDamageCac)
            {
                Debug.Log("Ici Player Collider : " + other.name + "!_touchPlayer && _canDamageCac");
                if (other.GetComponentInChildren<StateMachineAttack>().CurrentState != PlayerAttackState.PROTECTION)
                {
                    Debug.Log("Ici Player Collider : " + other.name + "!= PlayerAttackState.PROTECTION");
                    other.GetComponentInChildren<PlayerEntity>().LessLife(_bossEntity.Damage);
                    _touchPlayer = true;
                }
            }
        }
    }

    public void InitializeHand()
    {
        _canDamageCac = true;
        _touchPlayer = false;
    }

    public void SetActiveCollider(bool value)
    {
        gameObject.GetComponent<Collider>().enabled = value;
    }
}
