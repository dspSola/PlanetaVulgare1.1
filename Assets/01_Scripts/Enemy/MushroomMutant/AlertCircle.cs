using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertCircle : MonoBehaviour
{
    [SerializeField] BoolVariable _boolAlertSysthem;
    [SerializeField] MushroomManager _mushroomManager;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _mushroomManager.IsAlerting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && _boolAlertSysthem.value)
        {
            _mushroomManager.IsAlerting = true;
            Debug.Log("VU!!!!");
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
