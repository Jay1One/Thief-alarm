using UnityEngine;

public class Player : MonoBehaviour
{ 
    const string AnimatorSpeed = "speed";
    const string AnimatorIsFacingLeft = "isFacingLeft";
    
    [SerializeField] private float _moveSpeed = 5f;
    
    private bool _isFacingLeft = false;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        int movementDirection = 0;
        
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection ++;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementDirection --; 
        }

        if (movementDirection!=0)
        {
            _isFacingLeft = movementDirection < 0;
            _animator.SetBool(AnimatorIsFacingLeft, _isFacingLeft);
        }
        
        _animator.SetInteger(AnimatorSpeed, movementDirection);
        
        transform.position += new Vector3(movementDirection * _moveSpeed * Time.deltaTime, 0, 0);
    }
}
