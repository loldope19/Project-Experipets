using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("Day Management")]
    [SerializeField] private int currentDay = 1;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI dayCounterText;
    [SerializeField] private Button endDayButton;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }
        else { Instance = this; }
    }

    private void Start()
    {
        UpdateDayUI();
    }

    private void Update()
    {
        if (TaskManager.Instance != null && endDayButton != null)
        {
            endDayButton.interactable = TaskManager.Instance.AreAllTasksComplete();
        }
    }

    public void AdvanceToNextDay()
    {
        currentDay++;
        UpdateDayUI();

        PetStats.Instance.DecayStatsForNewDay();

        TaskManager.Instance.LoadTasksForDay(currentDay);

        Debug.Log($"Advanced to Day {currentDay}. Stats have decayed.");
    }

    private void UpdateDayUI()
    {
        if (dayCounterText != null)
        {
            dayCounterText.text = $"Day {currentDay:D3}";
        }
    }
}