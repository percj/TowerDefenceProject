using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickLogic : MonoBehaviour
{
    public float speed;
    [SerializeField] VariableJoystick variableJoystick;
    Animator animator;
    CharacterController characterController;
    public float gravity = 20.0F;
    internal bool cameraAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if (!cameraAnimation)
        {
            variableJoystick.gameObject.SetActive(true);
            var x = variableJoystick.Horizontal;
            var z = variableJoystick.Vertical;
            if (x != 0 || z != 0)
            {
                Vector3 move = new Vector3(x, 0f, z);
                transform.rotation = Quaternion.LookRotation(move);
                move.y -= gravity * Time.deltaTime;
                characterController.Move(move * speed * Time.deltaTime);
                animator.SetBool("run", true);
                animator.SetFloat("Speed", (move).magnitude);
            }
            else
            {
                Vector3 move = new Vector3(x, 0f, z);
                move.y -= gravity * Time.deltaTime;
                characterController.Move(move * speed * Time.deltaTime);
                animator.SetFloat("Speed",0);
                animator.SetBool("run", false);
            }
        }
        else
        {
            variableJoystick.OnPointerUp(new PointerEventData(EventSystem.current));
            variableJoystick.gameObject.SetActive(false);

        }
    }
}
