using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionOverlapBoxTester : MonoBehaviour
{
    #region Show In Inspector

    [Header("Parameters")]

    [SerializeField]
    private Transform _center;

    [SerializeField]
    private Vector3 _halfExtents;

    [SerializeField]
    private LayerMask _layerMask;

    [Header("Debug")]

    [SerializeField]
    private bool _drawGizmos;

    [SerializeField]
    private Color _gizmosColor;

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        _activeColor = _gizmosColor * 1.2f;
        _inactiveColor = _gizmosColor * .8f;
    }

    private void Update()
    {
        if (TestCollision())
        {
            _color = _activeColor;
        }
        else
        {
            _color = _inactiveColor;
        }
    }

    #endregion


    #region Public methods

    public bool TestCollision(Vector3 center, Vector3 halfExtents, Quaternion orientation, LayerMask layerMask)
    {
        return Physics.OverlapBoxNonAlloc(center, halfExtents, _colliderBuffer, orientation, layerMask) > 0;
    }

    public bool TestCollision()
    {
        return TestCollision(_center.position, _halfExtents, Quaternion.identity, _layerMask);
    }

    #endregion


    #region Debug

    private void OnDrawGizmos()
    {
        if (!_drawGizmos)
        {
            return;
        }

        Gizmos.color = _color;

        Gizmos.DrawWireCube(_center.position, _halfExtents * 2);
    }

    #endregion


    #region Private

    private Collider[] _colliderBuffer = new Collider[1];
    private Color _activeColor;
    private Color _inactiveColor;
    private Color _color;

    #endregion
}
