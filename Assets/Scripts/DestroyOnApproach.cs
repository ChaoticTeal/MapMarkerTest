using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnApproach : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Destroy this object when the player approaches
        if (other.tag == playerTag)
            Destroy(gameObject);
    }
}
