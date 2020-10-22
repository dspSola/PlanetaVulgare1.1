using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [Header("Parameters")]
    public int m_deplacement = 3;


    [SerializeField] Material _bloodSun;
    [SerializeField] Material _sunnySun;

    private void Start()
    {
        RenderSettings.skybox = _bloodSun;
    }



    private void OnCollisionEnter(Collision other)
    {   
        //if (other.gameObject.CompareTag("PlayerColl"))
        //{
            other.transform.position = _destination - Vector3.forward * m_deplacement;
            other.transform.Rotate(Vector3.up * 180);

            if(this.name== "Quad_Portal_A")
            {
                _destination = GameObject.Find("Quad_Portal_B").transform.position;
                RenderSettings.skybox = _sunnySun;
            }
            else
            {
                _destination = GameObject.Find("Quad_Portal_A").transform.position;
                RenderSettings.skybox = _bloodSun;
            }

        //}
    }

    private Vector3 _destination;
}
