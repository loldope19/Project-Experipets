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
        
    public void AddProgress(float amount, TaskCategories taskCat)
    {
        if (taskCat == tasks[currentTask].taskCat)
        {
            taskBar.value += amount;
            if (taskBar.value >= taskBar.maxValue)
                nextTask();

        }
    } 

    public void nextTask()
    {
        currentTask++;
        currentDay++;

        dayCount.text = "Day " + currentDay.ToString();
        AssignTask();
    }
}
