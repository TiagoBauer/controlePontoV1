# controlePontoV1
Aplicação de controle de apontamento de horas em projeto, utilizando Swagger.
O desenvolvimento permite cadstrar Papeis, Equipes, Colaboradores, Projetos e Apontamentos. Também permite rodar uma pesquisa nas horas e minutos trabalhados dos projetos por equipe, projeto, colaborador e data inical e final. 

Passos para executar o sistema:
1# Configurar a estrutura do banco (Usado no desenvolvimento Sql Server 18)
  No Sql server é preciso criar a database "controle_Ponto" utilizando a opção de mouse, na pasta Database do servidor e selecionar a opção criar Nova database...
  ![image](https://user-images.githubusercontent.com/31293561/127750567-71ca827a-8010-4b21-b375-3a96c8bacf66.png)
  
  Logo em seguida é possivel criar um usuário de logon utilizando o mesmo procedimento porem na pasta de segurança, e selecionando a opção Novo > Usuário...
  ![image](https://user-images.githubusercontent.com/31293561/127750563-ed6ba8d5-e271-4925-9734-25c6d7826175.png)

2# Configurar as rota dos serviços e caminho do banco
  É preciso alterar o arquivo appSettings.json. Neste arquivo é preciso indicar qual o servidor, database, user, password usado do banco.
  Tamém é possivel aterar o caminho da aplicação caso desejar. 
    "ConnectionStrings": {
        "ServerConnection": "Server=<Servidor>;Database=controle_Ponto;uid=<user>;pwd=<pwd>",
        "baseUrl": "https://localhost:[porta]/controlePonto/api"
    }
  
3# Crias estrutura do banco
  No visualstudio, deve se utilizar a opção "Console do Gerenciador de Pacotes". 
  ![image](https://user-images.githubusercontent.com/31293561/127750613-4e99b70a-0ae7-4bdd-82db-68262c6b07b1.png)
  
  Nele basta colocar os seguintes comandos:
    * add-migration controlePonto  
    ![image](https://user-images.githubusercontent.com/31293561/127750647-42aa790e-abb8-4dce-868e-3735d0979976.png)
  
    * update-database 
    ![image](https://user-images.githubusercontent.com/31293561/127750654-af5882f4-8d29-4d4c-b387-d4b676b43e1c.png)

Assim que realizado estes serviços o sistema estará disponivel.
  
Utilizando o visualstudio é possivel executa-lo para testes.
  
Na primeira execução é preciso cadastrar os seguintes dados:
  * Papel
  * Equipe
  * Colaborador
  
É possivel utilizando o caminho /swagger no final da url.
Na UI do Swagger, é possivel enviar um post para as tabelas de Papel, Equipe e Colaborador. Para cadastrar é indicado preencher todos os campos do post.
![image](https://user-images.githubusercontent.com/31293561/127750776-7a2d3e0d-fcb1-4f95-b82e-497cd43b1c75.png)

** Itens a melhorar ** 
  - Login: Login utiliza um select ao banco para validar se o codigo e senha batem com o que está cadastrado. Caso seja verdadeiro o sistema direciona o usuário a pagina do menu. Para tratar isso, é possivel utilizar o methodo de Autenticação. Porem será necessária refatoração dos controladores e DbConnection.
  - Segurança: Com a correção da autenticação, o quesito da segurança já é complementado. Hoje é possivel acessar qualquer tela direcionando na URL. 
  - Papeis: Hoje existe no banco um cadastro de papeis. O intuito é permitir que futuramente sejam cadastrados mais papeis, porem todos tem acesso ao sistema por completo no momento. Para corrigir isso é necessário implementar IndentityRoles, e em conjunto com a autenticação é possivel criar varios niveis de acesso e permissão.
  - Estetica: O sistema utiliza no momento a visão padrão das classes geradas do Entity Framework. É possivel gerar varias alterações para deixar a tela mais agradavel ao usuário. É possivel implementar os Gets via Swagger, para retornar os nomes dos colaboradores, papeis e equipes. Hoje somente a numeração é apresentada no cadastro do Ponto e apontamento de horas.
  - Organização: É possivel corrigir a estruturação das classes e views para melhor entendimento da solução e clarificação do código. Cada view possui hoje seu controller individual, sendo que seria possivel segregar para cada tipo de informação, para encapsular melhor as funções e afins.
