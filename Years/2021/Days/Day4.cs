namespace AdventOfCode._2021;

public class Day4 : ISolution
{
    private static List<string> Input =>
    InputHelper.GetListString(2021, 4);

    //private static List<string> Input =>
    //    new List<string>
    //    {
    //        "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
    //        "",
    //        "22 13 17 11  0",
    //        " 8  2 23  4 24",
    //        "21  9 14 16  7",
    //        " 6 10  3 18  5",
    //        " 1 12 20 15 19",
    //        "",
    //        " 3 15  0  2 22",
    //        " 9 18 13 17  5",
    //        "19  8  7 25 23",
    //        "20 11 10 24  4",
    //        "14 21 16 12  6",
    //        "",
    //        "14 21 17 24  4",
    //        "10 16 15  9 19",
    //        "18  8 23 26 20",
    //        "22 11 13  6  5",
    //        " 2  0 12  3  7",
    //    };

    public void Run()
    {
        Console.WriteLine("Part 1:");
        Console.WriteLine(Part1());
        Console.WriteLine();
        Console.WriteLine("Part 2:");
        Console.WriteLine(Part2());
    }

    private static int Part1()
    {
        string bingoBalls = Input[0];
        List<BingoCard> cards = GetCards();

        string[] draws = bingoBalls.Split(',');
        bool stop = false;

        for (int i = 0; i < draws.Length; i++)
        {
            foreach (BingoCard card in cards)
            {
                if (int.TryParse(draws[i], out int ball))

                    card.CheckNumber(ball, i);

                if (card.HasBingo)
                {
                    stop = true;
                    break;
                }
            }

            if (stop)
                break;
        }

        int countWinningBoards = cards.Count(card => card.HasBingo);

        BingoCard bingoCard = cards.Where(card => card.Score == cards.Max(card => card.Score)).FirstOrDefault();

        return bingoCard.Score;
    }

    private static int Part2()
    {
        string bingoBalls = Input[0];
        List<BingoCard> cards = GetCards();

        string[] draws = bingoBalls.Split(',');

        for (int i = 0; i < draws.Length; i++)
        {
            foreach (BingoCard card in cards)
            {
                if (!card.HasBingo)
                {
                    if (int.TryParse(draws[i], out int ball))
                        card.CheckNumber(ball, i);
                }
            }

            if (!cards.Any(card => !card.HasBingo))
                break;
        }

        BingoCard lastToWin = cards.Where(card => card.WonAtDraw == cards.Max(card => card.WonAtDraw)).FirstOrDefault();

        return lastToWin.Score;
    }

    private static List<BingoCard> GetCards()
    {
        List<BingoCard> cards = new();
        int rowNumber = 0;
        BingoCard card = null;

        for (int i = 1; i < Input.Count; i++)
        {
            if (Input[i] == "")
            {
                if (card == null)
                {
                    card = new BingoCard();
                    rowNumber = 0;
                }
                else
                {
                    cards.Add(card);
                    card = new BingoCard();
                    rowNumber = 0;
                }
            }
            else
            {
                card.AddRow(rowNumber, Input[i]);
                rowNumber++;
            }
        }

        cards.Add(card);

        return cards;
    }
}

public class BingoCard
{
    public BingoValue[,] Card { get; set; }
    public List<int> CheckedNumbers { get; set; }
    public List<int> UncheckedNumbers { get; set; }

    private int _wonAtDraw { get; set; }
    public int WonAtDraw { get { return _wonAtDraw; } }

    private int _winningBall { get; set; }
    public int WinningBall { get { return _winningBall; } }

    private bool _hasBingo { get; set; } = false;
    public bool HasBingo { get { return _hasBingo; } }

    private int _score { get; set; }
    public int Score { get { return _score; } }

    public BingoCard()
    {
        Card = new BingoValue[5, 5];
        CheckedNumbers = new();
        UncheckedNumbers = new();
    }

    public void AddRow(int rowNumber, string values)
    {
        string[] valueSplit = values.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (valueSplit.Length != 5)
            throw new Exception("check input");

        for (int i = 0; i < valueSplit.Length; i++)
        {
            if (int.TryParse(valueSplit[i], out int number))
            {
                Card[rowNumber, i] = new BingoValue(number);
                UncheckedNumbers.Add(number);
            }
        }
    }

    public void CheckNumber(int number, int turn)
    {
        if (HasBingo)
            return;

        bool found = false;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Card[i, j].Value == number)
                {
                    Card[i, j].IsChecked = true;
                    UncheckedNumbers.Remove(number);
                    CheckedNumbers.Add(number);

                    if (CheckHorizontal() || CheckVertical())
                    {
                        _hasBingo = true;
                        _winningBall = number;
                        _wonAtDraw = turn;
                        _score = UncheckedNumbers.Sum() * WinningBall;
                    }

                    found = true;
                    break;
                }
            }

            if (found)
                break;
        }
    }

    private bool CheckHorizontal()
    {
        bool horizontal = false;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Card[i, j].IsChecked)
                {
                    horizontal = true;
                }
                else
                {
                    horizontal = false;
                    break;
                }
            }

            if (horizontal)
                break;
        }

        return horizontal;
    }

    private bool CheckVertical()
    {
        bool vertical = false;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Card[j, i].IsChecked)
                {
                    vertical = true;
                }
                else
                {
                    vertical = false;
                    break;
                }
            }

            if (vertical)
                break;
        }

        return vertical;
    }
}

public class BingoValue
{
    public int Value { get; set; }
    public bool IsChecked { get; set; }

    public BingoValue(int value, bool check = false)
    {
        Value = value;
        IsChecked = check;
    }
}
