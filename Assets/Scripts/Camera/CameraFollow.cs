using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek karakter
    public Vector3 offset; // Kameran�n karaktere olan mesafesi

    private void LateUpdate()
    {
        // Kameran�n pozisyonunu karakterin pozisyonuna g�re g�ncelle
        transform.position = target.position + offset;
    }
}
