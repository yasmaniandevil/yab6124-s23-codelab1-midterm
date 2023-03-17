using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAndPlatformMove : MonoBehaviour
{
    private Animator _movePlatform = null;

    private bool startMovement = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _movePlatform = GetComponent<Animator>();
        //_animator.SetBool("PlatformMover", false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (startMovement)
            {
                _movePlatform.Play("MovingPlatformAnimation 1");
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
