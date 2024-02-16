using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public enum ItemType { bag , jump_boot , speed_boot };

    public ItemType type;
    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (type == ItemType.bag)
            {
                scoreManager.instance.addScore(10);
                Destroy(this.gameObject);
            }

            if (type == ItemType.jump_boot && !scoreManager.instance.movement.jump_buff)
            {
                scoreManager.instance.buff_jump();
                Destroy(this.gameObject);
            }

            if (type == ItemType.speed_boot && !scoreManager.instance.movement.speed_buff)
            {
                scoreManager.instance.buff_speed();
                Destroy(this.gameObject);
            }


        }
    }


}
