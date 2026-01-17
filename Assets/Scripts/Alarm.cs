
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _timeForMaxVolume;
    [SerializeField] private PlayerDetector _playerDetector;

    private float _volumeChangeSpeed;
    private AudioSource _audioSource;
    private float _currentVolume;
    private Coroutine _currentCoroutine;
    private bool _isPlaying;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _volumeChangeSpeed = _maxVolume / _timeForMaxVolume;
    }
    
    public void Play()
    {
        if (_currentCoroutine!=null)
        {
            StopCoroutine(_currentCoroutine);
        }
        
        _currentCoroutine = StartCoroutine(PlayCoroutine());
    }

    public void StopWithFade()
    {
        if (_currentCoroutine!=null)
        {
            StopCoroutine(_currentCoroutine);
        }
        
        _currentCoroutine = StartCoroutine(FadeCoroutine());
    }
    
    private IEnumerator PlayCoroutine()
    {
        if (!_isPlaying)
        {
            _audioSource.Play();
            _isPlaying = true;
        }
        
        while (_currentVolume < _maxVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume,
                _maxVolume, _volumeChangeSpeed * Time.deltaTime);
            
            _audioSource.volume = _currentVolume;
            
            yield return null;
        }
    }

    private IEnumerator FadeCoroutine()
    {
        while (_currentVolume > 0f)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume,
                0f, _volumeChangeSpeed * Time.deltaTime);
            
            _audioSource.volume = _currentVolume;
            
            yield return null;
        }
        
        _audioSource.Stop();
        _isPlaying = false;
    }
}
