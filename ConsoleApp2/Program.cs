using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
}

class Program
{
    static void Main()
    {
        List<List<Entry>> pages = new List<List<Entry>>();
        List<Entry> currentPage = new List<Entry>();
        int currentPageIndex = 0;

        while (true)
        {
            Console.Write("Podaj czas pracy maszyny (godziny i minuty oddzielone spacją): ");
            string input = Console.ReadLine();
            string[] inputParts = input.Split(' ');

            if (inputParts.Length != 2 ||
                !int.TryParse(inputParts[0], out int hours) ||
                !int.TryParse(inputParts[1], out int minutes) ||
                hours < 0 || minutes < 0 || minutes >= 60)
            {
                Console.WriteLine("Błąd: Nieprawidłowy czas pracy.");
                continue;
            }

            Entry entry = new Entry { Hours = hours, Minutes = minutes };
            currentPage.Add(entry);

            if (currentPage.Count == 3)
            {
                pages.Add(new List<Entry>(currentPage));
                currentPage.Clear();
                currentPageIndex++;

                Console.Write($"Strona {currentPageIndex} zapełniona. Czy chcesz kontynuować na kolejnej stronie? (T/N): ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (char.ToUpper(choice) == 'N')
                {
                    break;
                }
            }
        }

        // Podsumowanie czasu pracy na każdej stronie
        for (int i = 0; i < pages.Count; i++)
        {
            int totalHours = 0;
            int totalMinutes = 0;
            foreach (Entry entry in pages[i])
            {
                totalHours += entry.Hours;
                totalMinutes += entry.Minutes;
            }
            totalHours += totalMinutes / 60;
            totalMinutes %= 60;

            Console.WriteLine($"Podsumowanie strona {i + 1}: {totalHours} godzin {totalMinutes} minut.");
        }

        // Podsumowanie ogólnego czasu pracy
        int overallHours = 0;
        int overallMinutes = 0;
        foreach (var page in pages)
        {
            foreach (Entry entry in page)
            {
                overallHours += entry.Hours;
                overallMinutes += entry.Minutes;
            }
        }
        overallHours += overallMinutes / 60;
        overallMinutes %= 60;

        Console.WriteLine($"Podsumowanie ogólne: {overallHours} godzin {overallMinutes} minut.");
    }
}
