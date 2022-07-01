import type IUniqueIdentifier from "./IUniqueIdentifier";

export default interface IParticipation extends IUniqueIdentifier
{
    eventId: string;
    personId?: string;
    companyId?: string;
    paymentTypeId: string;
}