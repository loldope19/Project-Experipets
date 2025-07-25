using UnityEngine;

public class TemporaryObject : MonoBehaviour
{
    void Start() { Destroy(gameObject, 3f); }
}