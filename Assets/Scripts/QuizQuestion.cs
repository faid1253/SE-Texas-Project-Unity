using UnityEngine;

[System.Serializable]
public class QuizQuestion
{
    public string question;
    public string[] options; // 3 answers
    public int correctIndex; // 0, 1, or 2
}