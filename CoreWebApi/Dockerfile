FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY /publish /app 
EXPOSE 80
ENTRYPOINT ["dotnet", "CoreWebApi.dll"]