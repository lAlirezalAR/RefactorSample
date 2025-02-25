#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Contact/Contact.Api/Contact.Api.csproj", "Services/Contact/Contact.Api/"]
COPY ["Utilities/Utilities.Framework/Utilities.Framework.csproj", "Utilities/Utilities.Framework/"]
COPY ["Services/Contact/Contact.Infrastructure/Contact.Infrastructure.csproj", "Services/Contact/Contact.Infrastructure/"]
COPY ["Services/Contact/Contact.Domain/Contact.Domain.csproj", "Services/Contact/Contact.Domain/"]
COPY ["Services/Contact/Contact.Application/Contact.Application.csproj", "Services/Contact/Contact.Application/"]
RUN dotnet restore "Services/Contact/Contact.Api/Contact.Api.csproj"
COPY . .
WORKDIR "/src/Services/Contact/Contact.Api"
RUN dotnet build "Contact.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contact.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contact.Api.dll"]