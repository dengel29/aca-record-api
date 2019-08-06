using System;
using System.Collections.Generic;

namespace AcademicRecordAPI.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanPlay { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}
