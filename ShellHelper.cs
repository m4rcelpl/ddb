using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ddb
{
    /// <summary>
    /// reference
    /// https://loune.net/2017/06/running-shell-bash-commands-in-net-core/
    /// </summary>
    public static class ShellHelper
    {
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            using Process process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/sh",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            string standardOutput = process.StandardOutput.ReadToEnd();
            string standardError = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return String.IsNullOrEmpty(standardOutput) ? standardError : standardOutput;

        }
    }
}
