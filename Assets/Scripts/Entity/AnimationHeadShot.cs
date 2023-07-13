using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHeadShot : MonoBehaviour
{
   
    private float count = 0f;
    
    private void Update()
    {
        if ( count < 2.5f )
        {
            count += Time.deltaTime;
        }
        if (count > 2.5f)
        {
            gameObject.SetActive(false);
            count = 0f;
        }

    }
}
