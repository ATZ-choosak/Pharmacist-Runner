using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomBox : MonoBehaviour
{
    enum Type {box , police_stand , item_boot};

    [SerializeField]
    private List<Vector3> pointSpawn = new List<Vector3>();

    [SerializeField]
    private GameObject box;

    [SerializeField]
    private Type type = Type.box;

    void Start()
    {
        int random_box_count = randomLength(4);

        if (type == Type.item_boot)
        {
            random_box_count = randomLength(10) % 4 == 0 ? 1 : 0;
        }

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
        Vector3 rotaion = Vector3.zero;

        if (type == Type.box)
        {
            rotaion = new Vector3(-90, 180, 0);
        }

        if (type == Type.police_stand)
        {
            rotaion = new Vector3(0, 180, 0);
        }

        GameObject new_box = Instantiate(box , Vector3.zero , Quaternion.Euler(rotaion));
        new_box.transform.SetParent(this.transform);
        new_box.transform.localPosition = pos;
    }


    int randomLength(int len)
    {
        return Random.Range(0 , len);
    }
}
