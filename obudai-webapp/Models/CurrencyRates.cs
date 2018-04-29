using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace obudai_webapp
{
    // inherit to implement web API access
    public interface ICurrencyRates
    {
        // USD per unit of CURRENCY
        int GetRateFor(string currency);
    }

    // TODO change to WEB API access (fetch currencies...)
    public class CurrencyRates : ICurrencyRates
    {
        static object creationLock = new object();
        static CurrencyRates instance;


        public int GetRateFor(string currency)
        {
            return 211; // dummy value
        }

        public static ICurrencyRates GetInstance()
        {
            lock (creationLock)
            {
                if (instance == null)
                {
                    instance = new CurrencyRates();
                }
                return instance;
            }
        }
    }
}