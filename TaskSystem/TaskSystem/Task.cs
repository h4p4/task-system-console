using System;
using System.Collections.Generic;
using System.Text;

namespace TaskSystem
{
    [Serializable]
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public User CreatorUser { get; set; }
        public User AcceptedUser { get; set; }
        public string TaskStatus { get; set; }
        public Task(
                     int Id,
                     string Title,
                     string Description,
                     DateTime PublicationDate,
                     User CreatorUser,
                     User AcceptedUser,
                     string TaskStatus
                    )
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.PublicationDate = PublicationDate;
            this.CreatorUser = CreatorUser;
            this.AcceptedUser = AcceptedUser;
            this.TaskStatus = TaskStatus;
        }
        public Task()
        {
        }
    }
}
