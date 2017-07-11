using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XamTime.Models
{


    public class UserRole
    {
        public string RoleId;
        public string RoleName;
    }

    [XmlRoot("ArrayOfClientAccount", Namespace = "http://tempuri.org/")]
    public class ClientAccounts 
    {
        [XmlElement("ClientAccount")]
        public List<ClientAccount> Accounts { get; set; }
    }
    public class ClientAccount
    {
        public string AccountId { get; set; }
        public string AccountName { get; set; }
    }

    public class ClientProject
    {
        public string ProjectId;
        public string ProjectName;
    }

    public class ProjectTask
    {
        public string TaskId;
        public string TaskName;
        public string TaskStatus;
        public string TaskStatusLabel;
    }

    public class ProjectTaskStatus
    {
        public string TaskStatusId;
        public string TaskStatusName;
    }

    public class TimeEntryUpdateStatus
    {
        public string Message;
        public bool Success;
    }


    public class TimeEntryValue
    {
        public string TaskId;
        public long TimeEntryId;
        public string StartDate;
        public string EndDate;
        public string Project;
        public bool IsTicket;
        public string TaskTitle;
        public string TaskStatus;
        public string SummaryNotes;
        public string InternalNotes;
        public long RoleID;
        public bool IsBillable;
    }


    public class TimeEntryRecord
    {
        public ClientAccount Account;
        public ClientProject Project;
        public ProjectTask Task;
        public ProjectTaskStatus TaskStatus;
    }

}
