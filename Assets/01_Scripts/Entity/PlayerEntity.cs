using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : Entity
{
    [SerializeField] private HUDLifePlayer _hUDLifePlayer;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerEventStory _playerEventStory;
    [SerializeField] private BruteAnimatorController _bruteAnimatorController;
    [SerializeField] private Transform _playerTransform, _targetBottom, _targetMidle, _targetTop, _debugTop, _debugDown;

    [SerializeField] private float _rageMax, _rage, _coefRage, _coefTimeLessRage, _timeRage, _timeRageMax, _valueRageAddAttack, _valueRageAddLessLife;

    [SerializeField] private GameObject _canvasDie;
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private List<AudioClip> _sfxAudioClips;

    [SerializeField] private bool _ifPlayerIsDebuged;
    [SerializeField] private LayerMask _layerMaskDebug;
    [SerializeField] private StateMachineVertical _stateMachineVertical;
    [SerializeField] private StateMachineHorizontal _stateMachineHorizontal;
    [SerializeField] private StateMachineAttack _stateMachineAttack;

    public override void InitializeEntity()
    {
        base.InitializeEntity();
        _playerData.LifeCoef = base.CoefLife;
        _hUDLifePlayer.SetLife(base.CoefLife);
        _hUDLifePlayer.SetRage(_coefRage);
    }

    //private void Awake()
    //{
    //    _playerEventStory.TakeSaveData();
    //}

    private void Start()
    {
        if (_hUDLifePlayer == null)
        {
            _hUDLifePlayer = GameObject.Find("HUDLifePlayer").GetComponent<HUDLifePlayer>();
        }
        if (base.InitializeOnStart)
        {
            InitializeEntity();
        }

        if (_playerEventStory.PosCheckPointDie != Vector3.zero)
        {
            _playerTransform.position = _playerEventStory.PosCheckPointDie;
        }
        else if (_playerEventStory.PosSave != Vector3.zero)
        {
            _playerTransform.position = _playerEventStory.PosSave;
        }
    }

    private void Update()
    {
        if (_timeRage > 0)
        {
            _timeRage -= Time.deltaTime;
        }
        else
        {
            if (_rage > 0 && _rage < _rageMax)
            {
                LessRageTime();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_ifPlayerIsDebuged)
        {
            if (_stateMachineVertical.CurrentState != PlayerVerticalState.GROUNDED)
            {
                bool touchUp = false;
                bool touchDown = false;
                Vector3 vectorHit = Vector3.zero;

                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(_debugDown.position, _debugDown.TransformDirection(Vector3.up), out hit, Mathf.Infinity, _layerMaskDebug))
                {
                    Debug.DrawRay(_debugDown.position, _debugDown.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                    touchUp = true;
                    vectorHit = hit.point;
                }
                else
                {
                    Debug.DrawRay(_debugDown.position, _debugDown.TransformDirection(Vector3.up) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }

                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(_debugTop.position, _debugTop.TransformDirection(Vector3.down), out hit, Mathf.Infinity, _layerMaskDebug))
                {
                    Debug.DrawRay(_debugTop.position, _debugTop.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                    touchDown = true;
                    vectorHit = hit.point;
                }
                else
                {
                    Debug.DrawRay(_debugTop.position, _debugTop.TransformDirection(Vector3.down) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }

                if (touchUp || touchDown)
                {
                    _playerTransform.position = vectorHit + new Vector3(0, 1, 0);
                    _ifPlayerIsDebuged = false;
                }
            }
            else
            {
                _bruteAnimatorController.DebugAnimator();
                _stateMachineAttack.DebugPlayer();
                _ifPlayerIsDebuged = false;
            }
        }
    }

    //private void OnGUI()
    //{
    //    if (_style == null)
    //    {
    //        _style = new GUIStyle("button");
    //        _style.fontSize = 24;
    //        _style.alignment = TextAnchor.MiddleLeft;
    //        _style.padding = new RectOffset(15, 15, 0, 0);
    //    }
    //    using (new GUILayout.AreaScope(new Rect(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.9f, Screen.width * 0.2f, Screen.height * 0.1f)))
    //    {
    //        using (new GUILayout.VerticalScope())
    //        {
    //            GUILayout.Button($"Life: {base.Life}/{base.LifeMax}", _style, GUILayout.ExpandHeight(true));
    //        }
    //    }
    //}

    public override void AddLife(float value)
    {
        base.AddLife(value);
        _playerData.LifeCoef = base.CoefLife;
        _hUDLifePlayer.SetLife(base.CoefLife);
    }

    public override void LessLife(float value)
    {
        base.LessLife(value);
        _playerData.LifeCoef = base.CoefLife;
        _hUDLifePlayer.SetLife(base.CoefLife);
        _bruteAnimatorController.SetHurt(true);

        AddRage(_valueRageAddLessLife);

        if (base.Life <= 0)
        {
            _bruteAnimatorController.SetDeath(true);
            _canvasDie.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void AddRage(float value)
    {
        if(_timeRage != _timeRageMax)
        {
            _timeRage = _timeRageMax;
        }

        if (_rage + value >= _rageMax)
        {
            _rage = _rageMax;
        }
        else
        {
            _rage += value;
        }
        _coefRage = _rage / _rageMax;
        _hUDLifePlayer.SetRage(_coefRage);
    }
    public void LessRage(float value)
    {
        if (_rage - value < 0)
        {
            _rage = 0;
        }
        else
        {
            _rage -= value;
        }
        _coefRage = _rage / _rageMax;
        _hUDLifePlayer.SetRage(_coefRage);
    }

    public void LessRageTime()
    {
        _rage -= Time.deltaTime * _coefTimeLessRage;
        _coefRage = _rage / _rageMax;
        _hUDLifePlayer.SetRage(_coefRage);
    }

    public void ClickOnRetry()
    {
        _canvasDie.SetActive(false);

        _playerTransform.position = _playerEventStory.PosCheckPointDie;

        Cursor.lockState = CursorLockMode.Locked;
        base.Life = base.LifeMax;
        base.CoefLife = base.Life / base.LifeMax;
        _playerData.LifeCoef = base.CoefLife;
        _hUDLifePlayer.SetLife(base.CoefLife);
        _bruteAnimatorController.SetDeath(false);

        //GameObject.Find("Totems").GetComponent<ManagerTotemRoeload>().ReloadScene();
        _playerEventStory.ReloadScene();

        SceneManager.LoadScene("Main");
    }

    public void AddTotemEarth(Totem totem, Vector3 posCheckDie, bool valueAddLife)
    {
        //_hUDLifePlayer.AddTotem(totem.SpriteTotem);
        _hUDLifePlayer.SetIconColor(totem.FillColor, 0);
        _playerEventStory.AddTotemEarth();
        //if (!valueAddLife)
        //{
        //    UpgradeLife(50);
        //}
        _playerEventStory.PosCheckPointDie = posCheckDie;
    }
    public void AddTotemFire(Totem totem, Vector3 posCheckDie, bool valueAddLife)
    {
        //_hUDLifePlayer.AddTotem(totem.SpriteTotem);
        _hUDLifePlayer.SetIconColor(totem.FillColor, 1);
        _playerEventStory.AddTotemFire();
        //if (!valueAddLife)
        //{
        //    UpgradeLife(50);
        //}
        _playerEventStory.PosCheckPointDie = posCheckDie;
    }
    public void AddTotemWater(Totem totem, Vector3 posCheckDie, bool valueAddLife)
    {
        //_hUDLifePlayer.AddTotem(totem.SpriteTotem);
        _hUDLifePlayer.SetIconColor(totem.FillColor, 2);
        _playerEventStory.AddTotemWater();
        //if (!valueAddLife)
        //{
        //    UpgradeLife(50);
        //}
        _playerEventStory.PosCheckPointDie = posCheckDie;
    }
    public void AddTotemWind(Totem totem, Vector3 posCheckDie, bool valueAddLife)
    {
        //_hUDLifePlayer.AddTotem(totem.SpriteTotem);
        _hUDLifePlayer.SetIconColor(totem.FillColor, 3);
        _playerEventStory.AddTotemWind();
        //if (!valueAddLife)
        //{
        //    UpgradeLife(50);
        //}
        _playerEventStory.PosCheckPointDie = posCheckDie;
    }

    public void LifeToLifeMax()
    {
        base.Life = base.LifeMax;
        base.CoefLife = base.Life / base.LifeMax;
        _playerData.LifeCoef = base.CoefLife;
        _hUDLifePlayer.SetLife(base.CoefLife);
    }

    public void PlayOnShootSfxSound(AudioClip audioClip)
    {
        _sfxAudioSource.PlayOneShot(audioClip);
    }

    public void PlayBiteApple()
    {
        _sfxAudioSource.PlayOneShot(_sfxAudioClips[0]);
    }

    public void DebugPlayer()
    {
        _ifPlayerIsDebuged = true;
    }

    private GUIStyle _style;

    public float RageMax { get => _rageMax; set => _rageMax = value; }
    public float Rage { get => _rage; set => _rage = value; }
    public float ValueRageAddAttack { get => _valueRageAddAttack; set => _valueRageAddAttack = value; }
    public Transform TargetBottom { get => _targetBottom; set => _targetBottom = value; }
    public Transform TargetMidle { get => _targetMidle; set => _targetMidle = value; }
    public Transform TargetTop { get => _targetTop; set => _targetTop = value; }
    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
}
