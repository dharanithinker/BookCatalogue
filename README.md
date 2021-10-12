# BookCatalogue
BookCatalogue microservice which includes the CRUD operations on books.


In this microservice user can perform CRUD operations and based on the key events the data will be published into RabbitMQ. Further actions will be taken care based on the
message type.

Exception Handling implemented globally. Because of this implementation try catch blocks not used inside business logic layer.

**Technologies Used : **
.NET Core(5.0)
EF Core(5.0)
RabbitMQ(6.2.2)
Swagger(SwashBuckle) - 5.6.3

**Design Patterns Used**
Singleton Design Pattern
Repository Design Pattern

