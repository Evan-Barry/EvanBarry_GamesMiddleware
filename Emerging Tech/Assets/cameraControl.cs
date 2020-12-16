using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0,speed*Time.deltaTime,0,Space.World);
        }

        if(Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0,-speed*Time.deltaTime,0,Space.World);
        }
    }
}
