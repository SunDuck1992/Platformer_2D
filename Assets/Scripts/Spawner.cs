using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Skull _skull;
    [SerializeField] private Transform[] _pointsOfSpawn;
    [SerializeField] private float _secondsBetweenSpawn;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var waitForSeconds = new WaitForSeconds(_secondsBetweenSpawn);

        foreach (var point in _pointsOfSpawn)
        {
            Instantiate(_skull, point.transform.position, Quaternion.identity);
            yield return waitForSeconds;
        }
    }
}
