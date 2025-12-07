using UnityEngine;

[CreateAssetMenu(fileName = "QuestionDatabase", menuName = "Quiz/Database")]
public class QuestionDatabase : ScriptableObject
{
    public Question[] questions;
}