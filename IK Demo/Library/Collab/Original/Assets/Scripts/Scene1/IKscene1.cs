using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Refer to FastIKFabric.cs for IK implementations
public class IKscene1 : MonoBehaviour
{

    /// NOTE: Indices start from end to root (since this script is attached to the end-effector)
    /// 
    //  root
    //  (joint[N]) (bonelen[N]) (joint[N-1]) (bonelen[N-1]) (joint[N-2])...
    //   x--------------------x--------------------x---...


    #region publicVariables
    /// Distance when the solver stops
    public float Delta = 0.001f;
    /// Chain length of bones
    public int ChainLength = 2;
    /// Target the chain should bend to
    public Transform Target;
    /// Do we want the joints to be only moveable in 2D?
    public bool fixedView;
    #endregion


    #region privateVariables
    /// Size of system
    protected float CompleteLength;
    /// Pointer of root joint position
    protected Transform Root;
    /// End-effector's position
    protected Transform EndEffector;
    /// List of joint positions (from root to leaf)
    protected Transform[] Joints;
    /// List of joint lengths
    protected float[] BonesLength;
    protected float Joint0Angle;
    protected float Joint1Angle;
    //protected Vector3[] StartDirectionSucc;    
    //protected Quaternion[] StartRotationBone;
    //protected Quaternion StartRotationTarget;
    #endregion





    /// Strength of going back to the start position.
    /// Solver iterations per update
    //public Transform Pole;
    //public int Iterations = 10;
    //[Range(0, 1)]
    //public float SnapBackStrength = 1f;


    /// Start is called before the first frame update
    void Start()
    {

        //Get position of the red ball
        //GameObject player = GameObject.Find("Player");
        //playerPos = player.transform;
        Debug.Log("transform: " + transform.position);

        // Initialize private variables
        Joints = new Transform[ChainLength + 1];
        BonesLength = new float[ChainLength];

        // Find the root starting from end-effector
        EndEffector = transform;
        Root = transform;
        for (var i = 0; i < ChainLength; i++)
        {
            if (Root == null)
            {
                throw new UnityException("ChainLength and Heirarchy are inconsistent. Please check it!");
            }
            Root = Root.parent;
        }

        // Initialize Joints, CompleteLength, BonesLength [from end-effector to root]
        CompleteLength = 0;
        var current = transform;
        for (int i = Joints.Length - 1; i >= 0; i--)
        {
            Joints[i] = current;
            if (i < Joints.Length - 1)
            {
                BonesLength[i] = (Joints[i].position - Joints[i + 1].position).magnitude;
                CompleteLength += BonesLength[i];
            }
            current = current.parent;
        }
    }

    

    /// Update is called once per frame
    void Update()
    {
        //Debugger Tool (press Space)
        if (Input.GetKeyDown("space")) {
            Debug.Log("End-effector pos: " + EndEffector.position);
            Debug.Log("Root pos: " + Root.position);
            Debug.Log("Size of Structure: " + Joints.Length + " joints, " + BonesLength.Length + " bones.");
            //From root to leaf (global x,y,z)
            for (int i = 0; i < Joints.Length; i++)
            {
                Debug.Log("Position of Joint at " + i + ": " + Joints[i].position);
            }

            for (int i = 0; i < BonesLength.Length; i++) {
                Debug.Log("bone length:" + BonesLength[i]);
            }
        }


        //ResolveIK();
        ResolveIK2();
    }

