﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject.Find("GameManager").GetComponent<PopMaisons>().RandomizeGame();
    }
}
