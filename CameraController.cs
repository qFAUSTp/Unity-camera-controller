using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private float panSpeed = 5f;
    private float scrollScale = 1f;
    private float sensitivity = 3f;

    // Use this for initialization
    void Start () {
    	
    }

    // Update is called once per frame
    void Update() {

        Vector3 pos = new Vector3(0,0,0);
        Quaternion rot = transform.rotation;


        if (Input.GetMouseButton(0))
        {
            Debug.Log("Pressed left click.");
            //While empty
        }

        if (Input.GetMouseButton(1))
        {
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            rot = Quaternion.Euler(rot.eulerAngles.x - mouseY, rot.eulerAngles.y + mouseX, rot.eulerAngles.z);

            //Debug.Log("x:" + rot.eulerAngles.x + ";  y:" + rot.eulerAngles.y + ";  z:" + rot.eulerAngles.z);
        }

        if (Input.GetKey("w"))
        {
            pos.z = 0 + panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("s"))
        {
            pos.z = 0 - panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            pos.x = 0 - panSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("d"))
        {
            pos.x = 0 + panSpeed * Time.deltaTime;
        }

        pos.y = 0 - Input.mouseScrollDelta.y * scrollScale;

        pos = BasisRotate(pos, rot);

        transform.position += pos; 
        transform.rotation = rot;
        
    }

    private Vector3 BasisRotate(Vector3 posIn, Quaternion rot)
    {
        Vector3 posOut = new Vector3(0, 0, 0);

        posOut.x = posIn.z * Mathf.Sin(rot.eulerAngles.y * Mathf.PI / 180)
                 + posIn.x * Mathf.Cos(rot.eulerAngles.y * Mathf.PI / 180);

        posOut.y = posIn.y;

        posOut.z = posIn.z * Mathf.Cos(rot.eulerAngles.y * Mathf.PI / 180)
                 - posIn.x * Mathf.Sin(rot.eulerAngles.y * Mathf.PI / 180);
  
        return posOut;
    }

}
