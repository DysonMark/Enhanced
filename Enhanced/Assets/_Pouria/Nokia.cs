using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Enhanced.Dyson.Player; 

public class Nokia : MonoBehaviour
{
    [System.Serializable]
    public enum PillEffectType
    {
        None,
        UnblurVision,
        SpeedBoost
    }

    [System.Serializable]
    public struct Pill
    {
        public string pillName;
        public string effect;
        public string duration;
        public bool discovered;
        public PillEffectType effectType;
    }

    [Header("Phone UI Root")]
    public GameObject phoneRoot;

    [Header("UI Elements")]
    public TextMeshProUGUI displayText;
    public Button prevButton;
    public Button nextButton;

    [Header("Pill List")]
    public List<Pill> pills;

    

    [Header("Effect Targets")]
    public GameObject blurEffectObject;
    public PlayerController playerController;
    public float speedMultiplier = 2f;
    public float boostDuration = 10f;

    private int currentIndex = 0;
    private bool isPhoneOpen = false;
    private Coroutine speedBoostCoroutine;

    private void Start()
    {
        prevButton.onClick.AddListener(PreviousEntry);
        nextButton.onClick.AddListener(NextEntry);
        phoneRoot.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) OpenPhone();
        else if (Input.GetKeyDown(KeyCode.Escape)) ClosePhone();

        if (!isPhoneOpen) return;

        if (Input.GetKeyDown(KeyCode.RightArrow)) NextEntry();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) PreviousEntry();
        if (Input.GetKeyDown(KeyCode.Space)) ActivateCurrentPill();
    }

    private void OpenPhone()
    {
        isPhoneOpen = true;
        phoneRoot.SetActive(true);
        ShowCurrentEntry();
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
    }

    private void ClosePhone()
    {
        isPhoneOpen = false;
        phoneRoot.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        
    }

    private void ShowCurrentEntry()
    {
        if (pills == null || pills.Count == 0)
        {
            displayText.text = "No pill data.";
            return;
        }

        var pill = pills[currentIndex];

        if (!pill.discovered)
        {
            displayText.text = "--- UNKNOWN PILL ---\nData Unavailable";
            return;
        }

        displayText.text =
            $"- {pill.pillName} -\n" +
            $"Effect: {pill.effect}\n" +
            $"Press SPACE to select" 
            
            ;
    }

    public void NextEntry()
    {
        if (pills.Count == 0) return;
        currentIndex = (currentIndex + 1) % pills.Count;
        ShowCurrentEntry();
    }

    public void PreviousEntry()
    {
        if (pills.Count == 0) return;
        currentIndex = (currentIndex - 1 + pills.Count) % pills.Count;
        ShowCurrentEntry();
    }
    

    private void ActivateCurrentPill()
    {
        if (pills.Count == 0) return;
        var pill = pills[currentIndex];
        if (!pill.discovered) return;

        switch (pill.effectType)
        {
            case PillEffectType.UnblurVision:
                if (blurEffectObject != null)
                    blurEffectObject.SetActive(false);
                break;

            case PillEffectType.SpeedBoost:
                if (speedBoostCoroutine != null)
                    StopCoroutine(speedBoostCoroutine);
                speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine());
                break;

            default:
                Debug.Log("No effect assigned.");
                break;
        }
    }

    private IEnumerator SpeedBoostRoutine()
    {
        if (playerController == null) yield break;

        float originalSpeed = playerController.speed;
        playerController.speed *= speedMultiplier;

        yield return new WaitForSecondsRealtime(boostDuration);

        playerController.speed = originalSpeed;
    }
}
