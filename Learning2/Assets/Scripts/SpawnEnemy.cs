using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Transform))]
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private GameObject _enemy;
    private Transform _transform;
    private int _currentPoint = 0;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _points = new Transform[_transform.childCount];
        SettingPoint();
        StartCoroutine(Initialization());
    }

    private IEnumerator Initialization()
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
        for (int i = 0; i < _transform.childCount; i++)
        {
            _points[i] = _transform.GetChild(i);
        }
    }

}
