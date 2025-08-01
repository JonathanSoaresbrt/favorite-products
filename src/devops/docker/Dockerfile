FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./favorite-products.sln ./
COPY ./src/Favorite.Products.API/Favorite.Products.API.csproj ./src/Favorite.Products.API/
COPY ./src/Favorite.Products.Application/Favorite.Products.Application.csproj ./src/Favorite.Products.Application/
COPY ./src/Favorite.Products.Domain/Favorite.Products.Domain.csproj ./src/Favorite.Products.Domain/
COPY ./src/Favorite.Products.Infra/Favorite.Products.Infra.csproj ./src/Favorite.Products.Infra/
COPY ./tests/Favorite.Products.Domain.Tests/Favorite.Products.Domain.Tests.csproj ./tests/Favorite.Products.Domain.Tests/

RUN dotnet restore

COPY ./src ./src
COPY ./tests ./tests

RUN dotnet build -c Release

RUN dotnet test tests/Favorite.Products.Domain.Tests/Favorite.Products.Domain.Tests.csproj --no-build --nologo --verbosity minimal

FROM build AS publish
RUN dotnet publish src/Favorite.Products.API/Favorite.Products.API.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Favorite.Products.API.dll"]
