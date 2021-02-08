FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine AS builder

WORKDIR /app

COPY ./src ./

# download dependencies (like 'npm install')
RUN dotnet restore

# compile
RUN dotnet publish -c Release -o out -r linux-musl-x64 --self-contained=true

# change image
FROM mcr.microsoft.com/dotnet/runtime-deps:3.1-alpine AS runtime
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 5000

RUN apk add --no-cache curl fontconfig &&\
    curl -O https://noto-website.storage.googleapis.com/pkgs/NotoSansCJKjp-hinted.zip && \
    mkdir -p /usr/share/fonts/NotoSansCJKjp && \
    mkdir -p /temp && \
    unzip NotoSansCJKjp-hinted.zip -d /temp && \
    cp /temp/NotoSansCJKjp-Regular.otf /usr/share/fonts/NotoSansCJKjp/ && \
    rm NotoSansCJKjp-hinted.zip
RUN rm -rf /temp && \
    fc-cache -fv 

# run program
ENTRYPOINT [ "./diodocs" ]