    /// IK 2D solver based on:
    /// https://cs184.eecs.berkeley.edu/sp20/lecture/18-51/intro-to-animation-and-kinematic
    void ResolveIK()
    {
        Vector3 p = Target.position;
        float l1 = BonesLength[1];
        float l2 = BonesLength[0];
        float rotateJoint0;
        float rotateJoint1;

        float numerator2 = Mathf.Pow(p.y, 2.0f) + Mathf.Pow(p.x, 2.0f) - Mathf.Pow(l1, 2.0f) - Mathf.Pow(l2, 2.0f);
        float demoninator2 = 2 * l1 * l2;
        float theta2 = Mathf.Acos(numerator2 / demoninator2);
        float NewJoint1Angle = theta2 * Mathf.Rad2Deg;

        //if (Joint1Angle.Equals(null)) {
        //    Joint1Angle = NewJoint1Angle;
        //    rotateJoint1 = Joint1Angle;
        //}
        //else
        //{
        //    rotateJoint1 = NewJoint1Angle - Joint1Angle;
        //    Joint1Angle = NewJoint1Angle;
        //}

        float numerator1 = -1.0f * p.y * l2 * Mathf.Sin(theta2) + p.x * (l1 + l2* Mathf.Cos(theta2));
        float demoninator1 = p.x * l2 * Mathf.Sin(theta2) + p.y * (l1 + l2 * Mathf.Cos(theta2)); 
        float theta1 = numerator1 / demoninator1;
        float NewJoint0Angle = theta1 * Mathf.Rad2Deg;

        //if (Joint0Angle.Equals(null))
        //{
        //    Joint0Angle = NewJoint0Angle;
        //    rotateJoint0 = Joint0Angle;
        //}
        //else
        //{
        //    rotateJoint0 = NewJoint0Angle - Joint0Angle;
        //    Joint0Angle = NewJoint0Angle;
        //}

        //Debug.Log("rotate joint0:" + rotateJoint0);
        //Debug.Log("rotate joint1:" + rotateJoint1);

        Root.transform.rotation = Quaternion.Euler(0, 0, NewJoint0Angle);
        Joints[1].transform.rotation = Quaternion.Euler(0, 0, NewJoint1Angle);
        //Root.transform.Rotate(Vector3.back * rotateJoint0, Space.World);
        //Joints[1].transform.Rotate(Vector3.forward * -rotateJoint1, Space.World);

    }




    void ResolveIK2()
    {
        Transform Joint0 = Joints[0];
        Transform Joint1 = Joints[1];
        float length0 = BonesLength[0];
        float length1 = BonesLength[1];

        float jointAngle0;
        float jointAngle1;

        Vector3 diff = Target.position - Joint0.position;

        float length2;
        float atan;

        length2 = Vector3.Distance(Joint0.position, Target.position);

        //x-z
        Vector2 xz = new Vector2(diff.x, diff.z);

        //FIXME to enable fixed view rotation in 2D
        if (!fixedView)
        {
            var sign = Target.position.x < 0 ? -1 : 1;
        }

        // Angle from Joint0 and Target
        // FIXME <-- Put option to enable y-rotation
        if (!fixedView)
        {
            atan = Mathf.Atan2(diff.y, xz.magnitude) * Mathf.Rad2Deg;
        }
        else
        {
            atan = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        }
        //print($"{xz.magnitude}, {diff.x}, { xz.magnitude * sign}");
        

        // Is the target reachable?
        // If not, we stretch as far as possible
        if (length0 + length1 < length2)
        {
            jointAngle0 = atan - 90;
            jointAngle1 = 0f;
        }
        else
        {
            float cosAngle0 = ((length2 * length2) + (length0 * length0) - (length1 * length1)) / (2 * length2 * length0);
            float angle0 = Mathf.Acos(cosAngle0) * Mathf.Rad2Deg;

            float cosAngle1 = ((length1 * length1) + (length0 * length0) - (length2 * length2)) / (2 * length1 * length0);
            float angle1 = Mathf.Acos(cosAngle1) * Mathf.Rad2Deg;

            jointAngle0 = atan + angle0 - 90.0f;
            jointAngle1 = 180f - angle1;
        }

        // rotate about y-axis
        var dy = Target.position.z - Joint0.position.z;
        var dx = Target.position.x - Joint0.position.x;
        var yRot = -Mathf.Atan2(dy, dx);

        Vector3 Euler0 = Joint0.transform.localEulerAngles;
        Euler0.z = jointAngle0;

        // FIXME <-- Put option to enable y-rotation
        if (!fixedView)
        {
            Euler0.y = Mathf.Rad2Deg * yRot;
            Joint0.transform.localEulerAngles = Euler0;
        }

        Vector3 Euler1 = Joint1.transform.localEulerAngles;
        Euler1.z = -jointAngle1;
        //Euler1.y = Mathf.Rad2Deg * yRot;
        Joint1.transform.localEulerAngles = Euler1;
    }
}
