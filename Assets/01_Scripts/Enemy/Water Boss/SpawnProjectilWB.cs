using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectilWB : MonoBehaviour
{
    public GameObject _firepoint;
    public List<GameObject> vfx = new List<GameObject>();

    private GameObject effectToSpawn;

    private void Start()
    {
        effectToSpawn = vfx[0];
    }

    private void Update()
    {
        //if(Input.GetMouseButton(2))
        //{
        //    SpawnVFX();
        //}
    }

    private void SpawnVFX()
    {
        GameObject vfx;
        if(_firepoint != null)
        {
            vfx = Instantiate(effectToSpawn, _firepoint.transform.position, _firepoint.transform.rotation);
        }
        else
        {
            Debug.Log("No Fire Poinr");
        }
    }
}
