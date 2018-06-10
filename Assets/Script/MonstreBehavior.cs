using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstreBehavior : MonoBehaviour {

    public bool hasCollided = false;
    public GameObject ShittyPizzaPrefab;

    PopMaisons pop;
    HUDReferences hud;
    // Use this for initialization
    void Start () {
        pop = GameObject.Find("GameManager").GetComponent<PopMaisons>();
        hud = GameObject.Find("HUD").GetComponent<HUDReferences>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (hasCollided == false)
        {
            hasCollided = true;
            int pizzaIndex = ++pop.data.curPizza;
            if (pizzaIndex < pop.data.nbPizza)
            {
                hud.PizzaNOK(pizzaIndex);
                Instantiate(ShittyPizzaPrefab, collision.gameObject.transform.position + Vector3.right * 0.2f + Vector3.up * 0.3f, Quaternion.identity);
            }
        }
    }
}
