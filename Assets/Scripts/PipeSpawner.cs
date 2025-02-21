using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PipeSpawner : MonoBehaviour
{
    public Canvas gameOverScreen;
    public Canvas mainMenuScreen;
    public GameObject pipe;
    public float spawnTimer = 5f;
    private float time = 0f;
    public bool isGameOver = false;
    public GameObject bird;
    public TMP_Text overlayText;
    private int score;
    public TMP_Text currScore;
    public TMP_Text bestScore;
    void Start()
    {
        Time.timeScale = 0f;
    }

    public void playButton()
    {
        mainMenuScreen.gameObject.SetActive(false);
        bird.SetActive(true);
        Time.timeScale = 1f;
        spawnPipe(0f);
        overlayText.gameObject.SetActive(true);
    }

    public void quitButton()
    {
        Application.Quit();
    }

    void FixedUpdate()
    {
        if(!isGameOver)
        {
            if(time >= spawnTimer)
            {
                spawnPipe(Random.Range(-2.2f,4.75f));
                time = 0;
            }
            time++;
        }
    }

    void spawnPipe(float position)
    {
        GameObject newPipe = Instantiate(pipe, new Vector3(11f, position, 0), Quaternion.identity);
        newPipe.transform.parent = this.transform;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void gameOver()
    {
        isGameOver = true;
        gameOverScreen.gameObject.SetActive(true);
        overlayText.gameObject.SetActive(false);
        
        score = int.Parse(overlayText.text);
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            savedHighScore = score;
        }

        currScore.text += score;
        bestScore.text += savedHighScore;
    }

}
