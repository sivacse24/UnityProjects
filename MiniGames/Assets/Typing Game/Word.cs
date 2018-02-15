using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word  {

	public string word;
	private int typeIndex;

	private WordDisplay displayTxt;
	public Word(string _word,WordDisplay _displayTxt)
	{
		word = _word;
		displayTxt = _displayTxt;
		displayTxt.SetWord(word);
		typeIndex = 0;
	}

	public char getNextLetter()
	{
		return word[typeIndex];
	}

	public void updateTyepIndex()
	{
		typeIndex++;
		displayTxt.removeLetter();
	}

	public bool hasWordEnd()
	{
		if(typeIndex >= word.Length)
		{
			displayTxt.removeWord();
			return true;
		}
		return false;
	}
}
