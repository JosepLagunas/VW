using System;

namespace Laklp.Platform.Data.Entities
{
    public class User : Traceable
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname1 { get; set; }
        public string Surname2 { get; set; }
        public Employee Role { get; set; }
        public UserType UserType { get; set; }
    }
}