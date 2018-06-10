using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShittyDrop : MonoBehaviour {

    public float force = 5.0f;
    public float forceTorque = 5.0f;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(Vector3.right * force, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(Vector3.back, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(transform.up * force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
