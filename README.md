# MicroSaaS Template

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](#)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](#)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](#)

Template base para criação de projetos **MicroSaaS** com **ASP.NET Core 8**, arquitetura **DDD**, banco **ClickHouse** e integração com APIs externas.

> ⚠️ Este repositório é voltado para **estudo, experimentação e testes de arquitetura**. Não é recomendado para uso direto em produção sem ajustes.

---

## 📁 Estrutura do Projeto


---

## 🔄 Branches Padrão

- `dev` – Desenvolvimento contínuo
- `sandbox` – Homologação / pré-produção
- `master` – Produção

> O fluxo de trabalho segue: `dev` → `sandbox` → `master`

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 8
- Domain-Driven Design (DDD)
- ClickHouse
- ReceitaWS ou outras APIs públicas
- Frontend com Razor Pages ou SPA

---

## 🧪 Executando os Testes

```bash
dotnet test
