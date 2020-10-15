using UnityEngine;

public class DetectionPlayer : MonoBehaviour
{
    [SerializeField] Transform _playerHeadTarget;

    private void Awake()
    {
        _transform = transform;
        _coneTrigger = GetComponent<Collider>();
    }

    private void Update()
    {
        if(_playerIsTrigger)
        {
            RaycastHit hit;
            Ray ray = new Ray(_transform.position, _playerHeadTarget.position);
            Physics.Raycast(ray, out hit);

            Debug.DrawLine(_transform.position, _playerHeadTarget.position, Color.red);
            Debug.Log("le player est destecté !!!");
            _isPatrolling = false;
        }
        else
        {
            Debug.DrawLine(_transform.position, _playerHeadTarget.position, Color.green);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            _playerIsTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            _playerIsTrigger = false;
            _isPatrolling = true;
        }
    }

    private Transform _transform;
    private Collider _coneTrigger;

    private bool _playerIsTrigger;
    private bool _isPatrolling;

    public bool PlayerIsTrigger { get => _playerIsTrigger; set => _playerIsTrigger = value; }
}
