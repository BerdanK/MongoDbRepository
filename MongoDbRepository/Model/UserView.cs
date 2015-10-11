using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDbRepository.Base;

namespace MongoDbRepository.Model
{
    public class UserView : Entity
    {
        public string Adi { get; set; }
    }
}
