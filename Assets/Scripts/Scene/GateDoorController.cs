using UnityEngine;

public class GateDoorController : MonoBehaviour
{
    public float openSpeed = 2.0f; // Kapının açılma hızı
    public float openHeight = 5.0f; // Kapının ne kadar yükseleceği

    private Vector3 initialPosition; // Kapının başlangıç pozisyonu
    private bool isOpening = false; // Kapının açılıp açılmadığını kontrol eder
    private bool isClosing = false; // Kapının kapanıp kapanmadığını kontrol eder

    void Start()
    {
        initialPosition = transform.position; // Kapının başlangıç pozisyonunu kaydet
    }

    void Update()
    {
        if (isOpening)
        {
            Vector3 targetPosition = initialPosition + Vector3.up * openHeight; // Hedef pozisyonu hesapla
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, openSpeed * Time.deltaTime); // Kapıyı hareket ettir

            if (transform.position == targetPosition)
            {
                isOpening = false; // Hareket tamamlandığında durdur
            }
        }
        else if (isClosing)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, openSpeed * Time.deltaTime); // Kapıyı başlangıç pozisyonuna hareket ettir

            if (transform.position == initialPosition)
            {
                isClosing = false; // Hareket tamamlandığında durdur
            }
        }
    }

    public void OpenGate()
    {
        if (!isOpening && transform.position == initialPosition) // Kapı açılmıyorsa ve kapı başlangıç pozisyonundaysa
        {
            isOpening = true; // Kapıyı aç
            isClosing = false; // Kapama işlemini durdur
        }
    }

    public void CloseGate()
    {
        if (!isClosing && transform.position == initialPosition + Vector3.up * openHeight) // Kapı kapanmıyorsa ve kapı açık pozisyondaysa
        {
            isOpening = false; // Açma işlemini durdur
            isClosing = true; // Kapıyı kapat
        }
    }
}
