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

    void submit()
    {
        try
        {
            string path = @"\Problems\" + Topic_list.currentlySelected.ToString() + @"\" + Problem_list.currentlySelected.ToString();
            string sourcePath = Directory.GetCurrentDirectory();
            string destPath = sourcePath + path;
            string sourceFile = Path.Combine(sourcePath, "a.exe");
            string destFile = Path.Combine(destPath + @"\input", "a.exe");
            File.Copy(sourceFile, destFile, true);
            sourceFile = destFile = "";
            sourceFile = Path.Combine(sourcePath, "submit.bat");
            destFile = Path.Combine(destPath + @"\input", "submit.bat");
            File.Copy(sourceFile, destFile, true);

            ProcessStartInfo processinfo = new ProcessStartInfo(sourcePath + path + @"\input\submit.bat");
            processinfo.CreateNoWindow = true;
            processinfo.UseShellExecute = false;
            var process = Process.Start(processinfo);
            Stopwatch sw = Stopwatch.StartNew();
            while (!process.HasExited)
            {
                if (sw.Elapsed.TotalMilliseconds >= 10000)
                {
                    sw.Stop();
                    foreach (var p in Process.GetProcessesByName("cmd"))
                    {
                        p.Kill();
                    }
                    foreach (var p in Process.GetProcessesByName("a"))
                    {
                        p.Kill();
                    }
                    print("time limit");
                    ToastManager.Show("Time limit exceeded", 2.0f, Color.white, new Color32(0, 230, 118, 255), 20);
                    return;
                }

            }
            int code = process.ExitCode;
            process.Close();
            print(code);
            for (int i = 1; i <= 10; ++i)
            {
                FileStream result = new FileStream(sourcePath + path + @"\input\output" + i.ToString() + ".txt", FileMode.Open);
                FileStream output = new FileStream(sourcePath + path + @"\output\output" + i.ToString() + ".txt", FileMode.Open);
                StreamReader SR = new StreamReader(result);
                string res = SR.ReadToEnd();
                SR = new StreamReader(output);
                string main = SR.ReadToEnd();
                SR.Close(); result.Close(); output.Close();
                if (!main.Contains(res))
                {
                    ToastManager.Show("Wrong Answer on Test " + i.ToString(), 2.0f, Color.white, Color.red, 20);
                    return;
                }
            }
            ToastManager.Show("Accepted", 2.0f, Color.white, Color.green, 20);
            
        }
        catch (Exception exc) { UnityEngine.Debug.Log(exc.Message); return; }
        
    }
    void compile()
    {
        //code of compile button
        list.text = null;
        FileStream FS;
        //taking the code in a cpp file named "code.cpp"
        FS = new FileStream("code.cpp", FileMode.Truncate);
        StreamWriter SW = new StreamWriter(FS);
        SW.Write(editor.text);
		SW.Close();FS.Close();
        //calling the batch file to compile
        list.text = "";
       ProcessStartInfo processinfo = new ProcessStartInfo("cmd.exe", "cmd /c start /b compile.bat");
        processinfo.CreateNoWindow = true;
        processinfo.UseShellExecute = false;
        processinfo.RedirectStandardError = true;
        Process process;
        process = Process.Start(processinfo);
		list.text = process.StandardError.ReadToEnd();//if there is any error,
                                                   //incremente it in the inputField called "list"
        process.Close();

    }
    
   
	void run()
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
            print(runtime.ExitCode);
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
    public void Compile()
    {
        // btn.enabled = false;
        compile();
        //btn.enabled = true;
        ToastManager.Show("Compilation compeleted.");
    }
}

