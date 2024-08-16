using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek karakter
    public Vector3 offset; // Kameranýn karaktere olan mesafesi

    private void LateUpdate()
    {
        // Kameranýn pozisyonunu karakterin pozisyonuna göre güncelle
        transform.position = target.position + offset;
    }
}
