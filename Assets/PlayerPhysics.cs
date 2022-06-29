using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    public Transform pivot;
    Vector3 currentEulerAngles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 screen_mouse_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.y);
        //Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(screen_mouse_pos);

        //Vector3 diff = mouse_pos - transform.position;

        //transform.rotation = Quaternion.LookRotation(diff, Vector3.up);
        Vector3 WorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 Difference = WorldPoint - transform.position;
        Difference.Normalize();

        float RotationZ = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, RotationZ - 90);
    }
}
