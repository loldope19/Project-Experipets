using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("Day Management")]
    [SerializeField] private int currentDay = 1;
    [SerializeField] private int currentChapter = 0;
    private bool canAdvanceChapter = false;
    public bool CanAdvanceChapter() { return canAdvanceChapter; }

    [Header("Mess Spawning")]
    public static int messCount = 0;
    public GameObject poopPrefab;
    public SpawnArea spawnArea;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI dayCounterText;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }
        else { Instance = this; }
    }

    private void Start()
    {
        UpdateDayUI();
        TaskManager.Instance.LoadChapterTasks(currentChapter);
    }

    public void EndDayAndDecayStats()
    {
        if (canAdvanceChapter)
        {
            AdvanceToNextChapter();
            canAdvanceChapter = false;
        }
        else
        {
            currentDay++;
            UpdateDayUI();
            if (PetStats.Instance != null) PetStats.Instance.DecayStatsForNewDay(messCount);
            SpawnPoop();
            TaskManager.Instance.PrepareForNextDay();
            Debug.Log($"Advanced to Day {currentDay}. Stats have decayed.");
        }
    }

    private void UpdateDayUI()
    {
        if (dayCounterText != null)
        {
            dayCounterText.text = $"Day {currentDay:D3}";
        }
    }

    public void AdvanceToNextChapter()
    {
        currentChapter++;
        // currentDay = 1;
        UpdateDayUI();
        Debug.Log($"Advancing to Chapter {currentChapter}");
        if (TaskManager.Instance != null)
        {
            TaskManager.Instance.LoadChapterTasks(currentChapter);
        }
    }

    private void SpawnPoop()
    {
        if (poopPrefab != null && spawnArea != null)
        {
            Vector2 spawnPosition = spawnArea.GetRandomPosition();
            GameObject newPoop = Instantiate(poopPrefab, spawnArea.transform);
            newPoop.GetComponent<RectTransform>().localPosition = spawnPosition;
            Debug.Log("Poop spawned at local position: " + spawnPosition);
        }
    }

    public void CompleteMajorTask()
    {
        canAdvanceChapter = true;
        Debug.Log("Major task complete! The player can now advance to the next chapter by ending the day.");
        // You could also trigger a dialogue here to inform the player.
    }
}