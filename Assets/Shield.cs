using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float RotateSpeed;
    public Transform Host;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   void Update()
    {
        this.transform.position = Host.position;
        this.transform.Rotate(0f,0f,RotateSpeed);
    }
}
