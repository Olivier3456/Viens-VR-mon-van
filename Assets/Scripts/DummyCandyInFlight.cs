using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCandyInFlight : MonoBehaviour
{
    private Rigidbody rb;
    private float velocity;
    private Vector3 direction;
    private Candy.CandyType type;
    [SerializeField]
    private GameObject[] candyTypes;
    public void SetDummyCandyParameters(float v, Vector3 dir, Candy.CandyType t)
    {
        velocity = v;
        direction = dir;
        type = t;
    }
    void OnEnable()
    {
        if (type == Candy.CandyType.Type1)
            candyTypes[0].SetActive(true);
        else
            candyTypes[1].SetActive(true);
        rb = GetComponent<Rigidbody>();
        rb.velocity = direction * velocity;
        Invoke("Disable", 1);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
        candyTypes[0].SetActive(false);
        candyTypes[1].SetActive(false);
    }

}
