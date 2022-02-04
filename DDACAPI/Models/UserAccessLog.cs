using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DDACAPI.Models
{
    public class UserAccessLog
    {

        public UserAccessLog()
            {
            }

        [Key]
        public int logid
        {
            get;
            set;
        }

        public int userid
        {
            get;
            set;
        }

        public DateTime login
        {
            get;
            set;
        }

        public DateTime logout
        {
            get;
            set;
        }

    }

   
}
