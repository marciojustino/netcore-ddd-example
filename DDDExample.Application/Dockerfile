FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DDDExample.Application/DDDExample.Application.csproj", "DDDExample.Application/"]
RUN dotnet restore "DDDExample.Application/DDDExample.Application.csproj"
COPY . .
WORKDIR "/src/DDDExample.Application"
RUN dotnet build "DDDExample.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DDDExample.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDDExample.Application.dll"]
