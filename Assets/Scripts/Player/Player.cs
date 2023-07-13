using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private GameObject gun;
    private GameObject body;
    private GameObject bullet;
    
    private GameObject enermy;
    private GameObject flyingGround;
    private bool isOnTarget;
    private bool isStart = false;
    private bool isForce = false;
    private bool isMaxAngle = false;
    private bool isenableEGun = false;
    private float clock;
    private bool isCabable = true;
    [SerializeField] private float force;
    [SerializeField] private float speedRotate;
    private bool bgAugioOn = true;
    private bool soundAudioOn = true;
    private int score;
    private bool isvalid = false;
    
    private BoxCollider2D enermybox;
    private Rigidbody2D playerGunRb;
    [SerializeField] private HeadEnermy headEnermy;
    [SerializeField] private GameObject homeScreen;
    public static Player instance;
    private int currentID ;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
        
    }
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("HomeBG");
        Debug.Log(TotalScore.instance.CurrentID + " currenID in Player");
        if(TotalScore.instance.IsReplay == true )
        {
            homeScreen.SetActive(false);
            TotalScore.instance.IsReplay = false;
        }
        
        currentID = TotalScore.instance.CurrentID;
        if( currentID == 2)
        {
            SetForRobotPlayer();
        }
        else if ( currentID == 1 )
        {
            SetForManPlayer();
        }
        score = 0;
        isOnTarget = false;
        clock = 0f;
        enermy = ObjectPooler.instance.SpawnFromPool("Enermy");
        flyingGround = ObjectPooler.instance.SpawnFromPool("Flying Ground");
        SetForGroundAndEnermy();
        
        enermybox = enermy.GetComponent<BoxCollider2D>();
        playerGunRb = gun.GetComponent<Rigidbody2D>();
    }

    public void SetForRobotPlayer()
    {
        gun = ObjectPooler.instance.SpawnFromPool("RobotGun");
        body = ObjectPooler.instance.SpawnFromPool("RobotBody");
        gun.transform.position = new Vector3(-1.45f, -2.59505f, 0f);
        body.transform.position = new Vector3(-1.5f, -2.61505f, 0f);
        
    }

    public void SetForManPlayer()
    {
        gun = ObjectPooler.instance.SpawnFromPool("manGun");
        body = ObjectPooler.instance.SpawnFromPool("manBody");
        gun.transform.position = new Vector3(-1.45f, -2.59505f, 0f);
        body.transform.position = new Vector3(-1.5f, -2.61505f, 0f);
        
    }

    public void SetActiveFalse()
    {
        gun.SetActive(false);
        body.SetActive(false);
       
    }
    private void Update()
    {
        if ( Input.GetMouseButtonDown(0) && !IsMouseOverUI() && !isForce )
        {
            isvalid = true;
        }
        if ( isvalid)
            RotateGun();
        if ( isForce )
        {
            if ( bullet.transform.position.y < -3f )
            {
                bullet.SetActive(false);
            }
            if (bullet.transform.position.y > 8f)
            {
                bullet.SetActive(false);
            }
            if (bullet.transform.position.x > 6.5f)
            {
                bullet.SetActive(false);
            }
        }
        if ( (Input.touchCount == 0 && isStart && !isForce) )
        {
            if (currentID == 2)
                RobotFire();
            else if (currentID == 1)
                Fire();
            isCabable = false;
            isForce = true;
            isStart = false;
        }
        if ( Input.touchCount == 0 && isForce )
            ChangeBulletRotation();
        if ( isForce && !bullet.active && enermybox.isActiveAndEnabled && playerGunRb.gravityScale == 0 )
        {
            isForce = false;
            isenableEGun = true;
        }
            
        if ( isenableEGun )
        {
            clock += Time.deltaTime;
        }
        if ( enermy != null && !enermybox.isActiveAndEnabled && isForce )
        {
            gun.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isOnTarget = true;
            isStart = false;
            isForce = false;
        }
        //if (clock > 1f && enermy.GetComponent<BoxCollider2D>().isActiveAndEnabled) ;
        
        
    }

    public void RotateGun()
    {
        
        if (Input.touchCount > 0 && !isMaxAngle && isCabable)
        {
            enermy.transform.parent = null;
            isStart = true;
            gun.transform.Rotate(Vector3.forward *Time.deltaTime*speedRotate);
            if (gun.transform.rotation.eulerAngles.z > 90f)
            {
                gun.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                isMaxAngle = true;
            }
        }
    }

    public void RobotFire()
    {
        FindObjectOfType<AudioManager>().Play("RobotFire");
        bullet = ObjectPooler.instance.SpawnFromPool("Bullet Robot");
        bullet.transform.position = gun.GetComponent<BoxCollider2D>().bounds.max;
        bullet.transform.Rotate(Vector3.forward, GetAngleGunRotation());
        SpriteRenderer spriteRendererGun = gun.GetComponent<SpriteRenderer>();
        Vector2 direction = new Vector2
        {
            x = spriteRendererGun.bounds.max.x - spriteRendererGun.bounds.min.x + 0.012f,
            y = spriteRendererGun.bounds.max.y - spriteRendererGun.bounds.min.y
        };

        if (!isMaxAngle)
            bullet.GetComponent<Rigidbody2D>().AddForce((direction) * (force + 800f), ForceMode2D.Force);
        else
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 8f) * force, ForceMode2D.Force);
        }
    }

    public void Fire()
    {
        FindObjectOfType<AudioManager>().Play("ManFire");
        Debug.Log("ManFire");
        bullet = ObjectPooler.instance.SpawnFromPool("Bullet");
        bullet.transform.position = gun.GetComponent<BoxCollider2D>().bounds.max;
        bullet.transform.Rotate(Vector3.forward,GetAngleGunRotation() );
        SpriteRenderer spriteRendererGun = gun.GetComponent<SpriteRenderer>();
        Debug.Log(bullet.transform.rotation.eulerAngles );
        Vector2 direction = new Vector2
        {
            x = spriteRendererGun.bounds.max.x - spriteRendererGun.bounds.min.x,
            y = spriteRendererGun.bounds.max.y - spriteRendererGun.bounds.min.y
        };
        if (!isMaxAngle)
            bullet.GetComponent<Rigidbody2D>().AddForce((direction)*(force + 1.1f*GetAngleGunRotation()),ForceMode2D.Force);
        else
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,1.3f)*force, ForceMode2D.Force);
        }
        
    }



    public float GetAngleGunRotation()
    {
        return gun.transform.rotation.eulerAngles.z;
    }

    public void ChangeBulletRotation()
    {
        float angle = Mathf.Atan2(bullet.GetComponent<Rigidbody2D>().velocity.y,bullet.GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }

    public void SetForGroundAndEnermy()
    {
        flyingGround.transform.position = new Vector3(Random.RandomRange(transform.position.x + 0.6f, transform.position.x + 0.8f), Random.RandomRange(transform.position.y + 0.95f, transform.position.y + 1.35f), 0f);
        enermy.transform.position = new Vector3(flyingGround.transform.position.x, flyingGround.transform.position.y + 0.425f , 0f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.CompareTag("Bullet Robot"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    public bool IsOnTarget
    {
        get { return isOnTarget; }
        set { isOnTarget = value; }
    }

    public float Clock
    {
        get { return clock; }
        set { clock = value; }
    }

    public bool ENABLEEGUN
    {
        get { return isenableEGun; }
        set { isenableEGun = value;}
    }


    public void Renew()
    {
        gun.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        isvalid = false;
        isForce = false;
        isMaxAngle = false;
        isenableEGun = false;
        clock = 0f;
        IsOnTarget = false;
        isCabable = true;
        headEnermy.IsHeadShot = false;
        FlyingGround.Instance.IsActived = false;
    }

    public void ChangeGO( )
    {
        //enermy = ObjectPooler.instance.SpawnFromPool("Enermy");
        //flyingGround = ObjectPooler.instance.SpawnFromPool("Flying Ground");
        enermy.SetActive(true);
        flyingGround.SetActive(true);
        
        enermy.transform.parent = flyingGround.transform;
        flyingGround.transform.position = new Vector3(Random.Range(4f, 5.8f), Random.Range(-2f, -0.5f), 0f);
        enermy.transform.position = new Vector3(flyingGround.transform.position.x, flyingGround.transform.position.y + 0.425f, 0f);
        Debug.Log("ChanbgeGO check IsOnTarget " + isOnTarget);
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }


    public bool SoundAudioOn
    {
        get { return soundAudioOn; }
        set { soundAudioOn = value; }
    }

    public bool BGAudioOn
    {
        get { return bgAugioOn; }
        set { bgAugioOn = value; }
    }

    
    public Rigidbody2D GetGunRb()
    {
        return gun.GetComponent<Rigidbody2D>();
    }

    public Rigidbody2D GetBodyRb()
    {
        return body.GetComponent<Rigidbody2D>();
    }

    

    //public bool IsPointerOverUIObject()
    //{
    //    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    //    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    //    return results.Count > 0;
    //}

    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject(0);
    }

    public int CurrentID
    {
        get { return currentID; }
        set { currentID = value; }
    }
}
