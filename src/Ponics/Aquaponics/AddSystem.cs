﻿using Ponics.Commands;
using Ponics.Kernel.Data;
using ServiceStack;

namespace Ponics.Aquaponics
{
    [Api("Add an Aquaponic System")]
    [Route("/aquaponic/systems", "POST")]
    public class AddSystem : Command, IDataCommand
    {
        public PonicsSystem System { get; set; }
    }
}