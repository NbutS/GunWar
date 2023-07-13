using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float posStart;
    [SerializeField] private float posEnd;

    private void Update()
    {
        
        MoveCloud();
        SetPos();
    }

    public void MoveCloud()
    {
        transform.position = new Vector3(transform.position.x - 0.003f, transform.position.y, transform.position.z);
        
    }

    public void SetPos()
    {
        if (transform.position.x < posEnd)
        {
            transform.position = new Vector3(posStart, transform.position.y, transform.position.z);
        }
    }
}
