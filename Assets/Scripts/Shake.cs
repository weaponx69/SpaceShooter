using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camAnimation;

    // Call this function to make the 
    // camera shake.
    public void CamShake()
    {
        camAnimation.SetTrigger("shake");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
