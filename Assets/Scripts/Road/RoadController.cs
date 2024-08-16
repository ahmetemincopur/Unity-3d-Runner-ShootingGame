using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Oyuncunun Transform bile�eni
    [SerializeField] private float moveDistance = 100f; // Yolun ta��naca�� mesafe

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Oyuncu Collider'a dokundu�unda
        {
            MoveRoadForward(); // Yolu ileri ta��
        }
    }

    private void MoveRoadForward()
    {
        // Yolun yeni konumunu ayarla
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance);
    }
}
