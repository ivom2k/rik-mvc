import type ICompany from "@/domain/ICompany";
import CompanyService from "@/services/app/companyService";
import { defineStore } from "pinia";

const companyService = new CompanyService();

export const useCompanyStore = defineStore("companyStore", {
    state: () => ({
        companies: [] as ICompany[]
    }),
    actions: {
        async fillCompanies(): Promise<void> {
            this.companies = await companyService.GetAllAsync();
        },
        async updateCompany(id: string, company: ICompany): Promise<void> {
            await companyService.UpdateAsync(id, company);
            await this.fillCompanies();
        },
        async createCompany(company: ICompany): Promise<void> {
            await companyService.AddAsync(company);
        },
        async deleteCompany(id: string): Promise<void> {
            await companyService.RemoveAsync(id);
            await this.fillCompanies();
        }
    }
})