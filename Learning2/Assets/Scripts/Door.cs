using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _character;
    private SpriteRenderer _colorChanger;
    private AudioSource _audioClip;
    private bool _inDoor = false;
    private bool _inHome = false;
    private Coroutine _light;

    private void Start()
    {
        _colorChanger = GetComponent<SpriteRenderer>();
        _audioClip = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_inDoor && Input.GetKeyDown(KeyCode.E) && !_inHome)
        {
            _character.position = new Vector3(_character.position.x, _character.position.y, 0.5f);
            gameObject.transform.Find("Walls").gameObject.SetActive(true);
            _inHome = true;
            StartCoroutine(_VolimeSound(_inHome));
            _light = StartCoroutine(_FlashingLight(_inHome));
        } 
        else if (_inDoor &&  Input.GetKeyDown(KeyCode.E) && _inHome)
        {           
            _character.position = new Vector3(_character.position.x, _character.position.y, -1);
            gameObject.transform.Find("Walls").gameObject.SetActive(false);
            _inHome = false;
            StopCoroutine(_light);
            _colorChanger.color = Color.white;
            StartCoroutine(_VolimeSound(_inHome));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inDoor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _inDoor = false;
    }

    private IEnumerator _VolimeSound(bool check)
    {
        if (check)
        {
            _audioClip.Play();
            for (int i = 0; i <= 10; i++)
            {
                _audioClip.volume = i * 0.1f;
                yield return new WaitForSeconds(0.25f);
            }
        }
        else if (!check)
        {
            for (int i = 9; i >= 0; i--)
            {
                _audioClip.volume = i * 0.1f;
                yield return new WaitForSeconds(0.25f);
            }
            _audioClip.Stop();
        }
    }

    private IEnumerator _FlashingLight(bool check)
    {
        while (check)
        {            
            if (_colorChanger.color == Color.white)
            {
                _colorChanger.color = Color.red;
            }
            else if (_colorChanger.color == Color.red)
            {
                _colorChanger.color = Color.white;
            }    
            yield return new WaitForSeconds(0.2f);
        }        
    }
}
