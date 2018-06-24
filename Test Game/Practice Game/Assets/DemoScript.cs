using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    [SerializeField]
    public Light myLight;

    void Update ()
    {
        if (Input.GetKeyDown("space"))
        {
            myLight.enabled = !myLight.enabled;
        }
    }

} // 21:14 https://unity3d.com/learn/tutorials/topics/scripting/coding-unity-absolute-beginner