using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;


public class SpellChecker 
{
    private WordDictionary wordDictionary;
    private Spelling spelling;
    // Start is called before the first frame update
    public SpellChecker()
    {
        wordDictionary = new WordDictionary();
        wordDictionary.DictionaryFile = "Assets/Resources/Dictionaries/en-US.dic";
        wordDictionary.Initialize();

        // Initialize the spell checker
        spelling = new Spelling();
        spelling.Dictionary = wordDictionary;
    }
    public bool CheckWord(string word)
    {
        // Check if the word is spelled correctly
        return spelling.TestWord(word);
    }

}
