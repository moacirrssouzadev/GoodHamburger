# Good Hamburger 🍔

**Sistema completo de gerenciamento de pedidos para lanchonete** com backend robusto em C# .NET 8 e frontend moderno em Blazor WebAssembly. Arquitetura profissional com clean code, SOLID principles, testes automatizados e containerização Docker.

---

## 📋 Índice

- [Visão Geral](#-visão-geral)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Quick Start](#-quick-start)
- [Componentes](#-componentes)
- [Arquitetura](#-arquitetura)
- [Features](#-features)
- [Menu e Desconto](#-menu-e-desconto)
- [Tecnologias](#-tecnologias)
- [Como Executar](#-como-executar)
- [Docker Compose](#-docker-compose)
- [Documentação Completa](#-documentação-completa)
- [Troubleshooting](#-troubleshooting)

---

## 🎯 Visão Geral

Good Hamburger é uma **solução full-stack** para gerenciar pedidos em uma lanchonete, incluindo:

### 🔙 Backend (API REST)
- **.NET 8 + C#** - Framework moderno e performático
- **Arquitetura em 4 camadas** - Domain, Application, Infrastructure, API
- **Entity Framework Core** - ORM com SQLite
- **30+ Testes Unitários** - 100% cobertura de regras de negócio
- **Swagger/OpenAPI** - Documentação interativa
- **Docker** - Pronto para produção

### 🎨 Frontend (Aplicação Web)
- **Blazor WebAssembly** - Renderização interativa C# no navegador
- **Arquitetura Feature-Based** - Código organizado por contexto
- **Bootstrap 5** - Design responsivo e moderno
- **Componentes Reutilizáveis** - Código limpo e manutenível
- **Docker** - Containerizado com Nginx e pronto para deploy

---

## 📂 Estrutura do Projeto

```
GoodHamburger/
│
├── GoodHamburger-Api/                  # 🔙 Backend
│   ├── src/
│   │   ├── GoodHamburger.Domain/       # Entidades e interfaces
│   │   ├── GoodHamburger.Application/  # Serviços e DTOs
│   │   ├── GoodHamburger.Infrastructure/ # Repositories e BD
│   │   └── GoodHamburger.API/          # Controllers e endpoints
│   ├── tests/
│   │   └── GoodHamburger.Tests/        # Testes unitários
│   ├── Dockerfile
│   ├── GoodHamburger.slnx
│   └── README.md                       # Documentação detalhada
│
├── GoodHamburger-Web/                  # 🎨 Frontend
│   ├── GoodHamburger.Web/
│   │   ├── Shared/                     # Componentes e serviços compartilhados
│   │   ├── Features/                   # Features isoladas (Menu, Orders, etc)
│   │   ├── wwwroot/                    # Arquivos estáticos (CSS, JS)
│   │   ├── Program.cs                  # Configuração
│   │   └── README.md                   # Documentação de arquitetura
│   ├── Dockerfile
│   ├── GoodHamburger.slnx
│   └── README.md                       # Documentação detalhada
│
├── docker-compose.yml                  # 🐳 Orquestração
├── README.md                            # 📄 Este arquivo
└── .gitignore                           # Configuração do Git

```

---

## 🚀 Quick Start

### Requisitos
- **.NET 8 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Docker Desktop** (opcional) - [Download](https://www.docker.com/products/docker-desktop)
- **Git**

### Opção 1: Executar Localmente (Desenvolvimento)

#### Terminal 1 - API
```bash
cd GoodHamburger-Api
dotnet restore
dotnet run --project src/GoodHamburger.API
```
API disponível em: **http://localhost:8080**
Swagger em: **http://localhost:8080/swagger**

#### Terminal 2 - Web
```bash
cd GoodHamburger-Web
dotnet restore
dotnet run --project GoodHamburger.Web
```
Web disponível em: **http://localhost:5114**

---

### Opção 2: Executar com Docker Compose (Recomendado)

```bash
# Na raiz do projeto
docker-compose up -d
```

Será iniciado:
- 🔙 **API** em http://localhost:8080
- 🎨 **Web** em http://localhost:5246

### Parar os Contêineres
```bash
docker-compose down
```

---

## 🧩 Componentes

### Camadas da Aplicação

```
┌──────────────────────────────────────────────┐
│         Frontend (Blazor + React)            │ ← Navegador
│  - Componentes Razor                         │
│  - Services (MenuService, OrderService)      │
│  - Features (Menu, Orders, CreateOrder)      │
└────────────────────┬─────────────────────────┘
                     │ HTTP/JSON
┌────────────────────▼─────────────────────────┐
│        API Layer (Controllers)               │ ← REST Endpoints
│  - MenuController (/api/menu)                │
│  - OrdersController (/api/orders)            │
└────────────────────┬─────────────────────────┘
                     │ Injeção de Dependência
┌────────────────────▼─────────────────────────┐
│    Application Layer (Services)              │ ← Regras de Negócio
│  - MenuService                               │
│  - OrderService                              │
│  - DiscountCalculator                        │
└────────────────────┬─────────────────────────┘
                     │
┌────────────────────▼─────────────────────────┐
│  Infrastructure Layer (Repositories)         │ ← Acesso a Dados
│  - MenuRepository                            │
│  - OrderRepository                           │
│  - GoodHamburgerDbContext (EF Core)          │
└────────────────────┬─────────────────────────┘
                     │
┌────────────────────▼─────────────────────────┐
│      Domain Layer (Entities)                 │ ← Modelos Puros
│  - MenuItem, Order, OrderItem, Enums         │
└──────────────────────────────────────────────┘
                     │
              SQLite Database
```

---

## 🎨 Arquitetura

### Backend: Clean Architecture (4 Camadas)

```
Domain Layer
  ├── Entities (MenuItem, Order, OrderItem)
  ├── Enums (ItemType, OrderStatus)
  └── Interfaces (Contracts)

Application Layer
  ├── DTOs (Transfer Objects)
  ├── Services (MenuService, OrderService)
  └── Helpers (DiscountCalculator)

Infrastructure Layer
  ├── Data (DbContext)
  ├── Repositories (DataAccess)
  └── Migrations (EF Core)

API Layer
  ├── Controllers (HTTP Endpoints)
  ├── Program.cs (Configuration)
  └── Startup (Dependency Injection)
```

### Frontend: Feature-Based Architecture

```
Shared (Reutilizável)
  ├── Components (MainLayout, NavMenu, etc)
  ├── Services (ApiClient, MenuService, OrderService)
  ├── Models (MenuItem, Order, etc)
  └── Helpers (Formatters, Validators)

Features (Isoladas por Contexto)
  ├── Menu/ (Visualizar cardápio)
  ├── Orders/ (Gerenciar pedidos)
  └── CreateOrder/ (Criar novo pedido)
```

---

## ✨ Features

### 🍽️ Feature: Menu
- Listar todos os itens do cardápio
- Filtrar por tipo (Sanduíches, Acompanhamentos, Bebidas)
- Visualizar preços e disponibilidade
- Selecionar itens para novo pedido

### 📦 Feature: Orders
- Listar todos os pedidos
- Visualizar detalhes completos
- Ver status e total com desconto
- Cancelar pedido
- Rastrear histórico

### ➕ Feature: CreateOrder
- Formulário intuitivo para criar pedido
- Seletor visual de itens
- Validação em tempo real
- Cálculo automático de desconto
- Confirmação com ID do pedido

---

## 🎁 Menu e Regras de Desconto

### Cardápio

| Categoria | Item | Preço |
|-----------|------|-------|
| 🥪 Sanduíches | X Burger | R$ 5,00 |
| | X Egg | R$ 4,50 |
| | X Bacon | R$ 7,00 |
| 🍟 Acompanhamentos | Batata Frita | R$ 2,00 |
| 🥤 Bebidas | Refrigerante | R$ 2,50 |

### Descontos Automáticos

| Combo | Desconto |
|-------|----------|
| Sanduíche + Batata + Refrigerante | **20%** |
| Sanduíche + Refrigerante | **15%** |
| Sanduíche + Batata | **10%** |
| Sanduíche Apenas | **0%** |

### Regras
- ✅ Cada pedido deve ter **1 sanduíche obrigatoriamente**
- ✅ Máximo 1 sanduíche por pedido
- ✅ Máximo 1 acompanhamento por pedido
- ✅ Máximo 1 bebida por pedido

---

## 📊 API Endpoints

### Base URL
```
http://localhost:5000/api
```

### 🍽️ Menu Endpoints

```http
GET /api/menu
```
Retorna lista de todos os itens do cardápio.

---

### 🛒 Orders Endpoints

```http
GET /api/orders
```
Lista todos os pedidos.

```http
GET /api/orders/{id}
```
Obtém pedido específico por ID.

```http
POST /api/orders
Content-Type: application/json

{
  "itemIds": [1, 4, 5]
}
```
Cria novo pedido com validação automática.

```http
PUT /api/orders/{id}
Content-Type: application/json

{
  "status": "Completed"
}
```
Atualiza status do pedido.

```http
DELETE /api/orders/{id}
```
Cancela pedido.

---

## 📦 Tecnologias

### Backend (API)
| Tecnologia | Versão | Uso |
|-----------|--------|-----|
| .NET | 8.0 | Runtime |
| C# | 12 | Linguagem |
| Entity Framework Core | 8.0.0 | ORM |
| SQLite | 8.0.0 | Banco de Dados |
| Swashbuckle.AspNetCore | 6.5.0 | Swagger/OpenAPI |
| xUnit | - | Testes |
| Moq | - | Mocks |

### Frontend (Web)
| Tecnologia | Versão | Uso |
|-----------|--------|-----|
| .NET | 8.0 | Runtime |
| Blazor Server | 8.0 | Framework Web |
| C# | 12 | Linguagem |
| Bootstrap | 5.3 | Estilos |
| Razor | - | Template Engine |
| JavaScript | ES6+ | Interatividade |

### DevOps
| Tecnologia | Uso |
|-----------|-----|
| Docker | Containerização |
| Docker Compose | Orquestração |

---

## 🚀 Como Executar

### 1️⃣ Desenvolvimento Local

#### Setup Inicial
```bash
# Clone o repositório
git clone <seu-repo>
cd GoodHamburger

# Instale .NET 8 SDK
# https://dotnet.microsoft.com/download/dotnet/8.0
```

#### Executar API
```bash
cd GoodHamburger-Api
dotnet restore
dotnet ef database update --project src/GoodHamburger.Infrastructure
dotnet run --project src/GoodHamburger.API
```

#### Executar Web (novo terminal)
```bash
cd GoodHamburger-Web
dotnet restore
dotnet run --project GoodHamburger.Web
```

### 2️⃣ Com Docker Compose

```bash
# Na raiz do projeto
docker-compose up -d

# Acompanhar logs
docker-compose logs -f

# Parar
docker-compose down
```

### 3️⃣ Build Individual

```bash
# API
cd GoodHamburger-Api
docker build -t goodhamburger-api .

# Web
cd GoodHamburger-Web
docker build -t goodhamburger-web .

# Executar
docker run -p 8080:8080 goodhamburger-api
docker run -p 3000:80 goodhamburger-web
```

---

## 🐳 Docker Compose

### docker-compose.yml

```yaml
services:
  api:
    image: goodhamburger-api
    build:
      context: ./GoodHamburger-Api
      dockerfile: src/GoodHamburger.API/Dockerfile
    container_name: goodhamburger-api
    ports:
      - "8080:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_HTTP_PORTS=8080
    volumes:
      - sqlite_data:/app/data
    healthcheck:
      test: ["CMD", "curl", "-f", "http://localhost:8080/health"]
      interval: 10s
      timeout: 5s
      retries: 3

  web:
    image: goodhamburger-web
    build:
      context: ./GoodHamburger-Web
      dockerfile: Dockerfile
    container_name: goodhamburger-web
    ports:
      - "3000:80"
    depends_on:
      - api
    environment:
      - API_BASE_URL=http://api:8080

volumes:
  sqlite_data:
```

### Acessar

| Serviço | URL |
|---------|-----|
| Web | http://localhost:5246 |
| API | http://localhost:8080 |
| Swagger | http://localhost:8080/swagger |

---

## 📚 Documentação Completa

Para documentação detalhada de cada projeto:

### 🔙 Backend API
[GoodHamburger-Api/README.md](./GoodHamburger-Api/README.md)

Inclui:
- Setup e instalação
- Arquitetura em detalhes
- Todos os endpoints
- Testes unitários
- Configuração Docker
- Troubleshooting

### 🎨 Frontend Web
[GoodHamburger-Web/README.md](./GoodHamburger-Web/README.md)

Inclui:
- Setup e instalação
- Arquitetura Feature-Based
- Componentes e Services
- Como adicionar Features
- Estilização e Responsividade
- Troubleshooting

---

## 🔄 Fluxo de Requisição Completo

```
1. Usuário acessa http://localhost:3000
                    ↓
2. Página de Pedidos carrega (Feature: Orders)
                    ↓
3. Web chama GET /api/orders
                    ↓
4. API retorna lista de pedidos
                    ↓
5. Usuário clica em "Novo Pedido"
                    ↓
6. Feature: CreateOrder carrega
                    ↓
7. Usuário seleciona itens (ex: X Burger, Batata, Refri)
                    ↓
8. Sistema calcula desconto (20%) em tempo real
                    ↓
9. Usuário confirma pedido
                    ↓
10. Web envia POST /api/orders com itemIds
                    ↓
11. API valida pedido
    - Verifica se tem sanduíche
    - Verifica itens duplicados
    - Calcula total e desconto
                    ↓
12. API salva no banco de dados
                    ↓
13. API retorna pedido criado
                    ↓
14. Web mostra confirmação com ID
                    ↓
15. Página de Pedidos atualiza automaticamente
```

---

## ✅ Testes

### Executar Testes da API

```bash
cd GoodHamburger-Api
dotnet test
```

### Cobertura

- **30+ testes** unitários
- **100% cobertura** de regras de negócio
- **DiscountCalculator**: Todos os 4 casos de desconto
- **OrderService**: Validações e operações CRUD

---

## 🔧 Configuração

### Variáveis de Ambiente

#### API
```bash
ASPNETCORE_ENVIRONMENT=Development      # Development | Production | Docker
ASPNETCORE_HTTP_PORTS=5000             # Porta HTTP
ConnectionStrings__DefaultConnection=Data Source=GoodHamburger.db
```

#### Web
```bash
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_HTTP_PORTS=3000
ApiSettings__BaseUrl=http://localhost:5000
```

### Arquivos de Configuração

```
GoodHamburger-Api/src/GoodHamburger.API/
├── appsettings.json                 # Produção
├── appsettings.Development.json     # Desenvolvimento
└── appsettings.Docker.json          # Docker

GoodHamburger-Web/wwwroot/
├── appsettings.json                 # Produção
└── appsettings.Development.json     # Desenvolvimento
```

---

## 🔍 Troubleshooting

### API não conecta com Web

**Solução:**
```json
// Em wwwroot/appsettings.json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5000"
  }
}
```

### Porta já em uso

**Solução:**
```bash
# Listar processos usando porta 5000
lsof -i :5000

# Matar processo
kill -9 <PID>

# Ou alterar porta em launchSettings.json
```

### Banco de dados corrompido

**Solução:**
```bash
cd GoodHamburger-Api
rm GoodHamburger.db
dotnet ef database update --project src/GoodHamburger.Infrastructure
```

### Docker não consegue conectar

**Solução:**
```bash
# Reiniciar contêineres
docker-compose restart

# Ver logs
docker-compose logs -f

# Limpar e reconstruir
docker-compose down -v
docker-compose up --build
```

---

## 📈 Roadmap

### Próximas Melhorias
- [ ] Autenticação JWT com roles
- [ ] Integração com payment gateway
- [ ] Notificações em tempo real (SignalR)
- [ ] Relatórios de vendas
- [ ] Sistema de cupons/promoções
- [ ] App mobile (Maui)
- [ ] Integração com redes sociais
- [ ] Analytics e metrics

---

## 📝 Convenções de Código

### C#
```csharp
public class MenuItem { }              // PascalCase
public interface IOrderService { }     // I + PascalCase
private string _name;                  // _camelCase
public string Name { get; set; }       // PascalCase
```

### Razor
```razor
<!-- Páginas -->
MenuPage.razor
OrdersPage.razor

<!-- Componentes -->
MenuList.razor
OrderCard.razor

<!-- Internos -->
_MenuItem.razor
```

---

## 👥 Contribução

### Como Contribuir

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

### Padrões

- ✅ Siga as convenções de código
- ✅ Escreva testes para novas features
- ✅ Atualize documentação
- ✅ Use mensagens de commit descritivas

---

## 📞 Suporte

### Problemas Comuns

**P: Por onde começo?**
R: Leia este README, depois consulte os READMEs detalhados de cada projeto.

**P: Qual versão do .NET preciso?**
R: .NET 8 SDK ou superior. [Download aqui](https://dotnet.microsoft.com/download/dotnet/8.0)

**P: Posso usar Windows/Mac/Linux?**
R: Sim! .NET 8 é multiplataforma.

**P: Posso usar sem Docker?**
R: Sim, execute localmente com `dotnet run`

---

## 📄 Licença

Este projeto está sob licença **MIT**. Veja o arquivo LICENSE para detalhes.

---

## 👨‍💻 Autores

**Good Hamburger Team**
- Email: contact@goodhamburger.com
- GitHub: [seu-usuario/goodhamburger](https://github.com)

---

## 📅 Versões

| Versão | Data | Mudanças |
|--------|------|----------|
| 1.0.0 | 2026-04-23 | Versão inicial com arquitetura completa |

---

## 🔗 Links Úteis

- [.NET 8 Documentation](https://learn.microsoft.com/dotnet/)
- [Blazor Documentation](https://learn.microsoft.com/blazor/)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [Docker Documentation](https://docs.docker.com/)
- [Bootstrap 5](https://getbootstrap.com/)

---

**Última atualização:** 23 de Abril de 2026

---

## 🎓 Para Começar Agora

### Desenvolvimento Rápido (5 minutos)
```bash
# 1. Clonar e navegar
git clone <repo>
cd GoodHamburger

# 2. Terminal 1 - API
cd GoodHamburger-Api && dotnet run --project src/GoodHamburger.API

# 3. Terminal 2 - Web
cd GoodHamburger-Web && dotnet run --project GoodHamburger.Web

# 4. Abrir no navegador
http://localhost:5108
```

### Com Docker (3 minutos)
```bash
# 1. Docker Compose
docker-compose up -d

# 2. Abrir no navegador
http://localhost:5246
```

---

**Bom desenvolvimento! 🚀 Qualquer dúvida, consulte a documentação detalhada de cada projeto.**
