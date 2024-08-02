using UnityEngine;

public class PuzzleArea : MonoBehaviour
{
    public GameObject objectToMove; // Hareket ettirmek istediðiniz GameObject
    public float moveDistance = 5f; // Ne kadar aþaðý ineceði
    public float moveSpeed = 2f; // Hareket hýzý

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
        // Kopya býrakýldýðýnda tetiklenen iþlem
        if (other.CompareTag("copy"))
        {
            isTriggered = true;
        }
    }

    void Update()
    {
        if (isTriggered && objectToMove != null)
        {
            // GameObject'i aþaðý doðru hareket ettir
            float step = moveSpeed * Time.deltaTime;
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position,
                new Vector3(originalPosition.x, originalPosition.y - moveDistance, originalPosition.z),
                step);

            // Hedefe ulaþýldýðýnda isTriggered'ý false yaparak hareketi durdurabilirsiniz
            if (Vector3.Distance(objectToMove.transform.position,
                new Vector3(originalPosition.x, originalPosition.y - moveDistance, originalPosition.z)) < 0.01f)
            {
                isTriggered = false;
            }
        }
    }
}
