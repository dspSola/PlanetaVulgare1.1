using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFly : MonoBehaviour
{
    [SerializeField] private float _speedFly, _timeChangeSens, _timeChangeSensMax;
    [SerializeField] private bool _sensFly;

    private void Update()
    {
        if(_sensFly)
        {
            transform.position += Vector3.up * Time.deltaTime * _speedFly;
        }
        else
        {
            transform.position -= Vector3.up * Time.deltaTime * _speedFly;
        }

        if(_timeChangeSens < _timeChangeSensMax)
        {
            _timeChangeSens += Time.deltaTime;
        }
        else
        {
            _sensFly = !_sensFly;
            _timeChangeSens = 0;
        }
    }
}
