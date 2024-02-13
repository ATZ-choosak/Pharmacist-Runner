using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadSpawner : MonoBehaviour
{
    public static roadSpawner Instance;
    public GameObject[] roadTile;
    Vector3 nextSpawnPoint;

    [SerializeField]
    int InitnumberRoad = 5;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnTile()
    {
        int index_random = Random.Range(0, roadTile.Length);
        GameObject temp = Instantiate(roadTile[index_random] , nextSpawnPoint , Quaternion.identity);
        nextSpawnPoint = temp.GetComponent<roadTileManager>().getNextSpawnPoint();
    }
    
    void Start()
    {
        for (int i = 0; i < InitnumberRoad; i++)
        {
            SpawnTile();
        }
    }

    
}
