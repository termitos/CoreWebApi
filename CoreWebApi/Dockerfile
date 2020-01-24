FROM microsoft/dotnet:2.2-sdk as build
WORKDIR /app
COPY . /app
RUN dotnet publish -r linux-musl-x64 -o out

FROM microsoft/dotnet:2.1-runtime-deps-alpine
WORKDIR /app
COPY --from=build /app/out ./
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080
ENV APP_VERSION=1.0.1
EXPOSE 8080/tcp
ENTRYPOINT [ "./CoreWebApi" ]
