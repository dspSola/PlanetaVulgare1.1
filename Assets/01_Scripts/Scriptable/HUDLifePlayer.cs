﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDLifePlayer : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private Image barLife_Blood;

    [SerializeField] private Color _goodColor;
    [SerializeField] private Color _middleColor;
    [SerializeField] private Color _badColor;

    [SerializeField] private List<Image> _imageTotems;
    [SerializeField] private int _nbTotem;

    private void Start()
    {
        for(int i = 0; i < _imageTotems.Count; i++)
        {
            _imageTotems[i].transform.parent.gameObject.SetActive(false);
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

    public void AddTotem(Sprite totemSprite)
    {
        _imageTotems[_nbTotem].transform.parent.gameObject.SetActive(true);
        _imageTotems[_nbTotem].sprite = totemSprite;
        _nbTotem++;
    }
}
