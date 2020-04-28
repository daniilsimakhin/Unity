using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private GameObject _enemy;
    private int _currentPoint = 0;

    private void Start()
    {
        _points = new Transform[GetComponent<Transform>().childCount];
        SettingPoint();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (_currentPoint < _points.Length)
        {
            Instantiate(_enemy, _points[_currentPoint]);
            int temp = (_currentPoint == _points.Length - 1) ? (_currentPoint = 0) : (_currentPoint++);
            yield return new WaitForSeconds(2f);           
        }
    }

    private void SettingPoint()
    {
        for (int i = 0; i < GetComponent<Transform>().childCount; i++)
        {
            _points[i] = GetComponent<Transform>().GetChild(i);
        }
    }

}
