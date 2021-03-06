﻿using System;
using Ponics.Components.Commands;
using Ponics.Kernel.Commands;
using ServiceStack;

namespace Ponics.Components.Queries
{
    [Api("Connects two a components")]
    [Route("/systems/{SystemId}/components/connections", "POST")]
    public class ConnectComponents : ICommand, IDataCommand
    {
        [ApiMember(Name = "SystemId", Description = "The id of a system",
            ParameterType = "path", DataType = "string", IsRequired = true)]
        [ApiAllowableValues("SystemId", typeof(Guid))]
        public Guid SystemId { get; set; }

        public ComponentConnection ComponentConnection { get; set; }
    }
}
