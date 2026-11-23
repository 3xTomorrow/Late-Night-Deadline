using System;
using System.Collections.Generic;
using System.Linq;
using GenerateScripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace Computer_Scripts
{
    public class AppWindowMath : ApplicationWindow
    {
        [SerializeField] private TMP_Text equationText;
        [SerializeField] private TMP_Text answersCorrectText;
        [SerializeField] private TMP_InputField equationInput;
        
        private Dictionary<string, double> _additionEquationsDictionary;
        private Dictionary<string,double> _subtractionEquations;
        private Dictionary<string,double> _multiplicationEquations;
        private Dictionary<string, double> _divisionEquations;
        
        private KeyValuePair<string, double>[] _equations;
        private bool[] _wasAnswered;

        private int _currentEquation;

        private int _answersCorrect;
        
        private Random _random;

        public override void Awake()
        {
            base.Awake();
            
            _additionEquationsDictionary = new Dictionary<string, double>();
            _subtractionEquations = new Dictionary<string, double>();
            _multiplicationEquations = new Dictionary<string, double>();
            _divisionEquations = new Dictionary<string, double>();
            _random = new Random();
        }
        
        public override void Start()
        {
            base.Start();
            
            _additionEquationsDictionary = GenerateEquations.Generate(GenerateEquations.Operation.Add, 5);
            _equations = DictToPair(_additionEquationsDictionary);
            _wasAnswered = new bool[_equations.Length];
        }

        public override void Update()
        {
            base.Update();
            equationText.text = _equations[_currentEquation].Key;
            answersCorrectText.text = _answersCorrect + "/" + _equations.Length;
        }

        public void NextEquation()
        {
            if (_currentEquation < _equations.Length-1)
            {
                _currentEquation++;
            }
        }

        public void PreviousEquation()
        {
            if (_currentEquation > 0)
            {
                _currentEquation--;
            }
        }
        
        public void SubmitAnswer()
        {
            if (CheckAnswer() && _answersCorrect < _equations.Length && !_wasAnswered[_currentEquation])
            {
                _wasAnswered[_currentEquation] = true;
                _answersCorrect++;
            }
        }

        private KeyValuePair<K, V>[] DictToPair<K, V>(Dictionary<K, V> dictionary)
        {
            KeyValuePair<K, V>[] pairs = new KeyValuePair<K, V>[dictionary.Count];
            int i = 0;
            foreach (KeyValuePair<K, V> pair in dictionary)
            {
                pairs[i] = pair;
                ++i;
            }
            return pairs;
        }

        private bool CheckAnswer()
        {
            return Mathf.Approximately((float)_equations[_currentEquation].Value,
                (float)double.Parse(equationInput.text));
        }
        
    }
}