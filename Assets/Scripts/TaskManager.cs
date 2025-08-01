using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    [Header("Task Data Source")]
    [SerializeField] private List<TasksScriptable> majorTasksInOrder;
    [SerializeField] private List<TasksScriptable> allMinorTasks;

    [Header("UI References")]
    [SerializeField] private GameObject taskListUIParent;
    [SerializeField] private GameObject taskUIPrefab;

    [Header("Major Task UI")]
    [SerializeField] private GameObject majorTaskUIContainer;
    [SerializeField] private Slider majorTaskSlider;

    [Header("External References")]
    [SerializeField] private PetCareUIManager uiManager;

    // --- Internal State ---
    private TasksScriptable activeMajorTask;
    private List<TasksScriptable> availableMinorTasks;
    private readonly List<TasksScriptable> dailyMinorTasks = new();
    private readonly List<TasksScriptable> completedMinorTasks = new();
    private readonly Dictionary<TasksScriptable, int> taskCooldowns = new();
    private readonly Dictionary<TasksScriptable, int> itemTaskProgress = new(); 
    public int minorTasksCompletedThisChapter = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }
        else { Instance = this; }
    }

    private void Update()
    {
        CheckAmountTasks();
    }

    public void LoadChapterTasks(int chapterNumber)
    {
        // Reset progress when a new chapter loads
        minorTasksCompletedThisChapter = 0;
        itemTaskProgress.Clear();
        completedMinorTasks.Clear();
        dailyMinorTasks.Clear();
        taskCooldowns.Clear();
        availableMinorTasks = new List<TasksScriptable>(allMinorTasks);

        // --- Major Task Logic ---
        // Prologue (Chapter 0) has no major task.
        if (chapterNumber == 0)
        {
            activeMajorTask = null;
            majorTaskUIContainer.SetActive(false);
        }
        else if (chapterNumber - 1 < majorTasksInOrder.Count)
        {
            activeMajorTask = majorTasksInOrder[chapterNumber - 1];
            majorTaskUIContainer.SetActive(true);
        }
        else
        {
            Debug.LogError($"Trying to load Chapter {chapterNumber} but no Major Task is assigned!");
            activeMajorTask = null;
            majorTaskUIContainer.SetActive(false);
        }

        SelectDailyTasks();
        UpdateTaskUI();
    }

    public void PrepareForNextDay()
    {
        // Cooldown logic remains the same
        if (taskCooldowns.Count > 0)
        {
            var cooledDownTasks = taskCooldowns.Keys.ToList();
            foreach (var task in cooledDownTasks)
            {
                taskCooldowns[task]--;
                if (taskCooldowns[task] <= 0) { taskCooldowns.Remove(task); }
            }
        }
        
        // Remove completed tasks from the daily list
        dailyMinorTasks.RemoveAll(task => IsTaskComplete(task));

        SelectDailyTasks();
        UpdateTaskUI();
    }

    private void SelectDailyTasks()
    {
        int tasksNeeded = 3 - dailyMinorTasks.Count;
        if (tasksNeeded <= 0) return;

        var selectableTasks = availableMinorTasks.Where(t => !taskCooldowns.ContainsKey(t) && !dailyMinorTasks.Contains(t)).ToList();

        for (int i = 0; i < tasksNeeded && selectableTasks.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, selectableTasks.Count);
            TasksScriptable newTask = selectableTasks[randomIndex];
            dailyMinorTasks.Add(newTask);
            selectableTasks.RemoveAt(randomIndex);

            // Reset progress for new item tasks
            if (newTask.goalType == TaskGoalType.UseSpecificItem)
            {
                itemTaskProgress[newTask] = 0;
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

                if (currentStatValue >= task.amountToReach) MarkTaskAsComplete(task);
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
        if (task.taskType == TaskType.Minor)
        {
            if (!completedMinorTasks.Contains(task))
            {
                completedMinorTasks.Add(task);
                minorTasksCompletedThisChapter++;
                taskCooldowns[task] = 3;
                Debug.Log($"Minor Task Completed: {task.description}. Progress: {minorTasksCompletedThisChapter}");

                CheckMajorTaskCompletion();
            }
        }
    }


    private void CheckMajorTaskCompletion()
    {
        if (activeMajorTask == null || IsTaskComplete(activeMajorTask)) return;

        if (minorTasksCompletedThisChapter >= activeMajorTask.minorTasksRequired)
        {
            Debug.Log($"MAJOR TASK COMPLETED: {activeMajorTask.description}");
            if (uiManager != null) uiManager.ShowMajorTaskCompletedPopup();

            DayManager.Instance.CompleteMajorTask();
        }
    }

    public bool IsTaskComplete(TasksScriptable task)
    {
        return completedMinorTasks.Contains(task);
    }

    private void UpdateTaskUI()
    {
        // Clear old minor tasks UI
        foreach (Transform child in taskListUIParent.transform) { Destroy(child.gameObject); }

        // --- Update Major Task UI ---
        if (activeMajorTask != null)
        {
            majorTaskSlider.maxValue = activeMajorTask.minorTasksRequired;
            majorTaskSlider.value = minorTasksCompletedThisChapter;
        }

        // --- Display 3 Daily Minor Tasks ---
        foreach (var task in dailyMinorTasks)
        {
            GameObject taskUIInstance = Instantiate(taskUIPrefab, taskListUIParent.transform);
            TaskUI taskUIComponent = taskUIInstance.GetComponent<TaskUI>();

            // We need a proper way to check if a task is complete for the UI
            bool isComplete = IsTaskComplete(task);
            taskUIComponent.Setup(task, isComplete);

            // Update progress text for item tasks
            if (task.goalType == TaskGoalType.UseSpecificItem)
            {
                int currentProgress = itemTaskProgress.ContainsKey(task) ? itemTaskProgress[task] : 0;
                taskUIComponent.UpdateProgress(currentProgress, task.requiredItemCount);
            }
        }
    }

}