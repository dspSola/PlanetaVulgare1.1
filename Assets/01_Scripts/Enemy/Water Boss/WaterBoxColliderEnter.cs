﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBoxColliderEnter : MonoBehaviour
{
    [SerializeField] private PlayerEventStory _playerEventStory;
    [SerializeField] private WaterBossEvent _waterBossEvent;
    [SerializeField] private GameObject _waterBlocks;
    [SerializeField] private bool _playerIn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            _playerEventStory.FightBossWater = true;
            _waterBlocks.SetActive(true);
            _playerIn = true;
            _waterBossEvent.ActiveWaterBoss();
            gameObject.SetActive(false);
        }
    }

    public void ResetWater()
    {
        _waterBlocks.SetActive(false);
        _playerIn = false;
    }
}
