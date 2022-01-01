import Vue from "vue"
import Vuetify from "vuetify/lib/framework"
import VueI18n from "vue-i18n"
import { en, ru } from "vuetify/lib/locale" // проблема с импортом локалей

Vue.use(Vuetify)
Vue.use(VueI18n)

const messages = {
  en: {
    $vuetify: {
      ...en,
      // локализации на английский еще нет.
    },
  },
  ru: {
    $vuetify: {
      ...ru,
      dataTable: {
        itemsPerPageText: 'Записей на странице',
      },
      searchText: "Поиск",
      notSpecified: "Не указан",
      task: "Задание",
      tasks: "Задания",
      author: "Автор",
      description: "Описание",
      testServerList: "Список тестовых серверов",
      unchoosen: "Не выбран",
      isTestServer: "Тестовый сервер",
      runNow: "Запустить сейчас",
      date: "Дата",
      datePlaceholder: "ДД.ММ.ГГГГ",
      or: "или",
      telephoneSet: "Телефонный аппарат",
      lastNameFirstNameAndSite: "Фамилия И.О и площадка",
      lastNameFirstName: "ФИО",
      lastNameFirstNameInEnglish: "ФИО на английском",
      userLogin: "Логин пользователя",
      login: "Логин",
      username: "Имя пользователя",
      position: "Должность",
      password: "Пароль",
      port: "Порт",
      line: "Линия",
      period: "Период",
      current: "Текущие",
      completed: "Завершенные",
      cancelTask: "Отменить задание",
      areYouSureYouWantToCancelTheTask:
        "Вы уверены что хотите отменить задание",
      ok: "Ок",
      cancel: "Отмена",
      yes: "Да",
      no: "Нет",
      phoneNumber: "Номер телефона",
      dateRun: "Время запуска",
      dateEnd: "Время завершения",
      isTesting: "Тестирование",
      status: "Статус",
      log: "Журнал",
      actions: "Действия",
      invalidCharacters: "Некоректные символы",
      theFieldMustNotBeEmpty: "Поле не должно быть пустым",
      theFieldыMustNotBeEmpty: "Поля не должны быть пустыми",
      allowedNumberOfCharacters50: "Допустимое количество символов: 50",
      allowedNumberOfCharacters128: "Допустимое количество символов: 128",
      testServerNotSelected: "Не выбран тестовый сервер",
      notSelected: "Не выбран",
      theTaskStartTimeIsInThePast: "Время запуска задачи находится в прошлом.",
      dateAndTimeAreRequired: "Дата и время обязательны.",
      phones: "Телефоны",
      name: "Наименование",
      ipAddress: "IP-адрес",
      isActive: "Активен",
      isBlocked: "Заблокирован",
      isHidden: "Заблокирован",
      changeOfPhoneOwner: "Cмена владельца телефона",
      deleteServer: "Удалить сервер",
      deleteRole: "Удалить роль",
      areYouSureYouWantToDeleteTheServer: "Вы действительно хотите удалить сервер?",
      areYouSureYouWantToDeleteTheUser: "Вы действительно хотите удалить пользователя?",
      areYouSureYouWantToDeleteTheRole: "Вы действительно хотите удалить роль?",
      close: "Закрыть",
      continue: "Продолжить",
      delete: "Удалить",
      manufacturer: " Производитель",
      model: "Модель",
      timezone: "Часовой пояс",
      taskExecutionTime: "Время выполнения задания",
      taskExecutionTimeDefault: "Время выполнения задания по умолчанию",
      mode: "Режим",
      turnOn: "Включить",
      servers: "Серверы",
      connect: "Подключить",
      edit: "Редактировать",
      isEnable: "Включен",
      connectServer: "Подключить сервер",
      editServer: "Редактировать сервер",
      all: "Все",
      logs: "Журнал",
      roles: "Роли",
      role: "Роль",
      type: "Тип",
      action: "Действие",
      add: "Добавить",
      addWithTesting: "Добавить с тестированием",
      save: "Сохранить",
      addUser: "Добавить нового пользователя",
      enterAccompanyingInformation: "Введите сопроводительную информацию о пользователе",
      addRole: "Добавить роль",
      addTask: "Добавить задание",
      addTaskForNumber: "Добавить задание для номера ",
      editUser: "Редактировать пользователя",
      editRole: "Редактировать роль",
      users: "Пользователи",
      pages: "Страницы",
      email: "E-mail",
      serverNodes: "Ноды сервера",
      clusterVersion: "Версия кластера",
      priority: "Приоритет",
      areYouSureYouWantToDeleteTheNode: "Вы действительно хотите удалить ноду?",
      aboutProduct: "О продукте",
      leave: "Выйти",
      join: "Войти",
      versionNumber: "Номер версии",
      decimalNumber: "Децимальный номер",
      serialNumber: "Серийный номер",
      contacts: "Контакты",
      additionally: "Дополнительно",
      informationOfProduct: "Информация о продукте",
      selectType: "Выбрать тип",
      sendNotificationsAboutTheStatusOfTheTaskByEmail: "Отсылать уведомления о состоянии задачи на Email",
      object: "Объект",
      permissions: "Разрешения",
      selectTask: "Выбрать задание",
      performTask: "Выполнить задание",
      perform: "Выполнить",
      selectAtLeastOnePhone: "Выберите хотя бы один телефон",
      appointed: "Назначена",
      warning: "Предупреждение",
      warningDesc: "В текущей версии информационной системы данная функциональность недоступна"
    },
  },
}

// Create VueI18n instance with options
const i18n = new VueI18n({
  locale: 'ru', // set locale
  fallbackLocale: 'en',
  messages, // set locale messages
})

const opts = {
  lang: {
    t: (key, ...params) => i18n.t(key, params),
  },
  options: {
    customProperties: true,
  },
  theme: {
    themes: {
      light: {
        primary: "#1976d3",
        secondary: "#b6b6b6",
        accent: "#8c9eff",
        error: "#e64040",
      },
    },
  },
}

export default new Vuetify(opts)
