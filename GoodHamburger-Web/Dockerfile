# Build stage - Blazor WebAssembly
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy project files and restore
COPY ["GoodHamburger.Web/GoodHamburger.Web.csproj", "GoodHamburger.Web/"]
RUN dotnet restore "GoodHamburger.Web/GoodHamburger.Web.csproj"

# Copy source code and publish as WebAssembly
COPY . .
WORKDIR "/app/GoodHamburger.Web"
RUN dotnet publish "GoodHamburger.Web.csproj" -c Release -o /app/publish

# Runtime stage - Nginx to serve WebAssembly static files
FROM nginx:alpine AS runtime

# Install curl for healthcheck
RUN apk add --no-cache curl

WORKDIR /usr/share/nginx/html

# Copy published Blazor WebAssembly files to Nginx
# Em .NET 8+, o publish de WASM coloca os arquivos estáticos diretamente na pasta de saída
# ou dentro de uma subpasta wwwroot. Vamos copiar o conteúdo da pasta de saída.
COPY --from=build /app/publish/wwwroot .

# Copy nginx configuration
COPY ./nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

HEALTHCHECK --interval=30s --timeout=3s --start-period=10s --retries=3 \
    CMD curl -f http://localhost/ || exit 1

ENTRYPOINT ["nginx", "-g", "daemon off;"]
