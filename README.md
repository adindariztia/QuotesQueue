# MessageQueue
A publisher/consumer message queue using RabbitMQ on .NET 6 with SignalR

## General Structure
This repository consists of 2 main solutions: Rabbit Send API as the publisher and ConsumerWebApp as the consumer.

## Usage
- First, install [Erlang](https://www.rabbitmq.com/which-erlang.html) and [RabbitMQ](https://www.rabbitmq.com/install-windows.html). 
- After the installation is finished, open RabbitMQ sbin folder (for Windows, it is generally located in `C:\Windows\System32\cmd.exe` ).
- run `rabbitmq-service start`
- run `dotnet run` in each Project's folder