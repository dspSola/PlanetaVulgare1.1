using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [Header("Parameters")]
    public int m_deplacement = 3;

    

    private void OnCollisionEnter(Collision other)
    {
        if(this.name== "Quad_Portal_A")
        {
            _destination = GameObject.Find("Quad_Portal_B").transform.position;
        }
        else
        {
            _destination = GameObject.Find("Quad_Portal_A").transform.position;
        }

        other.transform.position = _destination - Vector3.forward * m_deplacement;
        other.transform.Rotate(Vector3.up * 180);
    }

    private Vector3 _destination;
}
