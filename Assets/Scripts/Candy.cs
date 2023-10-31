using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    private CandyPool candyPool;
    private float lifeTime;

    public enum CandyType { Type1, Type2 };
    [SerializeField] private CandyType type;

    public void SetCandyProperties(CandyPool candyPool, float lifeTime)
    {
        this.candyPool = candyPool;
        this.lifeTime = lifeTime;
    }


    public void RestartLifeCycle()
    {
        Invoke("ReturnToPool", lifeTime);
    }

    private void ReturnToPool()
    {
        candyPool.ReturnCandyInPool(this);
    }

    public CandyType GetCandyType()
    {
        return type;
    }

}
