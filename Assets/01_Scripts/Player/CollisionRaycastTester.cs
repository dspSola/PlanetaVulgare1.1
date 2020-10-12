using UnityEngine;

public class CollisionRaycastTester : MonoBehaviour
{
    #region Show In Inspector

    [Header("Parameters")]

    [SerializeField]
    private float _length;

    [SerializeField]
    private Transform[] _origins;

    [SerializeField]
    private Vector3 _direction;

    [SerializeField]
    private LayerMask _layerMask;

    [Header("Debug")]

    [SerializeField]
    private bool _drawGizmos;

    [SerializeField]
    private Color _gizmosColor;

    [SerializeField] private string _typeOfGround;

    #endregion


    #region Public methods

    public bool TestCollision(float length, LayerMask layerMask)
    {
        foreach (var origin in _origins)
        {
            if (TestCollision(origin.position, _direction, length, layerMask, out _hit))
            {
                return true;
            }
        }

        return false;
    }

    public bool TestCollision()
    {
        return TestCollision(_length, _layerMask);
    }

    public bool AverageCollision(out Vector3 averageCollisionPosition)
    {
        int hitCount = 0;
        Vector3 combinedPosition = Vector3.zero;

        foreach (var origin in _origins)
        {
            if (TestCollision(origin.position, _direction, _length, _layerMask, out _hit))
            {
                _typeOfGround = _hit.transform.tag;
                combinedPosition += _hit.point;
                hitCount++;
            }
        }

        if (hitCount > 0)
        {
            averageCollisionPosition = combinedPosition / hitCount;
        }
        else
        {
            averageCollisionPosition = Vector3.zero;
        }

        _floorPosition = averageCollisionPosition;
        _hitCount = hitCount;

        return hitCount > 0;
    }

    public bool TestCollision(Vector3 origin, Vector3 direction, float length, LayerMask layerMask, out RaycastHit hit)
    {
        if (Physics.Raycast(origin, direction, out hit, length, layerMask))
        {
            return true;
        }
        else
        {
            hit = new RaycastHit();
            return false;
        }
    }

    #endregion


    #region Debug

    private void OnDrawGizmos()
    {
        if (!_drawGizmos)
        {
            return;
        }

        Gizmos.color = _gizmosColor;

        foreach (var origin in _origins)
        {
            Gizmos.DrawRay(origin.position, _direction * _length);
        }

    }

    #endregion


    #region Private

    private RaycastHit _hit;
    private Vector3 _floorPosition;
    private int _hitCount;

    public string TypeOfGround { get => _typeOfGround; set => _typeOfGround = value; }

    #endregion
}