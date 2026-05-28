using UnityEngine;

[CreateAssetMenu(fileName = "Evidence", menuName = "Scriptable Objects/Evidence")]
public class Evidence : ScriptableObject
{
    public int evidenceIndex;
    public string evidenceName;
    public Sprite evidenceSprite;
    public bool active;
}
