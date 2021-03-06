﻿using System;
using Ponics.Commands;
using Ponics.Kernel.Commands;
using Ponics.Organisms.Tolerances;
using ServiceStack;

namespace Ponics.Analysis.Levels.Commands
{
    public abstract class ToleranceCommand<TTolerance> : ICommand
        where TTolerance : Tolerance
    {
        [ApiMember(Name = "OrganismId", Description = "The id of an organism",
            ParameterType = "path", DataType = "string", IsRequired = true)]
        [ApiAllowableValues("OrganismId", typeof(Guid))]
        public Guid OrganismId { get; set; }
    }
}
