using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    [SerializeField] private AlterEnermy alterEnermy;
    
    [SerializeField] private HeadEnermy headEnermy;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            FindObjectOfType<AudioManager>().Play("HitEnermy");
            collision.gameObject.SetActive(false);
            headEnermy.IsHeadShot = false;
            if ( !headEnermy.IsHeadShot)
            {
                Player.instance.Score++;
                
            }
            gameObject.transform.parent = null;
            alterEnermy.gameObject.SetActive(true);
            alterEnermy.transform.position = transform.position;
            gameObject.SetActive(false);
            alterEnermy.GetComponent<BoxCollider2D>().enabled = false;
            alterEnermy.AddForceFor();
        }
        
    }

    public void MovePosX()
    {
        transform.position = new Vector3(transform.position.x - Time.deltaTime, transform.position.y, transform.position.z);
    }
    

}
