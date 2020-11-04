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
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if(name == "Player")
        {
            head = GameObject.Find("/Player/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:Neck/mixamorig:Head");
        }

        else if(name == "Zombie1")
        {
            head = GameObject.Find("/Zombie1/Base HumanPelvis/Base HumanSpine1/Base HumanSpine2/Base HumanRibcage/Base HumanNeck/Base HumanHead");
        }

        else if(name == "BaseHumanoidBotAvatar_Tpose")
        {
            head = GameObject.Find("/BaseHumanoidBotAvatar_Tpose/Root/Spine/Spine1/Spine2/Neck/Head");
        }

        h = Instantiate(hat, head.transform.position, Quaternion.identity) as GameObject;
        h.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //h.transform.position = transform.position;

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
    }
}
