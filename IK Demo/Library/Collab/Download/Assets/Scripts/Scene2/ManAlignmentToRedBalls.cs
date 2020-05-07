using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManAlignmentToRedBalls : MonoBehaviour
{
    //Where all the red ball's are located
    public Transform TargetBlock;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = TargetBlock.position + Vector3.up * -0.55f; // + Vector3.up * -0.7f
        transform.rotation = TargetBlock.rotation;
    }
}