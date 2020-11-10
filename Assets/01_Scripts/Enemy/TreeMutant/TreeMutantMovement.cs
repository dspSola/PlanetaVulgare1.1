using UnityEngine;
using UnityEngine.AI;

public class TreeMutantMovement : MonoBehaviour
{
    [SerializeField] NavMeshSurface _navMeshSurface;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        
    }

    Transform _transform;
}
