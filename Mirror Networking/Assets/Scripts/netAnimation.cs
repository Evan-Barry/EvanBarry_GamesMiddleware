using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class netAnimation : NetworkBehaviour
{

    public Animator animator;
    public NetworkAnimator netAnimator;
    public bool attack = false;
    public float inputX, inputY;

    public GameObject hat;
    GameObject h;

    public GameObject head;

    public bool ikActive = true;
    //public Transform headObj = null;
    public Transform lookObj = null;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        netAnimator = GetComponent<NetworkAnimator>();

        // if(name == "Player")
        // {
        //     head = GameObject.Find("/Player/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:Neck/mixamorig:Head");
        // }

        // else if(tag == "zombie")
        // {
        //     head = GameObject.Find("/" + name + "/Base HumanPelvis/Base HumanSpine1/Base HumanSpine2/Base HumanRibcage/Base HumanNeck/Base HumanHead");
        // }

        // else if(name == "BaseHumanoidBotAvatar_Tpose")
        // {
        //     head = GameObject.Find("/BaseHumanoidBotAvatar_Tpose/Root/Spine/Spine1/Spine2/Neck/Head");
        // }

        // h = Instantiate(hat, new Vector3(head.transform.position.x, head.transform.position.y + 0.1f, head.transform.position.z + 0.03f), Quaternion.identity) as GameObject;
        // h.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        // h.transform.parent = head.transform;
        //headObj = h.transform;
    }

    void Update()
    {
        checkAttack();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(base.hasAuthority)
        {
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");

            move(inputY, inputX);
            //updateHorizontal(horizontal);
            attackMethod();
        }
        //h.transform.position = transform.position;
        //if(transform.tag == "zombie")
        //{
            // inputY = Input.GetAxis("Vertical");
            // inputX = Input.GetAxis("Horizontal");
            // animator.SetFloat("InputY", inputY);
            // animator.SetFloat("InputX", inputX);

            // if(Input.GetMouseButtonDown(0))
            // {
            //     netAnimator.SetTrigger("Action");
            // }

            // if(Input.GetKeyDown(KeyCode.X))
            // {
            //     netAnimator.SetTrigger("Death");
            // }

            // if(Input.GetKeyDown(KeyCode.Alpha1))
            // {
            //     netAnimator.SetTrigger("taunt1");
            // }
        //}
    }
    
    void checkAttack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            attack = true;
        }
    }

    void move(float m, float t)
    {
        float moveRate = 1f;
        transform.Rotate(0, t * moveRate * Time.deltaTime, 0);
        //transform.Translate(0, 0, m * moveRate * Time.deltaTime);
        transform.position += new Vector3(m, 0f, 0f) * moveRate * Time.deltaTime;
        updateAnim(m, t);
    }

    void updateAnim(float hor, float ver)
    {
        animator.SetFloat("InputX", hor);
        animator.SetFloat("InputY", ver);
    }

    void attackMethod()
    {
        if(!attack)
        {
            return;
        }

        attack = true;

        string attackString = "Attack";
        if(netAnimator != null)
        {
            netAnimator.SetTrigger(Animator.StringToHash(attackString));
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

}
