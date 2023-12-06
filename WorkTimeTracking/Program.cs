using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

[TestFixture]
public class WorkTimeTrackingTests
{
    [Test]
    public void PageSummaryTest()
    {
        // Testowanie podsumowania strony
        List<Entry> page = new List<Entry> { new Entry { Hours = 1, Minutes = 30 }, new Entry { Hours = 2, Minutes = 15 }, new Entry { Hours = 0, Minutes = 45 } };
        int totalHours, totalMinutes;
        CalculatePageSummary(page, out totalHours, out totalMinutes);
        Assert.AreEqual(4, totalHours);
        Assert.AreEqual(30, totalMinutes);
    }

    [Test]
    public void OverallSummaryTest()
    {
        // Testowanie ogólnego podsumowania
        List<List<Entry>> pages = new List<List<Entry>>
        {
            new List<Entry> { new Entry { Hours = 1, Minutes = 30 }, new Entry { Hours = 2, Minutes = 15 }, new Entry { Hours = 0, Minutes = 45 } },
            new List<Entry> { new Entry { Hours = 2, Minutes = 0 }, new Entry { Hours = 1, Minutes = 45 }, new Entry { Hours = 1, Minutes = 0 } }
        };
        int overallHours, overallMinutes;
        CalculateOverallSummary(pages, out overallHours, out overallMinutes);
        Assert.AreEqual(9, overallHours);
        Assert.AreEqual(15, overallMinutes);
    }

    [Test]
    public void gowno()
    {
        Assert.AreEqual(1, 1);
    }

    private static void CalculatePageSummary(List<Entry> page, out int totalHours, out int totalMinutes)
    {
        totalHours = 0;
        totalMinutes = 0;
        foreach (Entry entry in page)
        {
            totalHours += entry.Hours;
            totalMinutes += entry.Minutes;
        }
        totalHours += totalMinutes / 60;
        totalMinutes %= 60;
    }

    private static void CalculateOverallSummary(List<List<Entry>> pages, out int overallHours, out int overallMinutes)
    {
        overallHours = 0;
        overallMinutes = 0;
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
    }
}
