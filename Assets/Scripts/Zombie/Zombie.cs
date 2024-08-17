using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] HealthBar healthBarScript;
    public float zombieHP = 100;
    bool zombieDead;

    Animator zombieAnimator;
    private Vector3 moveDirection = Vector3.forward;
    public float moveSpeed = 5f;
    GameObject target;
    public float chaseDistance;
    public float attackDistance = 3f;
    float distance;

    AudioSource voiceSource;
    public AudioClip zombieSound;

    // Start is called before the first frame update
    void Start()
    {
        zombieAnimator = this.GetComponent<Animator>();
        target = GameObject.Find("Target");
        currentHealth = zombieHP;

        healthBarScript.transform.gameObject.SetActive(false);
        StartCoroutine(PlayZombieSound());
    }
    private IEnumerator PlayZombieSound()
    {
        while (!zombieDead) // Sonsuz döngü
        {
            AudioSource.PlayClipAtPoint(zombieSound, transform.position);  // Zombi sesini çal
            yield return new WaitForSeconds(3f);   // 3 saniye bekle
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        // Zombinin Y eksenini sabit tut
        Vector3 currentPosition = transform.position;
        currentPosition.y = 0.45f; // Y eksenini zemin seviyesine sabitleyin (0f yerine zemine uygun deðeri kullanabilirsiniz)
        transform.position = currentPosition;

        this.transform.LookAt(target.transform.position);
        if (currentHealth <= 0)
        {
            zombieDead = true;
        }
        if (zombieDead == true)
        {
            zombieAnimator.SetBool("isDead", true);

            StartCoroutine(banish());
        }
        else
        {
            distance = Vector3.Distance(this.transform.position, target.transform.position);

            if (distance < attackDistance)
            {
                zombieAnimator.SetBool("isAttacking", true);
                this.transform.LookAt(target.transform.position);
                Destroy(this.gameObject);
            }
            else
            {
                // Automatic forward movement
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                    //run animation
                    zombieAnimator.SetBool("isAttacking", false);
                    this.transform.LookAt(target.transform.position);
                
            }
        }
    }


    public void zombieAattackDamage()
    {
        target.GetComponent<PlayerMovement>().playerTakeDamage();
    }
    IEnumerator banish()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
    public void takeDamageZombie()
    {
        currentHealth -= Random.Range(45, 55);
        healthBarScript.HealthBarProgress(currentHealth, zombieHP);

    }
}
