using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 10f;
    public float jumpForce = 10f;
    public float jumpBoost = 5f;

    public bool isGrounded;
    private bool finished = false;
    private bool over = false;
    
    public TextMeshProUGUI timerText;


    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        Rigidbody rbody = GetComponent<Rigidbody>();
        rbody.velocity += horizontalAxis * Vector3.right * Time.deltaTime * acceleration;

        Collider col = GetComponent<Collider>();
        float castAmount = col.bounds.extents.y - 0.02f;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, castAmount);
        
        rbody.velocity = new Vector3(Mathf.Clamp(rbody.velocity.x, -maxSpeed, maxSpeed), rbody.velocity.y, rbody.velocity.z);

        //space
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rbody.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
        }
        
        //arrows (rotate mario)
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        }
        
        //boost
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            maxSpeed *= 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            maxSpeed /= 2;
        }

        float xVelocity = Mathf.Clamp(rbody.velocity.x, -maxSpeed, maxSpeed);

        if (Mathf.Abs(horizontalAxis) < 0.1f)
        {
            xVelocity *= 0.9f;
        }
            
        rbody.velocity = new Vector3(xVelocity, rbody.velocity.y, rbody.velocity.z);

        float speed = rbody.velocity.magnitude;
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("Speed", speed);
        animator.SetBool("Jumping", !isGrounded);
        
        //timer
        int wholeSecond = (int)Mathf.Floor(Time.realtimeSinceStartup);
        if (!finished)
        {
            if (wholeSecond >= 100)
            {
                over = true;
                finished = true;
            }
            else
            {
                timerText.text = "Time \n" + (100 - wholeSecond);
            }
        }
        else
        {
            if (over)
            {
                timerText.text = "Time \n Game Over!";
            }
            else
            {
                timerText.text = "Time \n You won!";
            }
        }
    }

    //used for the time text
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            finished = true;
        }

        if (collision.gameObject.CompareTag("Respawn"))
        {
            finished = true;
            over = true;
        }
    }
}
