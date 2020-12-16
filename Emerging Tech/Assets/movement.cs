using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody rb;
    public float transX;
    public float transZ;
    public float speed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Camera.main.transform.SetParent(transform);
        // Camera.main.transform.localPosition = new Vector3(0, 180, -100);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {  
        transX = Input.GetAxis("Horizontal");
        transZ = Input.GetAxis("Vertical");
        
        Quaternion deltaRotation = Quaternion.Euler(transX * new Vector3(0,rotationSpeed,0) * Time.deltaTime);

        rb.MoveRotation(rb.rotation * deltaRotation);
        rb.MovePosition(rb.position + transform.forward * speed * transZ * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "textured_output")
        {
            Debug.Log("Collision");
        }
    }
}
