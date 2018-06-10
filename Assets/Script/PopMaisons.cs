using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopMaisons : MonoBehaviour
{
    public int totalPizzaOk = 0;
    public class GameData
    {
        public int nbPizza = 5;
        public int curPizza = -1;
        public int score = 0;
        public int nbOK = 0;
    }

    public GameData data = new GameData();

    public GameObject maisonLivre;
    public GameObject maisonPasLivre;
    public GameObject houseSpawn;
    public CanvasGroup canvas;
    public List<List<int>> memoryHouseSet = new List<List<int>>();
    public List<int> houseSet;
    public int nbMonstres = 5;
    public float motoSpeed = 2.0f;
    public float distance = 0.75f;
    public int nbHouse = 20;
    public int nbHouseToLivre = 12;
    public float timeBetweenSpawn = 0.5f;

    public bool firstime = true;

    // Use this for initialization
    void Start()
    {
        List<int> houseSet = RandomizeHouseSet();
        StartCoroutine(Pop(houseSet));
        DontDestroyOnLoad(this);
    }

    IEnumerator Pop(List<int> houseSet)
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        for (int i = 0; i < nbHouse; i++)
        {
            if (houseSet.Contains(i))
            {
                GameObject newHouse = Instantiate(maisonLivre, houseSpawn.transform);
                Vector3 position = newHouse.transform.position;
                newHouse.transform.position = new Vector3(position.x + distance * i, position.y, position.z);
            }
            else
            {
                GameObject newHouse = Instantiate(maisonPasLivre, houseSpawn.transform);
                Vector3 position = newHouse.transform.position;
                newHouse.transform.position = new Vector3(position.x + distance * i, position.y, position.z);
            }

            yield return new WaitForSeconds(timeBetweenSpawn);
        }
        StartCoroutine(FadeCanvasGroupAlpha(0f, 1f, canvas));
    }

    public List<int> RandomizeHouseSet()
    {
        houseSet = new List<int>();

        for (int i = 0; i < nbHouse; i++)
        {
            houseSet.Add(i);
        }

        for (int i = 0; i < nbHouse - nbHouseToLivre; i++)
        {
            int rand = Random.Range(0, houseSet.Count);
            houseSet.RemoveAt(rand);
        }


        foreach (int i in houseSet)
        {
            Debug.Log(i);
        }

        return houseSet;
    }

    public void RandomizeGame()
    {
        StartCoroutine(FadeCanvasGroupAlpha(0f, 1f, canvas, 2, true));
    }

    public IEnumerator FadeCanvasGroupAlpha(float startAlpha, float endAlpha, CanvasGroup canvasGroupToFadeAlpha, int scene = 1, bool propre = false)
    {

        float elapsedTime = 0f;
        float totalDuration = 0.4f;
        //Debug.Log("fade in");
        while (elapsedTime < totalDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / totalDuration);
            canvasGroupToFadeAlpha.alpha = currentAlpha;
            yield return null;
        }
        foreach (Transform transform in houseSpawn.transform)
        {
            Destroy(transform.gameObject);
        }
        //Debug.Log("fin fade in");
        SceneManager.LoadScene(scene);

        //Debug.Log("fade out");
        //Fix à l'arache pour défade le putain de fadeOutImage pour changement de scène
        elapsedTime = 0f;
        while (elapsedTime < totalDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(endAlpha, startAlpha, elapsedTime / totalDuration);
            //Debug.Log(currentAlpha);
            canvasGroupToFadeAlpha.alpha = currentAlpha;
            yield return null;
        }

        if (propre)
        {//Rechargement de partie

            int oldScore = data.score;
            oldScore  = oldScore - (4 - data.nbOK);
            data.score = oldScore;
            if (data.nbOK == 0 || oldScore < 0)
            {
                Debug.Log("perdu !");
                GameObject.Find("DADOOM").GetComponent<DadoomReferences>().loose.SetActive(true);
                GameObject.Find("DADOOM").GetComponent<DadoomReferences>().dunnoscore.text = data.score.ToString();
                GameObject.Find("DADOOM").GetComponent<DadoomReferences>().nbruns.text = (memoryHouseSet.Count+1).ToString();
                GameObject.Find("DADOOM").GetComponent<DadoomReferences>().pizzas.text = totalPizzaOk.ToString();

                StartCoroutine(WaitForInput());
            }
            else
            {

                data = new GameData();

                data.score = oldScore;

                //Ici ajouter comportement sacamerde

                memoryHouseSet.Add(houseSet);

                if (memoryHouseSet.Count >= 3 && Random.Range(0,10) < 4)
                {
                    StartCoroutine(YOLO());

                }
                else
                {

                    houseSet = RandomizeHouseSet();

                    motoSpeed = Mathf.Min(3f, motoSpeed + 0.2f);
                    timeBetweenSpawn = Mathf.Max(0.2f, timeBetweenSpawn - 0.05f);
                    StartCoroutine(Pop(houseSet));
                }
            }
        }
        else
        {
            GetComponent<AudioSource>().enabled = true;
        }
    }

    public IEnumerator YOLO()
    {
        if (firstime)
        {
            firstime = false;
            GameObject.Find("DADOOM").GetComponent<DadoomReferences>().transition.SetActive(true);
            yield return new WaitForSeconds(2f);

        }

        int randOrder = Random.Range(0, memoryHouseSet.Count);
        GameObject.Find("DADOOM").GetComponent<DadoomReferences>().orderText.text = (randOrder+1).ToString();
        houseSet = memoryHouseSet[randOrder];
        
        GameObject.Find("DADOOM").GetComponent<DadoomReferences>().challenge.SetActive(true);
        yield return new WaitForSeconds(1f);
        motoSpeed = Mathf.Min(3f, motoSpeed + 0.2f);
        timeBetweenSpawn = Mathf.Max(0.2f, timeBetweenSpawn - 0.05f);
        StartCoroutine(FadeCanvasGroupAlpha(0f, 1f, canvas));
    }

    public IEnumerator WaitForInput()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(PendDestroy());
                SceneManager.LoadScene(0);
            }

            yield return null;
        }
    }
    public IEnumerator PendDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
