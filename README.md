# API de Sistema de Tarefas

## Descrição

Esta API permite o cadastro de usuários e tarefas, além de atribuir tarefas a usuários. Ela utiliza autenticação JWT para segurança.

## Endpoints

### Tarefas
- **GET /api/tarefa**: Lista todas as tarefas.
- **GET /api/tarefa/{id}**: Obtém uma tarefa por ID.
- **POST /api/tarefa**: Cria uma nova tarefa.
- **PUT /api/tarefa/{id}**: Atualiza uma tarefa existente.
- **DELETE /api/tarefa/{id}**: Deleta uma tarefa por ID.

### Usuários
- **GET /api/user**: Lista todos os usuários.
- **GET /api/user/{id}**: Obtém um usuário por ID.
- **POST /api/user**: Cria um novo usuário.
- **PUT /api/user/{id}**: Atualiza um usuário existente.
- **DELETE /api/user/{id}**: Deleta um usuário por ID.

### Autenticação
- **POST /api/conta**: Faz login e gera um token JWT (usuário e senha: `admin`).

## Instalação

1. **Clone o repositório para sua máquina local**

2. **Abra o projeto em sua IDE preferida**.

3. **Atualize a configuração do banco de dados**:
    - No arquivo `appsettings.json`, ajuste a string de conexão para corresponder à sua configuração local:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "server=localhost;database=sua_base_de_dados;user=root;password=sua_senha;"
    }
    ```

4. **Execute o projeto**:
    ```bash
    dotnet run
    ```

5. **Acesse a API**:
    - A API estará disponível em [http://localhost:7058](http://localhost:7058)
