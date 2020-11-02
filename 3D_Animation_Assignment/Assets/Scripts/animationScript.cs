using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationScript : MonoBehaviour
{
    public Animator robot_movements;
    public Animator zombie_movements;
    GameObject robot;
    GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {
        robot = GameObject.FindGameObjectWithTag("robot");
        zombie = GameObject.FindGameObjectWithTag("zombie");

        robot_movements = robot.GetComponent<Animator>();
        zombie_movements = zombie.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            robot_movements.SetBool("isWalking", true);
            zombie_movements.SetBool("isWalking", true);
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (1f * Time.deltaTime));
            robot.transform.position += robot.transform.forward * Time.deltaTime;
            zombie.transform.position += zombie.transform.forward * Time.deltaTime;
        }

        else
        {
            robot_movements.SetBool("isWalking", false);
            zombie_movements.SetBool("isWalking", false);
        }

        if(Input.GetKey(KeyCode.D))
        {
            //transform.rotation.y += (1f * Time.deltaTime);
            //robot.transform.RotateAround(robot.transform.position, robot.transform.up, 90f * Time.deltaTime);
            //zombie.transform.RotateAround(zombie.transform.position, zombie.transform.up, 90f * Time.deltaTime);
            //robot_movements.SetBool("turnRight", true);
        }

        else
        {
            //robot_movements.SetBool("turnRight", false);
        }

        if(Input.GetKey(KeyCode.A))
        {
            //transform.rotation.y -= (1f * Time.deltaTime);
            //robot.transform.RotateAround(robot.transform.position, robot.transform.up, -90f * Time.deltaTime);
            //zombie.transform.RotateAround(zombie.transform.position, zombie.transform.up, -90f * Time.deltaTime);
            //robot_movements.SetBool("turnLeft", true);
        }

        else
        {
            //robot_movements.SetBool("turnLeft", false);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Jumping");
            robot_movements.SetBool("explosion", true);
            zombie_movements.SetBool("isAttacking", true);
        }

        else
        {
            robot_movements.SetBool("explosion", false);
            zombie_movements.SetBool("isAttacking", false);
        }
     
    }
}
