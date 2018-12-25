using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using System.IO;
using MaterialUI;
using UnityEngine.UI;
public class Submit_btn : MonoBehaviour {
    public MaterialDropdown Problem_list, Topic_list;
    public void activate()
    {
        string path = @"Problems\" + Topic_list.currentlySelected.ToString() + @"\" + Problem_list.currentlySelected.ToString();
        ProcessStartInfo processinfo = new ProcessStartInfo(path + @"\input\submit.bat");
        processinfo.RedirectStandardOutput = true;
        processinfo.CreateNoWindow = true;
        processinfo.UseShellExecute = false;
        var process = Process.Start(processinfo);
        Stopwatch sw = Stopwatch.StartNew();
        while(sw.Elapsed.TotalMilliseconds < 2000 && !process.HasExited)
        { 
            if(sw.Elapsed.TotalMilliseconds >= 2000)
            {
                sw.Stop();
                foreach(var p in Process.GetProcessesByName("a"))
                {
                    p.Kill();
                }
                print("time limit");
                return;
            }

        }
        for (int i = 1; i <= 10; ++i)
        {
            FileStream result = new FileStream(path + @"\input\output" + i.ToString() + ".txt", FileMode.Open);
            FileStream output = new FileStream(path + @"\output\output" + i.ToString() + ".txt", FileMode.Open);
            StreamReader SR = new StreamReader(result);
            string res = SR.ReadToEnd();
            SR = new StreamReader(output);
            string main = SR.ReadToEnd();
            SR.Close();result.Close();output.Close();
            if(!main.Contains(res))
            {
                ToastManager.Show("Wrong Answer on Test " + i.ToString(), 2.0f, Color.white, Color.red, 20);
                return;
            }        
        }
        ToastManager.Show("Accepted", 2.0f, Color.white, Color.green, 20);
    }

	
}
