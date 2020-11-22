using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    [SerializeField] Camera _cutSceneCam;
    [SerializeField] GameObjectData _playerCam;
    [SerializeField] GameObject _bandesNoires;
    [SerializeField] GameObjectData _playerGameObject;
    [SerializeField] GameObjectData _portalGameObject;
    [SerializeField] GameObjectData _particleBurst;

    private BoxCollider _cutSceneTrigger;
    private Animator _portalAnimator;

    private void Start()
    {
        _cutSceneTrigger = GetComponent<BoxCollider>();
        _portalAnimator = _portalGameObject._value.GetComponent<Animator>();
        _portalAnimator.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerColl"))
        {
            Debug.Log("Collision Detecté");
            _cutSceneCam.gameObject.SetActive(true);
            _playerCam._value.SetActive(false);
            _bandesNoires.SetActive(true);
            _playerGameObject._value.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            _portalAnimator.enabled = true;
            StartCoroutine(FinishCutSceneCoroutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerColl"))
        {
            Debug.Log("Collision Terminé");
            _cutSceneCam.gameObject.SetActive(false);
            _playerCam._value.SetActive(true);
            _bandesNoires.SetActive(false);
            _playerGameObject._value.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            _playerGameObject._value.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            gameObject.SetActive(false);
            _particleBurst._value.SetActive(false);
            _portalGameObject._value.SetActive(false);
        }
    }

    IEnumerator FinishCutSceneCoroutine()
    {
        yield return new WaitForSeconds(5);

        _cutSceneTrigger.size = new Vector3(0, 0, 0);
    }
}
