using System.Collections.Generic;
using UnityEngine;

public class WaitPoints00 : MonoBehaviour
{
    [SerializeField] private TransformArrayData _transformWaypoints;
    [SerializeField] private List<Transform> _transforms = new List<Transform>();

    private void Awake()
    {
        if (_transformWaypoints.Value.Count >= 4)
        {
            _transformWaypoints.Value.Clear();
        }
    }

    private void Start()
    {
        foreach (Transform t in _transforms)
        {
            _transformWaypoints.Value.Add(t);
        }
        //Debug.Log("le nombre d'éléments dans le array est de " + _transformWaypoints.Value.Count);
    }
}
