﻿namespace Mediagram.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
    }
}
