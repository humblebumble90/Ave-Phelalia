using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ObjectFactory
{
    private GameObject _object;
    private static ObjectFactory _instance;
    private Queue<GameObject> _e1Pool;
    private GameObject e1;
    private GameObject e2;
    private GameObject e3;
    private GameObject e4;
    private GameObject boss1;
    private GameObject boss2;
    private GameObject boss3;
    private ObjectFactory() { 
    }
    public static ObjectFactory getInstance()
    {
        if(_instance == null)
        {
            _instance = new ObjectFactory();
        }
        return _instance;
    }
    public void initialize()
    {
        _e1Pool = new Queue<GameObject>(5);
        e1 = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Asset/Prefabs/Enemy1.prefab", typeof(GameObject));
        Enumerable.Range(0, 5).ToList().ForEach(x => _e1Pool.Enqueue(e1));
    }
    public GameObject getObject(string name)
    {
        switch(name)
        {
            case "e1":
                _object = _e1Pool.Dequeue();
                _e1Pool.Enqueue(e1);
                //_e1Pool = new Queue<GameObject>();
                break;
            case "e2":

                _object = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Asset/Prefabs/Enemy2.prefab", typeof(GameObject));
                break;
            case "e3":
                _object = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Asset/Prefabs/Enemy3.prefab", typeof(GameObject));
                break;
            case "e4":
                _object = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Asset/Prefabs/Enemy4.prefab", typeof(GameObject));
                break;
            case "boss1":
                _object = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Asset/Prefabs/Boss1.prefab", typeof(GameObject));
                break;
            case "boss2":
                _object = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Asset/Prefabs/Boss2.prefab", typeof(GameObject));
                break;
            case "boss3":
                _object = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Asset/Prefabs/Boss3.prefab", typeof(GameObject));
                break;
            default:
                break;
        }
        return _object;
    }
}
