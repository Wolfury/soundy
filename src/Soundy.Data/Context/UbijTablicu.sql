DELETE FROM [TableName]
-- Set current ID to "1"
DBCC CHECKIDENT ([TableName], RESEED, 1)