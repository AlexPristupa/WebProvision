export const pages = [
  {
    idr: 1,
    ParentId: 0,
    name: "devices",
    IsLeaf: false,
    Action: "/devices",
    PrivateName: "Устройства",
  },
  {
    idr: 2,
    ParentId: 0,
    name: "tasks",
    IsLeaf: false,
    Action: "/tasks",
    PrivateName: "Задания",
  },
  {
    idr: 3,
    ParentId: 0,
    name: "configuration",
    IsLeaf: false,
    Action: "/configuration",
    PrivateName: "Конфигурация",
  },
  {
    idr: 4,
    ParentId: 0,
    name: "security",
    IsLeaf: false,
    Action: "/security",
    PrivateName: "Безопасность",
  },
  {
    idr: 5,
    ParentId: 3,
    name: "servers",
    IsLeaf: true,
    Action: "/configuration/servers",
    PrivateName: "Серверы",
  },
  {
    idr: 6,
    ParentId: 1,
    name: "phones",
    IsLeaf: true,
    Action: "/devices/phones",
    PrivateName: "Телефоны",
  },
  {
    idr: 7,
    ParentId: 4,
    name: "users",
    IsLeaf: true,
    Action: "/security/users",
    PrivateName: "Пользователи",
  },
  {
    idr: 8,
    ParentId: 4,
    name: "roles",
    IsLeaf: true,
    Action: "/security/roles",
    PrivateName: "Роли",
  },
  {
    idr: 9,
    ParentId: 4,
    name: "logs",
    IsLeaf: true,
    Action: "/security/logs",
    PrivateName: "Журнал",
  },
  {
    idr: 10,
    ParentId: 2,
    name: "current",
    IsLeaf: true,
    Action: "/tasks/current",
    PrivateName: "Текущие",
  },
  {
    idr: 11,
    ParentId: 2,
    name: "history",
    IsLeaf: true,
    Action: "/tasks/history",
    PrivateName: "Завершенные",
  },
]

export const logs = [
  {
    idr: 1,
    Date: "18.03.2020 12:29",
    Author: "admin",
    TypeId: 1,
    Action: "Вход",
    description: "Успешная попытка входа для admin.",
  },
  {
    idr: 2,
    Date: "18.03.2020 12:29",
    Author: "admin",
    TypeId: 1,
    Action: "Вход",
    description: "Успешная попытка смены номера.",
  },
  {
    idr: 3,
    Date: "18.03.2020 12:29",
    Author: "admin",
    TypeId: 3,
    Action: "Вход",
    description: "Успешная попытка чего-то еще.",
  },
  {
    idr: 4,
    Date: "20.03.2020 12:29",
    Author: "otherwho",
    TypeId: 2,
    Action: "Вход",
    description: "Ошибка входа в систему.",
  },
  {
    idr: 5,
    Date: "18.03.2020 12:29",
    Author: "admin",
    TypeId: 1,
    Action: "Вход",
    description: "Ошибка входа.",
  },
  {
    idr: 6,
    Date: "18.03.2020 12:29",
    Author: "admin",
    TypeId: 1,
    Action: "Вход",
    description: "Успешная попытка входа для admin.",
  },
  {
    idr: 7,
    Date: "18.03.2020 12:29",
    Author: "admin",
    TypeId: 3,
    Action: "Вход",
    description: "Успешная попытка входа для admin.",
  },
  {
    idr: 8,
    Date: "20.03.2020 12:29",
    Author: "otherwho",
    TypeId: 2,
    Action: "Вход",
    description: "Успешная попытка входа для admin.",
  },
]

export const logTypes = [
  {
    idr: 1,
    name: "Система",
  },
  {
    idr: 2,
    name: "Карточка мобильного телефона",
  },
  {
    idr: 3,
    name: "Другое",
  },
]
export const models = [
  {
    idr: 1,
    name: "T-1000",
  },
  {
    idr: 2,
    name: "T-800",
  },
  {
    idr: 3,
    name: "T-X",
  },
]

export const vendors = [
  {
    idr: 1,
    name: "Петрософт",
  },
  {
    idr: 2,
    name: "Гаспром",
  },
  {
    idr: 3,
    name: "Google",
  },
]

export const nodes = [
  {
    idr: 1,
    priority: 2,
    nodAddress: "129.168.167.200",
    name: "Node #1",
    serverId: 1,
  },
  {
    idr: 2,
    priority: 3,
    nodAddress: "192.108.80.230",
    name: "Node #2",
    serverId: 1,
  },
  {
    idr: 3,
    priority: 4,
    nodAddress: "129.168.80.230",
    name: "Node #3",
    serverId: 1,
  },
  {
    idr: 4,
    priority: 1,
    nodAddress: "129.168.167.230",
    name: "Node #4",
    serverId: 1,
  },
  {
    idr: 5,
    priority: 5,
    nodAddress: "192.108.80.230",
    name: "Node #5",
    serverId: 1,
  },
  {
    idr: 6,
    priority: 6,
    nodAddress: "192.168.167.230",
    name: "Node #6",
    serverId: 1,
  },
  {
    priority: 1597411159057,
    nodAddress: "192.108.80.230",
    name: "Node # 1597411159057",
    serverId: 40,
    idr: 7,
  },
  {
    priority: 1597411160024,
    nodAddress: "192.108.80.230",
    name: "Node # 1597411160024",
    serverId: 40,
    idr: 8,
  },
  {
    priority: 1597411160209,
    nodAddress: "192.108.80.230",
    name: "Node # 1597411160209",
    serverId: 40,
    idr: 9,
  },
  {
    priority: 1597411160398,
    nodAddress: "192.108.80.230",
    name: "Node # 1597411160398",
    serverId: 40,
    idr: 10,
  },
  {
    priority: 1597411160576,
    nodAddress: "192.108.80.230",
    name: "Node # 1597411160576",
    serverId: 40,
    idr: 11,
  },
]

export const forms = [
  {
    idr: 1,
    Title: "Добавить",
    Tag: "server_add_form",
    Fields: [
      {
        idr: 1,
        name: "name",
        Label: "Наименование",
        Type: "textfield",
        Required: true,
      },
      {
        idr: 2,
        name: "description",
        Label: "Описание",
        Type: "textarea",
        Required: false,
      },
    ],
  },
]

export const aboutProduct = [
  {
    idr: 1,
    Product: "something",
    Name: "Название проекта",
    Version: "0.563a",
    Dnumber: "31.05",
    SerialNumber: "Add later, not use now",
    Contacts: "+6742443244 CEO +365445645645 Телефон поддержки",
    AddInfo: "Что то еще",
  },
]

export const permissions = [
  {idr: 1, text: "Нет"},
  {idr: 2, text: "Чтение"},
  {idr: 3, text: "Чтение и запись"},
]

export const objects = [
  {idr: 1, text: "Телефоны", permissions: 1},
  {idr: 2, text: "Сервера", permissions: 1},
  {idr: 3, text: "Задания", permissions: 1},
  {idr: 4, text: "История", permissions: 2},
  {idr: 5, text: "Журнал", permissions: 2},
  {idr: 6, text: "Роли", permissions: 1},
  {idr: 7, text: "Пользователи", permissions: 1},
]