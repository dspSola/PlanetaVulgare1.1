using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPoints00 : MonoBehaviour
{
    [SerializeField] Transform[] _transforms;

    private void Awake()
    {
        for (int i = 0; i < _transforms.Length; i++)
        {
            _transform.position = _transforms[i].position;
            Debug.Log($"la position de waypoint {i} est:  {_transform.position}");
        }
    }

    Transform _transform;
}
