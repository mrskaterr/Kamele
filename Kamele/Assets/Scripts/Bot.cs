using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    static bool isTheEnd;
    private int roleIndex;
    private bool hasTicket;
    private bool isWaiting;
    private NavMeshAgent Agent;
    private Animator Anim;
    public GameObject TicketLokation;
    public GameObject WaitLokation;
    public GameObject TrainLokation;
    void Start()
    {
         StartCoroutine(EntryToTrain());
        hasTicket=false;
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        roleIndex=Random.Range(0,2);
        if(roleIndex==0)//kupujacy bilet
        {
            Agent.SetDestination(TicketLokation.transform.position);
        }
        else//nie kupujacy biletu
        {
            Agent.SetDestination(WaitLokation.transform.position);
        }
        
    }
    
    void Update()
    {
        if (Agent.remainingDistance > Agent.stoppingDistance)
        {
            Anim.SetFloat("Speed", 1f);
        }
        else
        {
            Anim.SetFloat("Speed", 0f);
            if(gameObject.transform.position.x == TicketLokation.transform.position.x 
            && gameObject.transform.position.z == TicketLokation.transform.position.z
            && gameObject.transform.position.y  < -1f
            && !hasTicket)
            {
                Anim.SetBool("TicketAnim",true);
                StartCoroutine(BuyTicket());
            }
            if(gameObject.transform.position.x == TrainLokation.transform.position.x 
            && gameObject.transform.position.z == TrainLokation.transform.position.z
            && gameObject.transform.position.y  < -1f)
            {
                Destroy(gameObject);
            }   
        }
    }
    IEnumerator EntryToTrain()
    {
        yield return new WaitForSeconds(40);
        Agent.SetDestination(TrainLokation.transform.position);
        isTheEnd=true;
    }
    IEnumerator BuyTicket()
    {
        yield return new WaitForSeconds(4);
        Anim.SetBool("TicketAnim",false);
        hasTicket=true;
        Agent.SetDestination(WaitLokation.transform.position);
    }
    IEnumerator Wait()
    {
        
        Agent.SetDestination(transform.position);
        yield return new WaitForSeconds(8);
        if(!hasTicket && roleIndex==0)
        {
            Agent.SetDestination(TicketLokation.transform.position);
        }
        if(isTheEnd)
        {
            Agent.SetDestination(TrainLokation.transform.position);
        }
        
    }
    void OnTriggerEnter(Collider collision) 
    {
        if(collision.gameObject.GetComponent<Bot>())
        {
           StartCoroutine(Wait());
        }
    }
}

