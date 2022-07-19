import type ICompany from "../domain/ICompany";
import type IPerson from "../domain/IPerson";

export function fixStartTimeFormat(startTime: string) {
    const [date, time] = startTime.split("T");
    const [year, month, day] = date.split("-");
    const [hours, minutes] = time.split(":");

    return `${day}.${month}.${year} ${hours}:${minutes}`;
}

export function isIPerson(person: any): person is IPerson {
    return (person as IPerson).fullName !== undefined;
}

export function isICompany(company: any): company is ICompany {
    return (company as ICompany).participantsCount !== undefined;
}

export function verifyPersonalIdentificationCode(pic: string): boolean {
    let result = false;

    let controlSum = 0;

    for (let i = 0; i < 10; i++) {
        if (i < 9) {
            controlSum += (parseInt(pic[i]) * (i + 1));
        } else {
            controlSum += (parseInt(pic[i]) * 1);
        }
    }

    let controlNumber = (controlSum / 11).toFixed(1);
    let controlCheckNumber;

    const remainder = controlSum - (parseInt(controlNumber) * 11);

    if (remainder < 10) {
        
        result = parseInt(pic[10]) === remainder;
    } else if (remainder >= 10) {
        controlSum = 0;
        controlCheckNumber = 0;

        let counter = 0;
        [3, 4, 5, 6, 7, 8, 9, 1, 2, 3].forEach(num => {
            controlSum += parseInt(pic[counter++]) * num;
        });

        controlNumber = (controlSum / 11).toFixed(1);
        controlCheckNumber = controlSum - (parseInt(controlNumber) * 11);
        
        if (controlCheckNumber < 10) {
            result = parseInt(pic[10]) === controlCheckNumber;
        } else if (controlCheckNumber >= 10){
            result = parseInt(pic[10]) === 0;
        }
    }

    return result;
}