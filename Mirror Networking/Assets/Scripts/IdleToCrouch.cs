using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleToCrouch : MonoBehaviour
{

    public Animator animator;

    public float inputX, inputY;

    public GameObject hat;
    GameObject h;

    public GameObject head;

    public bool ikActive = true;
    //public Transform headObj = null;
    public Transform lookObj = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

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

    // Update is called once per frame
    void Update()
    {
        //h.transform.position = transform.position;
        if(transform.tag == "Player")
        {
            inputY = Input.GetAxis("Vertical");
            inputX = Input.GetAxis("Horizontal");
            animator.SetFloat("InputY", inputY);
            animator.SetFloat("InputX", inputX);

            if(Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Action");
            }

            if(Input.GetKeyDown(KeyCode.X))
            {
                animator.SetTrigger("Death");
            }

            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                animator.SetTrigger("taunt1");
            }
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
