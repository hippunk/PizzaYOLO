using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameGenerator : MonoBehaviour {

    public GameObject[] narmolHouse;
    public GameObject[] specialHouse;
    public GameObject[] genPoint;

    public GameObject[] monsterGenPoints;
    public GameObject[] monsterPool;

    // Use this for initialization
    void Start () {
        List<int> poolPoint = new List<int>();
        for(int i = 0; i < genPoint.Length; i++)
        {
            poolPoint.Add(i);
        }

        foreach(GameObject go in specialHouse)
        {
            int index = poolPoint.ElementAt(Random.Range(0,poolPoint.Count));
            Instantiate(go, genPoint[index].transform);
            poolPoint.Remove(index);
        }

        foreach (int i in poolPoint)
        {
            int index = Random.Range(0, narmolHouse.Length);
            Instantiate(narmolHouse[index],genPoint[i].transform);
        }

        List<int> houseSet = GameObject.Find("GameManager").GetComponent<PopMaisons>().houseSet;

        foreach(int i in houseSet)
        {
            genPoint[i].GetComponentInChildren<BatimentController>().hasDelivery = true;
        }

        int nbMonstres = GameObject.Find("GameManager").GetComponent<PopMaisons>().nbMonstres++;
        List<int> poolPointMonstres = new List<int>();
        for (int i = 0; i < monsterGenPoints.Length; i++)
        {
            poolPointMonstres.Add(i);
        }

        for(int i = 0; i < nbMonstres; i++)
        {
            int index = poolPointMonstres.ElementAt(Random.Range(0, poolPointMonstres.Count));
            int indexMonstre = Random.Range(0, monsterPool.Length);

            GameObject go = Instantiate(monsterPool[indexMonstre], monsterGenPoints[index].transform);
            go.transform.position = go.transform.position + Vector3.forward * Random.Range(-0.4f, 0.4f)+ Vector3.left* Random.Range(-0.4f, 0.4f);
            poolPointMonstres.Remove(index);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
