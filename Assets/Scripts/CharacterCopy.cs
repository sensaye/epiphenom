using UnityEngine;

public class CharacterCopy : MonoBehaviour
{
    public GameObject copyPrefab; // Kopya nesnesinin prefab'ýný burada belirleyin
    private bool canDropCopy = false; // Kopya býrakma izni

    void Update()
    {
        if (canDropCopy && Input.GetKeyDown(KeyCode.C)) // 'C' tuþuna basýlýnca kopya býrak
        {
            DropCopy();
        }
    }

    private void DropCopy()
    {
        // Karakterin pozisyonunda ve rotasýnda bir kopya oluþtur
        GameObject copy = Instantiate(copyPrefab, transform.position, transform.rotation);

        //// Kopyayý sabitlemek için Rigidbody bileþenini kapat veya kaldýr
        //Rigidbody rb = copy.GetComponent<Rigidbody>();
        //if (rb != null)
        //{
        //    rb.isKinematic = true; // Kinematic yaparak fizik hesaplamalarýndan etkilenmemesini saðlar
        //}

        //// Kopya oluþturulduktan sonra hareket etmesini engelle
        //copy.GetComponent<Collider>().enabled = false; // Ýsteðe baðlý, kopyanýn çarpýþmalara tepki vermesini engelle

        // Kopyayý oluþturulduðu pozisyonda sabitle
        copy.transform.position = transform.position;
        copy.transform.rotation = transform.rotation;

        canDropCopy = false; // Kopya býrakýldýktan sonra izin kaldýr
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzleArea")) // Bu etiket ile alaný kontrol et
        {
            canDropCopy = true; // Karakter bu alana geldiðinde kopya býrakma izni ver
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PuzzleArea"))
        {
            canDropCopy = false; // Alaný terk edince kopya býrakma iznini kaldýr
        }
    }
}
