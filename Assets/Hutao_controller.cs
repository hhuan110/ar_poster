using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hutao_controller : MonoBehaviour
{
    Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Reset the "Crouch" trigger
            m_Animator.ResetTrigger("wave");
            m_Animator.ResetTrigger("hello");

            //Send the message to the Animator to activate the trigger parameter named "Jump"
            m_Animator.SetTrigger("Arm");
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Reset the "Jump" trigger
            m_Animator.ResetTrigger("Arm");
            m_Animator.ResetTrigger("hello");

            //Send the message to the Animator to activate the trigger parameter named "Crouch"
            m_Animator.SetTrigger("wave");
        }

        if (Input.GetKey(KeyCode.Q))
        {
            //Reset the "Jump" trigger
            m_Animator.ResetTrigger("Arm");
            m_Animator.ResetTrigger("wave");


            //Send the message to the Animator to activate the trigger parameter named "Crouch"
            m_Animator.SetTrigger("hello");
        }
    }
}
