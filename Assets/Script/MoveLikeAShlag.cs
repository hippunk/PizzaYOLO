using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLikeAShlag : MonoBehaviour {

    public float moveDistance = 20f;
    public float speed;
    public GameObject canvas;
    public GameObject main;

    // Update is called once per frame
    void Start () {
        StartCoroutine(MoveThis());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            canvas.SetActive(false);
            main.SetActive(true);
        }
    }

    IEnumerator MoveThis()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        Vector3 currentPosition = rectTransform.localPosition;
        float curDistance = 0;
        while(curDistance < moveDistance)
        {
            curDistance += Time.deltaTime * speed;
            rectTransform.localPosition = new Vector3(currentPosition.x, currentPosition.y + curDistance, currentPosition.z);
            yield return null;
        }
        curDistance = 0;
        currentPosition = rectTransform.localPosition;
        while (curDistance < moveDistance)
        {
            curDistance += Time.deltaTime * speed;
            rectTransform.localPosition = new Vector3(currentPosition.x, currentPosition.y - curDistance, currentPosition.z);
            yield return null;
        }

        StartCoroutine(MoveThis());
    }
}
