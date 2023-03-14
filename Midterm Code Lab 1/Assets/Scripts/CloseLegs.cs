using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLegs : MonoBehaviour
{
    //variable for the animator
    private Animator _animator;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //grab the animator component off this object set it to var i created
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //if you press z
        if (Input.GetKey(KeyCode.Z))
        {
            //then the bool i created is set to true and it runs the animation
            _animator.SetBool("LegsClosed", true);
        }
    }
}
