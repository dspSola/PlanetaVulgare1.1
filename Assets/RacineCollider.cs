using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacineCollider : MonoBehaviour
{
    [SerializeField] private RacineBehaviour _racineBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (other.gameObject.tag == "PlayerColl")
            {
                if (_racineBehaviour.CanTouchPlayer)
                {
                    if (!_racineBehaviour.TouchPlayer)
                    {
                        other.gameObject.GetComponentInChildren<PlayerEntity>().LessLife(5);
                        _racineBehaviour.TouchPlayer = true;
                    }
                }
            }
        }
    }
}