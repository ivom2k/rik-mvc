# rik-mvc

Rakendus on tehtud ASP.NET MVC ja Vue.js front-end raamistikus ning realiseeritud kasutades kihilist arhitektuuri.

## Andmebaasi diagram:
Diagram kirjeldab C# keele olemite atribuute ja nende tüüpe, mitte andmebaasi atribuute ja tüüpe.

![ERD](/domain.png "ERD")

## Rakenduse paigaldamine:

    Tekst

## Rakenduse arhitektuur:

| Kiht | Kirjeldus |
| --- | ----------- |
| Data Access Layer | Sisaldab endas ühendust andmebaasiga ja andmebaasi modeleerimiseks vajalikke klasse ([Domain.Models](Domain\Models)), mida kasutatakse andmete sisestamiseks, lugemiseks, muutmiseks ning kustutamiseks.<br> Lisaks kasutavad neid klasse repositooriumid millede meetodid võtavad ja tagastavad andmete transportimiseks mõeldud klasse ([DTO.RepositoryEntity](DTO.RepositoryEntity)). Andmebaasi modeleerimisel kasutatud klassid <i>mapitakse</i> AutoMapperi abil ([Mappers.RepositoryEntity](Mappers.RepositoryEntity)). Annab välja DbSet-id ([ApplicationDbContext.cs](Domain\ApplicationDbContext.cs)) mida repositooriumid kasutavad olemitega toimingute (lugemine, salvestamine jms) tegemiseks. |
| Unit of work | Koosneb repositooriumitest ja ühe üksuse (<i>unit</i>) töö salvestamise funktsioonist. Repositoorium kasutab andmebaasi sessioone andmete salvestamiseks.<br> Repositooriumid on üles ehitatud [IBaseRepository.cs](Repositories.Interfaces\IBaseRepository.cs) <i>interface</i> kasutades, mida täidab [BaseRepository.cs](Repositories\BaseRepository.cs) klass mida omakorda laiendavad rakenduse spetsiifilised (vt. andmebaasi diagrammi klasse) repositooriumid ([Repositooriumid](Repositories\App)). Unit Of Work ([UnitOfWork](UnitOfWork\Interfaces)) defineerib <i>interface</i> kasutades repositooriumite kui ka ühe üksuse (<i>unit</i>) töö salvestamise funktsioonid. ([AppUnitOfWork.cs](UnitOfWork\AppUnitOfWork.cs)) implementeerib repositooriumid kui ka muutuste salvestamise funktsioonid. |
| Business Logic Layer | Defineerib ([IAppBll.cs](BLL.Interfaces\App\IAppBll.cs)) rakenduse spetsiifilised teenused mis baseeruvad repositooriumitele. Teenus kasutab repositooriumit andmebaasiga "suhtlemiseks". Teenused kasutavad andmete transportimiseks mõeldud klasse ([DTO.ServiceEntity](DTO.ServiceEntity)) mis omakorda <i>mapitakse</i> AutoMapperi poolt repositooriumi DTO-st teenuse DTO-sse. ([Mappers.ServiceEntity](Mappers.ServiceEntity)). Teenuste olemasolu selles kihis võimaldab kasutada erinevaid teenuseid ühe meetodi tarvis. Näiteks defineerime meetodi ([GetEventWithParticipantsCount](BLL.Interfaces\App\IAppBll.cs)) <i>interface-s</i> ning selle implementeerimisel kasutame kahte erinevat teenust ([Bll.cs](BLL\App\Bll.cs)). |
| API | API koosneb kontrolleritest ([ApiControllers](WebApp\ApiControllers)), mis väljastavad AutoMapperi poolt <i>mapitud</i> ([Mappers.PublicEntity](Mappers.PublicEntity)) avalikke olemeid ([DTO.Public](DTO.Public)), mille sisu ei muutu. Kontrollerid kasutavad ärikihis ([Bll.cs](BLL\App\Bll.cs)) olevaid teenuseid andmetega töötamiseks. |

## Lahendamiseks kulunud aeg:

Hinnanguline aeg lahendamiseks on 87 tundi arvestades GitHubis olevate <i>commitide</i> aegu. Täiendavalt aeglustas esialgu tööd see, et ma pole eelnevalt Visual Studio Code-s ASP.NET MVC projekti teinud ning käsurea käsud olid natuke uuevõitu.