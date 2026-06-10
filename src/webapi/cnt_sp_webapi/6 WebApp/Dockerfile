FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "webapi/cnt_sp_webapi/6 WebApp/SP.WebApi.WebApp.csproj"
RUN dotnet publish "webapi/cnt_sp_webapi/6 WebApp/SP.WebApi.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "SP.WebApi.WebApp.dll"]
