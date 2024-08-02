using UnityEngine;

public class CharacterCopy : MonoBehaviour
{
    public GameObject copyPrefab; // Kopya nesnesinin prefab'�n� burada belirleyin
    private bool canDropCopy = false; // Kopya b�rakma izni

    void Update()
    {
        if (canDropCopy && Input.GetKeyDown(KeyCode.C)) // 'C' tu�una bas�l�nca kopya b�rak
        {
            DropCopy();
        }
    }

    private void DropCopy()
    {
        // Karakterin pozisyonunda ve rotas�nda bir kopya olu�tur
        GameObject copy = Instantiate(copyPrefab, transform.position, transform.rotation);

        //// Kopyay� sabitlemek i�in Rigidbody bile�enini kapat veya kald�r
        //Rigidbody rb = copy.GetComponent<Rigidbody>();
        //if (rb != null)
        //{
        //    rb.isKinematic = true; // Kinematic yaparak fizik hesaplamalar�ndan etkilenmemesini sa�lar
        //}

        //// Kopya olu�turulduktan sonra hareket etmesini engelle
        //copy.GetComponent<Collider>().enabled = false; // �ste�e ba�l�, kopyan�n �arp��malara tepki vermesini engelle

        // Kopyay� olu�turuldu�u pozisyonda sabitle
        copy.transform.position = transform.position;
        copy.transform.rotation = transform.rotation;

        canDropCopy = false; // Kopya b�rak�ld�ktan sonra izin kald�r
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzleArea")) // Bu etiket ile alan� kontrol et
        {
            canDropCopy = true; // Karakter bu alana geldi�inde kopya b�rakma izni ver
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PuzzleArea"))
        {
            canDropCopy = false; // Alan� terk edince kopya b�rakma iznini kald�r
        }
    }
}
