using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject man_Body;
    private GameObject man_Gun;
    private void Start()
    {
        man_Body = ObjectPooler.instance.SpawnFromPool("man_Body");
        man_Gun =  ObjectPooler.instance.SpawnFromPool("man_Gun");
        man_Body.transform.position = new Vector3(-1.478f, -2.59505f, 0f);
        man_Gun.transform.position = new Vector3(-1.534f, -2.61505f, 0f);
    }
}
