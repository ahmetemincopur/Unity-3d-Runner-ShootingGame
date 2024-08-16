using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Oyuncunun Transform bileþeni
    [SerializeField] private float moveDistance = 100f; // Yolun taþýnacaðý mesafe

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncu Collider'a dokunduðunda
        {
            MoveRoadForward(); // Yolu ileri taþý
        }
    }

    private void MoveRoadForward()
    {
        // Yolun yeni konumunu ayarla
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance);
    }
}
