using System;
using System.Diagnostics;
using System.Text;

namespace ddb
{
    /// <summary>
    /// reference
    /// https://loune.net/2017/06/running-shell-bash-commands-in-net-core/
    /// </summary>
    public static class HelperClass
    {
        public static string Bash(this StringBuilder cmd)
        {
            try
            {
                using Process process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{cmd.Replace("\"", "\\\"")}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                string standardOutput = process.StandardOutput.ReadToEnd();
                string standardError = process.StandardError.ReadToEnd();
                process.WaitForExit();

                return String.IsNullOrEmpty(standardOutput) ? standardError : standardOutput;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int GetMilisecund(int hour, int minute)
        {
            DateTime firstStart = DateTime.Today.AddHours(hour).AddMinutes(minute);
            if (hour < DateTime.Now.Hour || hour == DateTime.Now.Hour && minute < DateTime.Now.Minute)
            {
                firstStart = firstStart.AddDays(1);
            }

            TimeSpan span = firstStart - DateTime.Now;

            return (int)span.TotalMilliseconds;
        }
    }
}
