using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;

    public List<GameObject> pooledRockets = new List<GameObject>();
    public GameObject rockets;
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
        for (int i = 0; i < 20; i++)
        {
            GameObject rocketsToPool = Instantiate(rockets);
            rocketsToPool.SetActive(false);
            pooledRockets.Add(rocketsToPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetRocketFromPool()
    {
        for (int i = 0;i < pooledRockets.Count;i++)
        {
            if (!pooledRockets[i].gameObject.activeInHierarchy)
            {
                return pooledRockets[i];
               // StartCoroutine(DeactivateRocketAfter3Seconds(pooledRockets[i]));
            }
        }

        return null;
    }
    
}
