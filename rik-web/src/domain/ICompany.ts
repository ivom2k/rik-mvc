import type IUniqueIdentifier from "./IUniqueIdentifier";

export default interface ICompany extends IUniqueIdentifier
{
    name: string;
    code: number;
    participantsCount: number;
    notes: string;
}