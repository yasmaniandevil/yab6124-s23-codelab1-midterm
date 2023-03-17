using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    //how fast platform moves
    public float speed = 2;

    //stores inital position pf obj when game starts
    private Vector3 initalPos;

    //reference to transform componenet
    private Transform cachedTransform;

    //which point it resets
    public int resetPos;
    // Start is called before the first frame update
    void Start()
    {
        //set to the transform componenet
        cachedTransform = transform;
        //set to platform initial position
        initalPos = cachedTransform.position;
        //Debug.Log("initalposition: " + initalPos);
    }

    // Update is called once per frame
    void Update()
    {
        //newPos var stores current position of platform
        Vector3 newPos = transform.position;
        //Debug.Log("newpos current: " + transform.position);

        //moves it along x, increases by speed
        newPos.z += speed * Time.deltaTime;

        //moves platform
        transform.position = newPos;
        //Debug.Log("moving platform: " + newPos);

        //if platform x position is greater or equal to resetPos
        if (cachedTransform.position.z >= resetPos)
        {
            cachedTransform.position = initalPos;
        }
    }
}
