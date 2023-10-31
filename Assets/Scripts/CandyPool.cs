using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPool : MonoBehaviour
{
    [SerializeField] private GameObject[] CandyPrefabs;

    [SerializeField] private int candiesInPool = 20;
    [SerializeField] private float candiesLifeTime = 20;

    private List<Candy> waitingCandiesList;

    private void Start()
    {
        waitingCandiesList = new List<Candy>();

        for (int i = 0; i <= Mathf.Ceil(candiesInPool * 0.5f); i++)
        {
            for (int j = 0; j < CandyPrefabs.Length; j++)
            {
                Candy newCandy = Instantiate(CandyPrefabs[j], transform).GetComponent<Candy>();
                newCandy.SetCandyProperties(this, candiesLifeTime);
                newCandy.gameObject.SetActive(false);
                waitingCandiesList.Add(newCandy);
            }
        }
    }

    public Candy GetCandyFromPool(Vector3 newPosition)
    {
        if (waitingCandiesList.Count > 0)
        {
            int candyIndex = Random.Range(0, waitingCandiesList.Count);
            Candy candy = waitingCandiesList[candyIndex];
            waitingCandiesList.Remove(candy);
            candy.transform.position = newPosition;
            candy.gameObject.SetActive(true);
            candy.RestartLifeCycle();
            return candy;
        }
        else
        {
            Debug.Log("No more candies in pool");
            return null;
        }
    }

    public void AddCandyInPool(Candy candyToAdd)
    {
        candyToAdd.gameObject.SetActive(false);
        candyToAdd.CancelInvoke();
        waitingCandiesList.Add(candyToAdd);
    }
}
