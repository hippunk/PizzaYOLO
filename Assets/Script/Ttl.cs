using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ttl : MonoBehaviour {

    public float pizzaTTL = 1.0f;

    // Use this for initialization
    void Start () {
        StartCoroutine(TtlPizza());
    }

    IEnumerator TtlPizza()
    {
        yield return new WaitForSeconds(pizzaTTL);
        Destroy(this.gameObject);
    }
}
