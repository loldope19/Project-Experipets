using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("Day Management")]
    [SerializeField] private int currentDay = 1;
    [SerializeField] private int currentChapter = 1;

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
    }

    public void EndDayAndDecayStats()
    {
        currentDay++;
        UpdateDayUI();

        if (PetStats.Instance != null)
        {
            PetStats.Instance.DecayStatsForNewDay(messCount);
        }

        SpawnPoop();

        Debug.Log($"Advanced to Day {currentDay}. Stats have decayed.");
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
}