using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    [SerializeField] private PlayerEventStory _playerEventStory;
    [SerializeField] private string _totemName;
    [SerializeField] private Sprite _spriteTotem;
    [SerializeField] private Color _fillColor;
    [SerializeField] private GameObject _boss;
    [SerializeField] private Transform _posDiePlayerSave, _posRespwanBoss;
    [SerializeField] private bool _playerAlreadyPickUpInTheGame;
    [SerializeField] private BoolVariable _isCatchingTotem;

    private void Start()
    {
        if (_totemName == "Fire")
        {
           if(_playerEventStory.TotemFire && _playerEventStory.BossFire)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
        if (_totemName == "Earth")
        {
            if (_playerEventStory.TotemEarth && _playerEventStory.BossEarth)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
        if (_totemName == "Wind")
        {
            if (_playerEventStory.TotemWind && _playerEventStory.BossWind)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
        if (_totemName == "Water")
        {
            if (_playerEventStory.TotemWater && _playerEventStory.BossWater)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        _isCatchingTotem.value = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            if(other.gameObject.GetComponentInChildren<GetInputBrute>().UseInput.IsDown && other.gameObject.GetComponentInChildren<StateMachineAttack>().IsArmed == false)
            {
                if(_totemName == "Fire")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemFire(this, _posDiePlayerSave.position, _playerAlreadyPickUpInTheGame);
                }
                if (_totemName == "Earth")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemEarth(this, _posDiePlayerSave.position, _playerAlreadyPickUpInTheGame);
                }
                if (_totemName == "Wind")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemWind(this, _posDiePlayerSave.position, _playerAlreadyPickUpInTheGame);
                }
                if (_totemName == "Water")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemWater(this, _posDiePlayerSave.position, _playerAlreadyPickUpInTheGame);
                }

                if(!_playerAlreadyPickUpInTheGame)
                {
                    _playerAlreadyPickUpInTheGame = true;
                }

                _boss.SetActive(true);

                //Destroy(gameObject);
                gameObject.SetActive(false);
                _isCatchingTotem.value = true;
                Destroy(gameObject);
            }
        }
    }

    public void ResetBoss()
    {
        _boss.transform.position = _posRespwanBoss.position;
        _boss.GetComponentInChildren<BossEntity>().Life = _boss.GetComponentInChildren<BossEntity>().LifeMax;
        _boss.SetActive(false);
    }

    public string TotemName { get => _totemName; set => _totemName = value; }
    public Sprite SpriteTotem { get => _spriteTotem; set => _spriteTotem = value; }
    public Color FillColor { get => _fillColor; set => _fillColor = value; }
}
