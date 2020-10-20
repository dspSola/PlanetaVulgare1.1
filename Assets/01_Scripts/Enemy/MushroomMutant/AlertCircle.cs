using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertCircle : MonoBehaviour
{
    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            _isAlerted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _isAlerted = false;
        }
    }

    private SphereCollider _sphereCollider;
    public bool _isAlerted;

    public bool IsAlerted { get => _isAlerted;}
}
