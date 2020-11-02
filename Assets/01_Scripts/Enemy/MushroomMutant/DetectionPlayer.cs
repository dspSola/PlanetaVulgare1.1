using UnityEngine;

public class DetectionPlayer : MonoBehaviour
{
    [SerializeField] private ScriptableTransform _playerHeadTarget;
    [SerializeField] MushroomManager _mushroomManager;
    [SerializeField] BoolVariable _boolAlertSysthem;

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
            Ray ray = new Ray(_transform.position, _playerHeadTarget.value.position);
            Physics.Raycast(ray, out hit);

            Debug.DrawLine(_transform.position, _playerHeadTarget.value.position, Color.red);
            //Debug.Log("le player est destecté !!!");
            _mushroomManager.IsDetecting = true;
            _boolAlertSysthem.value = true;
        }
        else
        {
            //_boolAlertSysthem.value = false;
            Debug.DrawLine(_transform.position, _playerHeadTarget.value.position, Color.green);
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
            _mushroomManager.IsDetecting = false;
        }
    }

    private Transform _transform;
    private Collider _coneTrigger;

    private bool _playerIsTrigger;
   

    public bool PlayerIsTrigger { get => _playerIsTrigger;}
    
}
