# rik-mvc

Rakendus on tehtud ASP.NET MVC ja Vue.js front-end (eesrakendus) raamistikus ning realiseeritud kasutades kihilist arhitektuuri.

## Andmebaasi diagram:
Diagram kirjeldab C# keele olemite atribuute ja nende tüüpe, mitte andmebaasi atribuute ja tüüpe.

![ERD](/domain.png "ERD")

## Rakenduse paigaldamine:

    Eelduseks on Visual Studio Code ja Docker-i olemasolu

    1. Repositooriumi kloonimiseks:
        1.1 git clone https://github.com/ivom2k/rik-mvc.git
        1.2 cd rik-mvc
    2. ASP.NET MVC vajalike teekide allalaadimiseks, localhost sertifikaadi usaldamiseks ning projekti ehitamiseks:
        2.1 dotnet build
        2.2 dotnet dev-certs https --trust
    3. Vue.js eesrakenduse jaoks vajalikud sammud:
        3.1 cd rik-web
        3.2 npm install
    4. Andmebaasi jaoks MS SQL konteineri käivitamine (projekti juurkaustas):
        4.1 docker-compose up -d
    5. Andmebaasi tabelite loomiseks:
        5.1 dotnet ef update --project Domain --startup-project WebApp
    6. Rakenduste käivitamiseks:
        6.1 Tagarakenduse käivitamiskes:
            6.1.1 dotnet run --project WebApp
        6.2 Eesrakenduse käivitamiseks:
            6.2.1 cd rik-web
            6.2.2 npm run dev

## Rakenduse arhitektuur:

| Kiht | Kirjeldus |
| --- | ----------- |
| Data Access Layer | Sisaldab endas ühendust andmebaasiga ja andmebaasi modeleerimiseks vajalikke klasse ([Domain.Models](Domain/Models)), mida kasutatakse andmete sisestamiseks, lugemiseks, muutmiseks ning kustutamiseks.<br> Lisaks kasutavad neid klasse repositooriumid millede meetodid võtavad ja tagastavad andmete transportimiseks mõeldud klasse ([DTO.RepositoryEntity](DTO.RepositoryEntity)). Andmebaasi modeleerimisel kasutatud klassid <i>mapitakse</i> AutoMapperi abil ([Mappers.RepositoryEntity](Mappers.RepositoryEntity)). Annab välja DbSet-id ([ApplicationDbContext.cs](Domain/ApplicationDbContext.cs)) mida repositooriumid kasutavad olemitega toimingute (lugemine, salvestamine jms) tegemiseks. |
| Unit of work | Koosneb repositooriumitest ja ühe üksuse (<i>unit</i>) töö salvestamise funktsioonist. Repositoorium kasutab andmebaasi sessioone andmete salvestamiseks.<br> Repositooriumid on üles ehitatud [IBaseRepository.cs](Repositories.Interfaces/IBaseRepository.cs) <i>interface</i> kasutades, mida täidab [BaseRepository.cs](Repositories/BaseRepository.cs) klass mida omakorda laiendavad rakenduse spetsiifilised (vt. andmebaasi diagrammi klasse) repositooriumid ([Repositooriumid](Repositories/App)). Unit Of Work ([UnitOfWork](UnitOfWork/Interfaces)) defineerib <i>interface</i> kasutades repositooriumite kui ka ühe üksuse (<i>unit</i>) töö salvestamise funktsioonid. ([AppUnitOfWork.cs](UnitOfWork/AppUnitOfWork.cs)) implementeerib repositooriumid kui ka muutuste salvestamise funktsioonid. |
| Business Logic Layer | Defineerib ([IAppBll.cs](BLL.Interfaces/App/IAppBll.cs)) rakenduse spetsiifilised teenused mis baseeruvad repositooriumitele. Teenus kasutab repositooriumit andmebaasiga "suhtlemiseks". Teenused kasutavad andmete transportimiseks mõeldud klasse ([DTO.ServiceEntity](DTO.ServiceEntity)) mis omakorda <i>mapitakse</i> AutoMapperi poolt repositooriumi DTO-st teenuse DTO-sse. ([Mappers.ServiceEntity](Mappers.ServiceEntity)). Teenuste olemasolu selles kihis võimaldab kasutada erinevaid teenuseid ühe meetodi tarvis. Näiteks defineerime meetodi ([GetEventWithParticipantsCount](BLL.Interfaces/App/IAppBll.cs)) <i>interface-s</i> ning selle implementeerimisel kasutame kahte erinevat teenust ([Bll.cs](BLL/App/Bll.cs)). |
| API | API koosneb kontrolleritest ([ApiControllers](WebApp/ApiControllers)), mis väljastavad AutoMapperi poolt <i>mapitud</i> ([Mappers.PublicEntity](Mappers.PublicEntity)) avalikke olemeid ([DTO.Public](DTO.Public)), mille sisu ei muutu. Kontrollerid kasutavad ärikihis ([Bll.cs](BLL/App/Bll.cs)) olevaid teenuseid andmetega töötamiseks. |

Andmeid töödeldes liiguvad andmed läbi kõikide kihtide.

## Rakenduse kood:

| # | Projekt | Kirjeldus |
| --- | --- | --- |
| 1 | [Domain.Interfaces](Domain.Interfaces) | [IBaseEntityId.cs](Domain.Interfaces/IBaseEntityId.cs) interface kaudu anname mõista, et domeeni olemil peab olema väli Id. |
| 2 | [Domain.Base](Domain.Base) |  |

## Lahendamiseks kulunud aeg:

Hinnanguline aeg lahendamiseks on 87 tundi arvestades GitHubis olevate <i>commitide</i> aegu. Täiendavalt aeglustas esialgu tööd see, et ma pole eelnevalt Visual Studio Code-s ASP.NET MVC projekti teinud ning käsurea käsud olid natuke uuevõitu.