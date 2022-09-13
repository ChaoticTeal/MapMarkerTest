using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        // Rotate the world space marker to face the player
        transform.LookAt(target);
    }
}
