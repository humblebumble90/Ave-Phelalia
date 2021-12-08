using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private static ExplosionController _instance;
    private ExplosionController() { }
    public static ExplosionController getInstance()

    {
        if(_instance == null)
        {
            _instance = new ExplosionController();
        }
        return _instance;
    }
    public void destroy(GameObject go)
    {
        Destroy(go);
    }
}
