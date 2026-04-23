# Good Hamburger Web 🍔

Uma aplicação **web moderna** desenvolvida em **Blazor WebAssembly** (C# + .NET 8), com arquitetura **feature-based** e componentes reutilizáveis. Interface intuitiva para gerenciar pedidos, visualizar cardápio e acompanhar o status dos pedidos em tempo real.

---

## 📋 Índice

- [Características](#-características)
- [Requisitos](#-requisitos)
- [Instalação e Setup](#-instalação-e-setup)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Arquitetura Feature-Based](#-arquitetura-feature-based)
- [Componentes](#-componentes)
- [Features](#-features)
- [Serviços](#-serviços)
- [Modelos e DTOs](#-modelos-e-dtos)
- [Estilização](#-estilização)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Configuração](#-configuração)
- [Execução Local](#-execução-local)
- [Docker](#-docker)
- [Troubleshooting](#-troubleshooting)

---

## 🎯 Características

### Interface
- ✅ **Blazor WebAssembly**: Renderização interativa no navegador
- ✅ **Bootstrap 5**: Design responsivo e moderno
- ✅ **UI/UX Intuitiva**: Layout limpo e fácil de usar
- ✅ **Validação Frontend**: Feedback imediato ao usuário

### Funcionalidades
- ✅ **Visualizar Cardápio**: Itens com preços e detalhes
- ✅ **Criar Pedidos**: Interface intuitiva para montar combos
- ✅ **Cálculo Automático de Desconto**: Aplicação em tempo real
- ✅ **Gerenciar Pedidos**: Visualizar, editar e rastrear status
- ✅ **Histórico de Pedidos**: Ver pedidos anteriores

### Architecture
- ✅ **Feature-Based**: Organização por contexto de negócio
- ✅ **Shared Components**: Componentes reutilizáveis
- ✅ **Separation of Concerns**: Services, Models, Components bem definidos
- ✅ **Type-Safe**: Fortemente tipado com C#
- ✅ **Escalável**: Fácil adicionar novas features

---

## 🛠️ Requisitos

### Desenvolvimento Local
- **.NET 10 SDK** ou superior - [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **Visual Studio 2022** (recomendado) ou **VS Code** com C# DevKit
- **Git** - [Download](https://git-scm.com/)

### Runtime
- **Navegador moderno**: Chrome, Firefox, Edge, Safari (suporta WebSocket)
- **Conexão com API**: API GoodHamburger rodando em http://localhost:5000

### Docker
- **Docker Desktop** - [Download](https://www.docker.com/products/docker-desktop)
- **Docker Compose** v2.0+ (geralmente incluído)

---

## 📦 Instalação e Setup

### 1️⃣ Clone e Navegue

```bash
cd GoodHamburger-Web
```

### 2️⃣ Restaure as Dependências

```bash
dotnet restore
```

### 3️⃣ Execute a Aplicação

```bash
# Via .NET CLI
dotnet run --project GoodHamburger.Web

# Ou com hot-reload
dotnet watch run --project GoodHamburger.Web
```

A aplicação estará disponível em: **http://localhost:5114** (ou a porta configurada)

### 4️⃣ Verifique Conectividade com API

A aplicação tentará conectar com a API em `http://localhost:8080`. Se não conseguir:
- Verifique se a API está rodando
- Atualize a URL em `appsettings.json`

---

## 🏗️ Estrutura do Projeto

```
GoodHamburger-Web/
│
├── GoodHamburger.Web/                    # 🎯 Aplicação Blazor
│   │
│   ├── Shared/                           # 📦 Código Compartilhado
│   │   │
│   │   ├── Components/                   # Componentes Reutilizáveis
│   │   │   ├── Card.razor                # Card genérico
│   │   │   ├── MainLayout.razor          # Layout principal
│   │   │   ├── NavMenu.razor             # Menu de navegação
│   │   │   └── NotFound.razor            # Página 404
│   │   │
│   │   ├── Models/                       # Modelos Comuns
│   │   │   ├── MenuItem.cs               # Modelo de item do menu
│   │   │   ├── Order.cs                  # Modelo de pedido
│   │   │   └── OrderCreateDto.cs         # DTO para criar pedido
│   │   │
│   │   └── README.md                     # Documentação Shared
│   │
│   ├── Features/                         # ✨ Features Isoladas
│   │   │
│   │   ├── Menu/                         # Feature: Visualizar Cardápio
│   │   │   ├── MenuPage.razor            # Página raiz (@page "/menu")
│   │   │   └── Components/               # Componentes da feature
│   │   │       └── MenuItemRow.razor     # Linha da tabela de itens
│   │   │
│   │   ├── Orders/                       # Feature: Gerenciar Pedidos
│   │   │   ├── OrdersPage.razor          # Página raiz (@page "/orders")
│   │   │   └── Components/               # Componentes da feature
│   │   │       └── OrderCard.razor       # Card individual de pedido
│   │   │
│   │   ├── CreateOrder/                  # Feature: Criar Pedido
│   │   │   ├── CreateOrderPage.razor     # Página raiz (@page "/")
│   │   │   └── Components/               # Componentes da feature
│   │   │       ├── CategorySection.razor # Seção por categoria
│   │   │       └── MenuItemCard.razor    # Card de item para seleção
│   │   │
│   │   └── README.md
│   │
│   ├── Properties/
│   │   └── launchSettings.json           # Configuração de launch
│   │
│   ├── wwwroot/                          # 🌐 Arquivos Estáticos
│   │   ├── index.html                    # HTML principal
│   │   ├── appsettings.json              # Configs do cliente
│   │   ├── appsettings.Development.json
│   │   ├── css/
│   │   │   ├── app.css                   # Estilos globais
│   │   │   ├── bootstrap.min.css         # Bootstrap
│   │   │   └── custom/
│   │   │       ├── layout.css
│   │   │       ├── components.css
│   │   │       └── features.css
│   │   └── sample-data/
│   │       └── weather.json              # Dados de exemplo
│   │
│   ├── Program.cs                        # Configuração e startup
│   ├── App.razor                         # Componente raiz
│   ├── _Imports.razor                    # Imports globais
│   ├── GoodHamburger.Web.csproj          # Arquivo de projeto
│   ├── ARCHITECTURE.md                   # Documentação de arquitetura
│   └── README.md                         # README local
│
├── Dockerfile                             # 🐳 Build da imagem Docker
├── GoodHamburger.slnx                    # Solution file
└── README.md                              # Esta documentação
```

---

## 🎨 Arquitetura Feature-Based

### O que é Feature-Based Architecture?

Feature-Based Architecture organiza o código por **contextos de negócio** em vez de camadas técnicas. Cada feature é **independente e auto-contida**.

### Benefícios

| Benefício | Descrição |
|-----------|-----------|
| 🎯 **Coesão** | Código relacionado fica junto |
| 📦 **Modularidade** | Fácil entender uma feature isoladamente |
| 🚀 **Escalabilidade** | Adicionar/remover features é simples |
| 👥 **Colaboração** | Equipes trabalham em features diferentes |
| 🔄 **Reusabilidade** | Componentes compartilhados em múltiplas features |

### Estrutura da Feature

Cada feature segue o padrão:

```
Features/FeatureName/
├── FeatureNamePage.razor          # Página raiz (com @page)
├── Components/                    # Componentes específicos
│   ├── ComponentA.razor           # Componentes da feature
│   ├── ComponentB.razor
│   └── ComponentC.razor
├── Services/                      # Serviços específicos (opcional)
│   └── FeatureService.cs
└── README.md                      # Documentação da feature
```

### Shared vs Features

```
┌─────────────────────────────────────────┐
│         Shared (Reutilizável)           │
│  ┌─────────────────────────────────┐   │
│  │ Componentes Globais             │   │
│  │ - MainLayout                    │   │
│  │ - NavMenu                       │   │
│  │ - LoadingSpinner                │   │
│  └─────────────────────────────────┘   │
│  ┌─────────────────────────────────┐   │
│  │ Serviços Centralizados          │   │
│  │ - ApiClient                     │   │
│  │ - MenuService                   │   │
│  │ - OrderService                  │   │
│  └─────────────────────────────────┘   │
│  ┌─────────────────────────────────┐   │
│  │ Modelos Comuns                  │   │
│  │ - MenuItem, Order, etc          │   │
│  └─────────────────────────────────┘   │
└─────────────────────────────────────────┘
                     ↓
    ┌─────────────────────────────┐
    │   Features                  │
    ├─────────────────────────────┤
    │ Feature: Menu               │
    │ Feature: Orders             │
    │ Feature: CreateOrder        │
    └─────────────────────────────┘
```

---

## 🧩 Componentes

### Componentes Reutilizáveis (Shared)

#### 1. **MainLayout.razor**
Layout principal que envolve todas as páginas.

```html
<MainLayout>
  <NavMenu />
  <main class="container mt-4">
    @Body
  </main>
</MainLayout>
```

#### 2. **NavMenu.razor**
Menu de navegação com links para features.

```html
<nav class="navbar navbar-expand">
  <a class="navbar-brand" href="/">🍔 Good Hamburger</a>
  <ul class="navbar-nav">
    <li><NavLink href="/">Pedidos</NavLink></li>
    <li><NavLink href="/menu">Cardápio</NavLink></li>
    <li><NavLink href="/create-order">Novo Pedido</NavLink></li>
  </ul>
</nav>
```

#### 3. **Card.razor**
Componente de card genérico reutilizável.

---

## ✨ Features

### 1. Feature: Menu 🍽️

**Localização:** `Features/Menu/MenuPage.razor`

**Funcionalidades:**
- ✅ Listar todos os itens do cardápio
- ✅ Filtrar por tipo (Sanduíches, Acompanhamentos, Bebidas)
- ✅ Visualizar preço de cada item
- ✅ Status de disponibilidade

**Componentes:**
- `MenuItemRow.razor` - Linha individual na tabela de cardápio

**Exemplo de Uso:**
```html
@page "/menu"
<h2>Cardápio</h2>
<table>
    <MenuItemRow Item="item" />
</table>
```

---

### 2. Feature: Orders 📦

**Localização:** `Features/Orders/OrdersPage.razor`

**Funcionalidades:**
- ✅ Listar todos os pedidos do usuário
- ✅ Visualizar detalhes de cada pedido
- ✅ Ver status do pedido (Pendente, Preparando, Completo)
- ✅ Ver total e desconto aplicado
- ✅ Cancelar pedido
- ✅ Ver histórico de pedidos

**Componentes:**
- `OrderCard.razor` - Card individual com detalhes do pedido

**Exemplo de Uso:**
```html
@page "/orders"
<h2>Meus Pedidos</h2>
@foreach(var order in orders) {
    <OrderCard Order="order" />
}
```

---

### 3. Feature: CreateOrder ➕

**Localização:** `Features/CreateOrder/CreateOrderPage.razor`

**Funcionalidades:**
- ✅ Formulário para criar novo pedido
- ✅ Seletor visual de itens
- ✅ Cálculo automático de desconto
- ✅ Validação de combo (1 sanduíche obrigatório)
- ✅ Preview do total
- ✅ Submissão do pedido

**Componentes:**
- `CategorySection.razor` - Seção que agrupa itens por categoria
- `MenuItemCard.razor` - Card para selecionar item individual

**Exemplo de Uso:**
```html
@page "/"
<h2>Criar Novo Pedido</h2>
<CategorySection Category="Sandwich" />
```

---

## 🔧 Serviços

### Serviços Compartilhados (Shared/Services)

#### 1. **ApiClient.cs**
Cliente HTTP para comunicação com a API.

```csharp
public class ApiClient
{
    private readonly HttpClient _httpClient;
    
    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<T> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<T>();
    }
    
    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json"
        );
        var response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<T>();
    }
}
```

#### 2. **MenuService.cs**
Serviço para gerenciar dados do cardápio.

```csharp
public class MenuService
{
    private readonly ApiClient _apiClient;
    private List<MenuItem> _menuItems;
    
    public async Task<List<MenuItem>> GetMenuAsync()
    {
        _menuItems = await _apiClient.GetAsync<List<MenuItem>>("api/menu");
        return _menuItems;
    }
    
    public MenuItem GetItemById(int id)
    {
        return _menuItems.FirstOrDefault(m => m.Id == id);
    }
    
    public List<MenuItem> GetItemsByType(ItemType type)
    {
        return _menuItems.Where(m => m.Type == type).ToList();
    }
}
```

#### 3. **OrderService.cs**
Serviço para gerenciar pedidos.

```csharp
public class OrderService
{
    private readonly ApiClient _apiClient;
    
    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _apiClient.GetAsync<List<Order>>("api/orders");
    }
    
    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        return await _apiClient.GetAsync<Order>($"api/orders/{id}");
    }
    
    public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
    {
        return await _apiClient.PostAsync<Order>("api/orders", dto);
    }
    
    public async Task UpdateOrderStatusAsync(Guid id, string status)
    {
        await _apiClient.PutAsync($"api/orders/{id}", new { status });
    }
}
```

### Serviços Específicos de Feature

Cada feature pode ter seus próprios serviços para lógica específica:

```
Features/CreateOrder/Services/OrderCreationService.cs
Features/Orders/Services/OrderManagementService.cs
```

---

## 📊 Modelos e DTOs

### Models (Shared/Models)

#### **MenuItem.cs**
```csharp
public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }  // Sandwich, Side, Drink
    public decimal Price { get; set; }
    public bool Available { get; set; } = true;
    public string Description { get; set; }
}

public enum ItemType
{
    Sandwich,  // Sanduíche
    Side,      // Acompanhamento
    Drink      // Bebida
}
```

#### **Order.cs**
```csharp
public class Order
{
    public Guid Id { get; set; }
    public int[] Items { get; set; }
    public OrderItemDetail[] ItemDetails { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class OrderItemDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public enum OrderStatus
{
    Pending,     // Pendente
    Preparing,   // Preparando
    Completed,   // Completo
    Cancelled    // Cancelado
}
```

#### **OrderCreateDto.cs**
```csharp
public class OrderCreateDto
{
    public int[] ItemIds { get; set; }
    
    [Required(ErrorMessage = "Você deve selecionar itens")]
    public void Validate()
    {
        // Validação no cliente antes de enviar
        if (!ItemIds.Any())
            throw new ValidationException("Nenhum item selecionado");
            
        // Verificar se tem sanduíche
        var hasSandwich = ItemIds.Any(id => id >= 1 && id <= 3);
        if (!hasSandwich)
            throw new ValidationException("Pedido deve conter um sanduíche");
    }
}
```

---

## 🎨 Estilização

### Bootstrap 5
A aplicação usa **Bootstrap 5** para estilos responsivos.

```html
<!-- Botão -->
<button class="btn btn-primary">Criar Pedido</button>

<!-- Card -->
<div class="card">
    <div class="card-body">
        <h5 class="card-title">Pedido #123</h5>
        <p class="card-text">Total: R$ 10,00</p>
    </div>
</div>

<!-- Grid -->
<div class="row">
    <div class="col-md-6">...</div>
    <div class="col-md-6">...</div>
</div>
```

### Estilos Customizados

#### **wwwroot/css/app.css** - Estilos Globais
```css
:root {
    --primary-color: #ff6b35;
    --secondary-color: #004e89;
    --success-color: #2a9d8f;
    --danger-color: #e76f51;
}

body {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    background-color: #f5f5f5;
}

.navbar {
    background-color: var(--primary-color);
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.btn-primary {
    background-color: var(--primary-color);
    border-color: var(--primary-color);
}

.card {
    border: none;
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0,0,0,0.1);
}
```

#### **wwwroot/css/features/** - Estilos por Feature

Cada feature pode ter seu próprio CSS:
```
wwwroot/css/features/
├── menu.css
├── orders.css
└── create-order.css
```

---

## 📱 Responsividade

### Design Adaptativo

```html
<!-- Desktop: 3 colunas -->
<!-- Tablet: 2 colunas -->
<!-- Mobile: 1 coluna -->

<div class="row">
    <div class="col-lg-4 col-md-6 col-sm-12">
        <!-- Conteúdo -->
    </div>
</div>
```

### Breakpoints Bootstrap

| Breakpoint | Viewport | Classe |
|-----------|----------|---------|
| Extra Small | < 576px | `col-*` |
| Small | ≥ 576px | `col-sm-*` |
| Medium | ≥ 768px | `col-md-*` |
| Large | ≥ 992px | `col-lg-*` |
| Extra Large | ≥ 1200px | `col-xl-*` |

---

## 📦 Tecnologias Utilizadas

### Framework & Rendering
| Tecnologia | Versão | Propósito |
|-----------|--------|----------|
| **.NET** | 8.0 | Runtime |
| **Blazor Server** | 8.0 | Framework web interativo |
| **C#** | 12 | Linguagem de programação |
| **ASP.NET Core** | 8.0 | Framework web |

### Frontend
| Tecnologia | Versão | Propósito |
|-----------|--------|----------|
| **Bootstrap** | 5.3 | Framework CSS responsivo |
| **Razor** | - | Template engine |
| **JavaScript** | ES6+ | Interatividade |
| **CSS 3** | - | Estilização |

### HTTP & Communication
| Tecnologia | Propósito |
|-----------|----------|
| **HttpClient** | Comunicação com API |
| **JSON** | Serialização de dados |
| **SignalR** | Real-time (opcional) |

### Development
| Tecnologia | Propósito |
|-----------|----------|
| **dotnet CLI** | Build e execução |
| **Visual Studio 2022** | IDE |
| **Docker** | Containerização |

---

## ⚙️ Configuração

### launchSettings.json
```json
{
  "profiles": {
    "GoodHamburger.Web": {
      "commandName": "Project",
      "launchBrowser": true,
      "applicationUrl": "https://localhost:7098;http://localhost:5108",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

### appsettings.json
```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5000",
    "Timeout": 30
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### appsettings.Development.json
```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5000",
    "Timeout": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information"
    }
  }
}
```

### Program.cs
```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register services
builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<OrderService>();

// Configure HttpClient
builder.Services.AddHttpClient<ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
    client.Timeout = TimeSpan.FromSeconds(
        double.Parse(builder.Configuration["ApiSettings:Timeout"])
    );
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
```

---

## 🚀 Execução Local

### Opção 1: Via .NET CLI

```bash
# Restaurar dependências
dotnet restore

# Executar aplicação
dotnet run --project GoodHamburger.Web

# Com hot-reload (recompila ao salvar)
dotnet watch run --project GoodHamburger.Web
```

### Opção 2: Via Visual Studio

1. Abra `GoodHamburger.slnx`
2. Defina `GoodHamburger.Web` como startup project
3. Pressione `F5` ou clique em "Start Debugging"

### Opção 3: Via VS Code

```bash
# Abrir pasta no VS Code
code .

# Terminal integrado
dotnet watch run --project GoodHamburger.Web
```

### Acesso

```
http://localhost:5108
ou
https://localhost:7098 (HTTPS)
```

---

## 🐳 Docker

### Executar com Docker Compose

```bash
# Na raiz do projeto
docker-compose up -d
```

Isso irá:
1. Construir imagem da Web
2. Iniciar contêiner da Web
3. Expor na porta **3000**
4. Conectar com API no mesmo compose

### Acessar

```
http://localhost:5246
```

### Dockerfile

```dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "GoodHamburger.Web/GoodHamburger.Web.csproj"
RUN dotnet publish "GoodHamburger.Web/GoodHamburger.Web.csproj" \
    -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "GoodHamburger.Web.dll"]
```

### Logs Docker

```bash
docker-compose logs -f web
```

### Parar Contêineres

```bash
docker-compose down
```

---

## 🆕 Adicionar Nova Feature

### Passo 1: Criar Estrutura

```bash
mkdir GoodHamburger.Web/Features/YourFeature
mkdir GoodHamburger.Web/Features/YourFeature/Components
mkdir GoodHamburger.Web/Features/YourFeature/Services
```

### Passo 2: Criar Página

`Features/YourFeature/YourFeaturePage.razor`:
```razor
@page "/your-feature"
@using GoodHamburger.Web.Shared.Models
@using GoodHamburger.Web.Features.YourFeature.Components

<h2>Your Feature</h2>

@if (loading)
{
    <LoadingSpinner />
}
else if (items == null)
{
    <AlertBox Type="danger" Message="Erro ao carregar dados" />
}
else
{
    <YourComponent Items="items" />
}

@code {
    private bool loading = true;
    private List<MenuItem> items = new();
    
    protected override async Task OnInitializedAsync()
    {
        items = await menuService.GetMenuAsync();
        loading = false;
    }
}
```

### Passo 3: Criar Componentes

`Features/YourFeature/Components/YourComponent.razor`:
```razor
@using GoodHamburger.Web.Shared.Models

<div class="your-component">
    @foreach (var item in Items)
    {
        <div class="item">
            <h5>@item.Name</h5>
            <p>@item.Price.ToString("C")</p>
        </div>
    }
</div>

@code {
    [Parameter]
    public List<MenuItem> Items { get; set; } = new();
}
```

### Passo 4: Adicionar Navegação

Em `Shared/Components/NavMenu.razor`:
```html
<li>
    <NavLink href="/your-feature">Your Feature</NavLink>
</li>
```

---

## 🔍 Troubleshooting

### Erro: API não conecta

**Sintoma:** Erro ao listar cardápio/pedidos

**Solução:**
```csharp
// Em appsettings.json, atualize a URL
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5000"  // Verifique a porta
  }
}
```

### Erro: Porta já em uso

**Sintoma:** "Address already in use"

**Solução:**
```json
// Em launchSettings.json
{
  "applicationUrl": "https://localhost:7099;http://localhost:5109"
}
```

### Erro: CORS bloqueado

**Sintoma:** "Access to XMLHttpRequest has been blocked by CORS policy"

**Solução:**
Verifique se a API tem CORS habilitado em Program.cs:
```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
```

### Razor files não são reconhecidos

**Sintoma:** Intellisense não funciona em arquivos .razor

**Solução:**
1. Instale extensão "C# Dev Kit" no VS Code
2. Reinicie VS Code
3. Abra arquivo .razor novamente

### Dados não atualizam

**Sintoma:** Mudanças na API não aparecem na Web

**Solução:**
1. Limpe browser cache (Ctrl+Shift+Del)
2. Force refresh da página (Ctrl+F5)
3. Reinicie aplicação Blazor

---

## 📝 Convenções de Código

### Nomenclatura Razor

```razor
<!-- Páginas: PascalCase + "Page" -->
MenuPage.razor
OrdersPage.razor
CreateOrderPage.razor

<!-- Componentes: PascalCase -->
MenuList.razor
OrderCard.razor
ItemSelector.razor

<!-- Componentes internos: _ComponentName -->
_MenuItem.razor
_OrderItem.razor
```

### C# em Componentes

```csharp
// Parâmetros
[Parameter]
public List<MenuItem> Items { get; set; } = new();

[Parameter]
public EventCallback<int> OnItemSelected { get; set; }

// Variáveis locais
private bool isLoading = false;
private string errorMessage = string.Empty;

// Métodos
private async Task LoadDataAsync() { }
private void HandleClick() { }
```

### Binding de Dados

```razor
<!-- One-way: propriedade para UI -->
<div>@item.Name</div>

<!-- Two-way: propriedade <-> UI -->
<input @bind="searchText" />

<!-- Event binding -->
<button @onclick="HandleClick">Click</button>
<button @onclick="() => HandleClickWithParam(item.Id)">Click</button>
```

---

## 📚 Recursos Úteis

- [Documentação Blazor](https://learn.microsoft.com/blazor/)
- [Bootstrap 5 Docs](https://getbootstrap.com/docs/5.0)
- [Razor Syntax Reference](https://learn.microsoft.com/aspnet/core/mvc/views/razor)
- [Component Lifecycle](https://learn.microsoft.com/aspnet/core/blazor/components/lifecycle)

---

## 🎓 Boas Práticas

### Performance
- 🟢 Use `@key` em loops para melhorar renderização
- 🟢 Minimize re-renders desnecessários
- 🟢 Use `OnInitializedAsync` para dados iniciais
- 🟢 Cache dados quando possível

### Segurança
- 🔒 Valide entrada do usuário no frontend e backend
- 🔒 Use HTTPS em produção
- 🔒 Implemente autenticação/autorização
- 🔒 Sanitize dados antes de renderizar

### Manutenibilidade
- 📝 Documente componentes complexos
- 📝 Use nomes descritivos
- 📝 Mantenha componentes pequenos (<200 linhas)
- 📝 Reutilize código através de Shared

---

## 📞 Suporte

### Issues Comuns

**P: Por que a página fica em branco?**
R: Verifique console do navegador (F12) para erros JavaScript/Blazor

**P: Como fazer chamadas AJAX?**
R: Use `HttpClient` injetado via `ApiClient`

**P: Posso usar jQuery?**
R: Sim, mas prefira recursos nativos de Blazor

---

## 📄 Licença

Projeto sob licença MIT.

---

**Última atualização:** 23 de Abril de 2026
