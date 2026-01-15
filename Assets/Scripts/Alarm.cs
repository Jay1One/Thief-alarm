using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _timeForMaxVolume;

    private float _volumeChangeSpeed;
    private AudioSource _audioSource;
    private float _currentVolume;
    private Player _player;
    private bool _isPlaying;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _volumeChangeSpeed = _maxVolume / _timeForMaxVolume;
    }

    private void Update()
    {
        if (_player == null)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume,
                0f, _volumeChangeSpeed * Time.deltaTime);
            
            if (_currentVolume==0f && _isPlaying)
            {
                _audioSource.Stop();
                _isPlaying = false;
            }
        }
        
        else
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume,
                _maxVolume, _volumeChangeSpeed * Time.deltaTime);
        }
        
        _audioSource.volume = _currentVolume;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            if (!_isPlaying)
            {
                _audioSource.Play();
                _isPlaying = true;
            }
            
            _player = player;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            _player = null;
        }
    }
}
