﻿using UnityEngine;

public class AverageAttack : MonoBehaviour
{
    [SerializeField] ScriptableTransform _playerTransform;
    [SerializeField] float _attackDistance;

    private void Awake()
    {
        _transform = transform;
        _isWithinRange = false;
    }

    void Update()
    {
        float averageDistance;
        averageDistance = Vector3.Distance(_transform.position, _playerTransform.value.position);
        Debug.Log("moyen X: " + averageDistance);
        if(averageDistance <= _attackDistance)
        {
            _isWithinRange = true;
        }
        else
        {
            _isWithinRange = false;
        }
    }

    Transform _transform;
    bool _isWithinRange;

    public bool IsWithinRange { get => _isWithinRange; set => _isWithinRange = value; }
}
