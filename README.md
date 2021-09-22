# 1.  Descrição

Teste de uma nova forma mais simplificada de escrever serviços no dotnet 6.0

# 2. Endpoints

| Verbos | Url                    | Descrição                       |
| ------ | ---------------------- | ------------------------------- |
| Get    | /api/fornecedores/     | Lista todos os fornecedores     |
| Get    | /api/fornecedores/{id} | Retorna o fornecedor pela chave |
| Post   | /api/fornecedor/       | Adiciona um novo fornecedor     |
| Put    | /api/fornecedor/{id}   | Atualiza os dados do fornecedor |
| Delete | /api/fornecedor/{id}   | Remove um fornecedor pela chave |

# 3. Datasource - json

```
[
{
"Id": 1,
"Nome": "Jason Bourne",
"Tipo": "F",
"CpfCnpj": "11497733081"
},

{
"Id": 2,
"Nome": "John Wick",
"Tipo": "F",
"CpfCnpj": "46458198019"
},

{
"Id": 3,
"Nome": "Umbrella Corps",
"Tipo": "J",
"CpfCnpj": "97800104000112"
},

{
"Id": 4,
"Nome": "Kakaroto (Goku)",
"Tipo": "F",
"CpfCnpj": "52282995058"
},

{
"Id": 5,
"Nome": "Arasaka Corporation",
"Tipo": "J",
"CpfCnpj": "97641927000142"
}
]

```

# 4. Docker

## 4.1. Build imagem

Na raiz aonde está o Dockerfile

```
docker build -t algum_nome_imagem .
```

## 4.2. Executar imagem

```
docker run --name algum_nome -p 8088:80 -d algum_nome_imagem
```

## 4.3. Docker Hub

```
docker pull alissonrocio/minimalapi-dotnet6:latest
```

# 5. Kubernetes

Criação de um deployment com 3 réplicas e um nodeport para acesso local.

```
kubectl apply -f k8s/deployment.yaml
```

# 5. Deploy

```
https://minimalapi-dotnet6.azurewebsites.net/api/fornecedores
```