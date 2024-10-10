using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossArea : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            Debug.Log("Player loses");
        }
    }
}
