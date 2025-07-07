using UnityEngine;

[System.Serializable]
public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers = new string[4]; // A, B, C, D
    public int correctIndex;
    public Difficulty level;
}
