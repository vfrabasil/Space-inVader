using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour
{

    public GameObject life1, life2, life3;
    public GameObject player;
    public GameObject levelCanvas;
    public int enemyCount;

    public GameObject enemy1;
    public GameObject enemy2;    
    public GameObject enemy3;
    public GameObject enemy4;

    private int health;

    static int episode = 1; // episodo = level
    static int score;


    // Start is called before the first frame update
    void Start()
    {
        life1.gameObject.SetActive(true);
        life2.gameObject.SetActive(true);
        life3.gameObject.SetActive(true);



        //Debug.Log(enemyCount);

        setTextUIScore();
        setTextUIHiScore();

        generateEnemies((getEpisode()%6)+1);
        enemyCount =  GameObject.FindGameObjectsWithTag("Alien").Length;  // 16;
    }


    private void fila1(GameObject e) {
        Instantiate(e, new Vector3(-25f, 4.95f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(-18f, 4.95f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(-11f, 4.95f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3( -4f, 4.95f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(  3f, 4.95f, 0f), Quaternion.identity);
    }

    private void fila2(GameObject e) {
        Instantiate(e, new Vector3(-25f, 12.6f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(-18f, 12.6f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(-11f, 12.6f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3( -4f, 12.6f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(  3f, 12.6f, 0f), Quaternion.identity);
    }

    private void fila3(GameObject e) {
        Instantiate(e, new Vector3(-25f, 20.36f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(-18f, 20.36f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(-11f, 20.36f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3( -4f, 20.36f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(  3f, 20.36f, 0f), Quaternion.identity);
    }
    private void fila4(GameObject e) {
        Instantiate(e, new Vector3(-25f, 28.05f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(-18f, 28.05f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(-11f, 28.05f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3( -4f, 28.05f, 0f), Quaternion.identity);
        Instantiate(e, new Vector3(  3f, 28.05f, 0f), Quaternion.identity);
    }
    private void jefe1(GameObject e) {
        Instantiate(e, new Vector3(-11f, 28.05f, 0f), Quaternion.identity);
    }

    private void generateEnemies(int episode) {

        switch (episode)
        {
            case 1:
                fila1(enemy1);
                fila2(enemy1);
                fila3(enemy1);
                jefe1(enemy2);
                break;
            case 2:
                fila1(enemy1);
                fila2(enemy1);
                fila3(enemy2);
                jefe1(enemy2);
                break;
            case 3:
                fila1(enemy1);
                fila2(enemy2);
                fila3(enemy2);
                jefe1(enemy3);
                break;
            case 4:
                fila1(enemy1);
                fila2(enemy2);
                fila3(enemy3);
                jefe1(enemy3);
                break;
            case 5:
                fila1(enemy1);
                fila2(enemy3);
                fila3(enemy3);
                jefe1(enemy4);
                break;
            case 6:
                fila1(enemy1);
                fila2(enemy2);
                fila3(enemy3);
                jefe1(enemy4);
                break;

            default:
                fila1(enemy1);
                break;

        }
    }


    private void setTextUIHiScore()
    {
        var textUIComp = GameObject.Find("HiScore").GetComponent<Text>();


        if (score > PlayerPrefs.GetInt("HiScore",0))
        {
            PlayerPrefs.SetInt("HiScore", score);
        }

        textUIComp.text = "HI:" + PlayerPrefs.GetInt("HiScore", 0).ToString("00000");

    }


    private void setTextUIScore()
    {
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        textUIComp.text = score.ToString("00000");
    }


    private void SetlivesLeft()
    {
        if (player != null)
        {
            health = player.GetComponent<Spaceship>().livesLeft();
        }
    }

    public void decreseEnemy()
    {
        if (enemyCount > 0)
            enemyCount--;
    }

    public void setScore()
    {
        score += 10;
    }

    public int getScore()
    {
        return score;
    }

    public void reset()
    {
        score = 0;
        episode = 1;
    }


    public void nextEpisode()
    {
        episode += 1;
    }

    public int getEpisode()
    {
        return episode;
    }


    // Update is called once per frame
    void Update()
    {
        SetlivesLeft();

        if (health > 3)
            health = 3;

        switch (health)
        {
            case 3:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(true);
                life3.gameObject.SetActive(true);
                break;

            case 2:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(true);
                life3.gameObject.SetActive(false);
                break;

            case 1:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                break;

            case 0:
                life1.gameObject.SetActive(false);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                break;

        }

        if (enemyCount <= 0)
        {
            levelCanvas.GetComponent<LeverChangerScript>().FadeOUT();
            // SceneManager.LoadScene(1)


            StartCoroutine(WaitForSceneLoad());
            


        }       
    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(1);
        nextEpisode();
        SceneManager.LoadScene(getEpisode()%6 +1);  // 0-5 +1
        // Debug.Log("----------------- episode:" + getEpisode() + "**"+ ((getEpisode()%6)+1) + "-----------------");
        // SceneManager.LoadScene(1);
        // ApplySceneChanges();

    }

   /*
    private void ApplySceneChanges()
    {
        GameObject[] ties = GameObject.FindGameObjectsWithTag("Alien");

        Debug.Log("list: " + ties.Length);
        foreach (GameObject tie in ties)
        {
            tie.GetComponent<Aliens>().speed += 5;

            Debug.Log(tie.GetComponent<Aliens>().speed);
        }

    }
    */
   

}
