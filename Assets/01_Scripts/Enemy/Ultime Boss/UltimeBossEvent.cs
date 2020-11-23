using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimeBossEvent : MonoBehaviour
{
    [SerializeField] private PlayerEventStory _playerEventStory;
    [SerializeField] private GameObject _boss;
    [SerializeField] private Transform _posDiePlayerSave;
    [SerializeField] private BoolVariable _finalFight;

    private void Awake()
    {
        _finalFight.value = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            if (_playerEventStory.CptBossWin > 3)
            {
                _playerEventStory.PosCheckPointDie = _posDiePlayerSave.position;
                _boss.SetActive(true);
                _finalFight.value = true;
            }
        }
    }
}
