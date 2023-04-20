# Esta aplicação realiza o cadastro, validação de email e login de usuários.

Para executar a aplicação pode ser feito um dos seguintes passos:

### caso possua docker instalado

```
# em uma pasta de sua máquina execute o clone do repositório

git clone https://github.com/Mateus-N/user-api-com-envio-de-email.git

# apos isso abra o terminal na pasta onde foi feito o clone e execute o build das imagens

docker compose build

# agora é só executar a aplicação

docker compose up
```

### caso não possua o docker instalado*
*é necessário ter o MySql instalado

```
# em uma pasta de sua máquina execute o clone do repositório

git clone https://github.com/Mateus-N/user-api-com-envio-de-email.git

# apos isso abra o terminal na pasta onde foi feito o clone
# execute os seguintes comandos

cd EmailApp
dotnet run

# deixe esse terminal aberto executando a aplicação de email
# abra um segundo terminal na pasta onde foi feito o clone
# execute os seguintes comandos

cd UsuariosApi
dotnet run
```

## Com isso a aplicação já está sendo executada

A aplicação possui as seguintes rotas:
