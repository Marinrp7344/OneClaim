using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Verdict;

public class CaseManager : MonoBehaviour
{
    public static CaseManager Instance;

    public enum TrialStage { None, Information, Analysis, Verdict, Retrial }
    public TrialStage stage;
    public List<Case> cases;
    public List<Defendant> defendants;
    public Case currentCase;
    public Defendant currentDefendant;
    public Evidence currentEvidence;
    public int maxLies;
    public int lies;

    public VerdictType currentSentencing;
    public VerdictType retrialSentencing;
    public int sentenceLength;

    void Start()
    {
        Instance = this;
        EstablishCase();

    }

    public void EstablishCase()
    {
        ChooseCase();
        EstablishLyingOdds();
        EstablishClaims();
        ChooseEvidence();
        stage = TrialStage.Information;
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
                ApplyLiesToClaims(1);
                break;
            case Defendant.LyingScale.Low:
                ApplyLiesToClaims(5);
                break;
            case Defendant.LyingScale.Neutral:
                ApplyLiesToClaims(15);
                break;
            case Defendant.LyingScale.High:
                ApplyLiesToClaims(25);
                break;
            case Defendant.LyingScale.VeryHigh:
                ApplyLiesToClaims(40);
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

    public void MakeClaim(PlayerChoices playerChoice, Evidence evidence)
    {
        Verdict currentVerdict = currentCase.DetermineVerdict(playerChoice, evidence);
        ApplyVerdict(currentVerdict);
    }

    public void ApplyVerdict(Verdict verdict)
    {
        if (stage != TrialStage.Retrial)
        {
            if (verdict.verdictType == Verdict.VerdictType.Guilty)
            {
                currentSentencing = Verdict.VerdictType.Guilty;
                sentenceLength += Random.Range(verdict.minimumSentence, verdict.maximumSentence);
            }
            else
            {
                currentSentencing = Verdict.VerdictType.Innocent;
            }
        }
        else
        {
            if (currentSentencing == Verdict.VerdictType.Guilty && currentSentencing == Verdict.VerdictType.Guilty)
            {
                sentenceLength += Random.Range(verdict.minimumSentence, verdict.maximumSentence);
            }
            else if (currentSentencing == Verdict.VerdictType.Guilty && currentSentencing == Verdict.VerdictType.Innocent)
            {
                sentenceLength -= Random.Range(verdict.minimumSentence, verdict.maximumSentence);
                if (sentenceLength <= 0)
                {
                    currentSentencing = Verdict.VerdictType.Innocent;
                }
            }
        }
    }


}
