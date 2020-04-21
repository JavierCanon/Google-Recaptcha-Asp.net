# ByPass reCaptcha, RobotReCaptcha
ByPass reCaptcha

//Geral:

	O RobotRecaptcha é uma aplicação ConsoleAplication C# baseada em uma Macro.

//Como é feito:

	Semelhante a ideia de um Headless browser, o RobotReCaptcha interege direto com o seu navegador.
	É aberto o seu navegador padrão através de um ConsoleAplication, e são manipulados os movimentos do mouse.
	A solução do reCaptcha é feita através do reconhecimento de audio por uma API da Microsoft.

//Iniciando:
	
	Antes de iniciar o projeto defina o seu navegador padrão: Ex: Firefox, Google Chrome, etc...
	Ao iniciar o programa é possível que dê alguns erros de dependências, para resolver isso verifique as dependências e acesse o nuget package para obtê-las.
	O código esta em sua maior parte comentado, e no geral é bem simples, você não deve ter dificuldades.
	
	A primeira coisa que deve ser configurada é é definir na classe RobotBase.cs a página que sera visitada, dentro da função StarterBrowser(), defina o target "URL", que sera acessado.
 	O próximo passo é definir a posiçao cursor em cada interação, para facilitar isso esta incluso nesse projeto uma Aplicação WindowsFrom básica onde pode ser obtida a posição (x,y) do cursor.
	As funções contidas na Classe MouseBase.cs devem ser alteradas de acordo com a necessidade de cada página visitada.
	
	Agora é necessário que você possua uma key do Bing Speech API - Speech Recognition, que pode ser obitida em: https://azure.microsoft.com/en-us/services/cognitive-services/speech/
	Após obter a chave da API vá ate a classe SendPOST.cs, função GetAndSendAudio(), e coloque sua chave em  request.Headers["Ocp-Apim-Subscription-Key"] = "Sua chave";
	Defina os locais onde serão gravados os audios nas variáveis pathIn e pathOut.
	Após os passos básicos configurados compile e execute o projeto.
	É provável que as configurações não estejam totalmente de acordo logo na primeira tentativa, mas após o entendimento de como o programa funciona é só uma questão de ajustes.
	A chamada de todos os métodos estão definidos na classe SendMoves.cs, edite-os de acordo com sua necessidade.

//Informações adicionais:
	
	Existe alguns métodos que não são utilizados, algumas funcionalidades não relacionadas ao projeto em sí, como por ex a classe ScreenShot.cs, utilizada apenas para retirar um print da tela para checagem do resultado. Ou também a função Navigate que pode ser aproveitada como uma outra abordagem para navegação.
	Para que o audio seja decodificado corretamente é nessário estar em formato .wav de 16bit ou 8bit, o audio baixado pelo reCpatcha vem em .mp3, por isso a classe Mp3ToWav.cs para realizar a conversão do audio.
	
//Algumas Perguntas

	Porque não foi ultilizado um Headless browser?
		O Headless browser realmente facilitaria muito o processo, porem como o reCaptcha/Google analisa um "fingerprint" do browser, o uso de um Headless browser torna muito mais fácil a identificação de uma navegação automatizada, afinal qual usuário comum utiliza um Headless browser para navegação diária?
	
	Porque a navegação não foi feita por um HttpClient?
		O sistema de bot detect do Google já é extremamente eficiente contra esse tipo de abordagem, tornando a identificação de navegação atutomatizada instantânea.
	
	Porque foi utilizada essa abordágem tão arcaica de Macro?
		O objetivo é se aproximar o máximo possível de um usuário humano, sendo assim o meio mais eficiente foi manipular diretamente a API do Windos e simular um usuário.

//Alguns desafios encontrados:

	Devido se utilizar apenas uma abordagem de solução que é a decodificação por audio, após um determinado tempo isso passa fugir muito do padrão esperado pelo Google Analytics e sua rede passara a ser suspeita.
	O processo em sí não foi bloqueado como Bot, sendo assim não foi possível estabelecer um limite imedianto de quantas soluções é possível se obter em sequência, porem após um determinado tempo o google para de te enviar audios por suspeita de automatização em sua rede, esse bloqueio é um tanto quanto generalizado, não apenas para você mas sim para todos os usuários da sua faixa de ip em sua rede. Então recomendasse o uso de proxy ou VPN para ajudar no processo, uma vez que ainda não há dados o suficiente para identificar por quanto tempo ocorre o bloqueio. Caso contrário todos os usuário de do seu provedor de internet ed uam determinada região podem ser afetados.

	
