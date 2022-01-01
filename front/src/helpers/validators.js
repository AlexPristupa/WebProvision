export const required = (value) => {
  return value != null && value.length > 0 ? true : "Это обязательное поле"
}
export const requiredDate = (value) => {
  return value != null && value.length > 0 ? true : "Это обязательные поля"
}
export const requiredTime = (value) => {
  return value != null && value.length > 0 ? true : ""
}
export const maxLength50 = (value) => {
  return value != null && value.length <= 30 ? true : "Максимальное количество символов - 50."
}
export const maxLength128 = (value) => {
  return value != null && value.length <= 128 ? true : "Максимальное количество символов - 128."
}
