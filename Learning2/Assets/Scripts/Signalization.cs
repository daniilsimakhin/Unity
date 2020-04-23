using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signalization : MonoBehaviour
{
    [SerializeField] private Transform _character;
    private SpriteRenderer _colorChanger;
    private AudioSource _audioClip;
    private Coroutine _light;
    private bool _isDoorOpen = false;

    private void Start()
    {
        _colorChanger = GetComponent<SpriteRenderer>();
        _audioClip = GetComponent<AudioSource>();
    }

    void Update()
    {
        _IsDoorOpen(_isDoorOpen);
    }

    private void _IsDoorOpen(bool doorOpen)
    {
        if (doorOpen)
        {
            if (Input.GetKeyDown(KeyCode.E) && _character.position.z == -1)
            {
                _PlayerEnter(0.5f, true);
                StartCoroutine(_VolimeSound(0.5f, 5));
                _light = StartCoroutine(_FlashingLight(0.5f));
            }
            else if (Input.GetKeyDown(KeyCode.E) && _character.position.z == 0.5f)
            {
                _PlayerEnter(-1, false);
                StartCoroutine(_VolimeSound(-1, 5));
                StopCoroutine(_light);
                _colorChanger.color = Color.white;
            }
        }
    }

    private void _PlayerEnter(float changeLayer, bool walls)
    {
        _character.position = new Vector3(_character.position.x, _character.position.y, changeLayer);
        gameObject.transform.Find("Walls").gameObject.SetActive(walls);
    }


    private IEnumerator _VolimeSound(float changeLayer, float durationChangeVolume)
    {
        _audioClip.Play();
        for (int i = 0; i <= 10; i++)
        {
            //if (changeLayer == 0.5f)
            //{
            //    _audioClip.volume = i * 0.1f;
            //}
            //else
            //{
            //    _audioClip.volume = (10 - i) * 0.1f;
            //    if (_audioClip.volume == 0)
            //    {
            //        _audioClip.Stop();
            //    }
            //}
            float j = changeLayer == 0.5f ? (_audioClip.volume = i * 0.1f) : ((_audioClip.volume = (10 - i) * 0.1f) == 0) ? 10: -10;
            if (j == 10)
                _audioClip.Stop();
            yield return new WaitForSeconds(1 / durationChangeVolume);
        }
    }

    private IEnumerator _FlashingLight(float changeLayer)
    {
        while (changeLayer == 0.5f)
        {
            if (_colorChanger.color == Color.white)
            {
                _colorChanger.color = Color.red;
            }
            else if (_colorChanger.color == Color.red)
            {
                _colorChanger.color = Color.white;
            }
            //var x = changeLayer == 0.5f ? (_colorChanger.color  == Color.white) ? (_colorChanger.color = Color.red) : (_colorChanger.color = Color.white);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isDoorOpen = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isDoorOpen = false;
    }
}