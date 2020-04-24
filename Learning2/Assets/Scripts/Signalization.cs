using System.Collections;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer), typeof (AudioSource))]
public class Signalization : MonoBehaviour
{
    [SerializeField] private Transform _characterTransform;
    private SpriteRenderer _colorChanger;
    private AudioSource _audioClip;
    private Coroutine _light;
    private bool _isDoorOpen = false;

    private void Start()
    {
        _colorChanger = GetComponent<SpriteRenderer>();
        _audioClip = GetComponent<AudioSource>();
    }

    private void Update()
    {
        IsDoorOpen(_isDoorOpen);
    }

    private void IsDoorOpen(bool doorOpen)
    {
        if (doorOpen)
        {
            if (Input.GetKeyDown(KeyCode.E) && _characterTransform.position.z == -1)
            {
                PlayerEnter(0.5f, true);
                StartCoroutine(VolimeSound(0.5f, 5));
                _light = StartCoroutine(FlashingLight(0.5f));
            }
            else if (Input.GetKeyDown(KeyCode.E) && _characterTransform.position.z == 0.5f)
            {
                PlayerEnter(-1, false);
                StartCoroutine(VolimeSound(-1, 5));
                StopCoroutine(_light);
                _colorChanger.color = Color.white;
            }
        }
    }

    private void PlayerEnter(float changeLayer, bool walls)
    {
        _characterTransform.position = new Vector3(_characterTransform.position.x, _characterTransform.position.y, changeLayer);
        gameObject.transform.Find("Walls").gameObject.SetActive(walls);
    }

    private IEnumerator VolimeSound(float changeLayer, float durationChangeVolume)
    {
        _audioClip.Play();
        for (int i = 1; i <= 10; i++)
        {
            float audioVolume = changeLayer == 0.5f ? (_audioClip.volume = (0 + i) * 0.1f) : (_audioClip.volume = (10 - i) * 0.1f);                         
            if (audioVolume == 0)
                _audioClip.Stop();
            yield return new WaitForSeconds(1 / durationChangeVolume);
        }
    }

    private IEnumerator FlashingLight(float changeLayer)
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