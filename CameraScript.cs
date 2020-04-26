using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform target; //Target the camera should follow
    [SerializeField]
    private Vector3 offset; //the distance between target and camera
    [SerializeField]
    private float cameraMoveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        MoveCamera();
    }
    
    void MoveCamera()
    {
        var distanceBehind = -6;
        var yRot = target.rotation.eulerAngles.y;
        var posX = target.position.x + (distanceBehind  * (float)Math.Sin((Math.PI/180) * yRot));
        var posZ = target.position.z + (distanceBehind * (float)Math.Cos((Math.PI/180) * yRot));
        Vector3 endPos = new Vector3(posX, offset.y, posZ);
        
        transform.position = endPos;
        transform.LookAt(target);
    }
}
