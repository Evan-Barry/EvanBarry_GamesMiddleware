using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleToCrouch : MonoBehaviour
{

    public Animator animator;

    public float inputX, inputY;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
        animator.SetFloat("InputY", inputY);
        animator.SetFloat("InputX", inputX);
    }
}
