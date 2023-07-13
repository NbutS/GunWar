using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
    }
    private Dictionary<string, GameObject> poolDictionary;
    public List<Pool> pools;
    public static ObjectPooler instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
        poolDictionary = new Dictionary<string, GameObject>();
        foreach (var pool in pools)
        {
            GameObject obj = Instantiate(pool.prefab);
            obj.SetActive(false);
            poolDictionary.Add(pool.tag, obj);
        }
    }
    public GameObject SpawnFromPool( string tag )
    {
        GameObject obj = poolDictionary[tag];
        obj.SetActive(true);
        return obj;
    }
    public void TakeBackObject( string tag)
    {
        GameObject obj = poolDictionary[tag];
        obj.SetActive(false);
    }    

}
