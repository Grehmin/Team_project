# Team Project
Репозиторий для командной разработки первой комманды группы ПВ315.
Интервал актуальности с 18.03.2025 - ???

## Общая концепция проекта
Будет база данных, подключается к приложение на WinForms.
TODO: Добавить более подробное описание проекта

## Желаемая функциональность
- Авторизация, без авторизации основное окно приложения открываться не будет
- Просмотр общего ассортимента с возможностью заказа. У админов будет разблокирована кнопка добавления товара в ассортимент (?).
- Текущая корзина пользователя
- Просмотр Личного кабинета с историей заказов
- сюда пишем что ещё хотим реализовать

Храниться все будет в базе данных примерно таким образом:
- Users:Name, Password, id_Roles; | Для хранения данных авторизации
    Roles: id_Users, itsAdmin[bool] | -//-

TODO: Более подробно описать структуру проекта

Страницы в основной форме приложении:
- Страница авторизации              | (с невидимыми элементами для регистрации или отдельную страницу регистрации?)
- Форма основная                    | с переходами: Личный кабинет, About
- Страница личного кабинета         | с переходами: Основная, История заказов
- Страница истории заказов          | с переходами: Личный кабинет
- Страница About
- TODO: в будущем добавить больше структур, по необходимости


