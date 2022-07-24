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
    7. Testide käivitamiseks:
        7.1 dotnet test

## Rakenduse arhitektuur:

| Kiht | Kirjeldus |
| --- | ----------- |
| Data Access Layer | Sisaldab endas ühendust andmebaasiga ja andmebaasi modeleerimiseks vajalikke klasse ([Domain.Models](Domain/Models)), mida kasutatakse andmete sisestamiseks, lugemiseks, muutmiseks ning kustutamiseks.<br> Lisaks kasutavad neid klasse repositooriumid millede meetodid võtavad ja tagastavad andmete transportimiseks mõeldud klasse ([DTO.RepositoryEntity](DTO.RepositoryEntity)). Andmebaasi modeleerimisel kasutatud klassid <i>mapitakse</i> AutoMapperi abil ([Mappers.RepositoryEntity](Mappers.RepositoryEntity)). Annab välja DbSet-id ([ApplicationDbContext.cs](Domain/ApplicationDbContext.cs)) mida repositooriumid kasutavad olemitega toimingute (lugemine, salvestamine jms) tegemiseks. |
| Unit of work | Koosneb repositooriumitest ja ühe üksuse (<i>unit</i>) töö salvestamise funktsioonist. Repositoorium kasutab andmebaasi sessioone andmete salvestamiseks.<br> Repositooriumid on üles ehitatud [IBaseRepository.cs](Repositories.Interfaces/IBaseRepository.cs) liideseid kasutades, mida täidab [BaseRepository.cs](Repositories/BaseRepository.cs) klass mida omakorda laiendavad rakenduse spetsiifilised (vt. andmebaasi diagrammi klasse) repositooriumid ([Repositooriumid](Repositories/App)). Unit Of Work ([UnitOfWork](UnitOfWork/Interfaces)) defineerib liideseid kasutades repositooriumite kui ka ühe üksuse (<i>unit</i>) töö salvestamise funktsioonid. ([AppUnitOfWork.cs](UnitOfWork/AppUnitOfWork.cs)) implementeerib repositooriumid kui ka muutuste salvestamise funktsioonid. |
| Business Logic Layer | Defineerib ([IAppBll.cs](BLL.Interfaces/App/IAppBll.cs)) rakenduse spetsiifilised teenused mis baseeruvad repositooriumitele. Teenus kasutab repositooriumit andmebaasiga "suhtlemiseks". Teenused kasutavad andmete transportimiseks mõeldud klasse ([DTO.ServiceEntity](DTO.ServiceEntity)) mis omakorda <i>mapitakse</i> AutoMapperi poolt repositooriumi DTO-st teenuse DTO-sse. ([Mappers.ServiceEntity](Mappers.ServiceEntity)). Teenuste olemasolu selles kihis võimaldab kasutada erinevaid teenuseid ühe meetodi tarvis. Näiteks defineerime meetodi ([GetEventWithParticipantsCount](BLL.Interfaces/App/IAppBll.cs)) <i>interface-s</i> ning selle implementeerimisel kasutame kahte erinevat teenust ([Bll.cs](BLL/App/Bll.cs)). |
| API | API koosneb kontrolleritest ([ApiControllers](WebApp/ApiControllers)), mis väljastavad AutoMapperi poolt <i>mapitud</i> ([Mappers.PublicEntity](Mappers.PublicEntity)) avalikke olemeid ([DTO.Public](DTO.Public)), mille sisu ei muutu. Kontrollerid kasutavad ärikihis ([Bll.cs](BLL/App/Bll.cs)) olevaid teenuseid andmetega töötamiseks. |

Andmeid töödeldes liiguvad andmed läbi kõikide kihtide.

## Rakenduse kood:

