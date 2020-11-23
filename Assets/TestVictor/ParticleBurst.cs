using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurst : MonoBehaviour
{
    [SerializeField] private PlayerEventStory _playerEventStory;
    [SerializeField] GameObject _particleBurst;
    [SerializeField] GameObject _bloodSun;
    [SerializeField] GameObject _sunnySun;

    private void Start()
    {
        if(_playerEventStory.PlayerPassPortal)
        {
            _sunnySun.SetActive(true);
            _bloodSun.SetActive(false);
        }
    }

    public void ParticleBurstStart()
    {
        _particleBurst.SetActive(true);
    }
}
