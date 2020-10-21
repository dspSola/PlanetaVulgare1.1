﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : Entity
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerEventStory _playerEventStory;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _coefLife;

    [SerializeField] private GameObject _canvasDie;

    public override void InitializeEntity()
    {
        base.InitializeEntity();
        _coefLife = base.Life / base.LifeMax;
        _playerData.LifeCoef = _coefLife;
    }

    private void Start()
    {
        if (base.InitializeOnStart)
        {
            InitializeEntity();
        }
        _playerEventStory.Init();
    }

    private void OnGUI()
    {
        if (_style == null)
        {
            _style = new GUIStyle("button");
            _style.fontSize = 24;
            _style.alignment = TextAnchor.MiddleLeft;
            _style.padding = new RectOffset(15, 15, 0, 0);
        }
        using (new GUILayout.AreaScope(new Rect(Screen.width - Screen.width * 0.2f, Screen.height - Screen.height * 0.9f, Screen.width * 0.2f, Screen.height * 0.1f)))
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Button($"Life: {base.Life}/{base.LifeMax}", _style, GUILayout.ExpandHeight(true));
            }
        }
    }

    public override void AddLife(float value)
    {
        base.AddLife(value);
        _coefLife = base.Life / base.LifeMax;
        _playerData.LifeCoef = _coefLife;
    }

    public override void LessLife(float value)
    {
        base.LessLife(value);
        _coefLife = base.Life / base.LifeMax;
        _playerData.LifeCoef = _coefLife;
        if (base.Life <= 0)
        {
            _canvasDie.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ClickOnRetry()
    {
        _canvasDie.SetActive(false);
        _playerTransform.position = _playerEventStory.PosCheckPointDie;
        Cursor.lockState = CursorLockMode.Locked;
        base.Life = base.LifeMax;
        _coefLife = base.Life / base.LifeMax;
        _playerData.LifeCoef = _coefLife;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddTotemFire()
    {
        _playerEventStory.AddTotemFire();
        UpgradeLife(50);
    }
    public void AddTotemWind()
    {
        _playerEventStory.AddTotemWind();
        UpgradeLife(50);
    }
    public void AddTotemWater()
    {
        _playerEventStory.AddTotemWater();
        UpgradeLife(50);
    }
    public void AddTotemEarth()
    {
        _playerEventStory.AddTotemEarth();
        UpgradeLife(50);
    }

    private GUIStyle _style;

    public float CoefLife { get => _coefLife; set => _coefLife = value; }
}
