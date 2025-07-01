using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    [Header("Task Data")]
    [SerializeField] private List<TasksScriptable> allPossibleTasks;
    private List<TasksScriptable> tasksForCurrentDay;
    private Dictionary<TasksScriptable, int> itemTaskProgress;
    private Dictionary<TasksScriptable, bool> amountTaskCompleted;

    [Header("UI References")]
    [SerializeField] private GameObject taskListUIParent;
    [SerializeField] private GameObject taskUIPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }
        else { Instance = this; }
    }

    private void Start()
    {
        LoadTasksForDay(1);
    }

    private void Update()
    {
        CheckAmountTasks();
    }

    public void LoadTasksForDay(int day)
    {
        int tasksToSkip = (day - 1) * 3;
        tasksForCurrentDay = allPossibleTasks.Skip(tasksToSkip).Take(3).ToList();

        itemTaskProgress = new Dictionary<TasksScriptable, int>();
        amountTaskCompleted = new Dictionary<TasksScriptable, bool>();
        foreach (var task in tasksForCurrentDay)
        {
            if (task.goalType == TaskGoalType.UseSpecificItem)
            {
                itemTaskProgress.Add(task, 0);
            }
            else if (task.goalType == TaskGoalType.ReachAmount)
            {
                amountTaskCompleted.Add(task, false);
            }
        }
        UpdateTaskUI();
    }

    private void CheckAmountTasks()
    {
        if (tasksForCurrentDay == null) return;
        bool needsUiUpdate = false;

        foreach (var task in tasksForCurrentDay)
        {
            if (task.goalType == TaskGoalType.ReachAmount && !amountTaskCompleted[task])
            {
                float currentStatValue = 0;
                switch (task.taskCategory)
                {
                    case TaskCategories.Feed:
                        currentStatValue = PetStats.Instance.hunger;
                        break;
                    case TaskCategories.Clean:
                        currentStatValue = PetStats.Instance.cleanliness;
                        break;
                    case TaskCategories.Play:
                        currentStatValue = PetStats.Instance.happiness;
                        break;
                }

                if (currentStatValue >= task.amountToReach)
                {
                    amountTaskCompleted[task] = true;
                    needsUiUpdate = true;
                }
            }
        }

        if (needsUiUpdate)
        {
            UpdateTaskUI();
        }
    }

    public void OnItemUsed(ItemData itemUsed)
    {
        if (tasksForCurrentDay == null) return;
        foreach (var task in tasksForCurrentDay)
        {
            if (task.goalType == TaskGoalType.UseSpecificItem && task.requiredItem == itemUsed)
            {
                itemTaskProgress[task]++;
            }
        }
        UpdateTaskUI();
    }

    public bool AreAllTasksComplete()
    {
        if (tasksForCurrentDay == null || tasksForCurrentDay.Count == 0) return true;
        foreach (var task in tasksForCurrentDay)
        {
            if (task.goalType == TaskGoalType.UseSpecificItem)
            {
                if (itemTaskProgress[task] < task.requiredItemCount) return false;
            }
            else if (task.goalType == TaskGoalType.ReachAmount)
            {
                if (!amountTaskCompleted[task]) return false;
            }
        }
        return true;
    }

    private void UpdateTaskUI()
    {
        foreach (Transform child in taskListUIParent.transform) { Destroy(child.gameObject); }
        if (tasksForCurrentDay == null) return;

        foreach (var task in tasksForCurrentDay)
        {
            GameObject taskUIInstance = Instantiate(taskUIPrefab, taskListUIParent.transform);
            TextMeshProUGUI descriptionText = taskUIInstance.transform.Find("Description_Text").GetComponent<TextMeshProUGUI>();

            bool isComplete = false;
            if (task.goalType == TaskGoalType.UseSpecificItem)
            {
                int currentProgress = itemTaskProgress[task];
                int requiredCount = task.requiredItemCount;
                descriptionText.text = $"{task.description} ({currentProgress}/{requiredCount})";
                if (currentProgress >= requiredCount) isComplete = true;
            }
            else if (task.goalType == TaskGoalType.ReachAmount)
            {
                descriptionText.text = task.description;
                if (amountTaskCompleted[task]) isComplete = true;
            }

            if (isComplete)
            {
                descriptionText.fontStyle = FontStyles.Strikethrough;
                descriptionText.color = Color.gray;
            }
        }
    }
}