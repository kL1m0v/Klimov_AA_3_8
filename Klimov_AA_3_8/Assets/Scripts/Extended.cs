﻿using RotaryHeart.Lib.SerializableDictionary;

namespace Ziggurat
{
    public enum AnimationType : byte
	{
		Move = 0,
		FastAttack = 1,
		StrongAttack = 2,
		Die = 3
	}

	[System.Flags]
	public enum IgnoreAxisType : byte
	{
		None = 0,
		X = 1,
		Y = 2,
		Z = 4
	}

	[System.Serializable]
	public class AnimationKeyDictionary : SerializableDictionaryBase<AnimationType, string> { }
	
	public enum ColorBotZiggurat : byte
	{
		Red,
		Green,
		Blue
	}
}
