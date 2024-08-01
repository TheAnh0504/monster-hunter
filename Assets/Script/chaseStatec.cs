﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chaseStatec : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    bool temp = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (PlayerPrefs.GetInt("check") == 1) temp = true; else temp = false;
        if (temp)
        {
            Vector3 myVector = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"),
                PlayerPrefs.GetFloat("z"));
            float distance1 = Vector3.Distance(myVector, animator.transform.position);
            if (distance1 < PlayerPrefs.GetFloat("radius") * 2f)
            {
                // Tính toán đường ngắn nhất để tránh vụ nổ
                Vector3 avoidanceDirection = animator.transform.position - myVector;
                Vector3 avoidancePosition = animator.transform.position + avoidanceDirection.normalized;

                agent.SetDestination(avoidancePosition);

            }
            else if (distance > 1.7f)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                agent.SetDestination(player.position);
            }
        }
        else
        {
            if (distance > 1.7f)
            {
                agent.SetDestination(player.position);
            }
        }
        
 
        if (distance > 15f)
        {
            animator.SetBool("isChasing", false);
        }
        if (distance <= 2.5f)
        {
           animator.SetBool("isAttacking", true);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
