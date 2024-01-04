using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ziggurat;

public class CanvasManager : MonoBehaviour
{
	private GateManager _gateManager;
	private TMP_InputField _hpField;
	private TMP_InputField _speedField;
	private TMP_InputField _weekAttackField;
	private TMP_InputField _strongAttackField;
	private Slider _probabilityMissField;
	private Slider _probabilityDoubleDamageField;
	private Slider _probabilityWeekOrStrongAttack;
	
	public void OnOpeningCanvas()
	{
		gameObject.SetActive(true);
	}
	
	private void Awake()
	{
		_gateManager = GetComponentInParent<GateManager>();
		_hpField = GetComponentInChildren<InputFieldScript_Health>().gameObject.GetComponent<TMP_InputField>();
		_speedField = GetComponentInChildren<InputFieldScript_MoveSpeed>().gameObject.GetComponent<TMP_InputField>();
		_weekAttackField = GetComponentInChildren<InputFieldScript_DamageWeekAttack>().gameObject.GetComponent<TMP_InputField>();
		_strongAttackField = GetComponentInChildren<InputFieldScript_DamageStrongAttack>().gameObject.GetComponent<TMP_InputField>();
		_probabilityMissField  = GetComponentInChildren<SliderScript_ProbabilityMiss>().gameObject.GetComponent<Slider>();
		_probabilityDoubleDamageField  = GetComponentInChildren<SliderScripts_ProbabilityDoubleDamage>().gameObject.GetComponent<Slider>();
		_probabilityWeekOrStrongAttack = GetComponentInChildren<SliderScripts_ProbabilityWeekOrStrongAttack>().gameObject.GetComponent<Slider>();
	}
	
	private void OnEnable()
	{
		_hpField.onEndEdit.AddListener(GetValueHealth);
		_speedField.onEndEdit.AddListener(GetValueSpeed);
		_weekAttackField.onEndEdit.AddListener(GetValueWeekAttack);
		_strongAttackField.onEndEdit.AddListener(GetValueStrongAttack);
		_probabilityMissField.onValueChanged.AddListener(GetValueMiss);
		_probabilityDoubleDamageField.onValueChanged.AddListener(GetValueDoubleDamage);
		_probabilityWeekOrStrongAttack.onValueChanged.AddListener(GetValueWeekOrStrongAttack);
	}
	private void OnDisable()
	{
		_hpField.onEndEdit.RemoveListener(GetValueHealth);
		_speedField.onEndEdit.RemoveListener(GetValueSpeed);
		_weekAttackField.onEndEdit.RemoveListener(GetValueWeekAttack);
		_strongAttackField.onEndEdit.RemoveListener(GetValueStrongAttack);
		_probabilityMissField.onValueChanged.RemoveListener(GetValueMiss);
		_probabilityDoubleDamageField.onValueChanged.RemoveListener(GetValueDoubleDamage);
		_probabilityWeekOrStrongAttack.onValueChanged.RemoveListener(GetValueWeekOrStrongAttack);
	}
	
	private void GetValueHealth(string value)
	{
		int intValue;
		if (int.TryParse(value, out intValue))
		{
			_gateManager.HealtPointBot = intValue;
		}
	}
	
	private void GetValueSpeed(string value)
	{
		int intValue;
		if (int.TryParse(value, out intValue))
		{
			_gateManager.MoveSpeedBot = intValue;
		}
	}
	
	private void GetValueWeekAttack(string value)
	{
		int intValue;
		if (int.TryParse(value, out intValue))
		{
			_gateManager.WeekDamageBot = intValue;
		}
	}
	
	private void GetValueStrongAttack(string value)
	{
		int intValue;
		if (int.TryParse(value, out intValue))
		{
			_gateManager.StrongDamageBot = intValue;
		}
	}
	
	private void GetValueMiss(float value)
	{
		_gateManager.ProbabilityMissBot = value;
	}
	
	private void GetValueDoubleDamage(float value)
	{
		_gateManager.ProbabilityDoubleDamageBot = value;
		
	}
	
	private void GetValueWeekOrStrongAttack(float value)
	{
		_gateManager.ProbabilityWeekOrStrongAttack = value;
		
	}
}
	
	

