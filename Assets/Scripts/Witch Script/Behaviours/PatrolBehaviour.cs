
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public int numOfPositions = 3;
    public float patrolRange = 4;
    public float patrolSpeedMin = 2;
    public float patrolSpeedMax = 4;
    public int waitTime = 2;
    public bool isPausing;

    private Vector3[] patrolPositions;
    private int randomIndex;
    private int counter;
 
    private PatrolWait waitScript;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPositions = getRandomPositions(animator.transform);
        randomIndex = Random.Range(0, numOfPositions - 1);
        waitScript = animator.GetComponent<PatrolWait>();
        counter = 0;
        isPausing = true;
        waitScript.DoCoroutine(this); //pause
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Vector3 direction = animator.transform.position - patrolPositions[randomIndex];
        direction.y = 0f;

        if (!isPausing)
        {
            if (animator.transform.position.x != patrolPositions[randomIndex].x && animator.transform.position.z != patrolPositions[randomIndex].z)
            {
                //animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, patrolPositions[randomIndex], Random.Range(patrolSpeedMin, patrolSpeedMax) * Time.deltaTime);

            }
            else
            {
                counter++;
                randomIndex = Random.Range(0, numOfPositions - 1); // randomly choose next position
                waitScript.DoCoroutine(this);

                if (counter == numOfPositions)
                {
                    animator.SetBool("isIdle", true);
                    animator.SetBool("isPatrolling", false);
                }
            }
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
