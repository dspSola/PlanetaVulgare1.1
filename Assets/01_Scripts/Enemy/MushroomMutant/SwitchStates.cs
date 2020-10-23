using UnityEngine;

public class SwitchStates : MonoBehaviour
{
    private bool _isSwitchedState;

    public bool IsSwitchedState { get => _isSwitchedState; set => _isSwitchedState = value; }
}
