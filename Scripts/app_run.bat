    cd CoreWebApi
    docker build . -t corewebapi
    docker run -d -p 8080:80 corewebapi