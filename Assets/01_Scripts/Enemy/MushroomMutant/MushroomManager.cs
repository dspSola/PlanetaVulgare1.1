using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField] private MushroomEntity _mushroomEntity;
    [SerializeField] private DetectionPlayer _detectionPlayer;

    private void Awake()
    {
        //if (_mushroomEntity = null)
        //{
        //    _mushroomEntity = GetComponent<MushroomEntity>();
        //}
    }

    private void Start()
    {
        _isDead = false;
    }

    private void Update()
    {
        //_MushroomEntity.CurrentLife = Mathf.Clamp(_MushroomEntity.CurrentLife, _playerParameter.Damage, _MushroomEntity.LifeMax);

        //if (_damage)
        //{
        //    //_MushroomEntity.CurrentLife -= _playerParameter.Damage;
        //}
        //else
        //{
        //    _damage = false;
        //}

        //Debug.Log("life =" + _mushroomEntity.Life);
        if (_mushroomEntity.Life <= 0)
        {
            _isDead = true;
            //Debug.Log("pret a mourir");
        }
    }

    //public bool _damage;
    private bool _isDetecting;
    private bool _isAlerting;
    private bool _isHittingPlayer;
    private bool _isDead;

    public bool IsDetecting { get => _isDetecting; set => _isDetecting = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }
    public bool IsAlerting { get => _isAlerting; set => _isAlerting = value; }
}