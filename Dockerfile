FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Auto_Blog/Auto_Blog.csproj", "Auto_Blog/"]
RUN dotnet restore "Auto_Blog/Auto_Blog.csproj"
COPY . .
WORKDIR "/src/Auto_Blog"
RUN dotnet build "Auto_Blog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auto_Blog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auto_Blog.dll"]