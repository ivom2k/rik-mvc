import type IParticipation from "@/domain/IParticipation";
import ParticipationService from "@/services/app/participationService";
import { defineStore } from "pinia";

const participationService = new ParticipationService();

export const useParticipationStore = defineStore("participationStore",
{
    state: () => ({
        participations: [] as IParticipation[]
    }),
    actions: {
        async fillParticipations(): Promise<void> {
            this.participations = await participationService.GetAllAsync();
        },
        async getAllParticipationsWithEventIdAsync(idEvent: string): Promise<Array<IParticipation>> {
            return await participationService.GetAllWithEventAsync(idEvent);
        },
        async updateParticipation(id: string, participation: IParticipation): Promise<void> {
            await participationService.UpdateAsync(id, participation);
            await this.fillParticipations();
        },
        async createParticipation(participation: IParticipation): Promise<void> {
            await participationService.AddAsync(participation);
        },
        async deleteParticipation(id: string): Promise<void> {
            await participationService.RemoveAsync(id);
            await this.fillParticipations();
        }
    }
})