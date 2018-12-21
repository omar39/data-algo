using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using MaterialUI;

public class Submit : MonoBehaviour {
    public InputField editor;
    public InputField list;
	public InputField input;
    public MaterialButton btn;
    public MaterialDropdown Problem_list;
    public MaterialDropdown Topic_list;
    FileStream FS;
    ProcessStartInfo processinfo;
    void submit()
    {
        try
        {

            processinfo = new ProcessStartInfo("cmd.exe", "cmd /c start /b submit.bat");
           // processinfo.CreateNoWindow = true;
            processinfo.UseShellExecute = false;
            processinfo.RedirectStandardError = true;
            processinfo.RedirectStandardOutput = true;
            processinfo.RedirectStandardInput = true;
            var runtime = Process.Start(processinfo);
            string path = "Problems" + @"\" + Topic_list.currentlySelected.ToString() + @"\" + Problem_list.currentlySelected.ToString();
            runtime.StandardInput.WriteLine(path + @"\input");
            UnityEngine.Debug.Log(path + @"\input");
            string tmp = "";
            char c = (char)runtime.StandardOutput.Read();
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.Elapsed.TotalMilliseconds < 2000 && c != '\uffff' && tmp.Length <= 10000000)
            {

                if (c != '\f')
                    tmp += c;
                if (sw.Elapsed.TotalMilliseconds > 2000)
                {
                    sw.Stop();
                    foreach (var process in Process.GetProcessesByName("a"))
                    {
                        process.Kill();
                    }
                    ToastManager.Show("Time Limit Exceeded", 2.0f, Color.white, Color.red, 20);
                    return;
                }
                c = (char)runtime.StandardOutput.Read();
            }
            //tmp = runtime.StandardOutput.ReadToEnd ();
            sw.Stop();
            runtime.Close();

            list.text = null;
            char[] limit = new char[2];
            limit[0] = '/';
            limit[1] = 'f';
            string[] str = tmp.Split(limit);

            int index = 0;
            for (int i = 1; i <= 10; ++i)
            {
                FS = new FileStream(path + @"\output" + @"\output" + i.ToString() + ".txt", FileMode.Open);
                StreamReader SR = new StreamReader(FS);
                string res = SR.ReadToEnd();
                if (!tmp.Contains(res))
                {
                    FS.Close(); SR.Close();
                    ToastManager.Show("Wrong Answer on Test " + i.ToString(), 2.0f, Color.white, Color.red, 20);
                    return;
                }
                SR.Close(); FS.Close();
                index += 2;
            }
            ToastManager.Show("Accepted", 2.0f, Color.white, Color.green, 20);
        }
        catch (Exception exc) { UnityEngine.Debug.Log(exc.Message); return; }
        
    }
    void compile()
    {
        //code of compile button
        list.text = null;
        //taking the code in a cpp file named "code.cpp"
        FS = new FileStream("code.cpp", FileMode.Truncate);
        StreamWriter SW = new StreamWriter(FS);
        SW.Write(editor.text);
		SW.Close();FS.Close();
        //calling the batch file to compile
        list.text = "";
        processinfo = new ProcessStartInfo("cmd.exe", "cmd /c start /b compile.bat");
        processinfo.CreateNoWindow = true;
        processinfo.UseShellExecute = false;
        processinfo.RedirectStandardError = true;
        Process process;
        process = Process.Start(processinfo);
		list.text = process.StandardError.ReadToEnd();//if there is any error,
                                                   //incremente it in the inputField called "list"
        process.Close();

    }
    public void Compile()
    {
       // btn.enabled = false;
		compile();
        //btn.enabled = true;
        ToastManager.Show("Compilation compeleted.");
    }
    public void Test()
    {
        //btn.enabled = false;
        compile();
        if (list.text.Length <= 1)
        {
            submit();
        }

        else
            ToastManager.Show("Compilation Error", 2.0f, Color.white, Color.blue, 14);
        //btn.enabled = true;
    }
	public void run()
	{
		//run the "Run" button

		//taking the input in a file
		FileStream FS = new FileStream("input.txt", FileMode.Truncate);
		StreamWriter SW = new StreamWriter(FS);
		SW.Write(input.text);
		SW.Close();FS.Close();
		var process2 = new ProcessStartInfo("cmd.exe", "cmd /c start /b run.bat");
		process2.CreateNoWindow = true;
		process2.UseShellExecute = false;
		process2.RedirectStandardError = true;
		process2.RedirectStandardOutput = true;
		var runtime = Process.Start(process2);
		try
		{
			//calling the batch file to run the code and the input
			string tmp = "";
			char c = (char)runtime.StandardOutput.Read();
			// runtime.OutputDataReceived += (object s, DataReceivedEventArgs evnt) =>
			//           OutputLabel.Text += evnt.Data;//taking the output
			Stopwatch sw = Stopwatch.StartNew();
			while (sw.Elapsed.TotalMilliseconds < 2000 && c != '\uffff' && tmp.Length <= 10000000)
			{

				if(c != '\f')
					tmp += c;
				c = (char)runtime.StandardOutput.Read();
				if(sw.Elapsed.TotalMilliseconds > 2000)
				{
					sw.Stop();
					foreach(var process in Process.GetProcessesByName("a"))
					{
						process.Kill();
					}                
					ToastManager.Show("Time Limit Exceeded", 2.0f, Color.white, Color.red, 20);
					return;
				}

			}
			list.text = tmp;

			runtime.Close();
		}
		catch(Exception ex) {
			runtime.Close();
		}

	}
	public void Run()
	{
		compile ();
		if(list.text.Length <= 1)
			run ();
	}
}
