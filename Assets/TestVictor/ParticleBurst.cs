using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurst : MonoBehaviour
{
    [SerializeField] GameObject _particleBurst;

    public void ParticleBurstStart()
    {
        _particleBurst.SetActive(true);
    }
}
