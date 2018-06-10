using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : MonoBehaviour {

    public float forwardSpeed = 5f;
    public float lateralSpeed = 5f;

    // Use this for initialization
    void Start () {
        if(GameObject.Find("GameManager") != null)
            forwardSpeed = GameObject.Find("GameManager").GetComponent<PopMaisons>().motoSpeed;		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * lateralSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.right* Time.deltaTime * lateralSpeed);
        }

        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
    }
}
