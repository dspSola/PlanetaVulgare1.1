using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossSpellManager : MonoBehaviour
{
    [SerializeField] private FireBossAnimator _fireBossAnimatorMono;
    [SerializeField] private FireBossAgentController _fireBossAgentController;
    [SerializeField] private float _timeSpellRate, _timeSpellRateMax;
    [SerializeField] private bool _isInSpell;

    public GameObject _firepointLeft, _firePointRight;
    public List<GameObject> _vfxs = new List<GameObject>();

    private GameObject effectToSpawn;

    private void Start()
    {
        effectToSpawn = _vfxs[0];
    }

    private void Update()
    {
        if (!_isInSpell)
        {
            if (_timeSpellRate >= _timeSpellRateMax)
            {
                int randomChanceSpell = Random.Range(0, 100);
                //Debug.Log(random);
                if (randomChanceSpell < 75)
                {
                    int randomIndexSpell = Random.Range(1, 4);
                    //SetSpell(true, randomIndexSpell);
                    SetSpell(true, randomIndexSpell);
                }
                _timeSpellRate = 0;
            }
            else
            {
                _timeSpellRate += Time.deltaTime;
            }
        }
    }

    public void SpawnSpell(int indexVFX, string value)
    {

        if (value == "Left")
        {
            SpawnSpellLeft(indexVFX);
        }
        else if (value == "Right")
        {
            SpawnSpellRight(indexVFX);
        }
        else if (value == "Two")
        {
            SpawnSpellLeft(indexVFX);
            SpawnSpellRight(indexVFX);
        }
        else
        {
            Debug.Log("Value Not Egal Left Right Or Two");
        }
    }

    public void SpawnSpell(int indexVFX, Vector3 posPlayer)
    {
        SpawnOnPlayer(indexVFX, posPlayer);
    }

    private void SpawnSpellLeft(int indexVFX)
    {
        GameObject vfx;
        if (_firepointLeft != null)
        {
            Vector3 relativePos = _fireBossAgentController.PlayerTransform.position - _fireBossAgentController.FireBossTransform.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            vfx = Instantiate(_vfxs[indexVFX], _firepointLeft.transform.position, rotation);
        }
        else
        {
            Debug.Log("No Fire Poinr");
        }
    }

    private void SpawnSpellRight(int indexVFX)
    {
        GameObject vfx;
        if (_firepointLeft != null)
        {
            Vector3 relativePos = _fireBossAgentController.PlayerTransform.position - _fireBossAgentController.FireBossTransform.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            vfx = Instantiate(_vfxs[indexVFX], _firePointRight.transform.position, rotation);
        }
        else
        {
            Debug.Log("No Fire Poinr");
        }
    }

    private void SpawnOnPlayer(int indexVFX)
    {
        GameObject vfx;
        if (_firepointLeft != null)
        {
            Vector3 relativePos = _fireBossAgentController.PlayerTransform.position - _fireBossAgentController.FireBossTransform.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            vfx = Instantiate(_vfxs[indexVFX], _fireBossAgentController.PlayerTransform.position, rotation);
        }
        else
        {
            Debug.Log("No Fire Poinr");
        }
    }

    private void SpawnOnPlayer(int indexVFX, Vector3 posPlayer)
    {
        GameObject vfx;
        if (_firepointLeft != null)
        {
            Vector3 relativePos = _fireBossAgentController.PlayerTransform.position - _fireBossAgentController.FireBossTransform.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            vfx = Instantiate(_vfxs[indexVFX], posPlayer, rotation);
        }
        else
        {
            Debug.Log("No Fire Poinr");
        }
    }

    public void SetSpell(bool valueBool, int valueInt)
    {
        _fireBossAnimatorMono.SetSpell(valueBool, valueInt);
        _isInSpell = valueBool;
    }

    public bool IsInSpell { get => _isInSpell; set => _isInSpell = value; }
}
