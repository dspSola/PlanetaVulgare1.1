using UnityEngine;

public class TreeMutantAnimationControler : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _animator.SetFloat(_moveZId, _transform.position.z);
    }

    int _moveZId = Animator.StringToHash("MoveZ");

    Transform _transform;
}
