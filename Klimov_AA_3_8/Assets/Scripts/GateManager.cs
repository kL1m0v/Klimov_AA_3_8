using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class GateManager : MonoBehaviour
	{
		[SerializeField] public float _delayBetweenSpawn;
		[SerializeField] private Material _botMaterial;
		
		[SerializeField] private ColorBotZiggurat _colorZiggurat;
		
		private List<GameObject> _personalBots;
		
		#region  
		
		private int _healtPointBot = 100;
		private int _moveSpeedBot = 1;
		private int _weekDamageBot = 15;
		private int _strongDamageBot = 25;
		private float _probabilityMissBot = 0.5f; 
		private float _probabilityDoubleDamageBot = 0.5f;
		private float _probabilityWeekOrStrongAttack = 0.5f;

		public int HealtPointBot { get => _healtPointBot; set => _healtPointBot = value; }
		public int MoveSpeedBot { get => _moveSpeedBot; set => _moveSpeedBot = value; }
		public int WeekDamageBot { get => _weekDamageBot; set => _weekDamageBot = value; }
		public int StrongDamageBot { get => _strongDamageBot; set => _strongDamageBot = value; }
		public float ProbabilityMissBot { get => _probabilityMissBot; set => _probabilityMissBot = value; }
		public float ProbabilityDoubleDamageBot { get => _probabilityDoubleDamageBot; set => _probabilityDoubleDamageBot = value; }
		public float ProbabilityWeekOrStrongAttack { get => _probabilityWeekOrStrongAttack; set => _probabilityWeekOrStrongAttack = value; }

		public void SetHP(int hp)
		{
			_healtPointBot = hp;
		}

		#endregion

		private void Awake()
		{
			_personalBots = new();
		}
		
		
		private void WakeUpBot(List<GameObject> bots)
		{
			foreach(GameObject bot in bots) 
			{
				if (bot.activeSelf == false)
				{
					bot.transform.position = transform.position + new Vector3(0, 10f, 0);
					bot.GetComponent<MeshRenderer>().material = _botMaterial;
					BotManager movebot = bot.GetComponent<BotManager>();
					movebot._colorBot = _colorZiggurat;
					movebot.HealtPoint = HealtPointBot;
					movebot.MoveSpeed = MoveSpeedBot;
					movebot.WeekDamage = WeekDamageBot;
					movebot.StrongDamage = StrongDamageBot;
					movebot.ProbabilityMiss = ProbabilityMissBot;
					movebot.ProbabilityDoubleDamage = ProbabilityDoubleDamageBot;
					movebot.ProbabilityWeekOrStrongAttack = ProbabilityWeekOrStrongAttack;
					bot.SetActive(true);
					_personalBots.Add(bot);
					return;
				}
			}
		}
	
		public IEnumerator SpawnBots(List<GameObject> bots)
		{
			while(_delayBetweenSpawn > 0)
			{
				WakeUpBot(bots);
				yield return new WaitForSeconds(_delayBetweenSpawn);
			}
		}
		
	}
}