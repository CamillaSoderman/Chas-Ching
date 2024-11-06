using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chas_Ching.Core.Interfaces
{
    public interface IAccount
    {
        public void GetBalance();
        public void Deposit();
        public void Withdraw();
        public void Transfer();
    }
}
