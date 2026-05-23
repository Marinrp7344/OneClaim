using UnityEngine;

[CreateAssetMenu(fileName = "Defendant", menuName = "Scriptable Objects/Defendant")]
public class Defendant : ScriptableObject
{
    public enum Gender { NonBinary, Male, Female }
    public enum LyingScale { VeryLow, Low, Neutral, High, VeryHigh }
    public Gender gender;
    public string defendantName;
    public Sprite sprite;
    public LyingScale lyingScale;
}
