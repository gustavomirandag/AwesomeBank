using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeBank.Domain
{
    public class AccountHolder
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
    }
}
