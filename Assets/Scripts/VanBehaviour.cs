using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanBehaviour : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
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
        if(other.CompareTag("Girl"))
        {
            GetComponent<AudioSource>().Play();
            gameManager.ChildCaught();
        }
    }
}
