# Chat em Tempo Real com SignalR
Este projeto implementa um chat em tempo real usando ASP.NET Core e SignalR. Ele permite que os usuários enviem e recebam mensagens instantaneamente, com histórico de mensagens exibido ao entrar no chat e notificações de entrada e saída de usuários.

## Funcionalidades
- Envio e Recebimento de Mensagens em Tempo Real: Todos os usuários conectados recebem as mensagens enviadas instantaneamente.

- Histórico de Mensagens: Ao entrar, cada usuário visualiza as mensagens enviadas anteriormente.

- Notificações de Entrada e Saída de Usuários: Mensagens de sistema informam quando um usuário entra ou sai do chat.

## Tecnologias Utilizadas
- ASP.NET Core: Framework para desenvolvimento da aplicação.

- SignalR: Biblioteca para comunicação em tempo real entre cliente e servidor.

- JavaScript e HTML: Frontend básico para interação do usuário.

## Executando o Projeto
1. Clone o repositório: `https://github.com/gabrielrodri33/SignaIr-CP6.git`
2. Execute o projeto na IDE de sua escolha.
3. Abra no seu navegador `https://localhost:7026`

## Estrutura de pastas
<pre>
.
└── SignaIr-CP6/
    ├── Hubs/
    │   └── ChatHub.cs
    ├── Model/
    │   └── ChatMessage.cs
    ├── Pages/
    │   ├── Shared/
    │   │   ├── _Layout.cshtml
    │   │   └── _ValidationScriptsPartial.cshtml
    │   ├── _ViewImports.cshtml
    │   ├── _ViewStart.cshtml
    │   ├── Error.cshtml
    │   ├── Index.cshtml
    │   └── Integrantes.cshtml
    ├── appsettings.json
    ├── libman.json
    └── Program.cs
</pre>

## Integrantes 

- Gabriel Siqueira Rodrigues RM:98626

- Gustavo de Oliveira Azevedo RM:550548

- Isabella Jorge Ferreira RM:552329

- Mateus Mantovani Araújo RM:98524

- Juan de Godoy RM:551408