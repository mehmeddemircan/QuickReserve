﻿namespace QuickReserve.Application.Features.Users.Dtos
{
    public class UserByIdDto 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
