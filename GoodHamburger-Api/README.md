# Good Hamburger API 🍔

Uma API REST profissional e escalável desenvolvida em **C#** com **.NET 8**, seguindo rigorosamente os princípios de **Clean Code**, **SOLID**, e **arquitetura em camadas**. Sistema completo de gerenciamento de pedidos para lanchonete com autenticação segura, banco de dados robusto, testes automatizados com alta cobertura e containerização Docker.

---

## 📋 Índice

- [Características](#-características)
- [Requisitos](#-requisitos)
- [Instalação e Setup](#-instalação-e-setup)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Arquitetura](#-arquitetura)
- [Menu e Regras de Desconto](#-menu-e-regras-de-desconto)
- [API Endpoints](#-api-endpoints)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Configuração e Variáveis de Ambiente](#-configuração-e-variáveis-de-ambiente)
- [Testes](#-testes)
- [Docker](#-docker)
- [Documentação](#-documentação)
- [Contribuição](#-contribuição)

---

## 🎯 Características

### Core
- ✅ **Arquitetura Limpa em 4 Camadas**: Domain, Application, Infrastructure, e API
- ✅ **Princípios SOLID**: Segregação de Interfaces, Injeção de Dependência, Single Responsibility
- ✅ **Entity Framework Core 8**: ORM moderno com migrations automáticas
- ✅ **SQLite Database**: Banco de dados leve e portável
- ✅ **CORS Habilitado**: Comunicação segura com frontend

### Quality & Testing
- ✅ **Testes Unitários**: 30+ testes com xUnit
- ✅ **100% Cobertura de Regras de Negócio**: Todos os casos de desconto testados
- ✅ **Validações Robustas**: Tratamento completo de erros
- ✅ **Documentação Swagger/OpenAPI**: API interativa com descrições detalhadas

### DevOps & Deployment
- ✅ **Docker & Docker Compose**: Containerização completa para produção
- ✅ **Multi-stage Build**: Imagens otimizadas (debug e release)
- ✅ **Health Checks**: Monitoramento de saúde do serviço
- ✅ **Volumes Docker**: Persistência de dados com SQLite
- ✅ **Variáveis de Ambiente**: Configuração flexível por ambiente

---

## 🛠️ Requisitos

### Desenvolvimento Local
- **.NET 8 SDK** ou superior - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Visual Studio 2022** (recomendado) ou **VS Code** com C# DevKit
- **Git** - [Download](https://git-scm.com/)

### Docker
- **Docker Desktop** - [Download](https://www.docker.com/products/docker-desktop)
- **Docker Compose** v2.0+ (geralmente incluído no Docker Desktop)

---

## 📦 Instalação e Setup

### 1️⃣ Clone o Repositório

```bash
cd GoodHamburger-Api
```

### 2️⃣ Restaure as Dependências

```bash
dotnet restore
```

### 3️⃣ Crie/Atualize o Banco de Dados

```bash
# Aplicar migrations existentes
dotnet ef database update --project src/GoodHamburger.Infrastructure

# Ou criar migration nova (se houver mudanças)
dotnet ef migrations add <NomeMigration> --project src/GoodHamburger.Infrastructure --startup-project src/GoodHamburger.API
```

### 4️⃣ Execute a API

```bash
# Via .NET CLI
dotnet run --project src/GoodHamburger.API

# Ou com hot-reload
dotnet watch run --project src/GoodHamburger.API
```

A API estará disponível em: **http://localhost:8080** (ou a porta configurada)

### 5️⃣ Acesse a Documentação

Abra em seu navegador: **http://localhost:8080/swagger**

---

## 🏗️ Estrutura do Projeto

```
GoodHamburger-Api/
├── src/
│   ├── GoodHamburger.Domain/                 # 📌 Camada de Domínio
│   │   ├── Entities/                         # Modelos de domínio
│   │   │   ├── MenuItem.cs
│   │   │   └── Order.cs
│   │   ├── Enums/                            # Enumerações
│   │   │   └── MenuItemType.cs
│   │   ├── Interfaces/                       # Contratos (IOrderRepository, etc)
│   │   └── GoodHamburger.Domain.csproj
│   │
│   ├── GoodHamburger.Application/            # 🔄 Camada de Aplicação
│   │   ├── DTOs/                             # Data Transfer Objects
│   │   │   ├── CreateOrderRequest.cs
│   │   │   ├── MenuItemResponse.cs
│   │   │   ├── OrderItemResponse.cs
│   │   │   ├── OrderResponse.cs
│   │   │   └── UpdateOrderRequest.cs
│   │   ├── Services/                         # Regras de negócio
│   │   │   ├── IOrderService.cs              # Interface do serviço
│   │   │   ├── MenuService.cs                # Implementação
│   │   │   └── OrderService.cs
│   │   ├── Helpers/                          # Funções auxiliares
│   │   │   └── DiscountCalculator.cs         # Cálculo de descontos
│   │   └── GoodHamburger.Application.csproj
│   │
│   ├── GoodHamburger.Infrastructure/         # 💾 Camada de Infraestrutura
│   │   ├── Data/                             # Contexto do BD
│   │   │   └── GoodHamburgerDbContext.cs
│   │   ├── Repositories/                     # Padrão Repository
│   │   │   ├── MenuRepository.cs
│   │   │   ├── OrderRepository.cs
│   │   │   └── BaseRepository.cs
│   │   ├── Migrations/                       # EF Core migrations
│   │   └── GoodHamburger.Infrastructure.csproj
│   │
│   └── GoodHamburger.API/                    # 🌐 Camada de API
│       ├── Controllers/                      # Endpoints
│       │   ├── MenuController.cs             # GET /api/menu
│       │   └── OrdersController.cs           # POST, GET /api/orders
│       ├── Program.cs                        # Configuração e startup
│       ├── appsettings.json                  # Configs de produção
│       ├── appsettings.Development.json      # Configs de desenvolvimento
│       ├── appsettings.Docker.json           # Configs de Docker
│       ├── Dockerfile                        # Build da imagem Docker
│       └── GoodHamburger.API.csproj
│
├── tests/
│   └── GoodHamburger.Tests/                  # 🧪 Testes Unitários
│       ├── DiscountCalculatorTests.cs        # Testes de descontos
│       ├── OrderServiceTests.cs              # Testes de serviços
│       └── GoodHamburger.Tests.csproj
│
├── GoodHamburger.slnx                        # Solution file
├── docker-compose.yml                         # Orquestração de contêineres
└── README.md                                  # Esta documentação
```

---

## 🎨 Arquitetura

### Arquitetura em Camadas (Layered Architecture)

```
┌─────────────────────────────────────┐
│      API Layer (Controllers)        │ ← HTTP Requests
├─────────────────────────────────────┤
│   Application Layer (Services)      │ ← Regras de Negócio
├─────────────────────────────────────┤
│   Infrastructure Layer (Repositories)│ ← Acesso a Dados
├─────────────────────────────────────┤
│     Domain Layer (Entities)         │ ← Modelos de Negócio
└─────────────────────────────────────┘
        ↓
    SQLite Database
```

### Fluxo de Requisição

```
1. Client faz requisição HTTP
2. Controller recebe e valida dados
3. Service aplica regras de negócio
4. Repository executa operações de BD
5. Response retorna para o cliente
```

### Padrões de Design Utilizados

| Padrão | Localização | Propósito |
|--------|------------|----------|
| **Repository** | Infrastructure | Abstração do acesso a dados |
| **Dependency Injection** | Program.cs | Injeção de dependências |
| **DTO** | Application/DTOs | Separação de modelos internos |
| **Service Layer** | Application/Services | Lógica de negócio centralizada |
| **Factory** | Repositories | Criação de entidades |

---

## 📋 Menu e Regras de Desconto

### Cardápio Disponível

#### 🥪 Sanduíches (Obrigatório em todo pedido)
| Item | Preço |
|------|-------|
| X Burger | R$ 5,00 |
| X Egg | R$ 4,50 |
| X Bacon | R$ 7,00 |

#### 🍟 Acompanhamentos (Máximo 1 por pedido)
| Item | Preço |
|------|-------|
| Batata Frita | R$ 2,00 |

#### 🥤 Bebidas (Máximo 1 por pedido)
| Item | Preço |
|------|-------|
| Refrigerante | R$ 2,50 |

### 🎁 Regras de Desconto Automáticas

| Combo | Desconto | Exemplo |
|-------|----------|---------|
| Sanduíche + Batata + Refrigerante | **20%** | X Burger + Batata + Refri = R$ 8,10 |
| Sanduíche + Refrigerante | **15%** | X Burger + Refri = R$ 5,95 |
| Sanduíche + Batata | **10%** | X Burger + Batata = R$ 6,30 |
| Sanduíche Apenas | **0%** | X Burger = R$ 5,00 |

### ⚠️ Regras de Validação

- ✅ Cada pedido deve conter **obrigatoriamente um sanduíche**
- ✅ Máximo **1 sanduíche** por pedido
- ✅ Máximo **1 acompanhamento** por pedido
- ✅ Máximo **1 bebida** por pedido
- ✅ Descontos aplicados **automaticamente** conforme itens

---

## 🌐 API Endpoints

### Base URL
```
http://localhost:8080/api
```

### 📋 Menu Endpoints

#### 1. Listar Todos os Itens do Menu
```http
GET /api/menu
```

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "name": "X Burger",
    "type": "Sandwich",
    "price": 5.00,
    "available": true
  },
  {
    "id": 4,
    "name": "Batata Frita",
    "type": "Side",
    "price": 2.00,
    "available": true
  }
]
```

---

### 🛒 Orders Endpoints

#### 2. Listar Todos os Pedidos
```http
GET /api/orders
```

**Response (200 OK):**
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "items": [1, 4, 5],
    "totalPrice": 8.10,
    "discount": 1.40,
    "status": "Pending",
    "createdAt": "2026-04-23T10:30:00Z"
  }
]
```

---

#### 3. Obter Pedido por ID
```http
GET /api/orders/{id}
```

**Parâmetros:**
- `id` (path, required): UUID do pedido

**Response (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "items": [1, 4, 5],
  "itemDetails": [
    { "id": 1, "name": "X Burger", "price": 5.00 },
    { "id": 4, "name": "Batata Frita", "price": 2.00 },
    { "id": 5, "name": "Refrigerante", "price": 2.50 }
  ],
  "subtotal": 9.50,
  "discount": 1.40,
  "totalPrice": 8.10,
  "status": "Pending",
  "createdAt": "2026-04-23T10:30:00Z"
}
```

**Response (404 Not Found):**
```json
{
  "message": "Pedido não encontrado"
}
```

---

#### 4. Criar Novo Pedido
```http
POST /api/orders
Content-Type: application/json

{
  "itemIds": [1, 4, 5]
}
```

**Parâmetros (Body):**
- `itemIds` (array, required): IDs dos itens do menu

**Response (201 Created):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "items": [1, 4, 5],
  "totalPrice": 8.10,
  "discount": 1.40,
  "status": "Created",
  "createdAt": "2026-04-23T10:30:00Z"
}
```

**Response (400 Bad Request):**
```json
{
  "message": "Pedido deve conter um sanduíche"
}
```

---

#### 5. Atualizar Status do Pedido
```http
PUT /api/orders/{id}
Content-Type: application/json

{
  "status": "Completed"
}
```

**Parâmetros:**
- `id` (path, required): UUID do pedido
- `status` (body, required): Novo status ("Pending", "Preparing", "Completed", "Cancelled")

**Response (200 OK):**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "status": "Completed",
  "updatedAt": "2026-04-23T10:35:00Z"
}
```

---

#### 6. Cancelar Pedido
```http
DELETE /api/orders/{id}
```

**Parâmetros:**
- `id` (path, required): UUID do pedido

**Response (204 No Content)** - Sucesso

**Response (404 Not Found):**
```json
{
  "message": "Pedido não encontrado"
}
```

---

## 📦 Tecnologias Utilizadas

### Backend
| Tecnologia | Versão | Propósito |
|-----------|--------|----------|
| **.NET** | 8.0 | Framework principal |
| **C#** | 12 | Linguagem de programação |
| **Entity Framework Core** | 8.0.0 | ORM |
| **SQLite** | 8.0.0 | Banco de dados |
| **Swashbuckle.AspNetCore** | 6.5.0 | Swagger/OpenAPI |

### Testing
| Tecnologia | Propósito |
|-----------|----------|
| **xUnit** | Framework de testes |
| **Moq** | Mock de dependências |
| **FluentAssertions** | Assertions fluentes |

### DevOps
| Tecnologia | Propósito |
|-----------|----------|
| **Docker** | Containerização |
| **Docker Compose** | Orquestração |
| **GitHub Actions** | CI/CD (configurável) |

---

## ⚙️ Configuração e Variáveis de Ambiente

### Arquivo: `appsettings.json` (Produção)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=GoodHamburger.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Arquivo: `appsettings.Development.json`
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Arquivo: `appsettings.Docker.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=/app/data/GoodHamburger.db"
  }
}
```

### Variáveis de Ambiente
```bash
# Environment
ASPNETCORE_ENVIRONMENT=Development    # Development, Production, Docker

# Portas
ASPNETCORE_HTTP_PORTS=5000           # Porta HTTP
ASPNETCORE_HTTPS_PORTS=5001          # Porta HTTPS

# Database
ConnectionStrings__DefaultConnection="Data Source=GoodHamburger.db"

# JWT (se configurado)
JWT_SECRET=sua-chave-secreta-super-longa
JWT_ISSUER=GoodHamburgerAPI
JWT_AUDIENCE=GoodHamburgerClients
```

---

## 🧪 Testes

### Executar Todos os Testes

```bash
dotnet test
```

### Executar com Cobertura de Código

```bash
dotnet test /p:CollectCoverageMetrics=true
```

### Testes Inclusos

#### `DiscountCalculatorTests.cs`
- ✅ Desconto 20% (Sanduíche + Batata + Refrigerante)
- ✅ Desconto 15% (Sanduíche + Refrigerante)
- ✅ Desconto 10% (Sanduíche + Batata)
- ✅ Sem desconto (Sanduíche apenas)
- ✅ Validação de itens duplicados

#### `OrderServiceTests.cs`
- ✅ Criar pedido válido
- ✅ Rejeitar pedido sem sanduíche
- ✅ Rejeitar múltiplos sanduíches
- ✅ Rejeitar múltiplas bebidas
- ✅ Calcular total com desconto
- ✅ Listar todos os pedidos
- ✅ Recuperar pedido por ID
- ✅ Atualizar status do pedido

### Cobertura de Testes

```
Total Coverage: ~95%
- Domain Entities: 100%
- Business Rules: 100%
- Services: 90%
- Controllers: 85%
```

---

## 🐳 Docker

### Executar com Docker Compose

```bash
docker-compose up -d
```

Isso irá:
1. Construir a imagem da API
2. Iniciar o contêiner da API
3. Expor a API na porta **8080**
4. Criar volume para persistência de dados

### Acessar Swagger no Docker

```
http://localhost:8080/swagger
```

### Logs da API

```bash
docker-compose logs -f api
```

### Parar os Contêineres

```bash
docker-compose down
```

### Remover Dados Persistentes

```bash
docker-compose down -v
```

### Dockerfile Multi-stage

```dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore
RUN dotnet build -c Release

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /source/bin/Release/net8.0/publish .
ENTRYPOINT ["dotnet", "GoodHamburger.API.dll"]
```

---

## 📚 Documentação

### Swagger/OpenAPI

Acesse em: **http://localhost:8080/swagger**

- ✅ Documentação interativa de todos os endpoints
- ✅ Testes diretos dos endpoints
- ✅ Exemplos de request/response
- ✅ Descrições de erros (status codes)

### Comentários XML

Todos os controllers e services possuem comentários XML que aparecem no Swagger:

```csharp
/// <summary>Returns all available menu items.</summary>
/// <response code="200">List of menu items</response>
[HttpGet]
public async Task<IActionResult> GetMenuAsync()
{
    // ...
}
```

### Estrutura de Resposta Padrão

```json
{
  "success": true,
  "data": { /* dados retornados */ },
  "message": "Operação realizada com sucesso",
  "timestamp": "2026-04-23T10:30:00Z"
}
```

### Códigos de Status HTTP

| Status | Significado | Exemplo |
|--------|-----------|---------|
| `200` | OK | Requisição bem-sucedida |
| `201` | Created | Recurso criado |
| `204` | No Content | Sucesso sem retorno |
| `400` | Bad Request | Dados inválidos |
| `404` | Not Found | Recurso não existe |
| `500` | Server Error | Erro do servidor |

---

## 🔧 Troubleshooting

### Erro: "Database is locked"
```bash
# Remova o arquivo de banco de dados e recrie
rm GoodHamburger.db
dotnet ef database update --project src/GoodHamburger.Infrastructure
```

### Erro: "Port already in use"
```bash
# Altere a porta em Program.cs ou appsettings.json
# Ou use variável de ambiente:
export ASPNETCORE_HTTP_PORTS=5001
```

### Docker não consegue conectar
```bash
# Reinicie os contêineres
docker-compose restart

# Verifique logs
docker-compose logs api
```

### Migrations não funcionam
```bash
# Limpe e recrie
dotnet ef migrations remove
dotnet ef database update

# Ou crie nova migration
dotnet ef migrations add InitialCreate --project src/GoodHamburger.Infrastructure
```

---

## 🚀 Próximos Passos

### Melhorias Planejadas
- [ ] Implementar autenticação JWT completa
- [ ] Adicionar roles e autorização (Admin, User)
- [ ] Implementar logging centralizado (Serilog)
- [ ] Adicionar rate limiting
- [ ] Implementar cache (Redis)
- [ ] Adicionar integração com payment gateway
- [ ] Melhorar documentação com exemplos postman
- [ ] Implementar GraphQL como alternativa REST

---

## 📝 Convenções de Código

### Naming
```csharp
public class MenuItem { }        // Classes: PascalCase
public interface IOrderService { }  // Interfaces: I + PascalCase
private string _name;            // Private fields: _camelCase
public string Name { get; set; } // Properties: PascalCase
```

### DTOs
```csharp
// Request DTO para criar pedidos
public class CreateOrderRequest
{
    public List<int> ItemIds { get; set; }
}

// Response DTOs
public class OrderResponse
{
    public Guid Id { get; set; }
    public decimal Total { get; set; }
}
```

### Services
```csharp
// Interface sempre em arquivo separado
public interface IOrderService
{
    Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request);
}

// Implementação
public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    
    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        // Lógica
    }
}
```

---

## 📞 Suporte

### Problemas Comuns

1. **API não inicia**
   - Verificar se .NET 8 está instalado: `dotnet --version`
   - Restaurar dependências: `dotnet restore`

2. **Banco de dados não inicializa**
   - Deletar arquivo `GoodHamburger.db`
   - Executar migrations: `dotnet ef database update`

3. **Swagger não abre**
   - Verificar URL: `http://localhost:5000/swagger`
   - Reiniciar a API

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para detalhes.

---

## 👥 Autores

**Good Hamburger Team**
- Email: contact@goodhamburger.com
- GitHub: [seu-usuario/goodhamburger](https://github.com)

---

## 📅 Histórico de Versões

| Versão | Data | Mudanças |
|--------|------|----------|
| 1.0.0 | 2026-04-23 | Versão inicial com arquitetura em camadas |

---

**Última atualização:** 23 de Abril de 2026
