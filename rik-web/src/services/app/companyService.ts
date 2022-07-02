import type ICompany from "@/domain/ICompany";
import BaseService from "../base/baseService";

export default class CompanyService extends BaseService<ICompany> {
    constructor() {
        super("companies");
    }
}