using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeatScript : MonoBehaviour
{
    public void Start()
    {
        Debug.Log(gameObject.name);
        Destroy(gameObject, 0.7f);
    }
}
