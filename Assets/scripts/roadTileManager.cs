using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadTileManager : MonoBehaviour
{
    [SerializeField]
    private Transform nextSpawnPoint;

    public Vector3 getNextSpawnPoint() { return nextSpawnPoint.position; }

    private void OnTriggerExit(Collider other)
    {
        roadSpawner.Instance.SpawnTile();
        Destroy(this.gameObject , 2.0f);
    }
}
