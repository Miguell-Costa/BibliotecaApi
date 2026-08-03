# Biblioteca API

## Tecnologias

-   ASP.NET Core Web API
-   Entity Framework Core
-   SQL Server
-   ASP.NET Identity
-   JWT (RSA - chave pública/privada)
-   Swagger
-   Google Books API

## Arquitetura

-   Controllers
-   Services
-   Repositories
-   DTOs
-   Mapeamentos
-   Result`<T>`{=html} para operações

## Autenticação

-   API de autenticação separada
-   Validação de JWT através de chave pública obtida por HTTP
-   Policies para autorização

## Funcionalidades implementadas

### Livros

-   Criar
-   Listar
-   Obter por Id
-   Atualizar
-   Apagar

### Autores

-   Criar
-   Listar
-   Obter por Id
-   Atualizar
-   Apagar

### Categorias

-   Criar
-   Listar
-   Obter por Id
-   Atualizar
-   Apagar

### Exemplares

-   Criar exemplar
-   Listar exemplares
-   Listar exemplares de um livro
-   Obter exemplar por Id
-   Atualizar exemplar
-   Apagar exemplar

## Integração Google Books

-   Pesquisa por ISBN
-   Consumo da API através de HttpClient
-   Desserialização da resposta em DTOs
-   Importação automática de livros
-   Criação automática do autor caso não exista
-   Criação automática da categoria caso não exista
-   Conversão do ano de publicação
-   Importação da capa do livro

## Regras de negócio

-   ISBN único
-   Validação da existência de autor
-   Validação da existência de categoria
-   Impedir apagar categorias com livros associados
-   Importação apenas quando o livro é encontrado
-   Validação da existência de autor e categoria durante a importação

## Próximos passos

-   Empréstimos
-   Reservas
-   Gestão de disponibilidade dos exemplares
-   Paginação e pesquisa
-   Testes unitários
