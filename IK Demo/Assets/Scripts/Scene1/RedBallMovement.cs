using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallMovement : MonoBehaviour
{

    //Make red ball move in a fixed linear path, otherwise make them manual
    public bool doFixedMovement;
    public bool doManualMovement;
    public bool PauseForManual;

    int iterationTime = 100;
    int currIterTime = 0;
    Vector3 v = new Vector3(-1, 1, 0);

    //Will move the object this script is given to
    void Update()
    {
        if (doFixedMovement)
        {
            GetComponent<Animator>().enabled = false;
            //transform.Rotate(0, Time.deltaTime * 90, 0);
            if (currIterTime != iterationTime)
            {
                transform.position += Time.deltaTime * v;
                currIterTime++;
            }
            else
            {
                v.y *= -1;
                v.x *= -1;
                currIterTime = 0;
            }
        }
        else if (doManualMovement)
        {
            GetComponent<Animator>().enabled = false;
            //Move Straight
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("forward");
                //transform.Translate(transform.forward * 5 * Time.deltaTime);
                transform.position += transform.up * Time.deltaTime * 5;
            }
            //Move Backward
            else if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("back");
                transform.position += transform.up * Time.deltaTime * -5;
            }
            //Rotate Clockwise
            else if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("turn clockwise");
                transform.position += transform.right * Time.deltaTime * -5;
            }
            //Rotate CounterClockwise
            else if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("turn counterclockwise");
                transform.position += transform.right * Time.deltaTime * 5;
            }
            //Move to left side
            else if (Input.GetKey(KeyCode.X))
            {
                Debug.Log("turn counterclockwise");
                transform.position += new Vector3(0, 0, 1) * Time.deltaTime * 5;
            }
            //Move to right side
            else if (Input.GetKey(KeyCode.Z))
            {
                Debug.Log("turn counterclockwise");
                transform.position += new Vector3(0, 0, 1) * Time.deltaTime * -5;
            }
        }
        else if (PauseForManual)
        {
            Debug.Log("Do nothing");
            //do nothing :)
        }
        else {
            GetComponent<Animator>().enabled = true;
        }
    }
}
