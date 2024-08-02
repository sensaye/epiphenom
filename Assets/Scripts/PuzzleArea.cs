using UnityEngine;

public class PuzzleArea : MonoBehaviour
{
    public GameObject objectToMove; // Hareket ettirmek istedi�iniz GameObject
    public float moveDistance = 5f; // Ne kadar a�a�� inece�i
    public float moveSpeed = 2f; // Hareket h�z�

    private bool isTriggered = false;
    private Vector3 originalPosition;

    void Start()
    {
        if (objectToMove != null)
        {
            originalPosition = objectToMove.transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Kopya b�rak�ld���nda tetiklenen i�lem
        if (other.CompareTag("copy"))
        {
            isTriggered = true;
        }
    }

    void Update()
    {
        if (isTriggered && objectToMove != null)
        {
            // GameObject'i a�a�� do�ru hareket ettir
            float step = moveSpeed * Time.deltaTime;
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position,
                new Vector3(originalPosition.x, originalPosition.y - moveDistance, originalPosition.z),
                step);

            // Hedefe ula��ld���nda isTriggered'� false yaparak hareketi durdurabilirsiniz
            if (Vector3.Distance(objectToMove.transform.position,
                new Vector3(originalPosition.x, originalPosition.y - moveDistance, originalPosition.z)) < 0.01f)
            {
                isTriggered = false;
            }
        }
    }
}
