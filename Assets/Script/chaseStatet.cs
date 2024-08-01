using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chaseStatet : StateMachineBehaviour
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
            bool checkrandom = true;
            if ((distance1 < PlayerPrefs.GetFloat("radius") * 1.5f) && checkrandom)
            {

                // Tính toán một vị trí ngẫu nhiên để quái vật di chuyển ngược lại bom
                Vector3 randomDirection = Random.onUnitSphere * 1.5f;
                while ((Vector3.Distance(myVector, animator.transform.position + randomDirection) <=
                    PlayerPrefs.GetFloat("radius") * 1.5f) && checkrandom)
                {
                    randomDirection = Random.onUnitSphere * 1.5f;
                }
                checkrandom = false;
                Vector3 newPosition = animator.transform.position + randomDirection;

                // Đặt điểm đến mới cho NavMeshAgent để quái vật di chuyển đến vị trí mới
                agent.SetDestination(newPosition);
            }
            else if (distance > 1f && (Vector3.Distance(myVector, animator.transform.position)
                > PlayerPrefs.GetFloat("radius") * 1.2f))
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                agent.SetDestination(player.position);
            }
            else checkrandom = true;

        }
        else
        {
            if (distance > 1f)
            {
                agent.SetDestination(player.position);
            }
        }


        if (distance > 12f)
        {
            animator.SetBool("isChasing", false);
        }
        if (distance <= 2f)
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
