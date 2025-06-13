using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class TaskManager : MonoBehaviour
{
    [SerializeField] public List<TasksScriptable> tasks;
    public int currentTask = 0;
    public int currentDay = 1;

    [Header("-- UI --")]
    public TMP_Text dayCount;
    public Slider taskBar;
    public TMP_Text taskText;

    // Start is called before the first frame update
    void Start()
    {
        // get currentTask num from save data
        // get currentDay num from save data
        dayCount.text = "Day " + currentDay.ToString();
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        AssignTask();
    }

    public void AssignTask()
    {
        if (tasks.Count > currentTask) {
            TasksScriptable task = this.tasks[currentTask];

            taskBar.maxValue = task.amount;
            taskBar.minValue = 0;
            taskBar.value = 0;

            taskText.text = task.message;
        }
        else
        {
            currentTask = 0;
            AssignTask();
        }
    }

    /// <summary>
    /// Item Effects for Task Manager
    /// </summary>
    /// <param name="itemData">The ScriptableObject of the item being used.</param>
    public void ApplyItemEffects(ItemData itemData)
    {
        if (itemData == null) return;

        float effect = 0;
        switch (tasks[currentTask].taskCat)
        {
            case TaskCategories.Feed:
                effect = itemData.hungerEffect;
                break;
            case TaskCategories.Clean:
                effect = itemData.cleanlinessEffect;
                break;
            case TaskCategories.Play:
                effect = itemData.happinessEffect;
                break;
        }

        AddProgress(effect);
    }

    public void AddProgress(float amount)
    {
        taskBar.value += amount;
        if (taskBar.value >= taskBar.maxValue)
            nextTask();
    } 

    public void nextTask()
    {
        currentTask++;
        currentDay++;

        dayCount.text = "Day " + currentDay.ToString();
        AssignTask();
    }
}
