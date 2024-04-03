using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RodCollider : MonoBehaviour
{
    public static RodCollider _instance;
    [NonSerialized]
    public static bool isColliding;

    [NonSerialized] 
    public static string collidingTag;

    private void Awake()
    {
        _instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
        collidingTag = other.tag;
    }
    
    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
        collidingTag = null;
    }
}
