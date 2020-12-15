using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class netAnimation : NetworkBehaviour
{

    public Animator animator;
    public NetworkAnimator netAnimator;
    public bool action = false;
    public float inputX, inputY;

    public bool ikActive = true;
    public Transform lookObj = null;

    public GameObject objectPrefab;

    //public GameObject[] cubes;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        netAnimator = GetComponent<NetworkAnimator>();
    }

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 2, -2);
        //Camera.main.transform.localRotation = new Quaternion(20, 0, 0, 0);
    }

    void Update()
    {
        if(base.hasAuthority)
        {
            // if(lookObj == null)
            // {
            //     lookObj = GameObject.FindGameObjectWithTag("objectSpawn").transform;
            // }

            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");

            move(inputY, inputX);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                action = true;
            }
            attackMethod();

            if(Input.GetKeyDown(KeyCode.I))
            {
                spawnItem();
            }

            findCubes();

        }
    }

    void move(float m, float t)
    {
        //float moveRate = 1f;
        float turnRate = 20f;

        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, t * turnRate * Time.deltaTime, 0);
            Debug.Log("Turn Right");
        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, t * turnRate * Time.deltaTime, 0);
            Debug.Log("Turn Left");
        }

        if(Input.GetKey(KeyCode.W))
        {
            //transform.position += Vector3.forward * moveRate * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.S))
        {
            //transform.position += Vector3.back * moveRate * Time.deltaTime;
        }
        //transform.position += new Vector3(m, 0f, 0f) * moveRate * Time.deltaTime;
        //transform.position += Vector3.forward * moveRate * Time.deltaTime;
        updateAnim(m, t);
    }

    void updateAnim(float hor, float ver)
    {
        animator.SetFloat("InputX", hor);
        animator.SetFloat("InputY", ver);
    }

    void attackMethod()
    {
        if(!action)
        {
            return;
        }

        action = false;

        string actionString = "Action";
        if(netAnimator != null)
        {
            netAnimator.SetTrigger(Animator.StringToHash(actionString));
        }
    }

    void OnAnimatorIK()
    {
        if(animator)
        {
            if(ikActive)
            {
                if(lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }
            }

            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }

    [Command]
    void spawnItem()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), 0.05f, Random.Range(-5, 5));
        GameObject newObject = Instantiate(objectPrefab.gameObject, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(newObject);
    }

    void findCubes()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("cube");
        GameObject nearestCube = null;

        if(cubes != null || cubes.Length > 0)
        {
            nearestCube = cubes[0];

            foreach(GameObject cube in cubes)
            {
                if(Vector3.Distance(cube.transform.position, transform.position) < Vector3.Distance(nearestCube.transform.position, transform.position))
                {
                    nearestCube = cube;
                }
            }

            lookObj = nearestCube.transform;
        }
    }

}
