# DotNet.MicroSaaS.RabbitMQ

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](#)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](#)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](#)

Projeto base para utilização de um pacote NuGet que implementa integração performática, resiliente e observável com **RabbitMQ**, ideal para MicroSaaS ou sistemas de mensageria intensiva.

---

## 📁 Estrutura de Pastas

```
DotNet.MicroSaaS.RabbitMQ/
├── Configuration/
├── Core/
│   └── Interfaces/
├── Infrastructure/
├── Handlers/
├── Models/
├── Program.cs
└── DotNet.MicroSaaS.RabbitMQ.csproj
```

---

## 🚀 Tecnologias Utilizadas

- .NET 8
- RabbitMQ.Client
- Microsoft.Extensions.DependencyInjection
- System.Text.Json
- Serilog (observabilidade)
- Polly (resiliência)

---

## 💡 Recursos Adicionais

### 🔁 Resiliência com Polly

- Retry
- Timeout
- Circuit Breaker

### 👁 Observabilidade com Serilog

- Console Logging
- File Logging
- Estrutura pronta para uso com Application Insights ou Seq

---

## 📦 Instalação dos pacotes

```bash
dotnet add package RabbitMQ.Client
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package System.Text.Json
dotnet add package Microsoft.Extensions.Logging.Abstractions
dotnet add package Serilog
dotnet add package Serilog.Sinks.Console
dotnet add package Polly
```

---

## ▶️ Como executar

```bash
dotnet restore
dotnet run
```

---

## 📤 Exemplo de Publicação

```csharp
await publisher.PublishAsync("", "fila-teste", new TestMessage { Texto = "Olá via NuGet!" });
```

## 📥 Exemplo de Consumo Sob Demanda

```csharp
var mensagem = pullConsumer.ObterMensagem<TestMessage>("fila-teste");
```

## 🔄 Exemplo de Consumo Contínuo

O handler `TestMessageHandler` será executado automaticamente via DI.

---

## ✅ Finalidade

Este projeto exemplifica como construir um **conector leve, robusto e escalável** com RabbitMQ, preparado para sistemas modernos orientados a eventos.
