using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public float moveSpeed = 5f;
    private Vector3 moveDirection = Vector3.forward;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
