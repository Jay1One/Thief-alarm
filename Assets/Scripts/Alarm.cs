
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
    private bool _isPlaying;
    private bool _isStopping;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _volumeChangeSpeed = _maxVolume / _timeForMaxVolume;
    }
    
    public void Play()
    {
        if (!_isPlaying)
        {
            StartCoroutine(PlayCoroutine());
        }
        
        else
        {
            _isStopping = false;
        }
    }

    public void StopWithFade()
    {
        _isStopping = true;
    }
    
    private IEnumerator PlayCoroutine()
    {
        _isPlaying = true;
        _audioSource.Play();
        
        do
        {
            if (_isStopping)
            {
                _currentVolume = Mathf.MoveTowards(_currentVolume,
                    0f, _volumeChangeSpeed * Time.deltaTime);
            }
        
            else
            {
                _currentVolume = Mathf.MoveTowards(_currentVolume,
                    _maxVolume, _volumeChangeSpeed * Time.deltaTime);
            }
        
            _audioSource.volume = _currentVolume;
            
            yield return null;
        } 
        while (_currentVolume>0f);
        
        _audioSource.Stop();
        _isStopping = false;
        _isPlaying = false;
    }
}
