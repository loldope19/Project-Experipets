using UnityEngine;

public class PetRubTarget : MonoBehaviour
{
    private float cleanCooldown = 0.4f;
    private float lastCleanTime;

    public void ApplyCleaning()
    {
        if (Time.time > lastCleanTime + cleanCooldown)
        {
            Debug.Log("Applying cleaning effect!");
            PetStats.Instance.Clean(1);
            lastCleanTime = Time.time;

            // optional: trigger a happy particle effect or sound here
        }
    }

    public void ApplyMood()
    {
        if (Time.time > lastCleanTime + cleanCooldown)
        {
            Debug.Log("Applying cleaning effect!");
            PetStats.Instance.Play(1);
            lastCleanTime = Time.time;

            // optional: trigger a happy particle effect or sound here
        }
    }
}