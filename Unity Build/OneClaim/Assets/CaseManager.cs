using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public enum TrialStage { None, Information, Analysis, Verdict, Retrial }
    public TrialStage stage;
    public List<Case> cases;
    public List<Defendant> defendants;
    public Case currentCase;
    public Defendant currentDefendant;
    public Evidence currentEvidence;
    public int maxLies;
    public int lies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChooseCase()
    {
        int randomCase = Random.Range(0, cases.Count);
        int randomDefendant = Random.Range(0, defendants.Count);
        currentCase = cases[randomCase];
        currentDefendant = defendants[randomDefendant];
    }

    public void EstablishLyingOdds()
    {
        int randomLyingOdds = Random.Range(0, 100);
        lies = 0;

        if (randomLyingOdds < 10)
        {
            currentDefendant.lyingScale = Defendant.LyingScale.VeryLow;
            maxLies = 1;
        }
        else if (randomLyingOdds < 45)
        {
            currentDefendant.lyingScale = Defendant.LyingScale.Low;
            maxLies = 1;
        }
        else if (randomLyingOdds < 70)
        {
            currentDefendant.lyingScale = Defendant.LyingScale.Neutral;
            maxLies = 1;
        }
        else if (randomLyingOdds < 90)
        {
            currentDefendant.lyingScale = Defendant.LyingScale.High;
            maxLies = 2;
        }
        else
        {
            currentDefendant.lyingScale = Defendant.LyingScale.VeryHigh;
            maxLies = 2;
        }
    }

    public void DefandantClaims(Defendant.LyingScale scale)
    {
        switch(scale) 
        {
            case Defendant.LyingScale.VeryLow:
                break;
            case Defendant.LyingScale.Low:
                break;
            case Defendant.LyingScale.Neutral:
                break;
            case Defendant.LyingScale.High:
                break;
            case Defendant.LyingScale.VeryHigh:
                break;
        }
    }

    public void ApplyLiesToClaims(int percentageChance)
    {
        foreach (Claim claim in currentCase.claims)
        {
            int chanceToLie = Random.Range(0, 100);

            if (chanceToLie < percentageChance && lies < maxLies)
            {
                claim.lying = true;
            }
        }
    }

    public void EstablishClaims()
    {
        foreach (Claim claim in currentCase.claims)
        {
            int randomAdmission = Random.Range(0, 2);

            if(randomAdmission == 0)
            {
                claim.trueClaim = claim.admitClaim;
                claim.displayedClaim = claim.admitClaim;
                if (claim.lying)
                {
                    claim.displayedClaim = claim.denyClaim;
                }
            }
            else
            {
                claim.trueClaim = claim.denyClaim;
                claim.displayedClaim = claim.denyClaim;
                if (claim.lying)
                {
                    claim.displayedClaim = claim.admitClaim;
                }
            }
        }
    }

    public void ChooseEvidence()
    {
        foreach(Claim claim in currentCase.claims)
        {
            claim.associatedEvidence.active = true;
            if(claim.trueClaim.admittance == AdmissionClaims.ClaimType.Deny)
            {
                claim.associatedEvidence.active = false;
            }
        }

        bool foundEvidence = false;
        while (foundEvidence == false)
        {
            int randomEvidence = Random.Range(0, currentCase.evidence.Count);
            if(currentCase.evidence[randomEvidence].active == true)
            {
                foundEvidence = true;
                currentEvidence = currentCase.evidence[randomEvidence];
            }
        }
    }
    

}
