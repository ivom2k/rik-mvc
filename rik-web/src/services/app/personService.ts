import type IPerson from "@/domain/IPerson";
import BaseService from "../base/baseService";

export default class PersonService extends BaseService<IPerson> {
    constructor() {
        super("persons");
        
    }
}