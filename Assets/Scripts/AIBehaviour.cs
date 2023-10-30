using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIBehaviour : MonoBehaviour
{

    [SerializeField]
    protected Animator _animator;
    [SerializeField]
    protected NavMeshAgent _agent;
    [SerializeField] 
    protected AudioSource _audioSource;
    [SerializeField]
    protected AudioClip[] _audioClips;
    [SerializeField]
    protected float sightRadius;
    [SerializeField]
    protected float sightAngle;
    [SerializeField]
    protected float walkingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
