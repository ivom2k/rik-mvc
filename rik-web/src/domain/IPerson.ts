import type IUniqueIdentifier from "./IUniqueIdentifier";

export default interface IPerson extends IUniqueIdentifier
{
    firstName: string;
    lastName: string;
    personalIdentificationCode: number;
    notes?: string;
    fullName?: string;
}