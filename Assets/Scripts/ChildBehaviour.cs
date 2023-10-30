using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBehaviour : AIBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Candy"))
        {
            _agent.destination = other.transform.position;
            _animator.SetTrigger("Walking");
        }
    }
}
