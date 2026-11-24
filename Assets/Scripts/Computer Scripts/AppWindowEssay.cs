using System.Collections.Generic;
using Computer_Scripts;
using GenerateScripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Computer_Scripts
{
    public class AppWindowEssay : ApplicationWindow
    {
        [SerializeField] private TMP_Text essayText;
        [SerializeField] private TMP_Text answeredText;
        [SerializeField] private TMP_InputField essayInput;

        [SerializeField] private int sentenceCount;
        
        private string[] _sentences;
        private bool[] _wasAnswered;
        
        private int _currentSentence;
        private int _answersCorrect;

        public override void Awake()
        {
            base.Awake();
            _sentences = new string[sentenceCount];
        }
        
        public override void Start()
        {
            base.Start();
            RandomSentenceGenerator.GenerateSentences(sentenceCount);
            _sentences = RandomSentenceGenerator.GetSentences().ToArray();
            _wasAnswered = new bool[_sentences.Length];
        }

        public override void Update()
        {
            base.Update();
            essayText.text = _sentences[_currentSentence];
            answeredText.text = _answersCorrect + " / " + sentenceCount;
        }

        public void PreviousSentence()
        {
            if(_currentSentence > 0)
            {
                _currentSentence--;
            }
        }

        public void NextSentence()
        {
            if (_currentSentence < _sentences.Length - 1)
            {
                _currentSentence++;
            }
        }

        public void SubmitAnswer()
        {
            if (CheckAnswer() && _answersCorrect < sentenceCount && !_wasAnswered[_currentSentence])
            {
                _wasAnswered[_currentSentence] = true;
                _answersCorrect++;
            }
        }

        private bool CheckAnswer()
        {
            return essayInput.text == _sentences[_currentSentence];
        }
    }
}
