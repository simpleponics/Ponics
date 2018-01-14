﻿using Ponics.Kernel.Commands;

namespace Ponics.Aquaponics.Commands
{
    public class AddSystemCommandHandler : ICommandHandler<AddSystem>
    {
        private readonly IDataCommandHandler<AddSystem> _addSystemDataCommandHandler;

        public AddSystemCommandHandler(IDataCommandHandler<AddSystem> addSystemDataCommandHandler)
        {
            _addSystemDataCommandHandler = addSystemDataCommandHandler;
        }


        public void Handle(AddSystem command)
        {
            _addSystemDataCommandHandler.Handle(command);
        }
    }
}