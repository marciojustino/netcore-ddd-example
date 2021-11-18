﻿using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Domain.Core.MessageBus
{
    public interface IMessageBusSender
    {
        Task<Result> PublishAsTopicAsync(string queueName, string message);
    }
}