using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    Transform lookAt;
    [SerializeField]
    Vector3 offset;
    [SerializeField]
    Camera cam;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);
        if(transform.position != pos){
            transform.position = pos;
        }
    }
}
