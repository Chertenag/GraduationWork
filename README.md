# GraduationWork

Данная работа представляет из себе решение, которое разделено на 3 основных части:

1. Data уровень - работа непосредственно с базой данных. Реализовано на основе Entity Framework. По сравнению с двумя другими подходами имеет следующие преимущества и недостатки.  
  
Преимущества:
- отсутсвие необходимости написания низкоуровневых запросов SQL;
- автоматизированные операции CRUD;
- EF упрощает определение отношений между сущностями и работу с ними.  
  
Недостатки:
- найменее производительный;
- в некоторых случаях написанные вручную SQL-запросы могут быть более эффективными, чем запросы, созданные EF.
2. Core уровень - здесь заключена вся бизнес логика касательно сущностей из БД. Является прослойкой между Web и Data уровнями. Контролирует валидацию данных и, если надо, их преобразование в обе стороны.
3. Web уровень - пользовательский уровень доступа к различным CRUD операциям в БД. Релизован на основе ASP.Net.
  
#### ER диаграмма БД.
![GraduationDataBase](https://github.com/Chertenag/GraduationWork/assets/79260785/68d90120-779b-406a-ac8d-58af548b73d6)
