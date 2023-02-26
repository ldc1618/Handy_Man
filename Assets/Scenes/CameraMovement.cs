using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform targ;
    public float smoother;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != targ.position) {
            Vector3 targPos = new Vector3(targ.position.x, targ.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targPos, smoother);
        }
    }
}
