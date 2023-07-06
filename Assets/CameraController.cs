using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(32.0f,0.0f,32.0f);
    Vector3 currSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float slowdownTimeScale = 2.0f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            float speedChange = currSpeed.z*.125f + moveSpeed.z*.875f;
            currSpeed.z = currSpeed.z*(1.0f-Time.deltaTime) + speedChange*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S)){
            float speedChange = currSpeed.z*.125f - moveSpeed.z*.875f;
            currSpeed.z = currSpeed.z*(1.0f-Time.deltaTime) + speedChange*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D)){
            float speedChange = currSpeed.x*.125f + moveSpeed.x*.875f;
            currSpeed.x = currSpeed.x*(1.0f-Time.deltaTime) + speedChange*Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A)){
            float speedChange = currSpeed.x*.125f - moveSpeed.x*.875f;
            currSpeed.x = currSpeed.x*(1.0f-Time.deltaTime) + speedChange*Time.deltaTime;
        }
        //Slowdown
        currSpeed = currSpeed*(1.0f-Time.deltaTime*slowdownTimeScale);

        transform.position += currSpeed*Time.deltaTime;
    }
}
