using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Gun : MonoBehaviour
{

    [SerializeField] private Player m_player;
    [SerializeField] private GameObject m_gun;
    [SerializeField] private GameObject m_enermy;
    [SerializeField] private float force;
    private Vector3 targetPoint;
    private GameObject m_bullet;
    private float angleRotation = 0f;
    private bool isFire = false;
    private float targetAngle;
    private bool isRotated = false;
   
    private void Start()
    {
        targetAngle = CalculatAngle();
        
    }
    private void Update()
    {
        if ( !m_player.IsOnTarget && m_player.Clock > 1f && m_player.ENABLEEGUN)
        {
            isFire = true;
            m_player.ENABLEEGUN = false;
            m_player.Clock = 0f;
            targetAngle = CalculatAngle();
            FindObjectOfType<AudioManager>().Play("PrepareFire");
        }
        if (isFire && angleRotation < targetAngle )
        {
            m_gun.transform.Rotate(Vector3.forward * Time.deltaTime *15f);
            angleRotation += Time.deltaTime * 15f;
        }
        if ( angleRotation > targetAngle)
        {
            isRotated = true;
        }
            
        if ( isRotated && isFire )
        {
            isFire = false;
            isRotated = false;
            FindObjectOfType<AudioManager>().Stop("PrepareFire");
            Fire();
        }
    }

    public void Fire()
    {
        //Vector3 childGlobalPosition = parentObject.transform.TransformPoint(childObject.transform.localPosition);
       
        m_bullet = ObjectPooler.instance.SpawnFromPool("E_Bullet");
        m_bullet.transform.position = m_gun.GetComponent<SpriteRenderer>().bounds.min;
        Vector3 Pos = m_enermy.transform.TransformPoint(transform.localPosition);
        Vector2 direction = new Vector2
        {
            x = targetPoint.x - Pos.x,
            y = targetPoint.y - Pos.y
        };
        m_bullet.GetComponent<Rigidbody2D>().AddForce(direction*force);
    }

    public float CalculatAngle()
    {
        Vector3 Pos = m_enermy.transform.TransformPoint(transform.localPosition);
        SetTargetPoint();
        float deltaX = Pos.x - targetPoint.x;
        float deltaY = Pos.y - targetPoint.y;
        float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
        return angle;
    }
    public void SetTargetPoint()
    {
        if ( Player.instance.CurrentID == 2 )
        {
            targetPoint = new Vector3(-1.57f, -2.64f, 0);
        }
        else
        {
            targetPoint = new Vector3(-1.58f, -2.64f, 0);
        }
    }
}
