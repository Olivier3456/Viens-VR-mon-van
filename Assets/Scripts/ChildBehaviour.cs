using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBehaviour : AIBehaviour
{
    private bool isWalkingTowardsCandy = false;
    private bool isWalkingHome = false;

    private Vector3 spawnPoint;
    public Vector3 SpawnPoint { get { return spawnPoint; } private set { spawnPoint = value; } }
    // Start is called before the first frame update
    void Start()
    {
        _animator.SetTrigger("Idle");
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isWalkingHome && Vector3.Distance(_agent.destination, transform.position) < 0.5) //_agent.remainingDistance < 0.5) _agent.remainingDistance is not calculated yet in the next frame after setting isWalkingHome to true
        {
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().SpawnNextChild();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Candy"))
        {
            if (!isWalkingTowardsCandy)
            {

                _agent.destination = other.transform.position;
                _agent.isStopped = false;
                _animator.ResetTrigger("Idle");
                _animator.ResetTrigger("Lifting");
                _animator.SetTrigger("Walking");
                isWalkingTowardsCandy = true;

            }
            else
            {
                if (_agent.remainingDistance <= 0.7)
                {
                    _animator.ResetTrigger("Idle");
                    _animator.ResetTrigger("Walking");
                    _animator.SetTrigger("Lifting");
                    _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];
                    other.GetComponent<Candy>().ReturnToPool();
                    _audioSource.Play();
                    _agent.isStopped = true;
                    isWalkingTowardsCandy = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Van"))
        {
            gameObject.SetActive(false);
        }
    }

    public void StopLifting()
    {
        isWalkingTowardsCandy = false;
        _animator.ResetTrigger("Lifting");
        _animator.ResetTrigger("Walking");
        _animator.SetTrigger("Idle");
    }

    public void GoBack()
    {
        _agent.destination = spawnPoint;
        _agent.isStopped = false;
        _animator.ResetTrigger("Idle");
        _animator.ResetTrigger("Lifting");
        _animator.SetTrigger("Walking");
        isWalkingHome = true;
    }
}
