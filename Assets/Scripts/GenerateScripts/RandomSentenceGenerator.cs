using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


/*
 * Generated using Claude AI w/ tweaks from me (3xTomorrow)
 * 
 * Reasoning: This would have taken time out of actually making the game to
 * generate these sentences
 * 
 * Prompt: Can you make me a short C# script that I can use in Unity that
 * generates random sentences and puts them into a list.
 */
namespace GenerateScripts
{
    public class RandomSentenceGenerator : MonoBehaviour
    {
        
        private static readonly string SubjectsFile = "subjects";
        private static readonly string VerbsFile = "verbs";
        private static readonly string ObjectsFile = "objects";
        private static readonly string EndingsFile = "endings";

        // The generated sentences end up here
        private static readonly List<string> Sentences = new List<string>();

        private static string[] _subjects;
        private static string[] _verbs;
        private static string[] _objects;
        private static string[] _endings;

        private static int[] _fileSizes;
        
        private void Awake()
        {
            LoadWordLists();
        }

        /// <summary>
        /// Returns the generated sentences.
        /// Make sure that you ran <see cref="RandomSentenceGenerator.GenerateSentences">GenerateSentences</see>
        /// at least once before calling this method or else this method will return null.
        /// </summary>
        /// <returns>List of sentences</returns>
        public static List<string> GetSentences()
        {
            return Sentences;
        }
        
        /// <summary>
        /// Generates random sentences based on the text files found in the Resources folder.
        /// Make sure to run me before calling <see cref="RandomSentenceGenerator.GetSentences">GetSentences</see>
        /// </summary>
        /// <param name="sentenceCount">How many sentences you want made</param>
        [ContextMenu("Generate Sentences")]
        public static void GenerateSentences(int sentenceCount)
        {
            if (sentenceCount < _fileSizes.Min())
            {
                // Lets the context menu work even if Awake hasn't run yet
                if (_subjects == null) LoadWordLists();

                Sentences.Clear();

                for (int i = 0; i < sentenceCount; i++)
                {
                    Sentences.Add($"{Pick(_subjects)} {Pick(_verbs)} {Pick(_objects)} {Pick(_endings)}");
                }
            }
            else
            {
                Debug.LogError("Too many sentences!");
            }

        }

        private static void LoadWordLists()
        {
            _subjects = LoadList(SubjectsFile);
            _verbs = LoadList(VerbsFile);
            _objects = LoadList(ObjectsFile);
            _endings = LoadList(EndingsFile);
            
            _fileSizes = new []{_subjects.Length, _verbs.Length, _objects.Length, _endings.Length};
        }

        private static string[] LoadList(string fileName)
        {
            TextAsset asset = Resources.Load<TextAsset>(fileName);

            if (asset == null)
            {
                Debug.LogError($"Could not find '{fileName}.txt' in a Resources folder.");
                return new[] { "???" };
            }

            // Split on new lines, trim whitespace, and skip blank lines
            string[] lines = asset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> words = new List<string>();

            foreach (string line in lines)
            {
                string trimmed = line.Trim();
                if (trimmed.Length > 0)
                {
                    words.Add(trimmed);
                }
            }

            return words.ToArray();
        }

        private static string Pick(string[] words)
        {
            return words[UnityEngine.Random.Range(0, words.Length)];
        }
    }

}