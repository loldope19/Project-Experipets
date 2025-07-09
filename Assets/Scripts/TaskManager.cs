using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    [Header("Task Data Source")]
    [SerializeField] private List<TasksScriptable> majorTasksInOrder;
    [SerializeField] private List<TasksScriptable> allMinorTasks;

    // --- Internal State Variables (Managed by the script) ---
    private TasksScriptable activeMajorTask;
    public TasksScriptable ActiveMajorTask { get { return activeMajorTask; } }

    private List<TasksScriptable> availableMinorTasks;
    private List<TasksScriptable> dailyMinorTasks;
    private List<TasksScriptable> completedChapterTasks;
    private Dictionary<TasksScriptable, int> taskCooldowns;

    // --- Progress Tracking ---
    private Dictionary<TasksScriptable, int> itemTaskProgress;


    [Header("UI References")]
    [SerializeField] private GameObject taskListUIParent;
    [SerializeField] private GameObject taskUIPrefab;

    [SerializeField] private PetCareUIManager uiManager;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }
        else { Instance = this; }
    }

    private void Start()
    {
        LoadChapterTasks(1);
    }

    private void Update()
    {
        CheckAmountTasks();
    }

    public void LoadChapterTasks(int chapterNumber)
    {
        if (chapterNumber - 1 < majorTasksInOrder.Count)
        {
            activeMajorTask = majorTasksInOrder[chapterNumber - 1];
        }
        else
        {
            Debug.LogError("Trying to load a chapter that doesn't have a Major Task assigned!");
            return;
        }

        // The pool of available minor tasks is ALWAYS the full list.
        availableMinorTasks = new List<TasksScriptable>(allMinorTasks);
        dailyMinorTasks = new List<TasksScriptable>();
        completedChapterTasks = new List<TasksScriptable>();
        taskCooldowns = new Dictionary<TasksScriptable, int>();
        itemTaskProgress = new Dictionary<TasksScriptable, int>();

        SelectDailyTasks();
        UpdateTaskUI();
    }

    public void PrepareForNextDay()
    {
        if (taskCooldowns.Count > 0)
        {
            var cooledDownTasks = new List<TasksScriptable>();
            foreach (var task in taskCooldowns.Keys.ToList())
            {
                taskCooldowns[task]--;
                if (taskCooldowns[task] <= 0) { cooledDownTasks.Add(task); }
            }
            foreach (var task in cooledDownTasks) { taskCooldowns.Remove(task); }
        }

        dailyMinorTasks.RemoveAll(task => IsTaskComplete(task));

        SelectDailyTasks();
        UpdateTaskUI();
    }

    private void SelectDailyTasks()
    {
        int tasksNeeded = 2 - dailyMinorTasks.Count;
        if (tasksNeeded <= 0) return;

        var selectableTasks = availableMinorTasks.Where(t => !taskCooldowns.ContainsKey(t) && !dailyMinorTasks.Contains(t)).ToList();

        for (int i = 0; i < tasksNeeded && selectableTasks.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, selectableTasks.Count);
            TasksScriptable newTask = selectableTasks[randomIndex];

            dailyMinorTasks.Add(newTask);
            selectableTasks.RemoveAt(randomIndex);

            if (newTask.goalType == TaskGoalType.UseSpecificItem && !itemTaskProgress.ContainsKey(newTask))
            {
                itemTaskProgress.Add(newTask, 0);
            }
        }
    }

    private void CheckAmountTasks()
    {
        if (dailyMinorTasks == null) return;

        foreach (var task in dailyMinorTasks.ToList())
        {
            if (task.goalType == TaskGoalType.ReachAmount && !IsTaskComplete(task))
            {
                float currentStatValue = 0;
                switch (task.taskCategory)
                {
                    case TaskCategories.Feed: currentStatValue = PetStats.Instance.hunger; break;
                    case TaskCategories.Clean: currentStatValue = PetStats.Instance.cleanliness; break;
                    case TaskCategories.Play: currentStatValue = PetStats.Instance.happiness; break;
                }

                if (currentStatValue >= task.amountToReach)
                {
                    MarkTaskAsComplete(task);
                    UpdateTaskUI();
                }
            }
        }
    }

    public void OnItemUsed(ItemData itemUsed)
    {
        if (dailyMinorTasks == null) return;

        foreach (var task in dailyMinorTasks)
        {
            if (task.goalType == TaskGoalType.UseSpecificItem && task.requiredItem == itemUsed && !IsTaskComplete(task))
            {
                itemTaskProgress[task]++;
                if (itemTaskProgress[task] >= task.requiredItemCount)
                {
                    MarkTaskAsComplete(task);
                }
                UpdateTaskUI();
            }
        }
    }

    private void MarkTaskAsComplete(TasksScriptable task)
    {
        if (completedChapterTasks.Contains(task)) return;

        completedChapterTasks.Add(task);
        Debug.Log($"Task Completed: {task.description}");

        if (task.taskType == TaskType.Minor)
        {
            taskCooldowns[task] = 3;
        }

        CheckMajorTaskCompletion();
    }

    private void CheckMajorTaskCompletion()
    {
        if (activeMajorTask == null || IsTaskComplete(activeMajorTask)) return;

        int minorTasksDone = completedChapterTasks.Count(t => t.taskType == TaskType.Minor);

        if (minorTasksDone >= activeMajorTask.minorTasksRequired)
        {
            MarkTaskAsComplete(activeMajorTask);
            Debug.Log($"MAJOR TASK COMPLETED: {activeMajorTask.description}");

            if (uiManager != null)
            {
                uiManager.ShowMajorTaskCompletedPopup();
            }
        }
    }

    public bool IsTaskComplete(TasksScriptable task)
    {
        return completedChapterTasks.Contains(task);
    }

    private void UpdateTaskUI()
    {
        // Clear old UI
        foreach (Transform child in taskListUIParent.transform) { Destroy(child.gameObject); }

        // Display Major Task
        if (activeMajorTask != null)
        {
            GameObject majorTaskUI = Instantiate(taskUIPrefab, taskListUIParent.transform);
            TextMeshProUGUI majorText = majorTaskUI.transform.Find("Description_Text").GetComponent<TextMeshProUGUI>();
            int progress = completedChapterTasks.Count(t => t.taskType == TaskType.Minor);
            majorText.text = $"{activeMajorTask.description} ({progress}/{activeMajorTask.minorTasksRequired})";
            if (IsTaskComplete(activeMajorTask))
            {
                majorText.fontStyle = FontStyles.Strikethrough;
                majorText.color = Color.gray;
            }
        }

        // Display the 2 Daily Minor Tasks
        if (dailyMinorTasks == null) return;
        foreach (var task in dailyMinorTasks)
        {
            GameObject taskUIInstance = Instantiate(taskUIPrefab, taskListUIParent.transform);
            TextMeshProUGUI descriptionText = taskUIInstance.transform.Find("Description_Text").GetComponent<TextMeshProUGUI>();

            if (task.goalType == TaskGoalType.UseSpecificItem)
            {
                // Ensure progress data exists before trying to access it
                int currentProgress = itemTaskProgress.ContainsKey(task) ? itemTaskProgress[task] : 0;
                descriptionText.text = $"{task.description} ({currentProgress}/{task.requiredItemCount})";
            }
            else
            {
                descriptionText.text = task.description;
            }

            if (IsTaskComplete(task))
            {
                descriptionText.fontStyle = FontStyles.Strikethrough;
                descriptionText.color = Color.gray;
            }
        }
    }

}