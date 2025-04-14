using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PillInfo
{
    public string pillName;
    public string effect;
    public bool discovered;
}

public class Nokia : MonoBehaviour
{
    [Header("Phone UI Root")]
    public GameObject phoneRoot;

    [Header("UI Elements")]
    public TextMeshProUGUI displayText;
    public Button prevButton;
    public Button nextButton;

    [Header("Pill Data")]
    public List<PillInfo> pillList;

    private int currentIndex = 0;
    private bool isPhoneOpen = false;

    private void Start()
    {
        prevButton.onClick.AddListener(PreviousEntry);
        nextButton.onClick.AddListener(NextEntry);

        phoneRoot.SetActive(false); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenPhone();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePhone();
        }

        if (!isPhoneOpen) return;

        if (Input.GetKeyDown(KeyCode.RightArrow)) NextEntry();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) PreviousEntry();
    }

    private void OpenPhone()
    {
        isPhoneOpen = true;
        phoneRoot.SetActive(true);
        ShowCurrentEntry();
        Time.timeScale = 0f; 
    }

    private void ClosePhone()
    {
        isPhoneOpen = false;
        phoneRoot.SetActive(false);
        Time.timeScale = 1f; 
    }

    private void ShowCurrentEntry()
    {
        if (pillList == null || pillList.Count == 0)
        {
            displayText.text = "No pill data.";
            return;
        }

        var pill = pillList[currentIndex];

        if (!pill.discovered)
        {
            displayText.text =
                "--- UNKNOWN PILL ---\n" +
                "Data Unavailable";
            return;
        }

        displayText.text =
            $"- {pill.pillName.ToUpper()} -\n" +
            $"Effect: {pill.effect}\n" +
            $"Discovered: Yes";
    }

    public void NextEntry()
    {
        if (pillList.Count == 0) return;
        currentIndex = (currentIndex + 1) % pillList.Count;
        ShowCurrentEntry();
    }

    public void PreviousEntry()
    {
        if (pillList.Count == 0) return;
        currentIndex = (currentIndex - 1 + pillList.Count) % pillList.Count;
        ShowCurrentEntry();
    }
}
