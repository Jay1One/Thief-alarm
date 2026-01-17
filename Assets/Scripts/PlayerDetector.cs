using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PlayerDetector : MonoBehaviour
{
    public UnityEvent <Player>OnPlayerDetected;
    public UnityEvent <Player>OnPlayerLost;
    
    private Player _player;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            _player = player;
            OnPlayerDetected?.Invoke(player);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            _player = null;
            OnPlayerLost?.Invoke(_player);
        }
    }
}
