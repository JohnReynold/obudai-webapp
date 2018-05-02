using obudai_webapp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace obudai_webapp.Controllers
{
    public class BalanceHandler : IBalance
    {
        static object creationLock = new object();
        static BalanceHandler instance;
        const double defaultBalanceValue = 5000;

        double balance = defaultBalanceValue;
        object balanceLock = new object();

        public static IBalance GetInstance()
        {
            lock (creationLock)
            {
                if (instance == null)
                {
                    instance = new BalanceHandler();
                }
                return instance;
            }
        }

        private BalanceContext context;
        private BalanceHandler()
        {
            context = new BalanceContext();
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
                if (context.BalanceItems.Count() == 0)
                {
                    throw new BalanceEmptyException("Balance context is empty");
                }
                else
                {
                    return context.BalanceItems.FirstOrDefault().BalanceValue;
                }
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
            if (context.BalanceItems.Count() == 0)
            {
                context.BalanceItems.Add(new Balance()
                {
                    ID = 1,
                    BalanceValue = value
                });
            }
            else
            {
                context.BalanceItems.FirstOrDefault().BalanceValue = value;
            }
        }
    }
}