using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellController : MonoBehaviour
{
    [SerializeField] private PlayerEntity _playerEntity;
    [SerializeField] private GameObject _VfxRage;
    [SerializeField] private Transform _positionMidlePlayer;

    public void SpawnSpellRage()
    {
        GameObject vfx;
        vfx = Instantiate(_VfxRage, _positionMidlePlayer.position, transform.rotation);
        vfx.GetComponent<SpellRage01>().SetPlayerEntity(_playerEntity);
        _playerEntity.LessRage(100);
    }
}
