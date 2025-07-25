using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SpawnArea : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public Vector2 GetRandomPosition()
    {
        Rect rect = rectTransform.rect;

        float randomX = Random.Range(rect.xMin, rect.xMax);
        float randomY = Random.Range(rect.yMin, rect.yMax);

        return new Vector2(randomX, randomY);
    }
}