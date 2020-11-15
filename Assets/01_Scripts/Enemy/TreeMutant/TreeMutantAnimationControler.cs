using UnityEngine;

public class TreeMutantAnimationControler : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] TreeMutantMovement _treeMutantMovement;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _animator.SetFloat(_speedId, _treeMutantMovement.SpeedAgent);
    }

    int _speedId = Animator.StringToHash("Speed");

    Transform _transform;
}
