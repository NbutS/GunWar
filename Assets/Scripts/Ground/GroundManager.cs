using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public static GroundManager instance;
    [SerializeField] private float distMove;

    private float count = 0f;
    private void Awake()
    {
        if ( instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }
    [SerializeField] private Ground[] grounds;
    private Queue<int> gOrder = new Queue<int>();
    private bool isMoveOrder;
    [SerializeField] private float targetPos;

    private void Start()
    {
        for (int i = 0; i < 3; i++ )
        {
            gOrder.Enqueue(i);
        }
        isMoveOrder = false;
    }

    private void Update()
    {
        if ( IMO )
        {
            int tmp = gOrder.Dequeue();
            gOrder.Enqueue(tmp);
            grounds[tmp].transform.position = new Vector3(targetPos, grounds[tmp].transform.position.y, grounds[tmp].transform.position.z);
            IMO = false;
            Debug.Log("change order ground");
        }


        if (Player.instance.IsOnTarget && count < distMove)
        {
            count += (Time.deltaTime*1.5f);
            foreach(var ground in grounds)
            {
                ground.MoveGround();
            }

        }
        if (count >= distMove)
        {
            Debug.Log("count>=distMove");
            IMO = true;
            Player.instance.IsOnTarget = false;
            Player.instance.Renew();

            count = 0f;
        }
    }

    public bool IMO
    {
        get { return isMoveOrder; }
        set { isMoveOrder = value; }
    }
    public float Count
    {
        get { return count; }
        set { count = value; }
    }
    public float DistMove
    {
        get { return distMove; }

    }

}
