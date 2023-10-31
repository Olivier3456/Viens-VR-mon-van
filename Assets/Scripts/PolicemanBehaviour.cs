using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicemanBehaviour : AIBehaviour
{
    [SerializeField]
    private float walkingSpeedWhenChasingChild;
    [SerializeField]
    private float wanderingRange;
    [SerializeField]
    private float maxWaitTime;


    private float waitTime;
    private float waitCounter;
    private bool walkingTowardsChild = false;
    private GameObject child;
    private bool hasWanderingDestination = false;
    private bool isWaiting = false;
    private bool bringingBack = false;
    // Start is called before the first frame update
    void Start()
    {
        _agent.destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(bringingBack)
        {
            if (_agent.remainingDistance < 3)
            {
                _audioSource.clip = _audioClips[1];
                _audioSource.Play();
                _animator.ResetTrigger("Walking");
                _animator.SetTrigger("Idle");
                bringingBack = false;
            }
            return;
        }
        if (walkingTowardsChild)
        {
            if(_agent.remainingDistance < 1)
            {
                _audioSource.clip = _audioClips[0];
                _agent.speed = walkingSpeed;
                _audioSource.Play();
                walkingTowardsChild = false;
                BringChildBack();
            }
            return;
        }
        if (isWaiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                isWaiting = false;
                waitCounter = 0;
            }
        }
        Wander();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Girl"))
        {
            _agent.destination = other.transform.position;
            if (!walkingTowardsChild)
            {
                child = other.GetComponentInParent<ChildBehaviour>().gameObject;
                _animator.ResetTrigger("Idle");
                _animator.SetTrigger("Walking");
                _agent.isStopped = false;
                _animator.speed = walkingSpeedWhenChasingChild / walkingSpeed;
                walkingTowardsChild = true;
                _agent.speed = walkingSpeedWhenChasingChild;
            }
        }
    }

    private void Wander()
    {
        if (isWaiting)
        {
            return;
        }
        if (!hasWanderingDestination)
        {
            float x = Random.Range(-wanderingRange, wanderingRange);
            float z = Random.Range(-wanderingRange, wanderingRange);
            _agent.destination += new Vector3(x, 0, z); 
            _agent.isStopped = false;
            hasWanderingDestination = true;
            _animator.ResetTrigger("Idle");
            _animator.SetTrigger("Walking");
        }
        else if (_agent.remainingDistance <= 0.1) 
        {
            waitTime = Random.Range(1, maxWaitTime);
            _agent.isStopped = true;
            hasWanderingDestination = false;
            isWaiting = true;
            _animator.ResetTrigger("Walking");
            _animator.SetTrigger("Idle");
        }
    }
    private void BringChildBack()
    {
        var childAI = child.GetComponent<ChildBehaviour>();
        childAI.GoBack();
        _agent.destination = childAI.SpawnPoint;
        bringingBack = true;
    }
}
