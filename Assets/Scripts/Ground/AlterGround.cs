using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterGround : MonoBehaviour
{
    [SerializeField] private GameObject flyingGround;
    
    
    
    private void Update()
    {
        
        MoveFlyingGr();
    }

    

    
    public void MoveFlyingGr()
    {
        if (Player.instance.IsOnTarget && transform.position.x > -2.45f)
        {
            //transform.position = new Vector3(transform.position.x - 0.02f, transform.position.y, transform.position.z);
            transform.Translate(Vector3.left* Time.deltaTime * 1.5f);
        }
        if ( transform.position.x < -2.45f)
        {
            gameObject.SetActive(false);
        }
    }
}
