using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Rigidbody2D rigidbodyGun;
    private Rigidbody2D rigidbodyPlayer;
    [SerializeField] private Animator animator;
    //[SerializeField] private GameObject player;
    [SerializeField] private LevelController lvController;
    private void Start()
    {
        rigidbodyGun = Player.instance.GetGunRb();
        rigidbodyPlayer = Player.instance.GetBodyRb();
    }
    public void Update()
    {
        if ( Player.instance.IsOnTarget && !animator.enabled)
        {
            animator.enabled = true;
        }
        if ( !Player.instance.IsOnTarget && animator.enabled)
        {
            animator.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);
            rigidbodyGun.gravityScale = 1;
            rigidbodyGun.AddForce(new Vector2(-0.5f, 40f), ForceMode2D.Force);
            rigidbodyPlayer.gravityScale = 1;
            rigidbodyPlayer.AddForce(new Vector2(-0.5f, 40f), ForceMode2D.Force);
            lvController.GameOver();
        }
        if ( collision.gameObject.CompareTag("E_Bullet"))
        {
            FindObjectOfType<AudioManager>().Play("HitPlayer");
            collision.gameObject.SetActive(false);
            rigidbodyGun.gravityScale = 1;
            rigidbodyGun.AddForce(new Vector2(-0.5f, 40f), ForceMode2D.Force);
            rigidbodyPlayer.gravityScale = 1;
            rigidbodyPlayer.AddForce(new Vector2(-0.5f, 40f), ForceMode2D.Force);
            lvController.GameOver();
        }
    }
}
