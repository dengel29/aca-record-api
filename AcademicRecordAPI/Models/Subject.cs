using System;

namespace AcademicRecordAPI.Models
{
    public class Subject
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public int Score { get; set; }

        public int PlayerForeignKey { get; set; }
        public Player Player { get; set; }
    }
}
