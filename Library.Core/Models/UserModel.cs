﻿using System.Collections.Generic;
using Library.Domain.Models;

namespace Library.Core.Models
{
    public class UserModel
    {
        public string UserProfileId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public string Skype { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public int Floor { get; set; }
        public int Room { get; set; }
        public string PlaceDescription { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public List<BookModel> Books { get; set; }
    }
}