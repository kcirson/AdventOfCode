using System;
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

            int count = 0;

            foreach (string answer in Input)
            {
                if (!string.IsNullOrEmpty(answer))
                {
                    current.AddAnswer(answer);
                }
                else
                {
                    answerGroups.Add(current);
                    count += current.GetAnswerCount();
                    current = new Group();
                }
            }

            answerGroups.Add(current);

            return answerGroups.Sum(g => g.GetAnswerCount());
        }

        private static int Part2()
        {
            return 0;
        }

        private class Group
        {
            List<char> AnsweredQuestions = new List<char>();

            public Group()
            {

            }

            public void AddAnswer(string answer)
            {
                foreach (char c in answer)
                {
                    if (!AnsweredQuestions.Contains(c))
                        AnsweredQuestions.Add(c);
                }
            }

            public int GetAnswerCount()
            {
                return AnsweredQuestions.Count;
            }
        }
    }
}
