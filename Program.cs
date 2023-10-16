using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string text = File.ReadAllText("C:\\Users\\covalew.valery\\Desktop\\input.docx", Encoding.UTF8);

        Console.WriteLine("Меню выбора действий:");
        Console.WriteLine("1. Найти слова, содержащие максимальное количество цифр.");
        Console.WriteLine("2. Найти самое длинное слово и определить, сколько раз оно встретилось в тексте.");
        Console.WriteLine("3. Заменить цифры от 0 до 9 на слова «ноль», «один», ..., «девять».");
        Console.WriteLine("4. Вывести на экран сначала вопросительные, а затем восклицательные предложения.");
        Console.WriteLine("5. Вывести на экран только предложения, не содержащие запятых.");
        Console.WriteLine("6. Найти слова, начинающиеся и заканчивающиеся на одну и ту же букву.");
        Console.WriteLine("Выберите номер действия:");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                FindWordsWithMaxDigits(text);
                break;
            case 2:
                FindLongestWord(text);
                break;
            case 3:
                ReplaceDigitsWithWords(text);
                break;
            case 4:
                PrintQuestionAndExclamationSentences(text);
                break;
            case 5:
                PrintSentencesWithoutCommas(text);
                break;
            case 6:
                FindWordsWithSameFirstAndLastLetter(text);
                break;
            default:
                Console.WriteLine("Некорректный выбор.");
                break;
        }
    }

    static void FindWordsWithMaxDigits(string text)
    {
        string[] words = text.Split(new[] { ' ', ',', ';', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        int maxDigitCount = 0;
        foreach (string word in words)
        {
            int digitCount = word.Count(char.IsDigit);
            if (digitCount > maxDigitCount)
            {
                maxDigitCount = digitCount;
            }
        }

        var wordsWithMaxDigits = words.Where(word => word.Count(char.IsDigit) == maxDigitCount);

        Console.WriteLine($"Слова с максимальным количеством цифр ({maxDigitCount}):");
        foreach (var word in wordsWithMaxDigits)
        {
            Console.WriteLine(word);
        }
    }

    static void FindLongestWord(string text)
    {
        string[] words = text.Split(new[] { ' ', ',', ';', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        string longestWord = "";
        int longestWordCount = 0;

        foreach (var word in words)
        {
            if (word.Length > longestWordCount)
            {
                longestWord = word;
                longestWordCount = word.Length;
            }
        }

        int occurrences = words.Count(word => word.Equals(longestWord));

        Console.WriteLine($"Самое длинное слово: {longestWord}");
        Console.WriteLine($"Количество вхождений: {occurrences}");
    }

    static void ReplaceDigitsWithWords(string text)
    {
        string[] digits = { "ноль", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять" };

        for (int i = 0; i < digits.Length; i++)
        {
            string digit = i.ToString();
            text = text.Replace(digit, digits[i]);
        }

        Console.WriteLine("Текст после замены цифр на слова:");
        Console.WriteLine(text);
    }

    static void PrintQuestionAndExclamationSentences(string text)
    {
        string[] sentences = Regex.Split(text, @"(?<=[.!?])\s+");

        var questionSentences = sentences.Where(sentence => sentence.Contains("?"));
        var exclamationSentences = sentences.Where(sentence => sentence.Contains("!"));

        Console.WriteLine("Вопросительные предложения:");
        foreach (var sentence in questionSentences)
        {
            Console.WriteLine(sentence);
        }

        Console.WriteLine("Восклицательные предложения:");
        foreach (var sentence in exclamationSentences)
        {
            Console.WriteLine(sentence);
        }
    }

    static void PrintSentencesWithoutCommas(string text)
    {
        string[] sentences = Regex.Split(text, @"(?<=[.!?])\s+");

        var sentencesWithoutCommas = sentences.Where(sentence => !sentence.Contains(","));

        Console.WriteLine("Предложения без запятых:");
        foreach (var sentence in sentencesWithoutCommas)
        {
            Console.WriteLine(sentence);
        }
    }

    static void FindWordsWithSameFirstAndLastLetter(string text)
    {
        string[] words = text.Split(new[] { ' ', ',', ';', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        var wordsWithSameFirstAndLastLetter = words.Where(word => word.Length > 1 && char.ToLower(word[0]) == char.ToLower(word[word.Length - 1]));

        Console.WriteLine("Слова, начинающиеся и заканчивающиеся на одну и ту же букву:");
        foreach (var word in wordsWithSameFirstAndLastLetter)
        {
            Console.WriteLine(word);
        }
    }
}