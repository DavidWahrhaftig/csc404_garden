
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public int numOfPositions = 3;
    public float patrolRange = 4;
    public float patrolSpeedMin = 2;
    public float patrolSpeedMax = 4;

    private Vector3[] patrolPositions;
    private int randomIndex;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPositions = getRandomPositions(animator.transform);
        randomIndex = Random.Range(0, numOfPositions - 1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

        
        if (animator.transform.position.x != patrolPositions[randomIndex].x && animator.transform.position.z != patrolPositions[randomIndex].z)
        {
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, patrolPositions[randomIndex], Random.Range(patrolSpeedMin, patrolSpeedMax) * Time.deltaTime);

        } else
        {
            // CoRoutine towait and rotate witch in the next direction
            randomIndex = Random.Range(0, numOfPositions - 1); // randomly choose next position

        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    private Vector3[] getRandomPositions(Transform origin)
    {
        Vector3[] patrolPositions = new Vector3[numOfPositions];

        float minX = origin.position.x - patrolRange;
        float maxX = origin.position.x + patrolRange;
        float minZ = origin.position.z - patrolRange;
        float maxZ = origin.position.z + patrolRange;

        for (int i = 0; i < numOfPositions; i++)
        {
            patrolPositions[i] = new Vector3(Random.Range(minX, maxX), origin.position.y, Random.Range(minZ, maxZ));
        }

        return patrolPositions;
    }



}
