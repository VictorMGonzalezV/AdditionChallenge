using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterLifetime : MonoBehaviour
{
    //This script can be attached to any game object that only needs to exist for a limited time, I'm using it for the particle system
    //Default 1 sec lifetime to prevent issues if someone forgets to set a lifetime
    [SerializeField]
    private float Lifetime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Lifetime);
    }


}
