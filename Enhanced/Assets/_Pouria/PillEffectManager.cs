using System.Collections;
using UnityEngine;
using Enhanced.Dyson.Player;

public class PillEffectManager : MonoBehaviour
{
    [Header("Blur Control")]
    public GameObject blurEffectObject;

    [Header("Player Settings")]
    public PlayerController playerController;
    public float speedMultiplier = 2f;
    public float boostDuration = 10f;

    private Coroutine speedBoostCoroutine;

    public void UnblurVision()
    {
        if (blurEffectObject != null)
            blurEffectObject.SetActive(false);
    }

    public void ApplySpeedBoost()
    {
        if (speedBoostCoroutine != null)
            StopCoroutine(speedBoostCoroutine);

        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine());
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