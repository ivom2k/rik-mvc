import type IPaymentType from "@/domain/IPaymentType";
import PaymentTypeService from "@/services/app/paymentTypeService";
import { defineStore } from "pinia";

const paymentTypeService = new PaymentTypeService();

export const usePaymentTypeStore = defineStore("paymentTypeStore",
{
    state: () => ({
        paymentTypes: [] as IPaymentType[]
    }),
    actions: {
        async fillPaymentTypes(): Promise<void> {
            this.paymentTypes = await paymentTypeService.GetAllAsync();
        },
        async updatePaymentType(id: string, paymentType: IPaymentType): Promise<void> {
            await paymentTypeService.UpdateAsync(id, paymentType);
            await this.fillPaymentTypes();
        },
        async createPaymentType(paymentType: IPaymentType): Promise<void> {
            await paymentTypeService.AddAsync(paymentType);
        },
        async deletePaymentType(id: string): Promise<void> {
            await paymentTypeService.RemoveAsync(id);
            await this.fillPaymentTypes();
        }
    }
})