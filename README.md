## 起動方法

### docker イメージをビルドして起動する方法

1. イメージをビルドする

   ```
   docker build -t [image name:image tag] -f deployments/Dockerfile .
   ```

   ex) `docker build -t diodocs_test:1.0 -f deployments/Dockerfile .`

2. イメージを起動する
   ```
   docker run --rm -p 5000:5000 -e APIKEY=[api key] [image name:image tag]
   ```
   ex) `docker run --rm -p 5000:5000 -e APIKEY=キー diodocs_test:1.0`

### docker-compose で起動する

1. `deployments/docker-compose.yaml`ファイルの`environments`を修正する

   ```
   environments:
       APIKEY: [api key]
   ```

   ex) `APIKEY: キー`

2. docker-compose を起動する
   ```
   docker-compose up
   ```

## テスト方法

- host は `localhost:5000`
- endpoint は `/api/v1/convert/pdf`
- request model は
  ```
  [
    {
      "template": [base64 encoded excel file],
      "backgroundImage": [base64 encoded background image file]
    }
  ] 

  ```
- response model は
  ```
  {
      "outputData": [base64 encoded pdf file]
  }
  ```
