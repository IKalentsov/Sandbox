// Добавить миграцию в нужную папку
add-migration Имя миграции -outputdir "Persistence/Migrations"
// Обновить базу данных
update-database
// Откатить миграцию
update-database -TargetMigration: Initial
// Удалить миграцию
remove-migration
