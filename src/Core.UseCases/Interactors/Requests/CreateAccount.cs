﻿namespace Core.UseCases.Interactors.Requests
{
    using Entities;
    using FailFast;
    using MediatR;

    public class CreateAccount : IRequest<Result<Account>>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Enabled { get; set; }
    }
}