using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentDay = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void AdvanceDay()
    {
        currentDay++;
    }
}