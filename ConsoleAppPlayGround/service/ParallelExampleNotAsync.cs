using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPlayGround.service;

public class ParallelExampleNotAsync
{


    public static void RunParallelExample(int TimerMilliseconds1, int TimerMilliseconds2)
    {
        List<string> myList = new List<string>();

        Parallel.Invoke(
              () => myList.Add(DoWork(TimerMilliseconds1, "Job 1")),
                () => myList.Add(DoWork(TimerMilliseconds2, "Job 2"))
                );

        myList.Add("Job 3: Manual Entry");

        Console.WriteLine(string.Join(" --> ", myList));


    }


    public static string DoWork(int milliseconds, string job)
    {
        Thread.Sleep(milliseconds);
        return job + " " + milliseconds.ToString();
    }

}
