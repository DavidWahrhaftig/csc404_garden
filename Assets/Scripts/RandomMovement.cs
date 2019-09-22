using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    //public float movingSpeed = 20f;
    public float timeForNewPath;
    bool inCoRoutine = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!inCoRoutine)
        {
            StartCoroutine(moveRandomly());
        }
    }

    IEnumerator moveRandomly()
    {
        inCoRoutine = true;

        getNewPath();
        yield return new WaitForSeconds(timeForNewPath);
        inCoRoutine = false;
    }

    void getNewPath()
    {
        navMeshAgent.SetDestination(getNewRandomPosition());
    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);

        Vector3 pos = new Vector3(x, transform.position.y, z);
        return pos;
    }

}