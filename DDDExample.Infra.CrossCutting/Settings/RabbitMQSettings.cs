using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Infra.CrossCutting.Settings
{
    public class RabbitMQSettings
    {
        public string ConfigSectionName { get; set; } = "RabbitMQ";

        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
