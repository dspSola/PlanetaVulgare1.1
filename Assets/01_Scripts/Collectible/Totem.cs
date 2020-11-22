using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    [SerializeField] private string _totemName;
    [SerializeField] private Sprite _spriteTotem;
    [SerializeField] private Color _fillColor;
    [SerializeField] private GameObject _boss;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            if(other.gameObject.GetComponentInChildren<GetInputBrute>().UseInput.IsDown && other.gameObject.GetComponentInChildren<StateMachineAttack>().IsArmed == false)
            {
                if(_totemName == "Fire")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemFire(this);
                }
                if (_totemName == "Earth")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemEarth(this);
                }
                if (_totemName == "Wind")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemWind(this);
                }
                if (_totemName == "Water")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemWater(this);
                }

                _boss.SetActive(true);

                Destroy(gameObject);
            }
        }
    }

    public string TotemName { get => _totemName; set => _totemName = value; }
    public Sprite SpriteTotem { get => _spriteTotem; set => _spriteTotem = value; }
    public Color FillColor { get => _fillColor; set => _fillColor = value; }
}
