using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("Day Management")]
    [SerializeField] private int currentDay = 1;
    [SerializeField] private int currentChapter = 0;

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
        StartCoroutine(StartChapterSequence(currentChapter));
    }

    private IEnumerator StartChapterSequence(int chapter)
    {
        yield return ChapterTitleManager.Instance.ShowTitleForChapter(chapter);

        //    (Dialogue tags are taken from the "IEPRFDV_ Story & Dialogue.pdf" document)
        switch (chapter)
        {
            case 0: // Prologue
                DialogueManager.Instance.StartDialogue("Prologue", "PROLOGUE_01_WakeUp");
                break;
            case 1: // Chapter 1
                DialogueManager.Instance.StartDialogue("Chapter1", "CHAPTER1_01_Intro");
                break;
            case 2: // Chapter 2
                DialogueManager.Instance.StartDialogue("Chapter2", "CHAPTER2_01_Intro");
                break;
            case 3: // Chapter 3
                DialogueManager.Instance.StartDialogue("Chapter3", "CHAPTER3_01_Intro");
                break;
            case 4: // Chapter 4
                DialogueManager.Instance.StartDialogue("Chapter4", "CHAPTER4_01_Intro");
                break;
        }
    }

    public int GetCurrentChapter() { return currentChapter; }

    public void EndDayAndDecayStats()
    {
        currentDay++;
        UpdateDayUI();
        if (PetStats.Instance != null) PetStats.Instance.DecayStatsForNewDay(messCount);
        SpawnPoop();
        TaskManager.Instance.PrepareForNextDay();
        Debug.Log($"Advanced to Day {currentDay}. Stats have decayed.");
    }

    private void UpdateDayUI()
    {
        if (dayCounterText != null)
        {
            dayCounterText.text = $"Day {currentDay:D3}";
        }
    }

    public void AdvanceToNextChapter(int chapterToEnd)
    {
        currentChapter = chapterToEnd;
        EndDayAndDecayStats();
        UpdateDayUI();
        Debug.Log($"Advancing to Chapter {currentChapter}");
        if (TaskManager.Instance != null)
        {
            TaskManager.Instance.LoadChapterTasks(currentChapter);
        }

        StartCoroutine(StartChapterSequence(currentChapter));
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