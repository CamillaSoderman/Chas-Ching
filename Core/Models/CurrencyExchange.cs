﻿using Chas_Ching.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Models
{
    internal class CurrencyExchange
    {
        private static readonly Dictionary<(CurrencyType from, CurrencyType to), decimal> ExchangeRates = new Dictionary<(CurrencyType, CurrencyType), decimal>
        {
            { (CurrencyType.SEK, CurrencyType.EUR), 0.086m},
            { (CurrencyType.SEK, CurrencyType.USD), 0.092m},

            { (CurrencyType.EUR, CurrencyType.SEK), 11.65m},
            { (CurrencyType.EUR, CurrencyType.USD), 1.07m},

            { (CurrencyType.USD, CurrencyType.SEK), 10.85m},
            { (CurrencyType.USD, CurrencyType.EUR), 0.93m},
        };
    }
}