//Outras abordágens:
	
	O projeto em sí ainda não foi aperfeiçoado, e ainda é muito estático, são necessárias a junção de mais abordágens, tal como o uso de uma MachineLearning para o reconhecimento de imagens junto ao reconhecimento de audio, um vasto conjunto de perfis que obtenham interações com aplicações do google para serem ultilizados junto ao navegador.
	Uma base de dados de diferentes utuário manipulando o mouse para ser utilizado um processo menos robotico de interação do cursor com o navegador.
	Enfim, são muitos desafios para uma pessoa só, quem quiser ajudar a contribuir sinta-se a vontade.

//Open Source:
	
	O serviço utilizado para reconhecimento de audio foi é particular mas pode ser utilizado como alternativa o CMUSphinx, consulte mais em: https://cmusphinx.github.io/
	

//General:

RobotRecaptcha is a ConsoleAplication C # application based on a Macro.

//How is done:

Similar to the idea of ​​a Headless browser, RobotReCaptcha interacts directly with your browser.
You open your default browser through a ConsoleAplication, and you manipulate the mouse movements.
The reCaptcha solution is made through audio recognition by a Microsoft API.

// Starting:

Before starting the project define your default browser: Ex: Firefox, Google Chrome, etc ...
When starting the program it is possible to give some dependency errors, to solve this check the dependencies and access the package nuget to obtain them.
The code is mostly commented on, and overall it's pretty simple, you should not have any difficulties.

The first thing that must be configured is to define in the class RobotBase.cs the page that will be visited, inside the function StarterBrowser (), set the target "URL", which will be accessed.
 The next step is to define the cursor position in each interaction, to facilitate this is included in this project a basic WindowsFrom Application where the position (x, y) of the cursor can be obtained.
The functions contained in the MouseBase.cs Class should be changed according to the necessity of each page visited.

It is now necessary for you to have a Bing Speech API key - Speech Recognition, which can be obtained at: https://azure.microsoft.com/en-us/services/cognitive-services/speech/
After getting the API key go to the class SendPOST.cs, function GetAndSendAudio (), and place your key in request.Headers ["Ocp-Apim-Subscription-Key"] = "Your key";
Define the locations where the audios will be written to the variables pathIn and pathOut.
After the basic steps configured compile and run the project.
It is likely that the settings do not fully agree on the first try, but once you understand how the program works, it's just a matter of tweaking.
The call of all methods are defined in the class SendMoves.cs, edit them according to your need.

//Additional Information:

There are some methods that are not used, some features not related to the project itself, such as the ScreenShot.cs class, used only to remove a print from the screen to check the result. Or the Navigate function that can be used as another approach to navigation.
In order for the audio to be correctly decoded it is necessary to be in 16bit or 8bit .wav format, the audio downloaded by reCpatcha comes in .mp3, so the Mp3ToWav.cs class to perform the audio conversion.

//Some questions

Why was not a Headless browser used?
The Headless browser would really make the process much easier, but as reCaptcha / Google analyzes a browser's fingerprint, the use of a Headless browser makes it much easier to identify an automated navigation, after which the average user uses a Headless browser for browsing daily?

Why the navigation was not made by an HttpClient?
Google's bot detection system is already extremely effective against this kind of approach, making the navigation identification instantaneous.

Why was this so archaic Macro approach used?
The goal is to get as close as possible to a human user, so the most efficient way was to directly manipulate the Windos API and simulate a user.

// Some challenges encountered:

Because only a solution approach is used, which is audio decoding, after a certain amount of time this is far from the standard expected by Google Analytics and your network would become suspicious.
The process itself was not blocked as Bot, so it was not possible to establish an immediate limit of how many solutions can be obtained in sequence, but after a certain time google to send audios for suspected automation in their network, this blocking is somewhat generalized, not just for you but for all users of your ip range on your network. Then recommend using proxy or VPN to help with the process, as there is not enough data yet to identify how long the blockade occurs. Otherwise all users of your internet provider and a certain region may be affected.


// Other approaches:

The project itself has not yet been improved, and is still very static, it is necessary to join more approaches, such as the use of MachineLearning for the recognition of images along with audio recognition, a vast set of profiles that obtain interactions with google applications to be used next to the browser.
A database of different utui manipulating the mouse to be used a less robotic process of cursor interaction with the browser.
In short, there are many challenges for a single person, those who want to help contribute feel at ease.

// Open Source:

The service used for audio recognition was private but CMUSphinx can be used as an alternative, see more at: https://cmusphinx.github.io/
