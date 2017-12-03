﻿using System;
using System.Collections.Generic;
using Ponics.AquaponicSystems;
using Ponics.Queries;
using ServiceStack;

namespace Ponics.Components
{
    [Api("Get a list of component connections")]
    [Route("/systems/{SystemsId}/components/connections", "GET")]
    public class GetConnections: Query<IList<ComponentConnection>>
    {
        [ApiMember(Name = "SystemId", Description = "The id of a system",
            ParameterType = "path", DataType = "string", IsRequired = true)]
        [ApiAllowableValues("SystemId", typeof(Guid))]
        public Guid SystemId { get; set; }
    }
}