| # | Projekt | Kirjeldus |
| --- | --- | --- |
| 1 | [Domain.Interfaces](Domain.Interfaces) | [IBaseEntityId.cs](Domain.Interfaces/IBaseEntityId.cs) liideste kaudu anname mõista, et domeeni olemil peab olema väli Id. |
| 2 | [Domain.Base](Domain.Base) | [BaseEntityId.cs](Domain.Base/BaseEntityId.cs) abstraktne klass domeeni olemi jaoks mis rakendab liidest [IBaseEntityId.cs](Domain.Interfaces/IBaseEntityId.cs) |
| 3 | [Domain](Domain) | Sisaldab endas andmebaasi modeleerimiseks vajalike olemeid ([Domain.Models](Domain/Models)) kus igaüks laiendab [BaseEntityId.cs](Domain.Base/BaseEntityId.cs) abstraktset klassi. Lisaks asub siin klass [ApplicationDbContext.cs](Domain/ApplicationDbContext.cs) mis esindab sessioone andmebassiga ning mida kasutatakse olemite lugemiseks ja salvestamiseks. |
| 4 | [Repositories.Interfaces](Repositories.Interfaces) | Defineerib baasrepositooriumis [IBaseRepository.cs](Repositories.Interfaces/IBaseRepository.cs) vajalikud meetodite signatuurid. Lisaks on seal ka rakenduse spetsiifilised repositooriumite liidesed, mis laiendavad baasrepositooriumi liideseid ning võimaldavad lisada olemi spetsiifilisi meetodite signatuure |
| 5 | [Repositories](Repositories) | Repositooriumite liideste realisatsioon klasside näol. Baasrepositooriumit laiendavad teised rakenduse spetsiifilised repositooriumid. |
| 6 | [Mappers.Interfaces](Mappers.Interfaces) | AutoMapperi liidesed defneerimaks <i>mapperi</i> meetodite signatuure ([IBaseMapper.cs](Mappers.Interfaces/IBaseMapper.cs)) |
| 7 | [Mappers](Mappers) | Baas<i>mapper</i>-i klass ([BaseMapper.cs](Mappers/BaseMapper.cs)) mis rakendab [IBaseMapper.cs](Mappers.Interfaces/IBaseMapper.cs) liidest ning sisaldab konfiguratioone ([Mappers/Configuration](Mappers/Configuration)) mis on [Program.cs](WebApp/Program.cs) kaudu vahekihina lisatud. |
| 8 | [DTO.RepositoryEntity](DTO.RepositoryEntity) | Andmete transportimiseks mõeldud klassid mida kasutavad repositooriumid. |
| 9 | [Mappers.RepositoryEntity](Mappers.RepositoryEntity) | Baas<i>mapper</i>-it ([BaseMapper.cs](Mappers/BaseMapper.cs)) laiendavad <i>mapperid</i>, mis teisendavad domeeni ([Domain.Models](Domain/Models)) olemid [DTO.RepositoryEntity](DTO.RepositoryEntity) olemiteks |
| 10 | [UnitOfWork](UnitOfWork) | Ühe üksuse töö jaoks vajalikud klassid ja liidesed. Liidesed määratlevad meetodite signatuurid ([IAppUnitOfWork.cs](UnitOfWork/Interfaces/IAppUnitOfWork.cs)) kus asuvad erinevate repositooriumite liidesed. [IUnitOfWork.cs](UnitOfWork/Interfaces/IUnitOfWork.cs) defineerib andmebaasiga tehtud muudatuste salvestamiseks vajalikud meetodid. |
| 11 | [BLL.Interfaces](BLL.Interfaces) | Äriloogika liidesed baasteenusele ([IEntityService.cs](BLL.Interfaces/IEntityService.cs)) ja rakenduse spetsiifilistele teenustele. ([Services](BLL.Interfaces/Services)). Lisaks äriloogika kihi jaoks liidesed [IAppBll.cs](BLL.Interfaces/App/IAppBll.cs) ja [IBll.cs](BLL.Interfaces/IBll.cs) |
| 12 | [BLL](BLL) | Äriloogika liideste realisatsioon. Baasteenus ([BaseEntityService.cs](BLL/BaseEntityService.cs)) mida laiendavad erinevad rakenduse spetsiifilised teenused ([BLL.Services](BLL/Services)). Lisaks äriloogika realisatsioon kus on kõik teenused ja meetodid mis kasutavad mitut teenust. Teenused kasutavad andmete töötlemiseks spetsiaalseid klasse ([DTO.ServiceEntity](DTO.ServiceEntity)) mis on mõeldud andmete transportimiseks. |
| 13 | [DTO.ServiceEntity](DTO.ServiceEntity) | Andmete transportimiseks mõeldud klassid mida kasutavad teenused. |
| 14 | [Mappers.ServiceEntity](Mappers.ServiceEntity) | Baas<i>mapper</i>-it ([BaseMapper.cs](Mappers/BaseMapper.cs)) laiendavad <i>mapperid</i>, mis teisendavad repositooriumi [DTO.RepositoryEntity](DTO.RepositoryEntity) olemid teenuste olemiteks ([Mappers.ServiceEntity](Mappers.ServiceEntity)) |
| 15 | [WebApp](WebApp) | Sisaldab API kontrollereid ([ApiControllers](WebApp/ApiControllers)) ja olulist [Program.cs](WebApp/Program.cs) faili mis lisab vahekihid (<i>middleware</i>). Api kontrollerid kasutavad andmte väljastamiseks teenuseid ([Services](BLL/Services)) kuid andmed <i>mapitakse</i> enne väljastamist. |
| 16 | [DTO.Public](DTO.Public) | Andmete transportimiseks mõeldud klassid mida kasutavad API kontrollerid. |
| 17 | [Mappers.PublicEntity](Mappers.PublicEntity) | Baas<i>mapper</i>-it ([BaseMapper.cs](Mappers/BaseMapper.cs)) laiendavad <i>mapperid</i>, mis teisendavad teenuste olemid avalikeks, klientidele väljastavateks olemiteks ([DTO.Public](DTO.Public)) |
| 18 | [Tests](Tests) | Moodultestid ja API kontrollerite testid |
| 19 | [rik-web](rik-web) | Vue.js eesrakendus |

## Lahendamiseks kulunud aeg:

Hinnanguline aeg lahendamiseks on 90 tundi arvestades GitHubis olevate <i>commitide</i> aegu. Täiendavalt aeglustas esialgu tööd see, et ma pole eelnevalt Visual Studio Code-s ASP.NET MVC projekti teinud ning käsurea käsud olid natuke uuevõitu.