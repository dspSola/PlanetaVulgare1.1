using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTotemRoeload : MonoBehaviour
{
    [SerializeField] private PlayerEventStory _playerEventStory;
    [SerializeField] private Totem _totemFire, _totemWater, _totemEarth, _totemWind;

    public void ReloadScene()
    {
        if (_playerEventStory.TotemFire && !_playerEventStory.BossFire)
        {
            _totemFire.gameObject.SetActive(true);
            _totemFire.ResetBoss();
        }
        if (_playerEventStory.TotemEarth && !_playerEventStory.BossEarth)
        {
            _totemEarth.gameObject.SetActive(true);
            _totemEarth.ResetBoss();
        }
        if (_playerEventStory.TotemWind && !_playerEventStory.BossWind)
        {
            _totemWind.gameObject.SetActive(true);
            _totemWind.ResetBoss();
        }
        if (_playerEventStory.TotemWater && !_playerEventStory.BossWater)
        {
            _totemWater.gameObject.SetActive(true);
            _totemWater.ResetBoss();
        }
    }
}
