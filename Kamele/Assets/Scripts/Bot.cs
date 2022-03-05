using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private int roleIndex;
    private NavMeshAgent Agent;
    private Animator Anim;
    [SerializeField] GameObject []Lokation;
    
    [SerializeField] GameObject Surface;
    [SerializeField] GameObject Joints;
    [SerializeField] GameObject mixamo;
    [SerializeField] GameObject Ragdoll;
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        roleIndex=Random.Range(0,Lokation.Length);
        Agent.SetDestination(Lokation[roleIndex].transform.position);
        
    }
    
    void Update()
    {
        
        if(Agent.enabled)
        {
            if (Agent.remainingDistance > Agent.stoppingDistance)
            {
                Anim.SetFloat("Speed", 1f);
            }
            else
            {
                Anim.SetFloat("Speed", 0f);
                roleIndex=Random.Range(0,Lokation.Length);
                Agent.SetDestination(Lokation[roleIndex].transform.position);
            }
        }
    }
    public void RagdollActivate()
    {
        Agent.enabled=false;
        Surface.SetActive(false);
        Joints.SetActive(false);
        mixamo.SetActive(false);
        Ragdoll.SetActive(true);
        GetComponent<Animator>().enabled=false;
    }
}

