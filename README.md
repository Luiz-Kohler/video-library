# Sistema de Locadora

## Um sistema completo de locadora feito para uma etapa do processo seletivo da empresa e-Autditoria.

### Banco de dados
- MySQL

### Backend (API)
- C# - .NET 6 
- DDD
- CQRS com MediatR
- EF Core
- Swagger
- Logging
- Fluent Validation
- Automapper
- Testes:
  - Unitários
  - Integração
  
### Frontend (Web)
  - React
  - Typescript
  - Ant Design
  - Axios
  - React Toastify
  
### Docker
- Docker Compose

### Como executar o sistema?
  - Primeiramente certifique-se que tenha Docker instalado na sua máquina:
     - Você consegue saber CMD (command prompt)
     - Executando o comando "docker --version", caso ele retorne a versão, então seu Docker esta instalado.
  - Clone ou baixe o arquivo deste repositório, clicando no botão "code": <br />
    ![image](https://user-images.githubusercontent.com/60172584/176812897-4dbccf13-8173-4370-867f-ae773d6a4005.png)
  - Após o sistema devidamente baixado na sua máquina, entre na sua pasta raiz, sendo ela essa: <br />
    ![image](https://user-images.githubusercontent.com/60172584/176813296-9bae8f8d-482c-47c8-a032-da52bab00d17.png)
  - Na pasta informada clique com o botão direito no mouse e selecione a opção "Open in Terminal"
  - Com o terminal aberto, execute o comando "docker compose up" e pronto, só precisa esperar
  - Link que o sistema (web) estara sendo executado: http://localhost:3001
  - Link do swagger com cada endpoint documentado: http://localhost:5000/swagger/index.html

### Como subir o ambiente para executar os testes de integração?
 - Entre na pasta backend/Locadora/Tests.Integration
 - Execute o comando "docker compose up" assim que o ambiente subir, você pode executar normalmente os testes de integração
