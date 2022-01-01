using System;

namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель для работы с заданиями
    /// </summary>
    public class TaskResponse
    {
        /// <summary>
        /// Номер задания
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// Номер телефона () Devices
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Наименование задания (Tasks.TaskListId => TasksListId.ServerId)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отображаемое наименование задания () Users
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Описание задания
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата и время запуска (Tasks.Id => TasksPool.TaskId)
        /// </summary>
        public DateTime? DateRun { get; set; }

        /// <summary>
        /// Тестовый сервер (Да/Нет)
        /// </summary>
        public string TestBench { get; set; }

        public int? ServerId { get; set; }

        public string NameServer { get; set; }
    }
}