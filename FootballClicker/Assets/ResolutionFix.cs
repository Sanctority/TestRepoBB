using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionFix : MonoBehaviour {

    private float orthographicSize = 5;

    private float aspect;

    void Start()
    {

        float screenHeightInUnits = Camera.main.orthographicSize * 2;
        float screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height; // basically height * screen aspect ratio;

        aspect = screenWidthInUnits / screenHeightInUnits - 0.3f;

        Debug.Log(aspect);

        Camera.main.projectionMatrix = Matrix4x4.Ortho(
                -orthographicSize * aspect, orthographicSize * aspect,
                -orthographicSize, orthographicSize,
               Camera.main.nearClipPlane, Camera.main.farClipPlane);
    }
}
