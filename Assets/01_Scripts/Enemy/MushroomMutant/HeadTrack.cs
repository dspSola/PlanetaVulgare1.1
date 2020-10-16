using UnityEngine;
using UnityEngine.PlayerLoop;

public class HeadTrack : MonoBehaviour
{
    [SerializeField] Transform _head;
    [SerializeField] Vector3 _offset;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position = _head.position - _offset;
        //_transform.rotation = _head.rotation;
    }

    Transform _transform;
}
