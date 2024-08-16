using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] float decreaseSpeed;
    float healthBarInstance = 1f;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam =Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, healthBarInstance, decreaseSpeed * Time.deltaTime);
        transform.rotation=cam.transform.rotation;
    }

    public void HealthBarProgress(float currentHealth,float maxHealth)
    {
        healthBarInstance=currentHealth/maxHealth;
        Debug.Log("halloooo"+healthBarInstance);
        transform.gameObject.SetActive(true);
    }
}
