using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook_APP
{
    public class ServiceClient
    {
        private static readonly CRUDService.PersonService _instance = new CRUDService.PersonService();

        public static CRUDService.PersonService Instance => _instance;
    }

}
