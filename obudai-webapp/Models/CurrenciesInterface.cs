using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace obudai_webapp
{
    // inherit to implement database access
    public interface ICurrencies
    {
        IEnumerable<string> GetCurrencyTypes();
        void ResetCurrencies();
        double GetCurrencyAmount(string currencyType);
        void SetCurrencyAmount(string currencyType, double value);
        double ChangeCurrencyAmount(string currencyType, double change);
    }

    // TODO change to DB access
    public class CurrenciesInterface : ICurrencies
    {
        static object creationLock = new object();
        static CurrenciesInterface instance;
        public static readonly string[] currencyTypes =
        {
            "BTC",
            "ETH",
            "XRP"
        };


        public IEnumerable<string> GetCurrencyTypes()
        {
            return currencyTypes;
        }

        Dictionary<string, double> currencies;

        void initCurrencies()
        {
            currencies = new Dictionary<string, double>();
            foreach (var item in currencyTypes)
            {
                currencies[item] = 0;
            }
        }

        public CurrenciesInterface()
        {
            initCurrencies();
        }

        public double GetCurrencyAmount(string currencyType)
        {
            lock (currencies)
            {
                if (!currencies.ContainsKey(currencyType))
                    throw new ArgumentException(
                        $"Currency {currencyType ?? "null"} is not a valid type."
                    );

                return currencies[currencyType]; 
            }
        }

        public void SetCurrencyAmount(string currencyType, double value)
        {
            lock (currencies)
            {
                if (!currencies.ContainsKey(currencyType))
                    throw new ArgumentException(
                        $"Currency {currencyType ?? "null"} is not a valid type."
                    );
                if (value < 0)
                    throw new ArgumentException(
                       "Currency value cannot be negative."
                   );

                currencies[currencyType] = value; 
            }
        }

        public double ChangeCurrencyAmount(string currencyType, double change)
        {
            lock (currencies)
            {
                if (!currencies.ContainsKey(currencyType))
                    throw new ArgumentException(
                        $"Currency {currencyType ?? "null"} is not a valid type."
                    );

                var newValue = currencies[currencyType] + change;
                if (newValue < 0)
                    throw new ArgumentException(
                       "Currency value cannot be negative."
                   );

                currencies[currencyType] = newValue;
                return newValue; 
            }
        }

        public static ICurrencies GetInstance()
        {
            lock (creationLock)
            {
                if (instance == null)
                {
                    instance = new CurrenciesInterface();
                }
                return instance; 
            }
        }

        public void ResetCurrencies()
        {
            lock (currencies)
            {
                initCurrencies();
            }
        }
    }
}