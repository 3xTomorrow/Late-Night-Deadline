using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace GenerateScripts
{
    /// <summary>
    /// This script is meant to generate equations for Math Application within
    /// the game
    /// <remarks>
    /// <b>Author:</b> 3xTomorrow
    /// </remarks>
    /// </summary>
    public class GenerateEquations : MonoBehaviour
    {
        public enum Operation
        {
            Add,
            Subtract,
            Multiply
        }
        
        private static Dictionary<string,int> _additionEquations;
        private static Dictionary<string,int> _subtractEquations;
        private static Dictionary<string,int> _multiplicationEquations;
        private static Dictionary<string,double> _divEquations;
        
        private static List<int> _numbers;
        private static List<int> _divNumbers;

        private void Awake()
        {
            _additionEquations = new Dictionary<string, int>();
            _subtractEquations = new Dictionary<string, int>();
            _multiplicationEquations = new Dictionary<string, int>();
            _divEquations = new Dictionary<string, double>();
            
            _numbers = Enumerable.Range(0, 100).ToList();
            _divNumbers = Enumerable.Range(1, 200).ToList();
        }

        /// <summary>
        /// Generates random addition equations based on a list of numbers (0-100)
        /// </summary>
        /// <returns>Dictionary of equations</returns>
        public static Dictionary<string,int> Generate(Operation operation, int times)
        {
            switch (operation)
            {
                case Operation.Add:
                    if (_additionEquations.Count != 0)
                        _additionEquations.Clear();
                    break;
                case Operation.Subtract:
                    if(_subtractEquations.Count != 0)
                        _subtractEquations.Clear();
                    break;
                case Operation.Multiply:
                    if(_multiplicationEquations.Count != 0)
                        _multiplicationEquations.Clear();
                    break;
            }
            
            ShuffleNumbers();
            Stack<int> numsStack = new Stack<int>(_numbers);
            for(int i =0;i<times;i++)
            {
                int num1 = numsStack.Pop();
                int num2 = numsStack.Pop();
                if (operation == Operation.Add)
                {
                    int solution =  num1 + num2;
                    string equation = num1 + " + " + num2;
                    _additionEquations.Add(equation,solution);
                } else if (operation == Operation.Subtract)
                {
                    int solution =  num1 - num2;
                    string equation = num1 + " - " + num2;
                    _subtractEquations.Add(equation,solution);
                } else if (operation == Operation.Multiply)
                {
                    int solution =  num1 * num2;
                    string equation = num1 + " * " + num2;
                    _multiplicationEquations.Add(equation,solution);
                }
            }

            return operation switch
            {
                Operation.Add => _additionEquations,
                Operation.Subtract => _subtractEquations,
                Operation.Multiply => _multiplicationEquations,
                _ => null
            };
        }

        public static Dictionary<string,double> GenerateDiv(int times)
        {
            if(_divEquations.Count != 0)
                _divEquations.Clear();
            
            ShuffleNumbers(true);
            Queue<int> numsStack = new Queue<int>(_divNumbers);
            for(int i =0;i<times;i++)
            {
                int num1 = numsStack.Dequeue();
                int num2 = numsStack.Dequeue();
                if (num1 > num2)
                {
                    double solution = Math.Round((double) num1 / num2, 2);
                    string equation = num1 + " / " + num2;
                    _divEquations.Add(equation,solution);
                }
            }

            return _divEquations;
        }

        /// <summary>
        /// Shuffles the numbers list to ensure randomness
        /// </summary>
        private static void ShuffleNumbers(bool isDiv = false)
        {
            Random rand = new Random();
            if (!isDiv)
                _numbers = _numbers.OrderBy(x => rand.Next()).ToList();
            else
                _divNumbers = _divNumbers.OrderBy(x => rand.Next()).ToList();
            
        }
    }
}