using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadTileManager : MonoBehaviour
{
    [SerializeField]
    private Transform nextSpawnPoint;

    [SerializeField]
    private GameObject[] building_1 , building_2;

    private void Start()
    {
        bool random = Random.Range(0, 10) % 2 == 0;
        int random_b1 = Random.Range(0 , building_1.Length);
        int random_b2 = Random.Range(0, building_2.Length);
        building_1[random_b1].SetActive( random ? false : true);
        building_2[random_b2].SetActive(random ? true : false);
    }

    public Vector3 getNextSpawnPoint() { return nextSpawnPoint.position; }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roadSpawner.Instance.SpawnTile();
            Destroy(this.gameObject, 2.0f);
        }
    }
}
