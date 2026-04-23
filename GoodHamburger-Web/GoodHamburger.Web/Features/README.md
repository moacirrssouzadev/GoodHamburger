# Features

Cada feature é um módulo independente da aplicação, com suas páginas, componentes locais e serviços.

## Estrutura de Feature

```
Features/
├── FeatureName/
│   ├── FeatureNamePage.razor    (Página raiz, @page)
│   ├── Components/              (Componentes específicos da feature)
│   └── Services/                (Serviços específicos da feature)
```

## Features Atuais

- **Orders**: Listagem e gerenciamento de pedidos
- **Menu**: Visualização do cardápio
- **CreateOrder**: Criar e editar pedidos
