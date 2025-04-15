using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public Button colorButton;
    public float gameTime = 30f;

    private int score = 0;
    private float timeLeft;
    private bool isGameOver = false;

    private float buttonTimer = 1f;
    private float currentButtonTime = 0f;
    void Start()
    {
        timeLeft = gameTime;
        UpdateScoreText();
        UpdateTimerText();
        SpawnButton();
        gameOverText.gameObject.SetActive(false);
    }
    void Update()
    {
        if (!isGameOver)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerText();
            currentButtonTime -= Time.deltaTime;
            if (currentButtonTime <= 0f)
            {
                SpawnButton();
            }
            if (timeLeft <= 0)
            {
                EndGame();
            }
        }
    }
    void UpdateScoreText()
    {
        scoreText.text = "Skor: " + score;
    }
    void UpdateTimerText()
    {
        timerText.text = "Süre: " + Mathf.Ceil(timeLeft).ToString();
    }
    void SpawnButton()
    {
        float xPos = Random.Range(-800f, 800f);
        float yPos = Random.Range(-400f, 400f);
        colorButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos,
       yPos);
        Color newColor;
        float redChance = 0.25f;
        if (Random.value<redChance)
        {
            newColor = Color.red;
        }
        else
        {
            newColor = new Color(Random.value, Random.value, Random.value);
        }
        colorButton.GetComponent<Image>().color = newColor;

        colorButton.onClick.RemoveAllListeners();
        colorButton.onClick.AddListener(() => ButtonClicked(newColor));

        currentButtonTime = buttonTimer;
    }
    void ButtonClicked(Color buttonColor)
    {
        if (!isGameOver)
        {
            
            if (buttonColor.r > 0.7f && buttonColor.g < 0.3f && buttonColor.b < 0.3f)
            {
                score += 10;
            }
            else
            {
                timeLeft -= 2f;
            }
            UpdateScoreText();
            SpawnButton();
        }
    }
    void EndGame()
    {
        isGameOver = true;
        gameOverText.text = "Oyun Bitti! Skor: " + score;
        gameOverText.gameObject.SetActive(true);
        colorButton.gameObject.SetActive(false);
    }
}
