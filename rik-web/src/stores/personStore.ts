import type IPerson from "@/domain/IPerson";
import PersonService from "@/services/app/personService";
import { defineStore } from "pinia";

const personService = new PersonService();

export const usePersonStore = defineStore("personStore",
{
    state: () => ({
        persons: [] as IPerson[]
    }),
    actions: {
        async fillPersons(): Promise<void> {
            this.persons = await personService.GetAllAsync();
        },
        async updatePerson(id: string, person: IPerson): Promise<void> {
            await personService.UpdateAsync(id, person);
            await this.fillPersons();
        },
        async createPerson(person: IPerson): Promise<void> {
            await personService.AddAsync(person);
        },
        async deletePerson(id: string): Promise<void> {
            await personService.RemoveAsync(id);
            await this.fillPersons();
        },
        async addPerson(person: IPerson): Promise<IPerson> {
            return await personService.CreateAsync(person);
        }
    }
})