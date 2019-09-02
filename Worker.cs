using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ddb
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("==🐳 Docker Database Backup is now starting!==");
            Console.WriteLine("==============================================");

            Stopwatch stopwatch = new Stopwatch();
            EVariables eVariables = new EVariables();
            StringBuilder filename = new StringBuilder();
            StringBuilder command = new StringBuilder();
            FileInfo fileInfo;
            long filesize = 0;
            int filecount = 0;
            int firstRunDelay = -1;

            Int32.TryParse(eVariables.DB_DUMP_FREQ, out int DB_DUMP_FREQ);
            if (DB_DUMP_FREQ <= 0)
                DB_DUMP_FREQ = 1;

            if (eVariables.DB_DUMP_BEGIN.Length == 4)
            {
                Int32.TryParse(eVariables.DB_DUMP_BEGIN.Substring(0, 2), out int DB_DUMP_BEGIN_HOUR);
                if (DB_DUMP_BEGIN_HOUR < 0 || DB_DUMP_BEGIN_HOUR > 23)
                    DB_DUMP_BEGIN_HOUR = -1;

                Int32.TryParse(eVariables.DB_DUMP_BEGIN.Substring(2), out int DB_DUMP_BEGIN_MINUTE);
                if (DB_DUMP_BEGIN_MINUTE < 0 || DB_DUMP_BEGIN_MINUTE > 59)
                    DB_DUMP_BEGIN_MINUTE = -1;

                firstRunDelay = HelperClass.GetMilisecund(DB_DUMP_BEGIN_HOUR, DB_DUMP_BEGIN_MINUTE);
            }

            Console.WriteLine($"Your options:{Environment.NewLine}MYSQL_ADRESS: {eVariables.MYSQL_ADRESS}{Environment.NewLine}MYSQL_PORT: {eVariables.MYSQL_PORT}{Environment.NewLine}MYSQL_USERNAME: {eVariables.MYSQL_USERNAME}{Environment.NewLine}MYSQL_PASSWORD: (****)🔐 [Length:{eVariables.MYSQL_PASSWORD.Length}]{Environment.NewLine}DB_DUMP_BEGIN: {eVariables.DB_DUMP_BEGIN}{Environment.NewLine}DB_DUMP_FREQ: {eVariables.DB_DUMP_FREQ}{Environment.NewLine}MYSQL_DB_NAMES: {eVariables.MYSQL_DB_NAMES}{Environment.NewLine}FILES_TO_KEEP: {eVariables.FILES_TO_KEEP}{Environment.NewLine}TZ: {eVariables.TZ}");

            command.Append("mysqldump");
            try
            {
                if (command.Bash().Contains("command not found"))
                {
                    Console.WriteLine($"[{DateTime.Now}][ERROR] 🤔 There is no mysqldump in system");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}][ERROR] 🤔 While searching for mysqldump: {ex.Message} | {ex.InnerException?.Message}");
                return;
            }
            Console.WriteLine($"[{DateTime.Now}][INFO] Your container current Date time is: {DateTime.Now} Timezone: {eVariables.TZ}");

            while (!stoppingToken.IsCancellationRequested)
            {
                if (firstRunDelay > 0)
                {
                    Console.WriteLine($"[{DateTime.Now}][INFO] ⏱ Next backup is set to: {DateTime.Now.AddMilliseconds(firstRunDelay)}");
                    await Task.Delay(firstRunDelay, stoppingToken);
                    firstRunDelay = -1;
                }
                else
                {
                    int nextStartMilliseconds = (DB_DUMP_FREQ * 60000) - Convert.ToInt32(stopwatch.ElapsedMilliseconds);

                    if (nextStartMilliseconds < 0)
                    {
                        nextStartMilliseconds = 0;
                        Console.WriteLine($"[{DateTime.Now}][WARNING] ⌛ Your backup has been taking longer than the set value of DB_DUMP_FREQ. Next will be executed immediately");
                    }

                    Console.WriteLine($"[{DateTime.Now}][INFO] ⏱ Next backup is set to: {DateTime.Now.AddMilliseconds(nextStartMilliseconds)}");
                    await Task.Delay(nextStartMilliseconds, stoppingToken);
                }

                stopwatch.Restart();
                filename.Clear();
                filename.Append(DateTime.Now.ToString("ddMMyyyy_HHmmss"));
                command.Clear();

                if (string.IsNullOrEmpty(eVariables.MYSQL_DB_NAMES))
                    command.Append($"mysqldump -h{eVariables.MYSQL_ADRESS} -P{eVariables.MYSQL_PORT} -u{eVariables.MYSQL_USERNAME} -p{eVariables.MYSQL_PASSWORD} --skip-lock-tables --all-databases | gzip -9 -c > /app/backup/{filename}.sql.gz");
                else
                    command.Append($"mysqldump -h{eVariables.MYSQL_ADRESS} -P{eVariables.MYSQL_PORT} -u{eVariables.MYSQL_USERNAME} -p{eVariables.MYSQL_PASSWORD} --skip-lock-tables --databases {eVariables.MYSQL_DB_NAMES} | gzip -9 -c > /app/backup/{filename}.sql.gz");

                try
                {
                    Console.WriteLine($"[{DateTime.Now}][INFO] 🐱‍👤 Start making backup...");
                    Console.WriteLine(command.Bash());
                    stopwatch.Stop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now}][ERROR] 🤔 While making backup: {ex.Message} | {ex.InnerException?.Message}");
                    stopwatch.Stop();
                }

                if (File.Exists($"/app/backup/{filename}.sql.gz"))
                {
                    filesize = 0;
                    filecount = 0;

                    fileInfo = new FileInfo($"/app/backup/{filename}.sql.gz");
                    filecount = Directory.GetFiles("/app/backup/", "*.gz").Length;
                    filesize = Directory.GetFiles("/app/backup/", "*.gz", SearchOption.AllDirectories).Sum(file => file.Length);

                    Console.WriteLine($"[{DateTime.Now}][INFO] 💾 Files is save in: /app/backup/{filename}.sql.gz (duration: {stopwatch.Elapsed.ToString("hh\\:mm\\:ss")} size: {fileInfo.Length.BytesToString()})");
                    Console.WriteLine($"[{DateTime.Now}][INFO] 📦 Now you have {filecount} file (*.gz) with a total size of {filesize}");
                }
                else
                {
                    Console.WriteLine($"[{DateTime.Now}][ERROR] 🤔 Something went wrong. File not found.");
                }

            }
        }
    }
}