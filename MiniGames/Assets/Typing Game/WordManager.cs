using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour {

	public List<Word> words;
	private Word activeWord=null;

	public WordSpawner wrodTxtSpawner;

	private float wordDelay	= 1.5f;
	private float nextWordTime = 0f;
	// Use this for initialization
	void Start () {
		/*addWord();
		addWord();
		addWord();*/

	}

	public void addWord()
	{
		Word word = new Word(WordGenerator.GetRandomWord(),wrodTxtSpawner.SpawnWord());
		words.Add(word);
	}

	public void TypeLetter(char typedLetter)
	{
		if(activeWord==null)
		{
			//first letter typed
			foreach(Word wrd in words)
			{
				if(wrd.getNextLetter()==typedLetter)
				{				
					wrd.updateTyepIndex();
					activeWord = wrd;	
					break	;	
				}

			}
		}
		else 
		{
			if(activeWord.getNextLetter()==typedLetter)
			{
				activeWord.updateTyepIndex();
			}
		}

		if(activeWord!=null && activeWord.hasWordEnd())
		{
			words.Remove(activeWord);
			activeWord = null;
		}

	}
	
	// Update is called once per frame
	void Update () {
		print(Time.deltaTime+"--"+nextWordTime);
		if(Time.time>=nextWordTime)
		{
			addWord();
			nextWordTime = Time.time+wordDelay;
		}
		
	}
}
