using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_Rotate : MonoBehaviour
{
    public float rotateSpeed;

    [Tooltip("Set to this rotate X, Y or Z")]
    public bool[]  rotationAxis;

    private void Update()
    {
        transform.Rotate(RotateObject(), Space.Self);
    }

    private Vector3 RotateObject()
    {   
        float[] rotations = new float[3];

        for(int axis = 0; axis < rotationAxis.Length; axis++)
        {
            if(rotationAxis[axis])
            {
                rotations[axis] = rotateSpeed * Time.deltaTime;
            }
            else
            {
                rotations[axis] = 0f;
            }
        }
        
        return new Vector3(rotations[0], rotations[1], rotations[2]);
    }
}