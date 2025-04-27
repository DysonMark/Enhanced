using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
        public string color;
        public string effect;
        public string duration;
        public PillEffectType effectType;
        public int unlockDay;
        [HideInInspector] public bool discovered;
    }

    [Header("Phone UI Root")]
    public GameObject phoneRoot;

    [Header("UI Elements")]
    public TextMeshProUGUI displayText;
    public Button prevButton;
    public Button nextButton;

    [Header("Pill List")]
    public List<Pill> pills;

    [Header("Blinking Text Settings")]
    public float blinkInterval = 0.5f;

    [Header("Effect Targets")]
    public GameObject blurEffectObject;
    public PlayerController playerController;
    public float speedMultiplier = 2f;
    public float boostDuration = 10f;

    private int currentIndex = 0;
    private bool isPhoneOpen = false;
    private Coroutine blinkCoroutine;
    private Coroutine speedBoostCoroutine;

    private void Start()
    {
        prevButton.onClick.AddListener(PreviousEntry);
        nextButton.onClick.AddListener(NextEntry);
        phoneRoot.SetActive(false);
        UpdatePillDiscovery();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPhoneOpen)
                ClosePhone();
            else
                OpenPhone();
        }

        if (Input.GetKeyDown(KeyCode.P)) RestartSceneAndAdvanceDay();

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

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
            displayText.enabled = true;
        }
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
            displayText.text = "Use Arrow keys to browse pills, SPACE to take them";
            return;
        }

        displayText.text =
            $"- {pill.pillName.ToUpper()} -\n" +
            $"Effect: {pill.effect}\n" +
            $"Duration: {pill.duration}\n";
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

    private IEnumerator BlinkText()
    {
        while (true)
        {
            displayText.enabled = !displayText.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
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

    
    private void UpdatePillDiscovery()
    {
        int currentDay = GameManager.Instance.currentDay;

        for (int i = 0; i < pills.Count; i++)
        {
            pills[i] = UpdatePillDiscoveredStatus(pills[i], currentDay);
        }
    }

    private Pill UpdatePillDiscoveredStatus(Pill pill, int day)
    {
        pill.discovered = (day >= pill.unlockDay);
        return pill;
    }
    
    private void RestartSceneAndAdvanceDay()
    {
        GameManager.Instance.AdvanceDay();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
