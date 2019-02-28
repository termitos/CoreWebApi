FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
COPY /publish /app 
EXPOSE 80
ENTRYPOINT ["dotnet", "CoreWebApi.dll"]