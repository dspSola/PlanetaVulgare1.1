using UnityEngine;

public class HeadTargetPlayer : MonoBehaviour
{
    [SerializeField] private ScriptableTransform _headTarget;

    private void Awake()
    {
        _headTarget.value = transform;
    }
}
