using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start Game butonuna basıldığında çağrılacak fonksiyon
    public void StartGame()
    {
        // "GameScene" adında bir sahnenin yükleneceğini varsayıyoruz
        // Sahne adını projenizdeki oyun sahnesinin adıyla değiştirin
        SceneManager.LoadScene(1);
    }

    // Options butonuna basıldığında çağrılacak fonksiyon
    public void OpenOptions()
    {
        // Burada bir seçenekler menüsü açabilirsiniz
        // Örneğin, bir Canvas veya Panel aktif hale getirilebilir
        Debug.Log("Options Button Clicked!");
    }

    // Exit butonuna basıldığında çağrılacak fonksiyon
    public void ExitGame()
    {
        // Oyun uygulamasını kapatır
        Debug.Log("Exit Button Clicked!");
        Application.Quit();

        // Eğer Unity Editor'da test ediyorsanız, aşağıdaki kodu da kullanabilirsiniz
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
