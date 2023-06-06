using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Movement")]
    public bool canRun;
    public CharacterController controller;
    public float moveSpeed = 5f;
    public float runAddSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioSource stepSound;
    public float StepVolume =0.4f;
    public bool atStation =false;

    public void SetAtStation(bool boolean)
    {
        atStation = boolean;
    }

    Vector3 velocity;
    bool isGrounded;

    void OnEnable()
    {
        stepSound.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.isPause) { return; }



        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y <= 0f)
            {
                stepSound.volume = StepVolume;
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            float run;
            if (canRun)
            {
                run = Input.GetAxis("Run");
            }
            else
            {
                run = 0f;
            }

        if (Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0)
            {
                //Debug.Log("walking");
                stepSound.UnPause();
            }
            else
            {
                //Debug.Log("not walking");
                stepSound.Pause();
            }

            if (run > 0)
            {
                stepSound.pitch = 1.6f;
            }
            else
            {
                stepSound.pitch = 1.0f;
            }


            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * (moveSpeed + run * runAddSpeed) * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                stepSound.volume = 0f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
    }
        

        

}
