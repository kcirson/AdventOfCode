﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    public static class Day6
    {
        private static List<string> Input =>
        InputHelper.GetInputString(2020, 6).Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();

        public static void Run()
        {
            Console.WriteLine("Part 1:");
            Console.WriteLine(Part1());
            Console.WriteLine();
            Console.WriteLine("Part 2:");
            Console.WriteLine(Part2());
        }

        private static int Part1()
        {
            List<Group> answerGroups = new List<Group>();
            Group current = new Group();

            foreach (string answer in Input)
            {
                if (!string.IsNullOrEmpty(answer))
                {
                    current.AddAnswer(answer);
                }
                else
                {
                    answerGroups.Add(current);
                    current = new Group();
                }
            }

            answerGroups.Add(current);

            return answerGroups.Sum(g => g.GetAnyAnswerCount());
        }

        private static int Part2()
        {
            List<Group> answerGroups = new List<Group>();
            Group current = new Group();

            foreach (string answer in Input)
            {
                if (!string.IsNullOrEmpty(answer))
                {
                    current.AddAnswer(answer);
                }
                else
                {
                    answerGroups.Add(current);
                    current = new Group();
                }
            }

            answerGroups.Add(current);

            return answerGroups.Sum(g => g.GetEveryoneSameAnswerCount());
        }

        private class Group
        {
            List<char> AnsweredQuestions = new List<char>();
            Dictionary<char, int> Answers = new Dictionary<char, int>();
            int GroupSize { get; set; } = 0;

            public Group()
            {

            }

            public void AddAnswer(string answer)
            {
                GroupSize++;

                foreach (char c in answer)
                {
                    if (!Answers.ContainsKey(c))
                    {
                        Answers.Add(c, 1);
                    }
                    else
                    {
                        Answers[c] = Answers[c] + 1;
                    }
                }
            }

            public int GetAnyAnswerCount()
            {
                return Answers.Keys.Count;
            }

            public int GetEveryoneSameAnswerCount()
            {
                return Answers.Where(pair => pair.Value == GroupSize).Count();
            }
        }
    }
}
