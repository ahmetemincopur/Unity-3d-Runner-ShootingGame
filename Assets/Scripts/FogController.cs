using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Oyuncunun Transform bileþeni
    [SerializeField] private float moveDistance = 30f; // Yolun taþýnacaðý mesafe
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncu Collider'a dokunduðunda
        {
            MoveFogForward(); // Yolu ileri taþý
        }
    }
    private void MoveFogForward()
    {
        // Yolun yeni konumunu ayarla
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance);
    }
}
