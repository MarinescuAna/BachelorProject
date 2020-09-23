using DataAccess.Domain.Models.Domain;
using System;
using System.Collections.Generic;


namespace TeamWork_Data
{
    public class User_Data
    {
        public List<User> GetUsers()
        {
            return new List<User> {
                new User
                {
                    FirstName="Angel",
                    LastName="Catalina",
                    Email="a.catalina@gamil.com",
                    Password="12345678",
                    Institution="ACE",
                    UserRole=Role.STUDENT
                },
                new User
                {
                    FirstName="Balasoiu",
                    LastName="Aurelia",
                    Email="b.aura@gamil.com",
                    Password="12345678",
                    Institution="ACE",
                    UserRole=Role.STUDENT
                },                
                new User
                {
                    FirstName="Marinescu",
                    LastName="Ana",
                    Email="m.ana@gamil.com",
                    Password="12345678",
                    Institution="ACE",
                    UserRole=Role.STUDENT
                },
                new User
                {
                    FirstName="Barbu",
                    LastName="Paula",
                    Email="b.paula@gamil.com",
                    Password="12345678",
                    Institution="ACE",
                    UserRole=Role.STUDENT
                },
                new User
                {
                    FirstName="Badica",
                    LastName="Costin",
                    Email="b.costin@gamil.com",
                    Password="12345678",
                    Institution="ACE",
                    UserRole=Role.TEACHER
                },
                new User
                {
                    FirstName="Ganea",
                    LastName="Eugen",
                    Email="ganezu79@gamil.com",
                    Password="12345678",
                    Institution="ACE",
                    UserRole=Role.TEACHER
                }
            };
        }
    }
}
