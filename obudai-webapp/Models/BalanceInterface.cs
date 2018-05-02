using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace obudai_webapp
{
    public class Balance
    {
        public int ID { get; set; }

        public double BalanceValue { get; set; }
    }
    // inherit to implement database access
    public interface IBalance
    {
        double GetDefaultBalanceValue();
        double GetBalance();
        double ResetBalance();
        //void SetBalance(double value);
        double ChangeBalance(double change);
    }
    // TODO change to DB access
    public class BalanceInterface : IBalance
    {
        static object creationLock = new object();
        static BalanceInterface instance;
        const double defaultBalanceValue = 5000;

        // store this in db
        double balance = defaultBalanceValue;
        object balanceLock = new object();

        public static IBalance GetInstance()
        {
            lock (creationLock)
            {
                if (instance == null)
                {
                    instance = new BalanceInterface();
                }
                return instance;
            }
        }

        public double ChangeBalance(double change)
        {
            var newValue = balance + change;
            _SetBalance(newValue);
            return newValue;
        }

        public double GetBalance()
        {
            lock (balanceLock)
            {
                return balance; 
            }
        }

        public double GetDefaultBalanceValue()
        {
            lock (balanceLock)
            {
                return defaultBalanceValue; 
            }
        }

        public double ResetBalance()
        {
            lock (balanceLock)
            {
                _SetBalance(defaultBalanceValue);
                return defaultBalanceValue; 
            }
        }

        void _SetBalance(double value)
        {
            if (value < 0)
                throw new InvalidOperationException("Cannot set balance below 0.");
            balance = value; 
        }

        //public void SetBalance(double value)
        //{
        //    lock (balanceLock)
        //    {
        //        _SetBalance(value); 
        //    }
        //}
    }
}