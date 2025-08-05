using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; // Required for IEnumerator

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel;
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public QuizQuestion[] questions;
    private int currentQuestion = 0;
    private int score = 0;

    void Start()
    {
        quizPanel.SetActive(false);
        if (resultPanel != null) resultPanel.SetActive(false);
    }

    public void StartQuiz()
{
    currentQuestion = 0;
    score = 0;

    if (InfoPanelManager.Instance != null && InfoPanelManager.Instance.infoPanelGameObject.activeSelf)
    {
        StartCoroutine(WaitForInfoPanelThenShowQuiz());
    }
    else
    {
        ShowQuizPanel();
    }
}

IEnumerator WaitForInfoPanelThenShowQuiz()
{
    while (InfoPanelManager.Instance.infoPanelGameObject.activeSelf)
    {
        yield return null;
    }
    ShowQuizPanel();
}

void ShowQuizPanel()
{
    quizPanel.SetActive(true);
    if (resultPanel != null) resultPanel.SetActive(false);
    ShowQuestion();
}

    void ShowQuestion()
    {
        QuizQuestion q = questions[currentQuestion];
        questionText.text = q.question;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.options[i];

            int index = i;
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    void CheckAnswer(int index)
    {
        if (index == questions[currentQuestion].correctIndex)
        {
            score++;
        }

        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    void EndQuiz()
    {
        quizPanel.SetActive(false);

        // If InfoPanel is still open, wait for it to close
        if (InfoPanelManager.Instance != null && InfoPanelManager.Instance.infoPanelGameObject.activeSelf)
        {
            StartCoroutine(WaitForInfoPanelClose());
        }
        else
        {
            ShowResults();
        }

        string currentScene = SceneManager.GetActiveScene().name;
        GameManager.Instance.MarkVisited(currentScene);
    }

    IEnumerator WaitForInfoPanelClose()
    {
        while (InfoPanelManager.Instance.infoPanelGameObject.activeSelf)
        {
            yield return null;
        }

        ShowResults();
    }

    void ShowResults()
    {
        if (resultPanel != null && resultText != null)
        {
            resultPanel.SetActive(true);
            resultText.text = $"You got {score}/{questions.Length} correct!";
        }
    }
}