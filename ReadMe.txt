გამარჯობა,

შევეცადე მაქსიმალურად დამეცვა SOLID პრინციპები. პროექტი არის MediatR, CQRS Pattern-ების გამოყენებით
აწყობილი.

რაც შეეხება უშუალოდ მუშაობას.

ბაზების დასამატებლად appsettings.json -> ConnectionStrings -> SQL - ში უნდა ჩაიწეროს მისამართი

მიგრაციის გასაკეთებლად აუცილებლად უნდა მოინიშნოს Default Project-ად src/NMMS.Persistence

და პროექტის დასაწყებად უნდა მოინიშნოს src/NMMS.API Set As StartUp Project

დისტრიბუტორის დამატების დროს პირველ რიგში აუცილებლად უნდა აიტვირთოს სურათი შემდეგ მიღებული UniqueId
მიებმება დისტრიბუტორს.
სურათის ასატვირთად აუცილებელია რომ D:\\ ამ მისამართით იყოს შექმნილი NMMSFiles პაპკა . დისკის სხვა სახელის
შემთხვევაში D:\\NMMSFiles <- ეს მისამართი შეიძლება შეიცვალოს appsettings.json ->FileManagerOptions->
StoragePath-ში

File Download-ის შემთხვევაში Swagger-ში მოსული Curl ში მისამართის გამოყენებით შეგიძლიათ ნახოთ თქვენს მიერ
ატვირთული სურათი.

Products
GetProductsSales ის მეთოდში სიმარტივისთვის დავტოვე დაკომენტარებული ველები რათა DateTime შემოწმება
გამარტივდეს. 

დროის სიმწირის გამო ვერ მოვასწარი დამემატებინა XUnitTest, Logging ასევე გამომესწორებინა პატარა ბაგები.
ასევე ვერ მოვასწარი "დისტრიბუტორის ბონუსი" ის გაკეთება.