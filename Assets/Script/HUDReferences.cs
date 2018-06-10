using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDReferences : MonoBehaviour {

    public Sprite pizzaLaunch;
    public Sprite pizzaOK;
    public Sprite pizzaNOK;

    public Image[] pizzas;
    public Text score;

    public void UpdateScore(int score)
    {
        this.score.text = score.ToString();
    }

    public void PizzaLaunch(int index)
    {
        pizzas[index].sprite = pizzaLaunch;
    }

    public void PizzaOK(int index)
    {
        pizzas[index].sprite = pizzaOK;
    }

    public void PizzaNOK(int index)
    {
        pizzas[index].sprite = pizzaNOK;
    }
}
