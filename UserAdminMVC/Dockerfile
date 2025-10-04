FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

ENV PORT=8080
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["UserAdminMVC.csproj", "./"]
RUN dotnet restore "UserAdminMVC.csproj"

COPY . .
RUN dotnet publish "UserAdminMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserAdminMVC.dll"]
