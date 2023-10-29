FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GB-Homework/GB-Homework.csproj", "GB-Homework/"]
RUN dotnet restore "GB-Homework/GB-Homework.csproj"
COPY . .
WORKDIR "/src/GB-Homework"
RUN dotnet build "GB-Homework.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GB-Homework.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GB-Homework.dll"]
