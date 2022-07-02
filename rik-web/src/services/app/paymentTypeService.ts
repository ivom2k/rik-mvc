import type IPaymentType from "@/domain/IPaymentType";
import BaseService from "../base/baseService";

export default class PaymentTypeService extends BaseService<IPaymentType> {
    constructor() {
        super("paymenttypes");
        
    }
}