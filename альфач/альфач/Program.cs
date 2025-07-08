using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace альфач
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[+] Запущена чистка следов NURSULTAN\n");
            //Console.WriteLine("прога тест");
            // 7. Удаление WV2Profile_nursultan
        try
        {
         string path = Path.Combine(
         Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
         @"..\LocalLow\NTFLoader\");
         path = Path.GetFullPath(path);
    if (Directory.Exists(path))
    {
        Directory.Delete(path, true);
        Console.WriteLine("[+] Удалена папка WV2Profile_nursultan: " + path);
    }
}
catch (Exception ex)
{
    Console.WriteLine("[-] Ошибка при удалении WV2Profile_nursultan: " + ex.Message);
}

            void DeleteFiles(string path, params string[] patterns)
            {
                try
                {
                    if (!Directory.Exists(path))
                        return;

                    foreach (string pattern in patterns)
                    {
                        string[] files = Directory.GetFiles(path, pattern);
                        foreach (string file in files)
                        {
                            File.Delete(file);
                            Console.WriteLine("[+] Удалено: " + file);
                        }
                    }
                }
                catch (Exception ex)
                {
                   Console.WriteLine("[-] Ошибка при удалении в " + path + ": " + ex.Message);
                }
            }

            // 1. Удаление Prefetch
            DeleteFiles(Environment.ExpandEnvironmentVariables(@"C:\Windows\Prefetch"), "NURSULTAN*.pf", "ALPHA 1.16.5*.pf");

            // 2. Удаление Recent
            DeleteFiles(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"Microsoft\Windows\Recent"), "*nursultan*");

            // 3. Удаление JumpLists
            DeleteFiles(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"Microsoft\Windows\Recent\AutomaticDestinations"), "*nursultan*");
            DeleteFiles(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"Microsoft\Windows\Recent\CustomDestinations"), "*nursultan*");

            // 4. Удаление TEMP-файлов
            DeleteFiles(Path.GetTempPath(), "*nursultan*");

            // 5. Удаление UserAssist
            try
            {
                RegistryKey userAssist = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Explorer\UserAssist", true);
                if (userAssist != null)
                {
                    foreach (string subkey in userAssist.GetSubKeyNames())
                        userAssist.DeleteSubKeyTree(subkey);
                /   Console.WriteLine("[+] UserAssist очищен");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[-] Ошибка при очистке UserAssist: " + ex.Message);
            }

            // 6. Удаление AppCompatCache
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(
                    @"SYSTEM\CurrentControlSet\Control\Session Manager\AppCompatCache", true);
                if (key != null)
                {
                    key.DeleteValue("AppCompatCache", false);
                    Console.WriteLine("[+] AppCompatCache очищен");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[-] Ошибка при очистке AppCompatCache: " + ex.Message);
            }

            Console.WriteLine("\n[+] Очистка завершена. Нажми Enter для выхода.");
            Console.ReadLine();
        }
        static void DeleteFiles(string path, string pattern)
        {
            try
            {
                if (!Directory.Exists(path))
                    return;

                string[] files = Directory.GetFiles(path, pattern);
                foreach (string file in files)
                {
                    File.Delete(file);
                    Console.WriteLine("[+] Удалено: " + file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[-] Ошибка при удалении в " + path + ": " + ex.Message);
            }

             // 8. Удаление MUICache следов
 try
 {
     RegistryKey muicache = Registry.CurrentUser.OpenSubKey(
         @"Software\Microsoft\Windows\ShellNoRoam\MUICache", true);

     if (muicache != null)
     {
         foreach (var val in muicache.GetValueNames())
         {
             if (val.ToLower().Contains("nursultan"))
             {
                 muicache.DeleteValue(val, false);
                 Console.WriteLine("[+] MUICache очищен: " + val);
             }
         }
     }
 }
 catch (Exception ex)
 {
     Console.WriteLine("[-] Ошибка при очистке MUICache: " + ex.Message);
 }
        }
    }
}
