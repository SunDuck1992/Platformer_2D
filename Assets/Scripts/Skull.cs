using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private float _distance;

    private void Start()
    {
        _playerTransform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        FindObjectOfType<SkullCounter>().Count++;
    }
}
