using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] PlayerEventStory _playerEventStory;

    Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerColl"))
        {
            _playerEventStory.PosCheckPointDie = _transform.position;
        }
    }
}
