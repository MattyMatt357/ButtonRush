using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;

    public List<GameObject> pooledRockets = new List<GameObject>();
    public GameObject rockets;

    public List<GameObject> pooledRocketExplosions = new List<GameObject>();
    public GameObject rocketExplosions;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
    void Start()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject rocketsToPool = Instantiate(rockets);
            rocketsToPool.SetActive(false);
            pooledRockets.Add(rocketsToPool);
        }

        for (int i = 0; i < 25; i++)
        {
            GameObject explosionsToPool = Instantiate(rocketExplosions);
            explosionsToPool.SetActive(false);
            pooledRocketExplosions.Add(explosionsToPool);
        }
    }

    public GameObject GetRocketFromPool()
    {
        for (int i = 0;i < pooledRockets.Count;i++)
        {
            if (!pooledRockets[i].gameObject.activeInHierarchy)
            {
                return pooledRockets[i];
            }
        }
        return null;
    }

    public GameObject GetRocketExplosionFromPool()
    {
        for (int i = 0; i < pooledRocketExplosions.Count; i++)
        {
            if (!pooledRocketExplosions[i].gameObject.activeInHierarchy)
            {
                return pooledRocketExplosions[i];
            }
        }
        return null;
    }

}
