import type ICompany from "@/domain/ICompany";
import type IPerson from "@/domain/IPerson";

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