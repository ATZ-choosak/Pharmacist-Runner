using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomBox : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> pointSpawn = new List<Vector3>();

    [SerializeField]
    private GameObject box;
    void Start()
    {
        int random_box_count = randomLength(4);

        List<Vector3> pointSpawn_clone = pointSpawn;

        for (int i = 0; i < random_box_count; i++)
        {
            int index_point = randomLength(pointSpawn_clone.Count);
            spawnBox(pointSpawn_clone[index_point]);
            pointSpawn_clone.RemoveAt(index_point);
        }
    }

    void spawnBox(Vector3 pos)
    {
        GameObject new_box = Instantiate(box , Vector3.zero , Quaternion.Euler(new Vector3(-90 , 180 , 0)));
        new_box.transform.SetParent(this.transform);
        new_box.transform.localPosition = pos;
    }


    int randomLength(int len)
    {
        return Random.Range(0 , len);
    }
}
