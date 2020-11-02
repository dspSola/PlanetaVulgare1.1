using UnityEngine;

public class DesactiveCone : MonoBehaviour
{
    [SerializeField] private GameObject _coneDetection;
    [SerializeField] private BoolVariable _isDesactivedCone;

    void Update()
    {
        if(_isDesactivedCone.value)
        {
            _coneDetection.SetActive(false);
        }
    }
}
