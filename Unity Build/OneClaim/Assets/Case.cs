using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Case", menuName = "Scriptable Objects/Case")]
public class Case : ScriptableObject
{
    public string charges;
    public List<Claim> claims;
    public List<Evidence> evidence;
    public List<PlayerChoices> playerChoices;

    public void DecideVerdict(PlayerChoices playerChoice, Evidence evidence)
    {

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

public class AdmissionClaims
{
    public enum ClaimType { None, Admit, Deny }
    public ClaimType admittance;
    public bool chosen;
    public string claimDescription;

}

public class PlayerChoices
{
    public int index;
    public string description;
}

public class Verdict
{
    public int playerChoiceIndex;
    public int evidenceIndex;
    public int minimumSentence;
    public int maximumSentence;
    public string verdict;


}


