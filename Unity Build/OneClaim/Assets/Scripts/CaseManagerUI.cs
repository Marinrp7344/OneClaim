using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CaseManagerUI : MonoBehaviour
{
    public static CaseManagerUI Instance;

    [Header("Stage Elements")]
    public InformationUI informationUI;
    public AnalysisUI analysisUI;
    public VerdictUI verdictUI;

    [Header("Header Elements")]
    public TextMeshProUGUI currentVerdict;
    public TextMeshProUGUI stage;
    public TextMeshProUGUI sentencing;

    [Header("Visual Elements")]
    public Image defendantSprite;

    [Header("Case Views")]
    public GameObject informationView;
    public GameObject analysisView;
    public GameObject verdictView;

    private void Start()
    {
        Instance = this;
    }

    public void DisplayDefendant()
    {
        defendantSprite.sprite = CaseManager.Instance.currentDefendant.sprite;
    }

    public void UpdateHeader()
    {
        currentVerdict.text = CaseManager.Instance.currentSentencing.ToString();
        stage.text = CaseManager.Instance.stage.ToString();
        sentencing.text = CaseManager.Instance.sentenceLength.ToString();
    }

    public void GoToInformation()
    {
        informationView.SetActive(true);
        analysisView.SetActive(false);
        verdictView.SetActive(false);
    }

    public void GoToAnalysis()
    {
        informationView.SetActive(false);
        analysisView.SetActive(true);
        verdictView.SetActive(false);
    }

    public void GoToVerdict()
    {
        informationView.SetActive(false);
        analysisView.SetActive(false);
        verdictView.SetActive(true);
    }

}
