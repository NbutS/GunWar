using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterEnermy : MonoBehaviour
{
    public void AddForceFor()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0.005f, 0.01f));
    }
    private void Update()
    {
        SetActiation();
    }
    public void SetActiation()
    {
        if (transform.position.y < -5.3f)
        {
            gameObject.SetActive(false);
            
        }
    }
}
