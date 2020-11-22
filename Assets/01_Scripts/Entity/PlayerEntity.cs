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
    [SerializeField] private Transform _playerTransform, _targetBottom, _targetMidle, _targetTop;

    [SerializeField] private float _rageMax, _rage, _coefRage, _coefTimeLessRage, _timeRage, _timeRageMax, _valueRageAddAttack, _valueRageAddLessLife;

    [SerializeField] private GameObject _canvasDie;

    public override void InitializeEntity()
    {
        base.InitializeEntity();
        _playerData.LifeCoef = base.CoefLife;
        _hUDLifePlayer.SetLife(base.CoefLife);
        _hUDLifePlayer.SetRage(_coefRage);
    }

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
        _playerEventStory.Init();
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
        SceneManager.LoadScene("Main");
    }

    public void AddTotemFire(Totem totem)
    {
        //_hUDLifePlayer.AddTotem(totem.SpriteTotem);
        _hUDLifePlayer.SetColor(totem.FillColor);
        _playerEventStory.AddTotemFire();
        UpgradeLife(50);
    }
    public void AddTotemWind(Totem totem)
    {
        //_hUDLifePlayer.AddTotem(totem.SpriteTotem);
        _hUDLifePlayer.SetColor(totem.FillColor);
        _playerEventStory.AddTotemWind();
        UpgradeLife(50);
    }
    public void AddTotemWater(Totem totem)
    {
        //_hUDLifePlayer.AddTotem(totem.SpriteTotem);
        _hUDLifePlayer.SetColor(totem.FillColor);
        _playerEventStory.AddTotemWater();
        UpgradeLife(50);
    }
    public void AddTotemEarth(Totem totem)
    {
        //_hUDLifePlayer.AddTotem(totem.SpriteTotem);
        _hUDLifePlayer.SetColor(totem.FillColor);
        _playerEventStory.AddTotemEarth();
        UpgradeLife(50);
    }

    public void LifeToLifeMax()
    {
        base.Life = base.LifeMax;
        base.CoefLife = base.Life / base.LifeMax;
        _playerData.LifeCoef = base.CoefLife;
        _hUDLifePlayer.SetLife(base.CoefLife);
    }

    private GUIStyle _style;

    public float RageMax { get => _rageMax; set => _rageMax = value; }
    public float Rage { get => _rage; set => _rage = value; }
    public float ValueRageAddAttack { get => _valueRageAddAttack; set => _valueRageAddAttack = value; }
    public Transform TargetBottom { get => _targetBottom; set => _targetBottom = value; }
    public Transform TargetMidle { get => _targetMidle; set => _targetMidle = value; }
    public Transform TargetTop { get => _targetTop; set => _targetTop = value; }
}
