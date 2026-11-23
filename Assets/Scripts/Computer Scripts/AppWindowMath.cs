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
        
        private Dictionary<string, int> _additionEquations;
        private Dictionary<string,int> _subtractionEquations;
        private Dictionary<string,int> _multiplicationEquations;
        private Dictionary<string, double> _divisionEquations;
        
        private Random _random;

        public override void Awake()
        {
            base.Awake();
            
            _additionEquations = new Dictionary<string, int>();
            _subtractionEquations = new Dictionary<string, int>();
            _multiplicationEquations = new Dictionary<string, int>();
            _divisionEquations = new Dictionary<string, double>();
            _random = new Random();
        }
        
        public override void Start()
        {
            base.Start();
            
            _additionEquations = GenerateEquations.Generate(GenerateEquations.Operation.Add, 5);
        }
        
    }
}