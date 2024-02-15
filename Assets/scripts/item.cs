using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    enum ItemType { bag , jump_boot };

    [SerializeField]
    private ItemType type;
    
    ItemType getType()
    {
        return type;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            scoreManager.instance.addScore(10);
            Destroy(this.gameObject);
        }
    }


}
