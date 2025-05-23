﻿namespace GraduationProjectWebApi.DTOS;
public class UserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }
    public Nationality Nationality { get; set; }
}
