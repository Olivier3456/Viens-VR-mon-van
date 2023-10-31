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
                CreateNewCandy(j);
            }
        }
    }

    private void CreateNewCandy(int index)
    {
        Candy newCandy = Instantiate(CandyPrefabs[index], transform).GetComponent<Candy>();
        newCandy.SetCandyProperties(this, candiesLifeTime);
        ReturnCandyInPool(newCandy);
    }

    public Candy GetCandyFromPool()
    {
        if (waitingCandiesList.Count > 0)
        {
            int candyIndex = Random.Range(0, waitingCandiesList.Count);
            Candy candy = waitingCandiesList[candyIndex];
            waitingCandiesList.Remove(candy);
            //candy.transform.position = newPosition;
            //candy.gameObject.SetActive(true);
            candy.RestartLifeCycle();
            return candy;
        }
        else
        {
            CreateNewCandy(Random.Range(0, CandyPrefabs.Length));
            return GetCandyFromPool();
        }
    }

    public void ReturnCandyInPool(Candy candyToAdd)
    {
        candyToAdd.CancelInvoke();
        candyToAdd.gameObject.SetActive(false);
        waitingCandiesList.Add(candyToAdd);
    }
}
