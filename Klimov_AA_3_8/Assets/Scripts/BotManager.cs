using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ziggurat;

public class BotManager : MonoBehaviour
{
	
	
	private GameObject _targetForAttack;
	private GameObject _initialTarget;
	private Rigidbody _rigidbody;
	private UnitEnvironment _unitEnvironment;
	
	private IReadOnlyDictionary<AnimationType, string> _configuration;
	
	public ColorBotZiggurat _colorBot;
	
	private float _maxVelocity = 10;
	private float _arrivalDistance = 10;
	
	private bool AmIAlive = true;
	
	
	
	#region  
		
	[SerializeField]private int _healtPoint;
	[SerializeField]private int _moveSpeed;
	[SerializeField]private int _weekDamage;
	[SerializeField]private int _strongDamage;
	[SerializeField]private float _probabilityMiss; 
	[SerializeField]private float _probabilityDoubleDamage;
	[SerializeField]private float _probabilityWeekOrStrongAttack;
	public int HealtPoint { get => _healtPoint; set => _healtPoint = value; }
	public int MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
	public int WeekDamage { get => _weekDamage; set => _weekDamage = value; }
	public int StrongDamage { get => _strongDamage; set => _strongDamage = value; }
	public float ProbabilityMiss { get => _probabilityMiss; set => _probabilityMiss = value; }
	public float ProbabilityDoubleDamage { get => _probabilityDoubleDamage; set => _probabilityDoubleDamage = value; }
	public float ProbabilityWeekOrStrongAttack { get => _probabilityWeekOrStrongAttack; set => _probabilityWeekOrStrongAttack = value; }

	#endregion


	private void Awake()
	{
		_targetForAttack = FindObjectOfType<InitialTarget>().gameObject;
		_unitEnvironment = GetComponent<UnitEnvironment>();
		_rigidbody = GetComponent<Rigidbody>();
		_initialTarget = _targetForAttack;
		_configuration = new Dictionary<AnimationType, string>(Resources.Load<Configuration>("BaseConfiguration").GetDictionary);
	}
	
	private void Update()
	{
		if(AmIAlive)
		{
			FindTarget();
			if(Vector3.Distance(transform.position, _targetForAttack.transform.position) < 2f && _targetForAttack != _initialTarget)
			{
			Attack();
			}
		}
		
		
	}
	
	private void FixedUpdate()
	{
		if(AmIAlive)
		{
			if(Vector3.Distance(transform.position, _targetForAttack.transform.position) >= 2f)
			{
				OnArrival();
			}
		}
	}
	private void OnEnable()
	{
		AmIAlive = true;
	}
	
	
	private Vector3 GetVelocity(IgnoreAxisType axis = IgnoreAxisType.None)
	{
		return UpdateIgnoreAxis(_rigidbody.velocity, axis);
	}
	private void SetVelocity(Vector3 velocity, IgnoreAxisType axis = IgnoreAxisType.None)
	{
		_rigidbody.velocity = UpdateIgnoreAxis(velocity, axis);
	}
	private Vector3 UpdateIgnoreAxis(Vector3 velocity, IgnoreAxisType axis)
	{
		switch (axis)
		{
			case IgnoreAxisType.None:
				return velocity;
			case IgnoreAxisType.X:
				velocity.x = 0;
				break;
			case IgnoreAxisType.Y:
				velocity.y = 0;
				break;
			case IgnoreAxisType.Z:
				velocity.z = 0;
				break;
		}
		
		return velocity;
	}

	
	private void OnArrival()
	{
		if(Vector3.Distance(transform.position, _targetForAttack.transform.position) < 2f)
		{
			_unitEnvironment.Moving(0);
		}
		else
		{
			_unitEnvironment.Moving(0.1f);
		}
		Vector3 desiredVelocity = (_targetForAttack.transform.position - transform.position).normalized * _maxVelocity;
		float sqrLengthDesiredVelocity = desiredVelocity.sqrMagnitude;
		if(sqrLengthDesiredVelocity < _arrivalDistance * _arrivalDistance)
		{
			desiredVelocity /= _arrivalDistance;
		}
		Vector3 steeringVelocity = Vector3.ClampMagnitude(desiredVelocity - GetVelocity(IgnoreAxisType.Y), _maxVelocity) / _rigidbody.mass;
		Vector3 velocity = Vector3.ClampMagnitude(GetVelocity() + steeringVelocity, _maxVelocity);
		SetVelocity(velocity * _moveSpeed);
	}
	
	private void FindTarget()
	{
		transform.LookAt(UpdateIgnoreAxis(_targetForAttack.transform.position, IgnoreAxisType.Y) + new Vector3(0, transform.position.y, 0));
		if(Collections.botPool.Count(b => b.activeSelf) == 0)
		{
			_targetForAttack = _initialTarget;
			return;
		}
		foreach(GameObject bot in Collections.botPool.Where(b => b.activeSelf == true))
		{
			if(gameObject != bot && _colorBot != bot.GetComponent<BotManager>()._colorBot && Vector3.Distance(transform.position, bot.transform.position) <= _arrivalDistance)
			{
				_targetForAttack = bot;
				break;
			}
			else
			{
				_targetForAttack = _initialTarget;
			}
		}
	}
	
	
	
	private void Attack()
	{
		if(Random.value < ProbabilityWeekOrStrongAttack)
			_unitEnvironment.StartAnimation(_configuration[AnimationType.FastAttack]);
		else
			_unitEnvironment.StartAnimation(_configuration[AnimationType.StrongAttack]);
	}
	
	private void DealingWeekDamage()
	{
		if (_targetForAttack.GetComponent<BotManager>() != null)
		{
			_targetForAttack.GetComponent<BotManager>()	.TakeDamage(_weekDamage * CalculateProbabilityMiss() * CalculateProbabilityDoubleDamage());
		}
		else
		{
			Debug.Log("Error: Target does not have BotManager component");
		}
	}
	private void DealingStrongDamage()
	{
		if (_targetForAttack.GetComponent<BotManager>() != null)
		{
			_targetForAttack.GetComponent<BotManager>()	.TakeDamage(_strongDamage * CalculateProbabilityMiss() * CalculateProbabilityDoubleDamage());
		}
		else
		{
			Debug.Log("Error: Target does not have BotManager component");
		}
	}
	
	
	
	public void TakeDamage(int damage)
	{
		_healtPoint -= damage;
		Death();
	}
	
	private void Death()
	{
		if(_healtPoint <= 0)
			AmIAlive = false;
			_unitEnvironment.StartAnimation(_configuration[AnimationType.Die]);
	}
	
	private int CalculateProbabilityMiss()
	{
		if(Random.value < _probabilityMiss)
			return 0;
		else
			return 1;
	}
	
	private int CalculateProbabilityDoubleDamage()
	{
		if(Random.value < _probabilityDoubleDamage)
			return 1;
		else
			return 2;
	}
	
	private void PerformAction(string action)
	{
		switch(action)
		{
			case "die":
				gameObject.SetActive(false);
				break;
			case "fast attack":
				DealingWeekDamage();
				break;
			case "strong attack":
				DealingStrongDamage();
				break;
		}
	}
}