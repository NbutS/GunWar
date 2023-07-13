using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEnermy : MonoBehaviour
{
    [SerializeField] private AlterEnermy alterEnermy;
    [SerializeField] private AnimationHeadShot aniHeadShot;
    
    private bool isHeadShot = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            FindObjectOfType<AudioManager>().Play("HitEnermy");
            collision.gameObject.SetActive(false);
            aniHeadShot.gameObject.SetActive(true);
            Player.instance.Score++;
            isHeadShot = true;
            gameObject.transform.parent.parent = null;
            alterEnermy.gameObject.SetActive(true);
            alterEnermy.transform.position = transform.parent.position;
            transform.parent.gameObject.SetActive(false);
            alterEnermy.GetComponent<BoxCollider2D>().enabled = false;
            alterEnermy.AddForceFor();
            Debug.Log("Head SHOT");

        }
    }

    public bool IsHeadShot
    {
        get { return isHeadShot; }
        set { isHeadShot = value; }
    }
}
