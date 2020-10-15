using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacineBehaviour : MonoBehaviour
{
    private Animator anim;
    private int _paramAwakeID;
    private int _paramAttackID;
    private int _paramDieID;

    [SerializeField]
    private Transform player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _paramAttackID = Animator.StringToHash("attack");
        _paramAwakeID = Animator.StringToHash("awake");
        _paramDieID = Animator.StringToHash("die");
    }

    private void Update()
    {
        // DEBUG INPUTS !

        if (Input.GetKeyDown(KeyCode.A))
        {
            // Line to awake it
            // Maybe in the Awake of the Racine Component ?
            anim.SetTrigger(_paramAwakeID);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Line to make it attack : only visually tho
            anim.SetTrigger(_paramAttackID);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // Line to make it die
            anim.SetTrigger(_paramDieID);
        }

        // -------------------------------------------
        // ALWAYS FOLLOW PLAYER
        // -------------------------------------------

        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 targetPostition = new Vector3(player.position.x,
                                       this.transform.position.y,
                                       player.position.z);
        transform.LookAt(targetPostition, Vector3.up);
    }

    // Event in animation : Destroy after die anim
    public void DieAfterAnim()
    {
        Destroy(this.gameObject);
    }
}
