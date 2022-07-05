import type IUniqueIdentifier from "./IUniqueIdentifier";

export default interface IEvent extends IUniqueIdentifier
{
    name: string;
    startTime: string;
    location: string;
    notes?: string;
    totalParticipants?: number;
}