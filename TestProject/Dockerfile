FROM mcr.microsoft.com/dotnet/sdk:7.0 AS test
WORKDIR /src
COPY ["GB-Homework/GB-Homework.csproj", "GB-Homework/"]
COPY ["TestProject/TestProject.csproj", "TestProject/"]
RUN dotnet restore "TestProject/TestProject.csproj"
COPY . .
WORKDIR "/src/TestProject"
RUN dotnet build "TestProject.csproj" -c Release -o /app/build
RUN dotnet restore

# we actually run the tests during build once.
RUN dotnet test
ENTRYPOINT ["dotnet", "test"]