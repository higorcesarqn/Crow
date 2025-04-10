# Crow

## Visão Geral

Crow é um projeto inspirado nos corvos de Game of Thrones, que entregavam mensagens. Assim como esses corvos, o projeto tem como objetivo facilitar a comunicação entre diferentes partes de um sistema distribuído, entregando dados e comandos de forma estruturada.

## Estrutura do Projeto

- **Crow**: Contém a implementação principal do sistema.
- **Crow.Contracts**: Define os contratos (interfaces, modelos) para as mensagens e dados compartilhados.
- **Crow.Command**: Projeto responsável pelo processamento de comandos.
- **Crow.Query**: Projeto dedicado à execução de consultas e recuperação de dados.
- **Crow.Tests**: Conjunto de testes para validar a funcionalidade dos outros projetos.

## Como Construir

Utilize os comandos do .NET para construir e testar o projeto:

```
dotnet build
dotnet test
```
