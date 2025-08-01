# Favorite.Products API

API RESTful para gerenciamento de clientes e produtos favoritos. Realiza validação de produtos via integração com a API pública fakestoreapi.com. Possui autenticação JWT e controle de acesso baseado em perfil de usuário.

## Requisitos

| Item                    | Versão               | Observação                                    |
|------------------------|----------------------|----------------------------------------------|
| .NET SDK               | 8.0 ou superior      | Necessário para execução local               |
| Banco de Dados         | PostgreSQL           | Migração é aplicada automaticamente          |
| Docker (opcional)      | 20 ou superior       | Necessário para executar com containers      |
| Docker Compose         | Incluído no Docker   | Deve estar disponível no terminal            |
| Sistemas compatíveis   | Windows, Linux, Mac  |                                              |

> Para executar com containers, é necessário ter o Docker instalado:  
> [https://www.docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop)

> A **porta 8080 deve estar liberada** para execução via Docker.

## Execução

### Opção 1: Docker (API + Banco)

Executa a API e o banco de dados em containers.

1. Acesse a pasta:

```bash
cd favorite-products/src/devops/docker
```

2. Execute:

```bash
docker compose up -d --build
```

A API ficará disponível em:

- `http://localhost:8080`

### Acessar Swagger (via Docker)

```
http://localhost:8080/favoriteproducts/swagger/index.html
```

---

### Opção 2: Execução local (API local, banco via Docker)

1. Acesse a pasta:

```bash
cd favorite-products/src/devops/docker
```

2. Execute somente o container do banco de dados:

```bash
docker compose up -d postgres
```

3. Em seguida, rode a API localmente:

```bash
dotnet run --project src/Favorite.Products.API
```

A API será iniciada em:

- `https://localhost:7126`

### Acessar Swagger (execução local)

```
https://localhost:7126/favoriteproducts/swagger/index.html
```

## Autenticação

A autenticação é feita via JWT. É necessário obter um token realizando login.

### Endpoint de login

```http
POST /api/auth/login
```

Credenciais disponíveis para teste:

| Email               | Senha  | Perfil |
|---------------------|--------|--------|
| admin@teste.com     | 123    | Admin  |
| user@teste.com      | 123    | User   |

A resposta conterá o token JWT. Ele deve ser enviado no header das requisições protegidas:

```
Authorization: Bearer {token}
```

---

## Acesso por perfil

| Ação                          | Verbo e Rota                           | Perfil autorizado    |
|------------------------------|----------------------------------------|----------------------|
| Criar cliente                | POST /api/customers                    | Admin                |
| Atualizar cliente            | PUT /api/customers/{id}                | Admin                |
| Consultar cliente            | GET /api/customers/{id}                | Admin e User         |
| Favoritar produto            | POST /api/customers/{id}/favorites     | Admin                |
| Listar favoritos             | GET /api/customers/{id}/favorites      | Admin e User         |

---

## Códigos de resposta

| Código | Significado                              |
|--------|------------------------------------------|
| 400    | Requisição inválida                      |
| 401    | Token ausente ou inválido                |
| 403    | Acesso não autorizado (perfil incorreto) |
| 404    | Recurso não encontrado                   |
| 409    | Conflito (e-mail ou favorito duplicado)  |

---

## Tecnologias utilizadas

- .NET 8
- PostgreSQL
- Entity Framework Core
- JWT
- Docker (opcional)
- Arquitetura separada por camadas: API / Application / Infra / Domain

---

## Integração externa

Validação de produtos é realizada via:

```
https://fakestoreapi.com/products/{id}
```

---

## Testes Unitários

O domínio da aplicação foi validado com uma suíte de **testes unitários completos**, utilizando o projeto `Favorite.Products.Domain.Tests`.

- Todos os testes foram executados com sucesso (15/15).
- Abrangem regras de negócio para:
  - Criação e atualização de clientes
  - Adição e remoção de produtos favoritos
  - Validações de domínio como preços, títulos e URLs inválidas
  - Lógica de ativação/inativação de entidades

### Resultados dos testes:

<img width="565" height="473" alt="image" src="https://github.com/user-attachments/assets/8d885df2-1468-4c01-b637-9ebe6f46f09f" />


## Arquitetura dos componentes

<img width="1536" height="1024" alt="Arqui componentes" src="https://github.com/user-attachments/assets/6ea198bd-f3f8-42bb-846f-adadd9740933" />

## Licença

Este projeto está licenciado sob a Licença MIT.
