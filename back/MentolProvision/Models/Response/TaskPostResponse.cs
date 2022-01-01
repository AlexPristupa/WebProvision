using MentolProvision.Enums;
using MentolProvision.Models.Response.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace MentolProvision.Models.Response
{
    public class TaskPostResponse
    {
        public TaskPostResponseMeta Meta { get; set; }
        public TaskPostResponseStatus Status { get; set; }
    }

    public class TaskPostResponseMeta
    {
        public string UUID { get; set; }
        public string UsersBooking { get; set; }
    }

    public class TaskPostResponseStatus
    {
        public int Code { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
    }

    public class TaskPostResponseBuilder
    {
        private IStringLocalizer _localizer;

        public TaskPostResponseBuilder(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public TaskPostResponseBuildResult BuildByCode(TaskPostResponseCode code, TaskPostResponseBuildData responseData)
        {
            if(!_statusInfoByCode.ContainsKey(code))
            {
                throw new ArgumentException($"Не найден ответ с кодом реультата {code}");
            }

            string usersBooking = string.Join(", ", responseData.UsersBooking.Distinct());
            string taskIds = string.Join(", ", responseData.TaskIds.Distinct());
            string phoneNumbers = string.Join(", ", responseData.PhoneNumbers.Distinct());

            var response = new TaskPostResponse();
            var statusInfo = _statusInfoByCode[code];

            response.Meta = new TaskPostResponseMeta
            {
                UUID = responseData.UUID,
                UsersBooking = usersBooking
            };

            response.Status = new TaskPostResponseStatus
            {
                Code = (int)code,
                Result = statusInfo.Result,
                Message = BuildMessage(statusInfo.MessagePattern, usersBooking, phoneNumbers, taskIds)
            };

            return new TaskPostResponseBuildResult { HttpCode = statusInfo.HttpCode, Response = response };
        }

        private string BuildMessage(string messagePattern, string userNames, string phoneNumber, string taskIds)
        {
            return messagePattern is null ? null : _localizer[messagePattern].Value?.Replace("{taskId}", taskIds)
                                                                                   ?.Replace("{devicePhoneNumber}", phoneNumber)
                                                                                   ?.Replace("{userBooking}", userNames);
        }

        private Dictionary<TaskPostResponseCode, TaskPostResponseInfo> _statusInfoByCode = new Dictionary<TaskPostResponseCode, TaskPostResponseInfo>()
        {
            { TaskPostResponseCode.TaskCreatedAndNotBlockedByAnotherUser, new TaskPostResponseInfo { HttpCode = 201, Result = OperationResult.Success, MessagePattern = "TASK_CREATED_SUCCESS" } },
            { TaskPostResponseCode.TaskNotExistAndNotBlockedByAnotherUser, new TaskPostResponseInfo { HttpCode = 202, Result = OperationResult.Success, MessagePattern = null } },
            { TaskPostResponseCode.TaskIsExistAndNotBlockedByAnotherUser, new TaskPostResponseInfo { HttpCode = 202, Result = OperationResult.Success, MessagePattern = null } },
            { TaskPostResponseCode.TaskIsExistAndResourceNotBlockedByAnotherUser, new TaskPostResponseInfo { HttpCode = 202, Result = OperationResult.Warning, MessagePattern = "TASK_IS_EXIST_FOR_NUMBER"} },
            { TaskPostResponseCode.ResourceBlockedByAnotherUser, new TaskPostResponseInfo { HttpCode = 403, Result = OperationResult.Error, MessagePattern = "TASK_IS_ALREADY_CREATED" } },
            { TaskPostResponseCode.TaskIsExistAndResourceBlockedByAnotherUser, new TaskPostResponseInfo { HttpCode = 403, Result = OperationResult.Error, MessagePattern = "TASK_IS_EXIST_FOR_NUMBER_AND_EDITED_BY_USER" } },
            { TaskPostResponseCode.ActionIsCanceled, new TaskPostResponseInfo { HttpCode = 410, Result = OperationResult.Error, MessagePattern = null } },
        };
    }

    public class TaskPostResponseInfo
    {
        public int HttpCode { get; set; }
        public string Result { get; set; }
        public string UUID { get; set; }
        public string MessagePattern { get; set; }
    }

    public class TaskPostResponseBuildData
    {
        public string UUID { get; set; }
        public IEnumerable<string> PhoneNumbers { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<string> UsersBooking { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<int> TaskIds { get; set; } = Enumerable.Empty<int>();
    }

    public class TaskPostResponseBuildResult
    {
        public int HttpCode { get; set; }
        public TaskPostResponse Response { get; set; }
    }

    public static class OperationResult
    {
        public const string Success = "success";
        public const string Warning = "warning";
        public const string Error = "error";
    }
}
