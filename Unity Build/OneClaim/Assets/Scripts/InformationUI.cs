using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationUI : MonoBehaviour
{
    [Header("Text Elements")]
    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI lyingTMP;
    public TextMeshProUGUI chargesTMP;
    public TextMeshProUGUI claimsTMP;

    [Header("Art Elements")]
    public Image defendantImage;

    public void UpdateInformationSheet()
    {
        nameTMP.text = "Name: " + CaseManager.Instance.currentCase.name;
        lyingTMP.text = "Lying Chances: " + CaseManager.Instance.currentDefendant.lyingScale.ToString();
        chargesTMP.text = "Charges: \n" + CaseManager.Instance.currentCase.charges;
        claimsTMP.text = "Claims: \n1. " + CaseManager.Instance.currentCase.claims[0].displayedClaim.claimDescription + 
            "\n2. " + CaseManager.Instance.currentCase.claims[1].displayedClaim.claimDescription + "\n3. " + CaseManager.Instance.currentCase.claims[2].displayedClaim.claimDescription; 
    }

    public void UpdateDefendant()
    {
        defendantImage.sprite =  CaseManagerUI.Instance.defendantSprite.sprite;
    }
}
