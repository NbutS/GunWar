using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGround : MonoBehaviour
{

    
    [SerializeField] private GameObject alterGround;
    private bool isSpawned = false;
    private bool isActived = false;

    public static FlyingGround Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            FindObjectOfType<AudioManager>().Play("HitGround");
            collision.gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if (Player.instance.IsOnTarget && GroundManager.instance.Count< GroundManager.instance.DistMove)
        {
            MovePosX();
        }
        CheckActived();
        
    }

    
    public void SpawnNew()
    {
        
        if (Player.instance.IsOnTarget)
        {
            isSpawned = true;
        }
        if ( isSpawned )
        {
            isSpawned = false;
            
            Player.instance.ChangeGO();
            
            //posX = Random.Range(4f, 5.8f);
            //posY = Random.Range(-2f, -0.5f);
            //enermy = ObjectPooler.instance.SpawnFromPool("Enermy");
            //flyingGround = ObjectPooler.instance.SpawnFromPool("Flying Ground");
            
            //enermy.transform.parent = flyingGround.transform;
            //flyingGround.transform.position = new Vector3(Random.Range(4f, 5.8f), Random.Range(-2f, -0.5f), 0f);
            //enermy.transform.position = new Vector3(flyingGround.transform.position.x, flyingGround.transform.position.y + 0.425f, 0f);
        }
    }


    public bool IsActived
    {
        get { return isActived; }
        set { isActived = value; }
    }


    public void CheckActived()
    {
        if ( Player.instance.IsOnTarget && !IsActived )
        {
            IsActived = true;
            alterGround.SetActive(true);
            alterGround.transform.position = gameObject.transform.position;
            gameObject.SetActive( false );
            SpawnNew();

        }
    }

    public void MovePosX()
    {
        //transform.position = new Vector3(transform.position.x - 0.02f, transform.position.y, transform.position.z);
        transform.Translate(Vector3.left * Time.deltaTime*1.5f);
    }
}
