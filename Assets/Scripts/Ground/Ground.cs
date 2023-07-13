using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            
            collision.gameObject.SetActive(false);
        }

    }

    
    public void MoveGround()
    {
        //transform.position = new Vector3(transform.position.x - 0.02f, transform.position.y, transform.position.z);
        transform.Translate(Vector3.left * Time.deltaTime* 1.5f);
    }
}
