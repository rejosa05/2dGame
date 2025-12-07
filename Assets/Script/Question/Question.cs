[System.Serializable]
public class Question
{
    public string prompt;
    public string[] options;
    public int answer; // index (0,1,2,3)
}