using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBehavior : MonoBehaviour {

    public float speed = 2.0f;
    public float pizzaTTL = 3.0f;
    public int pizzaIndex = -1;
    public GameObject explosion;

    PopMaisons pop;
    HUDReferences hud;

    // Use this for initialization
    void Start () {
        pop = GameObject.Find("GameManager").GetComponent<PopMaisons>();
        hud = GameObject.Find("HUD").GetComponent<HUDReferences>();
        StartCoroutine(TtlPizza());
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(Vector3.back*Time.deltaTime*speed);
        GetComponent<Rigidbody>().AddForce(transform.forward*-1 * speed, ForceMode.Impulse);
    }

    IEnumerator TtlPizza()
    {
       yield return new WaitForSeconds(pizzaTTL);
        hud.PizzaNOK(pizzaIndex);
       Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collide");
        if (collision.gameObject.GetComponent<BatimentController>())
        {
            BatimentController bc = collision.gameObject.GetComponent<BatimentController>();
            if (bc.hasDelivery && !bc.hasRecived)
            {

                bc.hasRecived = true;
                transform.Translate(Vector3.up * 0.1f);
                transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                StopAllCoroutines();
                GetComponent<Rigidbody>().isKinematic = true;
                hud.PizzaOK(pizzaIndex);
                pop.data.score += 1;
                pop.data.nbOK += 1;
                pop.totalPizzaOk += 1;
                hud.UpdateScore(pop.data.score);
                Destroy(this);
                //flip & colle
            }
            else
            {
                //Expolsion
                Instantiate(explosion, transform.position, Quaternion.identity);
                hud.PizzaNOK(pizzaIndex);
                Destroy(gameObject);
            }
        }
    }
}
