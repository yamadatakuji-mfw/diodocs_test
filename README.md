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
          "template": [base64 encoded excel file]
      }
  ]
  ```
- response model は
  ```
  {
      "outputData": [base64 encoded pdf file]
  }
  ```
- テスト方法
  1. 20 リクエストを同時に送る。　 → 　 pdf が 20 個作成される
  2. 処理が全部終わったら、次の 20 リクエストを送る。
  3. 15 回反復して、全 300 リクエスト、300 個の pdf が作成される
  4. 非常に容量が小さいものはフォント埋め込み失敗ケース
