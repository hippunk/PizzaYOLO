using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaGun : MonoBehaviour {

    public GameObject pizza;

    PopMaisons pop;
    HUDReferences hud;

    // Use this for initialization
    void Start () {
        pop = GameObject.Find("GameManager").GetComponent<PopMaisons>();
        hud = GameObject.Find("HUD").GetComponent<HUDReferences>();
        hud.UpdateScore(pop.data.score);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && pop.data.curPizza+1<pop.data.nbPizza)
        {

            pop.data.curPizza++;
            hud.PizzaLaunch(pop.data.curPizza);

            GameObject pizzaHop = Instantiate(pizza);
            pizzaHop.GetComponent<PizzaBehavior>().pizzaIndex = pop.data.curPizza;
            pizzaHop.transform.position = transform.position;
        }
	}
}
