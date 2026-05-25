using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Case", menuName = "Scriptable Objects/Case")]
public class Case : ScriptableObject
{
    public string charges;
    public List<Claim> claims;
    public List<Evidence> evidence;
    public List<PlayerChoices> playerChoices;
    public List<Verdict> verdicts;

    public Verdict DetermineVerdict(PlayerChoices playerChoice, Evidence evidence)
    {
        Verdict result = verdicts.FirstOrDefault(v => v.playerChoiceIndex == playerChoice.index && v.evidenceIndex == evidence.evidenceIndex);
        return result;
    }
}


[System.Serializable]
public class Claim
{
    public AdmissionClaims admitClaim;
    public AdmissionClaims denyClaim;
    public AdmissionClaims displayedClaim;
    public AdmissionClaims trueClaim;
    public bool lying;
    public Evidence associatedEvidence;
}

[System.Serializable]
public class AdmissionClaims
{
    public enum ClaimType { None, Admit, Deny }
    public ClaimType admittance;
    public bool chosen;
    public string claimDescription;

}

[System.Serializable]
public class PlayerChoices
{
    public int index;
    public string description;
}

[System.Serializable] 
public class Verdict
{
    public int playerChoiceIndex;
    public int evidenceIndex;
    public int minimumSentence;
    public int maximumSentence;
    public enum VerdictType { None, Guilty, Innocent };
    public VerdictType verdictType;

}


