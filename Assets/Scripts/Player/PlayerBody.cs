using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Rigidbody2D RbGun;
    private Vector3 oriPosLLeftMan;
    private Vector3 oriPosLRightMan;
    private Vector3 oriPosLLeftRobot;
    private Vector3 oriPosLRightRobot;
    private Vector3 absPosL;
    private Vector3 absPosR;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject lLeft;
    [SerializeField] private GameObject lRight;
    [SerializeField] private LevelController lvController;
    private void Start()
    {
        RbGun = Player.instance.GetGunRb();
        rigidbody = GetComponent<Rigidbody2D>();
        oriPosLLeftRobot = new Vector3(-0.068f, -0.186f, 0f);
        oriPosLRightRobot = new Vector3(0.076f, -0.188f, 0f);
        oriPosLLeftMan = new Vector3(-0.058f, -0.196f, 0f);
        oriPosLRightMan = new Vector3(0.078f, -0.196f, 0f);
        //RbGun = gun.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if ( Player.instance.IsOnTarget && !animator.enabled )
        {
            
            animator.enabled = true;
        }
        if ( !Player.instance.IsOnTarget && animator.enabled )
        {
            if (Player.instance.CurrentID == 1)
            {
                absPosL = transform.TransformPoint(oriPosLLeftMan);
                absPosR = transform.TransformPoint(oriPosLRightMan);
                lLeft.transform.position = absPosL;
                lRight.transform.position = absPosR;
            }
            else if (Player.instance.CurrentID == 2)
            {
                absPosL = transform.TransformPoint(oriPosLLeftRobot);
                absPosR = transform.TransformPoint(oriPosLRightRobot);
                lLeft.transform.position = absPosL;
                lRight.transform.position = absPosR;
            }
            animator.enabled = false;
        }
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            
            collision.gameObject.SetActive(false);
            rigidbody.gravityScale = 1;
            rigidbody.AddForce(new Vector2(-1f, 40f), ForceMode2D.Force);
            RbGun.gravityScale = 1;
            RbGun.AddForce(new Vector2(-1f, 40f), ForceMode2D.Force);
            lvController.GameOver();
        }
        if ( collision.gameObject.CompareTag("E_Bullet"))
        {
            FindObjectOfType<AudioManager>().Play("HitPlayer");
            collision.gameObject.SetActive(false);
            rigidbody.gravityScale = 1;
            rigidbody.AddForce(new Vector2(-1f, 40f), ForceMode2D.Force);
            RbGun.gravityScale = 1;
            RbGun.AddForce(new Vector2(-1f, 40f), ForceMode2D.Force);
            lvController.GameOver();
        }
    }
}
