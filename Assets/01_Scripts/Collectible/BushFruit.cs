using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushFruit : MonoBehaviour
{
    [SerializeField] private GameObject _fruitPrefab;
    [SerializeField] private List<GameObject> _fruits;
    [SerializeField] private bool _isFullFruit;
    [SerializeField] private int _cptFruitInBush;
    [SerializeField] private float _timeToInitFruit, _timeToInitFruitMax;

    private void Start()
    {
        //Instantiate(_bushPrefab, transform.position, transform.rotation, transform);
        InitFruits();
    }

    private void Update()
    {
        if (!_isFullFruit)
        {
            if (_timeToInitFruit < _timeToInitFruitMax)
            {
                _timeToInitFruit += Time.deltaTime;
            }
            else
            {
                InitFruits();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isFullFruit)
        {
            if (other.gameObject.layer == 8 && other.gameObject.tag == "PlayerColl")
            {
                if (other.gameObject.GetComponentInChildren<GetInputBrute>().UseInput.IsDown && other.gameObject.GetComponentInChildren<StateMachineAttack>().IsArmed == false)
                {
                    if (other.gameObject.GetComponentInChildren<PlayerEntity>().CoefLife < 1)
                    {
                        other.gameObject.GetComponentInChildren<PlayerEntity>().AddLife(Random.Range(10, 25));
                        other.gameObject.GetComponentInChildren<PlayerEntity>().PlayBiteApple();
                        PickFruit();
                    }
                }
            }
        }
    }

    public void InitFruits()
    {
        _timeToInitFruitMax = Random.Range(30, 90);
        _fruits = new List<GameObject>();
        int random = Random.Range(2, 5);
        for (int i = 0; i <= random; i++)
        {
            //Vector3 newPos = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            Vector3 newPos = transform.position + new Vector3(ReturnFloatPosSpawn(), Random.Range(0.3f, 0.6f), ReturnFloatPosSpawn());
            GameObject cloneFruitBush = Instantiate(_fruitPrefab, newPos, transform.rotation, transform);
            _fruits.Add(cloneFruitBush);
            _cptFruitInBush++;
        }
        _isFullFruit = true;
    }

    public void PickFruit()
    {
        _cptFruitInBush--;
        GameObject fruitToDestroy = _fruits[_cptFruitInBush];
        _fruits.Remove(fruitToDestroy);
        Destroy(fruitToDestroy);

        if(_cptFruitInBush == 0)
        {
            _isFullFruit = false;
        }
    }

    public float ReturnFloatPosSpawn()
    {
        int random = Random.Range(0, 100);

        float value = 0;

        if(random < 50)
        {
            value = Random.Range(-0.3f, -0.2f);
        }
        else
        {
            value = Random.Range(0.2f, 0.3f);
        }

        return value;
    }
}
