using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D col)
    {
        Destroy(col.gameObject);
        Debug.Log("Removed a object");
    }
}
