using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Oyuncunun Transform bile�eni
    [SerializeField] private float moveDistance = 30f; // Yolun ta��naca�� mesafe
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncu Collider'a dokundu�unda
        {
            MoveFogForward(); // Yolu ileri ta��
        }
    }
    private void MoveFogForward()
    {
        // Yolun yeni konumunu ayarla
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance);
    }
}
