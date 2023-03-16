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
        if (Input.GetKey(KeyCode.Z) && _triggered)
        {
            //then the bool i created is set to true and it runs the animation
            _animator.SetBool("LegsClosed", true);

            GameManager.Instance.Score++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("triggered entered");
            _triggered = true;
        }
    }
}
