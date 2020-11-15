using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossSpellManager : MonoBehaviour
{
    [SerializeField] private EarthBossAnimatorMono _earthBossAnimatorMono;
    [SerializeField] private EarthBossAgentController _earthBossAgentController;
    [SerializeField] private Transform _windBossTransformMesh, _posMidleSpell;
    [SerializeField] private float _timeSpellRate, _timeSpellRateMax, _rangeMaxForSPell;
    [SerializeField] private bool _canSpell, _isInSpell;

    public GameObject _firepointLeft, _firePointRight;
    public List<GameObject> _vfxs = new List<GameObject>();

    private GameObject effectToSpawn;

    private void Start()
    {
        effectToSpawn = _vfxs[0];
    }

    private void Update()
    {
        if(_earthBossAgentController.DistancePlayer > _rangeMaxForSPell)
        {
            _canSpell = true;
        }
        else
        {
            _canSpell = false;
        }
        if (!_isInSpell)
        {
            if (_timeSpellRate > _timeSpellRateMax)
            {
                if (_canSpell)
                {
                    _earthBossAnimatorMono.SetSpell(true, 1);
                }
                _timeSpellRateMax = Random.Range(_timeSpellRateMax - (_timeSpellRateMax / 2), _timeSpellRateMax + (_timeSpellRateMax / 2));
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
            Vector3 relativePos = _earthBossAgentController.PlayerTransform.position - _posMidleSpell.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            vfx = Instantiate(_vfxs[indexVFX], _firepointLeft.transform.position, rotation);
            vfx.GetComponent<EarthSpellRockBall>().Init(_firepointLeft.transform, _firePointRight.transform, _earthBossAgentController.PlayerTransform.GetComponentInChildren<PlayerEntity>().TargetTop);
        }
        else
        {
            Debug.Log("No Fire Poinr");
        }
    }

    private void SpawnSpellRight(int indexVFX)
    {
        GameObject vfx;
        if (_firePointRight != null)
        {
            Vector3 relativePos = _earthBossAgentController.PlayerTransform.position - _posMidleSpell.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            vfx = Instantiate(_vfxs[indexVFX], _firePointRight.transform.position, rotation);
            vfx.GetComponent<EarthSpellRockBall>().Init(_firepointLeft.transform, _firePointRight.transform, _earthBossAgentController.PlayerTransform.GetComponentInChildren<PlayerEntity>().TargetTop);
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
            Vector3 relativePos = _earthBossAgentController.PlayerTransform.position - _posMidleSpell.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            vfx = Instantiate(_vfxs[indexVFX], _earthBossAgentController.PlayerTransform.position, rotation);
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
            Vector3 relativePos = _earthBossAgentController.PlayerTransform.position - _windBossTransformMesh.position;
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
        _earthBossAnimatorMono.SetSpell(valueBool, valueInt);
        _isInSpell = valueBool;
    }

    public bool IsInSpell { get => _isInSpell; set => _isInSpell = value; }
    public bool CanSpell { get => _canSpell; set => _canSpell = value; }
}
