using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallManMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("up");
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("down");
            transform.Translate(Vector3.back * 5 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("left");
            transform.Translate(Vector3.left * 5 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("right");
            transform.Translate(Vector3.right * 5 * Time.deltaTime);
        }

    }
}