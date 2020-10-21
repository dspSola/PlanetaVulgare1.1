using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    [SerializeField] private string _totemName;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
        {
            if(other.gameObject.GetComponentInChildren<GetInputBrute>().UseInput.IsDown)
            {
                if(_totemName == "Fire")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemFire();
                }
                if (_totemName == "Earth")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemEarth();
                }
                if (_totemName == "Wind")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemWind();
                }
                if (_totemName == "Water")
                {
                    other.gameObject.GetComponentInChildren<PlayerEntity>().AddTotemWater();
                }

                Destroy(gameObject);
            }
        }
    }
}
