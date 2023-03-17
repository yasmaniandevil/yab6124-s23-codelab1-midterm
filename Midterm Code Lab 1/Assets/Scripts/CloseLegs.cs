using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CloseLegs : MonoBehaviour
{
    //variable for the animator
    private Animator _animator;

    private bool _triggered = false;

    private bool _scoreIncrement = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //grab the animator component off this object set it to var i created
        _animator = GetComponent<Animator>();

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("triggered entered");
            //triggered is set to true when the player collides with it
            _triggered = true;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //only update the score if the area has been triggered
        //if the user is pressing z
        if (Input.GetKey(KeyCode.Z) && _triggered && !_scoreIncrement)
        {
            //then the bool i created is set to true and it runs the animation
            _animator.SetBool("LegsClosed", true);
            GameManager.Instance.Score++;
            //set to true once score goes up
            //score only goes up when both conditions are true
            _scoreIncrement = true;

        }
    }

    void Function()
    {
        
    }
    
    //if you hit the trigger, and press z then the score will update

    
}
