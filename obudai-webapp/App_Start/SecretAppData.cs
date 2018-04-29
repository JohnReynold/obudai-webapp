using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace obudai_webapp
{
    public class SecretAppData
    {
        static object accessLock = new object();
        static string api_key;
        public static string GetApiKey()
        {
            lock (accessLock)
            {
                if (api_key == null)
                {
                    InitApiKey();
                }
                return api_key; 
            }
        }
        static void InitApiKey()
        {
            api_key = null;
            // try finding the key here
            try
            {
                // will not work in local if this is not the path
                api_key = File.ReadAllText(
                    @"D:\Documents\Felev8\obudai-webapp\obudai-webapp\secret.api_key.txt"
                );
            }
            catch (Exception) { }

            // otherwise try finding it in azure local dir...
            // gets pushed there on publish
            if (api_key == null)
            {
                api_key = File.ReadAllText(@"D:\home\site\wwwroot\bin\secret.api_key.txt");
            }
            api_key = api_key.Trim();
        }
    }
}