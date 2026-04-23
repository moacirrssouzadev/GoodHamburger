# 📁 Data Directory

Diretório para armazenar arquivos de dados da aplicação Good Hamburger API.

## Estrutura

```
data/
├── sqlite/           # Bancos de dados SQLite
│   ├── .gitkeep     # Garante que o diretório seja rastreado pelo git
│   └── *.db         # Arquivos do banco de dados (não rastreados)
└── README.md        # Este arquivo
```

## SQLite Database

- **Local Development**: `data/sqlite/GoodHamburger.db`
- **Docker Environment**: Configurado via variável de ambiente

### Migrations

Para aplicar as migrations do Entity Framework:

```bash
# Gerar migration
dotnet ef migrations add <MigrationName> --project GoodHamburger.Infrastructure

# Aplicar migrations
dotnet ef database update --project GoodHamburger.Infrastructure
```

### .gitignore

Os arquivos `.db` **não são rastreados** pelo git para evitar conflitos e manter o repositório limpo.

## Notas

- ✅ O diretório `sqlite/` é mantido no git via `.gitkeep`
- ✅ Os arquivos `.db` são ignorados pelo `.gitignore`
- ✅ Seguro para trabalhar localmente sem conflitos
