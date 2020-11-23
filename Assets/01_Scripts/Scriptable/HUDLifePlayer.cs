﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDLifePlayer : MonoBehaviour
{
    [SerializeField] private PlayerEventStory _playerEventStory;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Image barLife_Blood, _barRage;

    [SerializeField] private Color _goodColor;
    [SerializeField] private Color _middleColor;
    [SerializeField] private Color _badColor;

    [Header("Totems")]
    [SerializeField] private List<Image> _imageTotems;
    //[SerializeField] private Image _imageTotemsEarth;
    //[SerializeField] private Image _imageTotemsFire;
    //[SerializeField] private Image _imageTotemsWater;
    //[SerializeField] private Image _imageTotemsWind;


    [SerializeField] private int _nbTotem;
    [SerializeField] private Color _initColor, _fireTotemColor, _earthTotemColor, _waterTotemColor, _windTotemColor;

    public int NbTotem { get => _nbTotem; set => _nbTotem = value; }

    private void Start()
    {
        //_imageTotemsEarth.color = _intColor;
        //_imageTotemsFire.color = _intColor;
        //_imageTotemsWater.color = _intColor;
        //_imageTotemsWind.color = _intColor;

        for (int i = 0; i < _imageTotems.Count; i++)
        {
            //_imageTotems[i].transform.parent.gameObject.SetActive(false);
            _imageTotems[i].color = _initColor;
        }

        if (_playerEventStory.BossEarth && _playerEventStory.TotemEarth)
        {
            _imageTotems[0].color = _earthTotemColor;
        }
        if (_playerEventStory.BossFire && _playerEventStory.TotemFire)
        {
            _imageTotems[1].color = _fireTotemColor;
        }
        if (_playerEventStory.BossWater && _playerEventStory.TotemWater)
        {
            _imageTotems[2].color = _waterTotemColor;
        }
        if (_playerEventStory.BossWind && _playerEventStory.TotemWind)
        {
            _imageTotems[3].color = _windTotemColor;
        }
    }

    private void Update()
    {
        //SetLife(_playerData.LifeCoef);
    }

    public void SetLife(float value)
    {
        barLife_Blood.fillAmount = value;
        SetColor(value);
    }

    public void SetRage(float value)
    {
        _barRage.fillAmount = value;
    }

    void SetColor(float value)
    {
        if (value >= 0.5f)
        {
            barLife_Blood.color = _goodColor;
        }
        else if (value >= 0.25f && value < 0.5f)
        {
            barLife_Blood.color = _middleColor;
        }
        else
        {
            barLife_Blood.color = _badColor;
        }
    }

    //public void AddTotem(Sprite totemSprite)
    //{
    //    _imageTotems[_nbTotem].transform.parent.gameObject.SetActive(true);
    //    _imageTotems[_nbTotem].sprite = totemSprite;
    //    _nbTotem++;
    //}

    public void SetIconColor(Color color, int index)
    {
        _imageTotems[index].color = color;

        //_imageTotemsEarth.color = color;
        //_imageTotemsFire.color = color;
        //_imageTotemsWater.color = color;
        //_imageTotemsWind.color = color;
    }
}
