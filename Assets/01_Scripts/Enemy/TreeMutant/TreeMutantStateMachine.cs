using UnityEditor;
using UnityEngine;

public enum State
{
    IDLE,
    WALKING,
    PUNCHING,
    HURTING,
    DEATH,
}

public class TreeMutantStateMachine : MonoBehaviour
{
    #region Show in inspector

    /// <summary>
    /// Utilisé pour simuler les inputs du joueur.
    /// </summary>
    //[SerializeField]
    //private PlayerInputs _playerInputs;

    #endregion


    #region Unity Lifecycle

    /// <summary>
    /// Dans l'Update, on va mettre à jour la State Machine
    /// </summary>
    private void Update()
    {
        OnStateUpdate(_state);
    }

    #endregion


    #region State Machine

    /// <summary>
    /// Méthode appelée pendant une transition, au moment où on arrive dans l'état.
    /// C'est ici qu'on va mettre le code qui ne doit arriver qu'une fois quand on
    /// arrive dans l'état.
    /// </summary>
    /// <param name="state"></param>
    private void OnStateEnter(State state)
    {
        switch (state)
        {
            case State.IDLE:
                Debug.Log("[STANDING]\t<color=green>Entering state</color>");
                DoIdle();
                break;

            case State.WALKING:
                Debug.Log("[JUMPING]\t<color=green>Entering state</color>");
                DoWalk();
                break;

            case State.PUNCHING:
                Debug.Log("[DUCKING]\t<color=green>Entering state</color>");
                DoPunch();
                break;

            case State.HURTING:
                Debug.Log("[DIVING]\t<color=green>Entering state</color>");
                DoHurt();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }

    /// <summary>
    /// Méthode appelée pendant une transition, au moment où on sort de l'état.
    /// C'est ici qu'on va mettre le code par exemple pour remettre des valeurs à 0 
    /// en sortant de l'état.
    /// </summary>
    /// <param name="state"></param>
    private void OnStateExit(State state)
    {
        switch (state)
        {
            case State.IDLE:
                Debug.Log("[STANDING]\t<color=red>Exting state</color>");
                break;

            case State.WALKING:
                Debug.Log("[JUMPING]\t<color=red>Exting state</color>");
                break;

            case State.PUNCHING:
                Debug.Log("[DUCKING]\t<color=red>Exting state</color>");
                break;

            case State.HURTING:
                Debug.Log("[DIVING]\t<color=red>Exting state</color>");
                break;

            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }

    /// <summary>
    /// Méthode appelée à chaque Update avec l'état courant.
    /// C'est ici qu'on va mettre les transitions de l'état courant vers un autre.
    /// On va également mettre le code qui doit s'exécuter à chaque frame.
    /// </summary>
    /// <param name="state"></param>
    private void OnStateUpdate(State state)
    {
        switch (state)
        {
            case State.IDLE:
                Debug.Log("[STANDING]\t<color=blue>Staying in state</color>");

                // Transition Idle -> Walk
                //if (/*_playerInputs.IsInputJump*/)
                //{
                //    TransitionToState(state, State.WALKING);
                //    break;
                //}
                //// Transition Idle -> Punch
                //if (/*_playerInputs.IsInputDuck*/)
                //{
                //    TransitionToState(state, State.PUNCHING);
                //    break;
                //}
                break;

            case State.WALKING:
                Debug.Log("[JUMPING]\t<color=blue>Staying in state</color>");

                // Transition Walk -> Hurt
                //if (/*_playerInputs.IsInputDuck*/)
                //{
                //    TransitionToState(state, State.HURTING);
                //    break;
                //}
                //// Transition Walk -> Idle
                //else if (/*_playerInputs.IsInputGround*/)
                //{
                //    TransitionToState(state, State.IDLE);
                //    break;
                //}
                break;

            case State.PUNCHING:
                Debug.Log("[DUCKING]\t<color=blue>Staying in state</color>");

                // Transition punch -> Idle
                //if (/*!_playerInputs.IsInputDuck*/)
                //{
                //    TransitionToState(state, State.IDLE);
                //    break;
                //}
                break;

            case State.HURTING:
                Debug.Log("[DIVING]\t<color=blue>Staying in state</color>");

                // Transition hurt -> Idle
                //if (/*_playerInputs.IsInputGround*/)
                //{
                //    TransitionToState(state, State.IDLE);
                //    break;
                //}
                break;

            default:
                Debug.LogError("OnStateStay: Invalid state " + state.ToString());
                break;
        }
    }

    /// <summary>
    /// Méthode appelée pour transitionner d'un état à l'autre.
    /// </summary>
    /// <param name="fromState"></param>
    /// <param name="toState"></param>
    private void TransitionToState(State fromState, State toState)
    {
        OnStateExit(fromState);
        _state = toState;
        OnStateEnter(toState);
    }

    #endregion


    #region Private methods


    private void DoIdle()
    {
        // Animation, son, code, etc
    }

    private void DoWalk()
    {
        // Animation, son, code, etc
    }

    private void DoPunch()
    {
        // Animation, son, code, etc
    }

    private void DoHurt()
    {
        // Animation, son, code, etc
    }

    #endregion


    #region Debug

    private void OnGUI()
    {
        using (new GUILayout.AreaScope(new Rect(30, 30, 200, 100)))
        {
            GUILayout.Button($"Etat actuel : {_state}");
        }
    }

    #endregion


    #region Private

    /// <summary>
    /// L'état courant
    /// </summary>
    private State _state;

    #endregion
}
