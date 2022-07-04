using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToPivot : MonoBehaviour
{
    public GameObject pivot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = pivot.gameObject.transform.position;
    }
}
