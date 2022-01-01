using MentolProvision.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static MentolProvision.Controllers.TasksController;

namespace MentolProvision.Models.Request
{
    public class TaskPostRequest
    {
        [LocalizedRequired]
        public TaskMeta Meta { get; set; }
        [LocalizedRequired]
        public Task Task { get; set; }
    }

    public class TaskMeta
    {
        [LocalizedRequired]
        public string UUID { get; set; }
        [LocalizedRequired]
        public string Action { get; set; }
    }

    public class Task
    {
        [LocalizedRequired]
        public int TaskTypeId { get; set; } // required
        public string TaskDescription { get; set; }
        public DateTime? TaskDateRun { get; set; } // required if the field "action=create" 
        [LocalizedRequired]
        public int DeviceId { get; set; } // required
        public int? ServerTestId { get; set; } 
        public string TaskJsonNew { get; set; } // required if the field "action=create" 
    }
}
