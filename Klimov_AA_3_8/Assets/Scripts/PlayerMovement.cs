using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _speed;
	
	private PlayerInput _input;
	
	void Awake() 
	{
		_input = new();
	}

	void OnEnable()
	{
		_input.Enable();
	}
	
	void OnDisable()
	{
		_input.Disable();
	}

	
	void Update()
	{
		Move();
	}
	
	private void Move()
	{
		transform.position += _input.PlayerMap.Move.ReadValue<Vector3>() * Time.deltaTime * _speed;
	}
		
}
