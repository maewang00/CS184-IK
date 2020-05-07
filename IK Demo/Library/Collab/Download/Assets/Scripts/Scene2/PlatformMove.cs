using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{

    int iterationTime = 200;
    int currIterTime = 0;
    Vector3 v = new Vector3(-3, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currIterTime != iterationTime)
        {
            transform.position += Time.deltaTime * v;
            currIterTime++;
        }
        else
        {
            v.x *= -1;
            currIterTime = 0;
        }
    }
}
