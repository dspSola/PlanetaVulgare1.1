using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [Header("Parameters")]
    public int m_deplacement = 3;


    [SerializeField] Material _bloodSunSkybox;
    [SerializeField] Material _sunnySunSkybox;
    [SerializeField] GameObject _bloodSun;
    [SerializeField] GameObject _sunnySun;
 
    private void Start()
    {
        RenderSettings.skybox = _bloodSunSkybox;
        _bloodSun.SetActive(true);
        _sunnySun.SetActive(false);
    }



    private void OnCollisionEnter(Collision other)
    {   
        //if (other.gameObject.CompareTag("PlayerColl"))
        //{
            

        if(this.name== "Quad_Portal_A")
        {
            _destination = GameObject.Find("Quad_Portal_B").transform.position;
            RenderSettings.skybox = _sunnySunSkybox;
            _sunnySun.SetActive(true);
            _bloodSun.SetActive(false);

        }
        else
        {
            _destination = GameObject.Find("Quad_Portal_A").transform.position;
            RenderSettings.skybox = _bloodSunSkybox;
            _sunnySun.SetActive(false);
            _bloodSun.SetActive(true);
        }

        other.transform.position = _destination + Vector3.forward * m_deplacement;
        other.transform.Rotate(Vector3.up*90);

        //}
    }

    private Vector3 _destination;
}
