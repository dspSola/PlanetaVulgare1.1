using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimeBossSpellManager : MonoBehaviour
{
    [SerializeField] private UltimeBossAnimatorMono _ultimeBossAnimatorMono;
    [SerializeField] private UltimeBossAgentController _ultimeBossAgentController;
    [SerializeField] private Transform _ultimeBossTransformMesh, _posMidleSpell, _parentForSpell;
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
        if (_ultimeBossAgentController.DistancePlayer > _rangeMaxForSPell)
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
                    _ultimeBossAnimatorMono.SetSpell(true, Random.Range(1,5));
                    //_ultimeBossAnimatorMono.SetSpell(true, 4);
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

    public void FireSpell()
    {
        int cptMeteorite = Random.Range(3, 7);
        for (int i = 0; i <= cptMeteorite; i++)
        {
            Vector3 newPos = transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(4f, 10f), Random.Range(-5f, 5f));

            Vector3 relativePos = (_ultimeBossAgentController.PlayerTransform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(0.5f,2f), Random.Range(-2f, 2f))) - newPos;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            GameObject cloneMeteorite = Instantiate(_vfxs[0], newPos, rotation, _parentForSpell);

            float randomSizeScale = Random.Range(0.5f, 1.2f);
            cloneMeteorite.transform.localScale = new Vector3(randomSizeScale, randomSizeScale, randomSizeScale);
        }
    }

    public void EartSpell()
    {
        int cptRacine = Random.Range(3, 7);
        for (int i = 0; i <= cptRacine; i++)
        {
            Vector3 newPos = _ultimeBossAgentController.PlayerTransform.position + new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

            Vector3 relativePos = _ultimeBossAgentController.PlayerTransform.position - newPos;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            GameObject cloneRacine = Instantiate(_vfxs[1], newPos, rotation, _parentForSpell);

            float randomSizeScale = Random.Range(0.75f, 2.5f);
            cloneRacine.transform.localScale = new Vector3(randomSizeScale, randomSizeScale, randomSizeScale);
        }
    }

    public void WindSpell()
    {
        int cptTornade = Random.Range(1, 3);
        for (int i = 0; i <= cptTornade; i++)
        {
            Vector3 newPos = transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));

            GameObject cloneTornade = Instantiate(_vfxs[2], newPos, _vfxs[2].transform.rotation, _parentForSpell);
            cloneTornade.GetComponent<UltimeTornade>().InitTornade(transform);

            float randomSizeScale = Random.Range(100, 150);
            cloneTornade.transform.localScale = new Vector3(randomSizeScale, randomSizeScale, randomSizeScale);
        }
    }

    public void WaterSpell()
    {
        Vector3 newPos = _ultimeBossAgentController.PlayerTransform.position - (_ultimeBossAgentController.PlayerTransform.forward * 10);

        Vector3 relativePos = _ultimeBossAgentController.PlayerTransform.position - newPos;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        GameObject cloneTsunami = Instantiate(_vfxs[3], newPos, rotation, _parentForSpell);

        float randomSizeScale = Random.Range(0.75f, 1.5f);
        cloneTsunami.transform.localScale = new Vector3(randomSizeScale, randomSizeScale, randomSizeScale);
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
            Vector3 relativePos = _ultimeBossAgentController.PlayerTransform.position - _posMidleSpell.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            vfx = Instantiate(_vfxs[indexVFX], _firepointLeft.transform.position, rotation);
            vfx.GetComponent<EarthSpellRockBall>().Init(_firepointLeft.transform, _firePointRight.transform, _ultimeBossAgentController.PlayerTransform.GetComponentInChildren<PlayerEntity>().TargetTop);
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
            Vector3 relativePos = _ultimeBossAgentController.PlayerTransform.position - _posMidleSpell.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            vfx = Instantiate(_vfxs[indexVFX], _firePointRight.transform.position, rotation);
            vfx.GetComponent<EarthSpellRockBall>().Init(_firepointLeft.transform, _firePointRight.transform, _ultimeBossAgentController.PlayerTransform.GetComponentInChildren<PlayerEntity>().TargetTop);
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
            Vector3 relativePos = _ultimeBossAgentController.PlayerTransform.position - _posMidleSpell.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            vfx = Instantiate(_vfxs[indexVFX], _ultimeBossAgentController.PlayerTransform.position, rotation);
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
            Vector3 relativePos = _ultimeBossAgentController.PlayerTransform.position - _ultimeBossTransformMesh.position;
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
        _ultimeBossAnimatorMono.SetSpell(valueBool, valueInt);
        _isInSpell = valueBool;
    }

    public bool IsInSpell { get => _isInSpell; set => _isInSpell = value; }
    public bool CanSpell { get => _canSpell; set => _canSpell = value; }
}
