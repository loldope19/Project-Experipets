using UnityEngine;

public class ToolModeController : MonoBehaviour
{
    public static ToolModeController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("UI & World References")]
    [Tooltip("The parent RectTransform of the canvas where the laser will appear.")]
    [SerializeField] private RectTransform worldCanvasRect;
    [SerializeField] private RectTransform ballSpawnArea;

    [Header("Toy Prefabs")]
    public GameObject ballPrefab;
    public GameObject laserDotPrefab;

    private GameObject activeLaserDot;
    private ToolType currentToolMode = ToolType.None;

    public void SetToolMode(ToolType tool)
    {
        if (tool == ToolType.LaserPointer && !PlayerData.Instance.isPrologueComplete)
        {
            Debug.Log("Laser Pointer is disabled during the prologue.");
            currentToolMode = ToolType.None;
            DialogueManager.Instance.StartDialogue("Prologue", "PROLOGUE_Special_LaserPointer");
            return; // Exit the function before any other logic runs
        }

        if (currentToolMode == tool)
        {
            currentToolMode = ToolType.None;
        }
        else
        {
            currentToolMode = tool;
        }

        Debug.Log("Tool Mode set to: " + currentToolMode);

        if (activeLaserDot != null)
        {
            Destroy(activeLaserDot);
        }

        if (currentToolMode == ToolType.LaserPointer)
        {
            activeLaserDot = Instantiate(laserDotPrefab, worldCanvasRect);
        }
    }

    void Update()
    {
        bool isMouseInPlayArea = RectTransformUtility.RectangleContainsScreenPoint(ballSpawnArea, Input.mousePosition, Camera.main);
        switch (currentToolMode)
        {
            case ToolType.Ball:
                if (isMouseInPlayArea && Input.GetMouseButtonDown(0))
                {
                    ThrowBall();
                }
                break;

            case ToolType.LaserPointer:
                if (activeLaserDot != null)
                {
                    activeLaserDot.SetActive(isMouseInPlayArea);

                    if (isMouseInPlayArea)
                    {
                        Plane canvasPlane = new Plane(worldCanvasRect.forward, worldCanvasRect.position);
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (canvasPlane.Raycast(ray, out float enter))
                        {
                            Vector3 worldPoint = ray.GetPoint(enter);
                            activeLaserDot.transform.position = worldPoint;
                        }

                        PetStats.Instance.Play(2f * Time.deltaTime);
                    }
                }
                break;
        }
    }

    private void ThrowBall()
    {
        Plane canvasPlane = new Plane(worldCanvasRect.forward, worldCanvasRect.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (canvasPlane.Raycast(ray, out float enter))
        {
            Vector3 worldPoint = ray.GetPoint(enter);

            if (ballPrefab != null)
            {
                Instantiate(ballPrefab, worldPoint, Quaternion.identity, worldCanvasRect);
            }
        }

        PetStats.Instance.Play(15);

        if (!PlayerData.Instance.isPrologueComplete)
        {
            DialogueManager.Instance.StartDialogue("Prologue", "PROLOGUE_05_PlayTutorialComplete");
        }

        SetToolMode(ToolType.None);
    }
}