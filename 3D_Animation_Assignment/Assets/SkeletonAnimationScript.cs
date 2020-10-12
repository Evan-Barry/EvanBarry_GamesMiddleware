using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationScript : MonoBehaviour
{
    Animator skeleton_movements;

    // Start is called before the first frame update
    void Start()
    {
        skeleton_movements = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            skeleton_movements.SetBool("isWalking", true);
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (1f * Time.deltaTime));
            transform.position += transform.forward * Time.deltaTime;
        }

        else
        {
            skeleton_movements.SetBool("isWalking", false);
        }

        if(Input.GetKey(KeyCode.D))
        {
            //transform.rotation.y += (1f * Time.deltaTime);
            transform.RotateAround(transform.position, transform.up, 90f * Time.deltaTime);
            skeleton_movements.SetBool("turnRight", true);
        }

        else
        {
            skeleton_movements.SetBool("turnRight", false);
        }

        if(Input.GetKey(KeyCode.A))
        {
            //transform.rotation.y -= (1f * Time.deltaTime);
            transform.RotateAround(transform.position, transform.up, -90f * Time.deltaTime);
            skeleton_movements.SetBool("turnLeft", true);
        }

        else
        {
            skeleton_movements.SetBool("turnLeft", false);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Jumping");
            skeleton_movements.SetBool("isJumping", true);
        }

        else
        {
            skeleton_movements.SetBool("isJumping", false);
        }
     
    }
}
