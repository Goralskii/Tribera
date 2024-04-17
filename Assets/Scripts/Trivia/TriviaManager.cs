using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
public class TriviaManager : MonoBehaviour
{
    public TMP_Text questionText;    
    public Button[] answerButtons; // Suponiendo que tienes 4 botones para las respuestas    
    private List<Question> questions = new List<Question>();
    private List<int> questionIndexes = new List<int>(); // Índices de las preguntas que se han mostrado
    private int currentQuestionIndex = -1; // Índice de la pregunta actual
    private int totalQuestions = 0;
    private List<string> usedQuestions = new List<string>();    
    [SerializeField] private AnsManager m_gameManager = null; // Referencia al GameManager
    [Serializable]
    public class Question
    {
        public string question;
        public string[] answers;
        public int correctAnswerIndex;
        public string catName;
    }
    private void Update()
    {
        if(usedQuestions.Count == totalQuestions)
        {
            usedQuestions.Clear();
        }
    }
    void Start()
    {
        m_gameManager = FindObjectOfType<AnsManager>();
    }
    public void StartTrivia(string selectedCategories)
    {
        questions.Clear();
        questions = new List<Question>();        
        questionIndexes.Clear();
        questionIndexes = new List<int>();
        currentQuestionIndex = -1;
        totalQuestions = 0;
        LoadQuestionsFromFile("questions.txt", selectedCategories);
        ShuffleQuestions();
        ShowNextQuestion();
    }
    void LoadQuestionsFromFile(string fileName, string selectedCategories)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                totalQuestions++;
                string[] parts = line.Split(';');
                if (parts.Length == 7)
                {
                    string category = parts[0];
                    if (selectedCategories == category)
                    {
                        Question question = new Question();
                        question.question = parts[1];
                        question.answers = new string[4];
                        Array.Copy(parts, 2, question.answers, 0, 4);
                        question.correctAnswerIndex = int.Parse(parts[6]);
                        question.catName = category;
                        questions.Add(question);
                    }
                }
                else
                {
                }
            }
        }
        else
        {
            Debug.LogError("Questions file not found: " + filePath);
        }
    }
    void ShuffleQuestions()
    {
        // Genera una lista de índices de preguntas mezclada
        questionIndexes.Clear();
        for (int i = 0; i < questions.Count; i++)
        {
            questionIndexes.Add(i);
        }
        questionIndexes.Shuffle();
    }
    public void ShowNextQuestion()
    {
        foreach (Button button in answerButtons)
        {
            button.GetComponent<Image>().color = Color.white; // Restablecer a color blanco o el color original
        }
        if (AllQuestions())
        {
            Debug.Log("Todas las respuestas respondidas");
            m_gameManager.ShowWinnerScreen(true);
            StartCoroutine(m_gameManager.WaitAndEnd(5f));
            return;
        }
        else
        {
            if (questionIndexes.Count > 0)
            {
                int questionIndex = questionIndexes[currentQuestionIndex];
                while (usedQuestions.Contains(questions[questionIndex].question))
                {
                    currentQuestionIndex++;
                    questionIndex++;
                }
                Question currentQuestion = questions[questionIndex];
                questionText.text = currentQuestion.question;
                usedQuestions.Add(currentQuestion.question);
                // Mezclar el orden de las respuestas
                List<int> answerIndexes = new List<int>() { 0, 1, 2, 3 };
                answerIndexes.Shuffle();
                for (int i = 0; i < answerButtons.Length; i++)
                {
                    int answerIndex = answerIndexes[i];
                    answerButtons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[answerIndex];
                    answerButtons[i].onClick.RemoveAllListeners();
                    int buttonIndex = i;
                    answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerButtons[buttonIndex], answerIndex, currentQuestion.correctAnswerIndex));
                }
            }
            else
            {
                Debug.LogError("No questions available");
            }
        }
        
    }
    void OnAnswerSelected(Button selectedButton, int answerIndex, int correctAnswerIndex)
    {
        if (answerIndex == correctAnswerIndex)
        {
            StartCoroutine(m_gameManager.GiveAnswerRoutine(selectedButton, true));
        }
        else
        {   
            StartCoroutine(m_gameManager.GiveAnswerRoutine(selectedButton, false));
        }
    }
    public void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public bool AllQuestions()
    {
        currentQuestionIndex++;
        return false;
    }
}
// Método de extensión para mezclar una lista genérica
public static class ListExtensions
{
    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
