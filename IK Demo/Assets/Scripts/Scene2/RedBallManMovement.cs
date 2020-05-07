using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallManMovement : MonoBehaviour
{

    public Animator[] animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        setAllAnimators(false);
        //make a curved run
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("curve right");
            transform.Rotate(Vector3.up * 50 * Time.deltaTime);   
            transform.position += transform.right * Time.deltaTime * 5;
            setAllAnimators(true);
        }
        //make a curved run
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("curve left");
            transform.Rotate(Vector3.up * -50 * Time.deltaTime);
            transform.position += transform.right * Time.deltaTime * 5;
            setAllAnimators(true);
        }
        //Move Straight
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("forward");
            //transform.Translate(transform.forward * 5 * Time.deltaTime);
            transform.position += transform.right * Time.deltaTime * 5;
            setAllAnimators(true);
        }
        //Move Backward
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("back");
            transform.position += transform.right * Time.deltaTime * -5;
            setAllAnimators(true);
        }
        //Rotate Clockwise
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("turn clockwise");
            transform.Rotate(Vector3.up * -150 * Time.deltaTime);
            setAllAnimators(false);
        }
        //Rotate CounterClockwise
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("turn counterclockwise");
            transform.Rotate(Vector3.up * 150 * Time.deltaTime);
            setAllAnimators(false);
        }
    }



    void setAllAnimators(bool isMoving)
    {
        if (isMoving)
        {
            foreach (Animator anim in animator)
            {
                Debug.Log("NOT MOVING");
                anim.SetBool("notMoving", false);
            }
        }
        else
        {
            foreach (Animator anim in animator)
            {
                Debug.Log("MOVING");
                anim.SetBool("notMoving", true);
            }
        }
    }
}