using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class AIMovement : MonoBehaviour
{
    /*
    Ideas:
    Attacking and damage system.  DONE, NOT DEATH
    All AI go towards chasing scream.  
    When use a key, alerts closest AIs.  DONE
    If an AI see a chase, it joins.   
    When get hit, stuns for 3-5 seconds.  DONE
    */
    public Transform playerTransform;
    public float stoppingDistance = 2f;
    Vector3 currentDestination;
    public float mazeSize = 250;
    public float sightRange = 75;
    public bool IsStunned = false;
    public float StunTimer;
    public AIState state;
    public float LostSightTimer;
    public float SpottedTimer;
    public AudioSource audioSource;
    public float acceleration = 10f;
    public float deceleration = 60f;
    public float closeEnoughMeters = 4f;
    private Flashlight flashlight;
    private Key key;
    public float chasingSpeed;
    public float walkingSpeed;

    private NavMeshAgent navMeshAgent;

    public bool isChasing = false;
    float timer = 10f;
    float nextCheck = 0;

    public enum AIState
        {
        Stunned,
        Chasing,
        Roaming,
        Alerted,
        LostSight,
        Spotted
        }

    private void Start()
    {
        flashlight = GameObject.FindObjectOfType<Flashlight>();
        Physics.IgnoreCollision(flashlight.GetComponent<BoxCollider>(), GetComponent<CapsuleCollider>());
        key = GameObject.FindObjectOfType<Key>();
        Physics.IgnoreCollision(key.GetComponent<BoxCollider>(), GetComponent<CapsuleCollider>());
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        Stun();
        PickRandomTarget();

    }

    private void Update()
    {
        if(navMeshAgent)
        {
            if(navMeshAgent.hasPath)
            {
                navMeshAgent.acceleration = (navMeshAgent.remainingDistance < closeEnoughMeters) ? deceleration : acceleration;
            }
        }
        switch(state)
        {
            case AIState.Alerted:

                Alert(currentDestination);

                break;
            case AIState.Chasing:

                Chasing();

                break;
            case AIState.Roaming:

                Roaming();

                break;
            case AIState.Stunned:

                break;
            case AIState.LostSight:
                LostSight();

                break;
            case AIState.Spotted:
                Spotted();

                break;
            default:
                break;
        }
        //       StartCoroutine(Waiter());
        navMeshAgent.SetDestination(currentDestination);
    }
    public void PickRandomTarget()
    {
        currentDestination = new Vector3(
            Random.Range(0, mazeSize),
            .5f,
            Random.Range(0, mazeSize));
    }
    public bool checkForPlayer()
    {
        Ray ray = new Ray(transform.position, (playerTransform.position - transform.position).normalized * sightRange);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.tag.Equals("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public bool checkForChasingAlly()
    {
        foreach (GameObject ally in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Ray ray = new Ray(transform.position, (ally.transform.position - transform.position).normalized * sightRange);
            if (Physics.Raycast(ray, out RaycastHit hit) && !ally.Equals(gameObject))
            {
                if (hit.transform.tag.Equals("Enemy"))
                {
                    if(hit.transform.GetComponent<AIMovement>().state == AIState.Chasing)
                    {
                        return true;
                    }
                    
                }
            }


        }


        return false;
    }
/*IEnumerator Waiter()
{
    yield return new WaitForSeconds(1.5f);
       
        if(IsStunned)
        {
            StopCoroutine("Waiter");
            navMeshAgent.isStopped = true;
        }
    if (isChasing)
    {
        if (checkForPlayer())
        {
            currentDestination = playerTransform.position;
        }
        else
        {
            isChasing = false;
        }
    }
    else
    {
        if (checkForPlayer())
        {
            isChasing = true;
            currentDestination = playerTransform.position;
        }
        else if (nextCheck <= Time.time)
        {
            nextCheck = Time.time + timer;
            PickRandomTarget();
        }
    }
    navMeshAgent.SetDestination(currentDestination);
}*/
    public void OnCollisionEnter(Collision collision)
    {
        if(IsStunned)
        {
            return;
        }
        Health health = collision.transform.GetComponent<Health>();
        if (health != null)
        {
            health.PlayerHit();
            Stun();
        }

    }
    public void Stun()
    {
        state = AIState.Stunned;
        IsStunned = true;

        Invoke("ResetStun", StunTimer);
    }
    public void ResetStun()
    {

        IsStunned = false;
        navMeshAgent.isStopped = false;
        state = AIState.Roaming;
    }

    public void Alert(Vector3 destination)
    {
        currentDestination = destination;

       if(checkForPlayer())
        {
            state = AIState.Chasing;
        }
       else if(checkForChasingAlly())
        {
            currentDestination = playerTransform.position;
        }
        else if(Vector3.Distance(transform.position, currentDestination) < 1.5f)
        {
            state = AIState.Roaming;
        }

        
    }
    public void Chasing()
    {
        navMeshAgent.speed = chasingSpeed;
        if(!checkForPlayer())
        {
            LostSightTimer = Time.time + 0.25f;
            navMeshAgent.speed = walkingSpeed;
            state = AIState.LostSight;
        }
        else
        {
            currentDestination = playerTransform.position;
        }


    }
    public void Roaming()
    {
        if(checkForPlayer())
        {
            state = AIState.Spotted;
            audioSource.Play(0);
            SpottedTimer = Time.time + 2.5f;
        }
       else if(checkForChasingAlly())
        {
            state = AIState.Alerted;
            Alert(playerTransform.position);
        }

        else if(Vector3.Distance(transform.position, currentDestination) < 1.5f)
        {
            PickRandomTarget();
        }
        

    }

    public void LostSight()
    {
        if(checkForPlayer())
        {
            state = AIState.Chasing;
        }
        else if(Time.time > LostSightTimer)
        {
            state = AIState.Alerted;
            Alert(playerTransform.position);
        }
        else
        {
            currentDestination = playerTransform.position;
        }

    }
    public void Spotted()
    {

        navMeshAgent.isStopped = true;
        
        if(Time.time > SpottedTimer)
        {
            navMeshAgent.isStopped = false;
            currentDestination = playerTransform.position;
            state = AIState.Chasing;
        }
        

    }

}