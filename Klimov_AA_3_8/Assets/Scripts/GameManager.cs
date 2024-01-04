using UnityEngine;

namespace Ziggurat
{
    public class GameManager : MonoBehaviour
	{
	
		[SerializeField] private GameObject _botPrefab;
	
		[SerializeField] private byte _poolSize;
	
		private GateManager[] _gates;
		
		
		private void Awake()
		{
			FillPool();
			_gates = FindObjectsOfType<GateManager>();
			Collections.canvases = FindObjectsOfType<Canvas>();
		}
	
		private void Start()
		{
			foreach(GateManager gate in _gates)
			{
				StartCoroutine(gate.SpawnBots(Collections.botPool));
			}
		}
		
		
		private void FillPool()
		{
			Collections.botPool = new();
			for(int i = 0; i <= _poolSize; i++)
			{
				GameObject bot = Instantiate(_botPrefab);
				bot.SetActive(false);
				Collections.botPool.Add(bot);
			}
		}

		
	}
	
}
