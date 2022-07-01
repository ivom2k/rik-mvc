import type IUniqueIdentifier from "./IUniqueIdentifier"

export default interface IPaymentType extends IUniqueIdentifier
{
    type: string;
}