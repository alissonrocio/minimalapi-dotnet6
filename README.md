# 1. Descrição

Teste de uma nova forma mais simplificada de escrever serviços no dotnet 6.0

# 2. Endpoints

## [Get] - Lista Fornecedores

Url: /api/fornecedores/

Exemplo: [https://localhost:5001/api/fornecedores](https://localhost:5001/api/fornecedores)

## [Get] - Fornecedor pelo Id

Url: /api/fornecedores/1

Exemplo: [https://localhost:5001/api/fornecedores/1](https://localhost:5001/api/fornecedores/1)

exem

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

[http://localhost:8088/api/fornecedores](http://localhost:8088/api/fornecedores)

[http://localhost:8088/api/fornecedores/1](http://localhost:8088/api/fornecedores/1)
