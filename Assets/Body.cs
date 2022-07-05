using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public Transform body, hammerHead;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), hammerHead.GetComponent<BoxCollider2D>());   
    }

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    Vector3 offset = new Vector3(.5f, .5f, 0);
    //    GetComponent<Rigidbody2D>().MovePosition(pivot.gameObject.transform.position);
    //    //transform.position = pivot.gameObject.transform.position;
    //}

    void FixedUpdate()
    {
        //Vector3 mouseVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mouseVec.z = 0;

        // Screen center and mouse position in screen space
        float depth = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 center =
            new Vector3(Screen.width / 2, Screen.height / 2, depth);
        Vector3 mouse =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth);

        // Transform to world space
        center = Camera.main.ScreenToWorldPoint(center);
        mouse = Camera.main.ScreenToWorldPoint(mouse);

        // Compute mouseVec for hammer control
        Vector3 mouseVec = Vector3.ClampMagnitude(mouse - center, range);

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useLayerMask = true;
        contactFilter.layerMask = LayerMask.GetMask("Ground");
        Collider2D[] results = new Collider2D[5];
        if (hammerHead.GetComponent<Rigidbody2D>().OverlapCollider(
                contactFilter, results) > 0)  // If collided with scene objects
        {
            Vector3 targetBodyPos = hammerHead.position - mouseVec;

            Vector3 force = (targetBodyPos - body.position) * 80.0f;
            transform.GetComponent<Rigidbody2D>().AddForce(force);
            Debug.Log("Forced");

           transform.GetComponent<Rigidbody2D>().velocity =
                    Vector2.zero;
        }
        
        Vector3 newHammerPos = body.position + mouseVec;
        Vector3 hammerMoveVec = newHammerPos - hammerHead.position;
        newHammerPos = body.position + hammerMoveVec * 1.2f;

        hammerHead.GetComponent<Rigidbody2D>().MovePosition(newHammerPos);

        hammerHead.rotation = Quaternion.FromToRotation(
            Vector3.right, newHammerPos - body.position);
    }

}
