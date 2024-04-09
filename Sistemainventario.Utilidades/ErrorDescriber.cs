﻿using Microsoft.AspNetCore.Identity;

namespace SistemaInventario.Utilidades
{
    public class ErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "El password debe tener al menos una Letra Minúscula"
            };
        }
    }
}
