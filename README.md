# NuGet RabbitMQ Fidelizar+

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](#)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](#)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](#)

Projeto base para utilização de um pacote NuGet que implementa integração performática e escalável com **RabbitMQ**, ideal para aplicações que precisam publicar, consumir continuamente ou consumir sob demanda de filas.

> ⚠️ Este projeto é um exemplo funcional voltado para **uso real em MicroSaaS**, com foco em economia de recursos e alta performance.

---

## 📁 Estrutura do Projeto

```
FidelizarMais.RabbitMQ.NugetExample.Full/
├── Program.cs
├── TestMessage.cs
├── TestMessageHandler.cs
├── RabbitConnectionManager.cs
├── RabbitPublisher.cs
├── RabbitConsumer.cs
├── RabbitPullConsumer.cs
├── IRabbitConsumerHandler.cs
└── FidelizarMais.RabbitMQ.NugetExample.Full.csproj
```

---

## 🔄 Branches Padrão

- `dev` – Desenvolvimento contínuo
- `sandbox` – Homologação / pré-produção
- `master` – Produção

> O fluxo de trabalho segue: `dev` → `sandbox` → `master`

---

## 🚀 Tecnologias Utilizadas

- .NET Core 8
- RabbitMQ (via RabbitMQ.Client)
- Publicação e consumo via AMQP
- Estrutura modular e reutilizável
- Injeção de dependência com `AddRabbit(...)`

---

## 🧪 Executando o Projeto

```bash
dotnet restore
dotnet run
```

---

## 📤 Exemplo de Publicação

```csharp
await publisher.PublishAsync("", "fila-teste", new TestMessage { Texto = "Olá via NuGet!" });
```

---

## 📥 Exemplo de Consumo Sob Demanda

```csharp
var mensagem = pullConsumer.ObterMensagem<TestMessage>("fila-teste");
```

---

## 🔄 Exemplo de Consumo Contínuo

O handler `TestMessageHandler` é registrado via DI e escutará a fila automaticamente.

---

## 📚 Finalidade

Este projeto exemplifica como construir um **conector leve e robusto** com RabbitMQ sem consumir recursos desnecessários. Ideal para micro serviços, MicroSaaS e plataformas de eventos.
