using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public int score = 0;
    public float moveSpeed = 5f;
    private float leftBoundary = -6.5f;  // Sol sýnýr
    private float rightBoundary = 6.5f;  // Sað sýnýr
    public float laneChangeSpeed = 3f;
    public GameObject bulletPrefab; // Mermi prefabý
    public Transform firePoint; // Merminin çýkýþ noktasý
    public float fireRate = 0.5f; // Ateþ etme hýzý
    private float nextFireTime = 0.2f;
    Rigidbody rb;

    private Vector3 moveDirection = Vector3.forward;
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private float direction = 0f;

    AudioSource voiceSource;
    public AudioClip fireSound;
    public ParticleSystem muzzleFlash;

    #region Bullet
    [Header("Bullet Settings")]
    [SerializeField] Transform spawnGun;
    [SerializeField] float range;
    [SerializeField] LayerMask mask;
    [SerializeField] TrailRenderer bullet;
    #endregion


    public float Hp = 100f;
    bool isAlive;

    public bool isPlayerAlive()
    {
        return isAlive;
    }

    public void playerTakeDamage()
    {
        Hp -= Random.Range(5, 10);
    }
    private void FixedUpdate()
    {
        if (Hp <= 0)
        {
            isAlive = false;
        }
        if (isAlive == true)
        {
            // Automatic forward movement
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Horizontal movement based on direction
            Vector3 horizontalMove = new Vector3(direction, 0, 0) * laneChangeSpeed * Time.deltaTime;
            transform.Translate(horizontalMove, Space.World);
            RestrictPlayerMovement();
        }

    }

    // Input System reference
    private PlayerInput playerInput;

    // Animator reference
    private Animator animator;

    public float getHp()
    {
        return Hp;
    }
    private void RestrictPlayerMovement()
    {
        // Mevcut pozisyonu al
        Vector3 currentPosition = transform.position;

        // X pozisyonunu belirli sýnýrlar arasýnda tut
        currentPosition.x = Mathf.Clamp(currentPosition.x, leftBoundary, rightBoundary);

        // Sýnýrlandýrýlmýþ pozisyonu geri uygulayýn
        transform.position = currentPosition;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAlive = true;
        voiceSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // Register the input action
        playerInput.actions["Movement"].started += OnMoveStarted;
        playerInput.actions["Movement"].performed += OnMovePerformed;
        playerInput.actions["Movement"].canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        // Unregister the input action
        playerInput.actions["Movement"].started -= OnMoveStarted;
        playerInput.actions["Movement"].performed -= OnMovePerformed;
        playerInput.actions["Movement"].canceled -= OnMoveCanceled;
    }

    private void Update()
    {
        // Update animator parameters
        animator.SetFloat("Direction", direction);
        animator.SetFloat("isRunning", 1f); // Karakter sürekli koþuyor
        animator.SetBool("isShooting", Time.time > nextFireTime);

        // Automatic shooting
        if (isAlive && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // nextFireTime'ý güncelle
        }
    }
    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        if (context.control.device is Keyboard)
        {
            if (context.control.name == "a")
            {
                direction = -1f;
            }
            else if (context.control.name == "d")
            {
                direction = 1f;
            }
        }
        else if (context.control.device is Touchscreen)
        {
            startTouchPosition = context.ReadValue<Vector2>();
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        if (context.control.device is Touchscreen)
        {
            currentTouchPosition = context.ReadValue<Vector2>();
            float touchDeltaX = currentTouchPosition.x - startTouchPosition.x;

            // Normalize to -1 to 1 range based on screen width
            direction = Mathf.Clamp(touchDeltaX / Screen.width, -1f, 1f);
        }

        Debug.Log("Direction: " + direction);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (context.control.device is Keyboard)
        {
            direction = 0f;
        }
        else if (context.control.device is Touchscreen)
        {
            direction = 0f;
        }

        Debug.Log("Movement Canceled, Direction reset to zero.");
    }

    private IEnumerator Trigger()
    {
        if (Physics.Raycast(spawnGun.transform.position, spawnGun.forward, out RaycastHit hit, range, mask))
        {
            TrailRenderer trail = Instantiate(bullet, spawnGun.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));
            hit.collider.gameObject.GetComponent<Zombie>().takeDamageZombie();
        }
        yield return null;
    }
    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {

        float time = 0f;
        Vector3 startPos = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPos, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }


        trail.transform.position = hit.point;
        Destroy(trail.gameObject, trail.time);
    }

    private void Shoot()
    {
        StartCoroutine(Trigger());
        AudioSource.PlayClipAtPoint(fireSound, spawnGun.position);
        muzzleFlash.Play();
    }
}
